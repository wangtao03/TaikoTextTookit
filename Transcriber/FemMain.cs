using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.ListViewItem;

using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Transcriber
{
    public partial class FemMain : Form
    {
        private Control listView;

        public FemMain()
        {
            InitializeComponent();
        }

        private void BtnSysBrower_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                Title = "请选择 WordData 导出文件!",
                Filter = "WordData|WordData_*.xlsx",
                FileName = txtSysFilePath.TextLength > 0 ? Path.GetFileName(txtSysFilePath.Text) : "WordData_*.xlsx",
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSysFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnStoryBrower_Click(object sender, EventArgs e)
        {
            using var folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "请选择 StoryText 导出目录",
                ShowNewFolderButton = false,
                //RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = txtStoryDirPath.TextLength > 0 ? txtStoryDirPath.Text : Application.StartupPath,
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtStoryDirPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnSysClear_Click(object sender, EventArgs e)
        {
            lstvSystem.Items.Clear();
        }

        private void BtnStoryClear_Click(object sender, EventArgs e)
        {
            lstvStory.Items.Clear();
        }

        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            var listView = (ListView)sender;

            var files = ((Array)e.Data.GetData(DataFormats.FileDrop));

            var items = new List<ListViewItem>();

            for (int i = 0; i < files.Length; i++)
            {
                var file = files.GetValue(i).ToString();
                if (!Path.GetExtension(file).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)) continue;

                items.Add(new ListViewItem(new string[] { (listView.Items.Count + items.Count + 1).ToString(), Path.GetFileName(file), "0/0" })
                {
                    Tag = file,
                    ToolTipText = file,
                });
            }

            listView.Items.AddRange(items.ToArray());
        }

        private void Cibtrol_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            var textBox = (TextBox)sender;
            var file = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            switch (textBox.Name)
            {
                case "txtSysFilePath":
                    if (!Path.GetExtension(file).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)) return;
                    textBox.Text = file;
                    break;

                case "txtStoryDirPath":
                    if (Path.GetExtension(file).Length > 0)
                    {
                        file = Path.GetDirectoryName(file);
                    }
                    textBox.Text = file;
                    break;

                default:
                    break;
            }
        }

        private void BtnSysCopy_Click(object sender, EventArgs e)
        {
            if (txtSysFilePath.TextLength <= 0) return;
            if (lstvSystem.Items.Count <= 0) return;
            SystemCopy();
        }

        private void BtnStoryCopy_Click(object sender, EventArgs e)
        {
            if (txtStoryDirPath.TextLength <= 0) return;
            if (lstvStory.Items.Count <= 0) return;
            StroyCopy();
        }

        private void SystemCopy()
        {
            btnSysCopy.Enabled = false;

            var pairs = LoadExcelData(txtSysFilePath.Text);

            foreach (ListViewItem item in lstvSystem.Items)
            {
                CopyData(item.Tag.ToString(), pairs, item);
            }

            btnSysCopy.Enabled = true;
        }

        private void StroyCopy()
        {
            btnStoryCopy.Enabled = false;

            var dirPath = txtStoryDirPath.Text;

            foreach (ListViewItem item in lstvStory.Items)
            {
                var file = item.Tag.ToString();
                var xslx = $@"{dirPath}\{Path.GetFileNameWithoutExtension(file)}_03.xlsx";

                if (!File.Exists(xslx))
                {
                    File.AppendAllText(Path.ChangeExtension(file, "log"), "");
                    continue;
                }

                var pairs = LoadExcelData(xslx);

                CopyData(file, pairs, item);
            }
            btnStoryCopy.Enabled = true;
        }

        private Dictionary<string, string> LoadExcelData(string excelPath)
        {
            var dictionary = new Dictionary<string, string>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage(new FileInfo(excelPath)))
            {
                var workSheet = excelPackage.Workbook.Worksheets[0];
                var dimension = workSheet.Dimension;
                for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                {
                    var key = workSheet.Cells[i, 2].Text;
                    var value = workSheet.Cells[i, 3].Text;

                    if (!dictionary.TryAdd(key, value))
                    {
                        if (!dictionary[key].Equals(value, StringComparison.OrdinalIgnoreCase)) throw new ArgumentException();
                    }
                }
            }

            return dictionary;
        }

        private void CopyData(string path, Dictionary<string, string> pairs, ListViewItem item = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                var workSheet = excelPackage.Workbook.Worksheets[0];
                var dimension = workSheet.Dimension;
                var count = 0;
                for (int i = dimension.Start.Row; i <= dimension.End.Row; i++)
                {
                    Application.DoEvents();
                    var key = workSheet.Cells[i, 2].Text;
                    try
                    {
                        var value = pairs[key];
                        workSheet.Cells[i, 4].Value = value;
                        count++;
                        if (item != null)
                        {
                            item.Selected = true;
                            item.SubItems[2].Text = $"{count}/{dimension.End.Row}";
                        }
                    }
                    catch (Exception)
                    {
                        File.AppendAllLines(Path.ChangeExtension(path, "log"), new string[] { key });
                    }
                }

                if (count > 0) excelPackage.Save();
            }
        }

        private void TsmiOutput_Click(object sender, EventArgs e)
        {
            if (listView == null) return;
            var lstv = (ListView)listView;
            if (lstv.Items.Count <= 0) return;

            var saveFileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                Title = "请选择保存位置!",
                DefaultExt = "txt",
                Filter = "文本文件(*.txt)|*.txt",
                FileName = $"{lstv.Name} {DateTime.Now:yyyyMMddHHmmss}",
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            foreach (ListViewItem item in lstv.Items)
            {
                var sb = new StringBuilder();
                foreach (ListViewSubItem subItem in item.SubItems)
                {
                    sb.Append(subItem.Text);
                    sb.Append('\t');
                }
                File.AppendAllText($"{saveFileDialog.FileName}", $"{sb}\r\n");
            }
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var menuStrip = (ContextMenuStrip)sender;
            listView = menuStrip.SourceControl;
        }
    }
}