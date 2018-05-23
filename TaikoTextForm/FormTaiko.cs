using HexViewer;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Taiko3DS3;

namespace TaikoTextOutput
{
    public partial class FrmTaiko : Form
    {
        private SystemText taiko;
        private HexView hexViewer;
        private int selectIndex = -1;
        private string openFileName = string.Empty;

        public FrmTaiko()
        {
            InitializeComponent();
        }

        private void File_open_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "打开文本文件";
            openFileDialog.Filter = "文本文件|*.dat|所有文件|*.*";
            openFileDialog.DefaultExt = ".dat";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        private void OpenFile(string filename)
        {
            try
            {
                openFileName = Path.GetFileNameWithoutExtension(filename);
                byte[] bytes;
                bytes = File.ReadAllBytes(filename);
                taiko = new SystemText(bytes);
                hexViewer = new HexView(bytes);
                selectIndex = -1;
                ListViewUpdata();
                HexViewUpdata();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ListViewUpdata()
        {
            try
            {
                listView.Items.Clear();

                for (int i = 0; i < taiko.Count; i++)
                {
                    var ii = new ListViewItem(new string[] { taiko.GetText(i, TextType.identifier), taiko.GetText(i, TextType.content), taiko.GetText(i, TextType.translation) });
                    listView.Items.Add(ii);
                }

                ListView_ColumnClick(null, new ColumnClickEventArgs(0));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ListViewSelectUpdate()
        {
            try
            {
                if (taiko != null && listView.SelectedItems.Count > 0)
                {
                    if (listView.SelectedItems[0].Index != selectIndex)
                    {
                        TextSizeChecker();
                        selectIndex = listView.SelectedItems[0].Index;
                        textBox1.Text = taiko.GetText(selectIndex).Replace("\n", "\r\n");
                        textBox2.Text = taiko.GetText(selectIndex, TextType.translation).Replace("\n", "\r\n");

                        if (autocopyjp.Checked && textBox2.Text.Trim().Length <= 0)
                        {
                            textBox2.Text = textBox1.Text;
                        }

                        var temp = taiko.GetText(selectIndex);
                        var textSize = Encoding.UTF8.GetBytes(temp.Replace("\0", string.Empty)).Length;
                        HexViewUpdata(taiko.GetAddr(selectIndex), textSize);
                        TextSizeChecker();
                    }
                }
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }

        private void HexViewUpdata(int start = 0, int select = 0)
        {
            try
            {
                var startline = 0;
                var lenCount = hexViewer.Count;
                const int margin = 3;
                txt_Addr.Clear();
                txt_Hex.Clear();
                txt_utf8.Clear();

                var maxlen = txt_Addr.Height / 12;

                if (lenCount - margin > maxlen) startline = start / 16 - margin;
                if (lenCount - maxlen < startline) startline = lenCount - maxlen;

                var hexText = hexViewer.rows.ToArray(startline, maxlen);
                txt_Addr.Lines = (string[])hexText[0];
                txt_Hex.Lines = (string[])hexText[1];
                txt_utf8.Lines = (string[])hexText[2];

                if (select > 0)
                {
                    var startLine = (start - startline * 16) / 16 - 1;
                    var selLine = select / 16 - 1;
                    txt_Hex.Select((start - startline * 16) * 3 + startLine, select * 3 + selLine + 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool TextSizeChecker()
        {
            if (selectIndex < 0) return false;
            var content = taiko.GetText(selectIndex);
            var translation = textBox2.Text.Replace("\r\n", "\n");

            var contentSize = Encoding.UTF8.GetBytes(content.Replace("\0", string.Empty)).Length;
            var contentFullSize = Encoding.UTF8.GetBytes(content).Length;
            var translationSize = Encoding.UTF8.GetBytes(translation).Length;

            toolStripStatusLabel2.Text = $"原文长度: {contentSize} 字节, 实际可覆写: {contentFullSize} 字节; ";

            if (translationSize <= contentFullSize)
            {
                listView.Items[selectIndex].BackColor = listView.BackColor;
                toolStripStatusLabel3.Text = $"译文长度: {translationSize} 字节";
                return true;
            }
            else
            {
                listView.Items[selectIndex].BackColor = Color.Salmon;
                toolStripStatusLabel3.Text = $"译文长度: {translationSize} 字节, 已超出 {translationSize - contentFullSize} 字节";
                return false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh点mm分ss秒");
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            var w = (listView.Width - listView.Columns[0].Width) / 2 - 15;
            listView.Columns[1].Width = w;
            listView.Columns[2].Width = w;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ListView_ColumnClick(null, new ColumnClickEventArgs(0));
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            TextSizeChecker();
        }

        private void Copyjp_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox2.Text = textBox1.Text;
            }
        }

        private void Savecn_Click(object sender, EventArgs e)
        {
            try
            {
                if (taiko != null && (checkempty.Checked || textBox2.Text.Length > 0))
                {
                    listView.Items[selectIndex].SubItems[2].Text = textBox2.Text;
                    taiko.SetText(selectIndex, textBox2.Text.Replace("\r\n", "\n"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewUpdata();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            txt_Addr.Clear();
            txt_Hex.Clear();
            txt_utf8.Clear();
            listView.Items.Clear();
        }

        private void Editor_export_Click(object sender, EventArgs e)
        {
            if (taiko != null && taiko.Count > 0)
            {
                saveFileDialog.Title = "导出到Excel文件";
                saveFileDialog.Filter = "Excel文件|*.xlsx";
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.FileName = openFileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!taiko.Export(@saveFileDialog.FileName))
                    {
                        MessageBox.Show("导出失败!!!");
                        return;
                    }
                    MessageBox.Show("导出成功!!!");
                }
            }
        }

        private void Editor_import_Click(object sender, EventArgs e)
        {
            if (taiko != null && taiko.Count > 0)
            {
                openFileDialog.Title = "从Excel文件导入";
                openFileDialog.Filter = "Excel文件|*.xlsx";
                openFileDialog.DefaultExt = ".xlsx";
                openFileDialog.FileName = openFileName;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    switch (taiko.Import(openFileDialog.FileName))
                    {
                        case 0:
                            ListViewUpdata();
                            MessageBox.Show("导入成功!!!");
                            break;

                        case -5:
                            MessageBox.Show("导入内容与已打开的文件内容不符!");
                            break;

                        default:
                            MessageBox.Show("导入失败!");
                            break;
                    }
                }
            }
        }

        private void Up_Click(object sender, EventArgs e)
        {
            if (selectIndex > 0)
            {
                if (moveaotosave.Checked)
                {
                    Savecn_Click(null, new EventArgs());
                }
                listView.Items[selectIndex].Selected = false;
                listView.Items[selectIndex - 1].Selected = true;
                ListViewSelectUpdate();
            }
        }

        private void Down_Click(object sender, EventArgs e)
        {
            if (selectIndex < listView.Items.Count - 1)
            {
                if (moveaotosave.Checked)
                {
                    Savecn_Click(null, new EventArgs());
                }
                listView.Items[selectIndex].Selected = false;
                listView.Items[selectIndex + 1].Selected = true;
                ListViewSelectUpdate();
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            var path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            OpenFile(path);
        }

        private void File_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void File_save_Click(object sender, EventArgs e)
        {
            if (taiko != null && taiko.Count > 0)
            {
                saveFileDialog.Title = "保存文本文件";
                saveFileDialog.Filter = "文本文件|*.dat";
                saveFileDialog.DefaultExt = ".dat";
                saveFileDialog.FileName = openFileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    taiko.Save(saveFileDialog.FileName);
                    MessageBox.Show("保存完成!");
                }
            }
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!textBox2.Enabled) textBox2.Enabled = true;
            ListViewSelectUpdate();
        }

        private void File_saveas_Click(object sender, EventArgs e)
        {
            if (taiko != null && taiko.Count > 0)
            {
                saveFileDialog.Title = "另存为文本文件";
                saveFileDialog.Filter = "文本文件|*.dat";
                saveFileDialog.DefaultExt = ".dat";
                saveFileDialog.FileName = openFileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    taiko.SaveAs(saveFileDialog.FileName, taiko.TextTitle);
                    MessageBox.Show("另存完成!");
                }
            }
        }
    }
}