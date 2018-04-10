namespace TaikoTextOutput
{
    partial class FrmTaiko
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaiko));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "1112233",
            "AREAPOINT_TITLE_007",
            "........ ......."}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Salmon, null);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.file_open = new System.Windows.Forms.ToolStripMenuItem();
            this.file_save = new System.Windows.Forms.ToolStripMenuItem();
            this.file_saveas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.file_close = new System.Windows.Forms.ToolStripMenuItem();
            this.im_export = new System.Windows.Forms.ToolStripMenuItem();
            this.editor_import = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.editor_export = new System.Windows.Forms.ToolStripMenuItem();
            this.edit = new System.Windows.Forms.ToolStripMenuItem();
            this.up = new System.Windows.Forms.ToolStripMenuItem();
            this.down = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyjp = new System.Windows.Forms.ToolStripMenuItem();
            this.savecn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.checkempty = new System.Windows.Forms.ToolStripMenuItem();
            this.moveaotosave = new System.Windows.Forms.ToolStripMenuItem();
            this.autocopyjp = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Addr = new System.Windows.Forms.TextBox();
            this.txt_Hex = new System.Windows.Forms.TextBox();
            this.txt_utf8 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView = new System.Windows.Forms.ListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file,
            this.im_export,
            this.edit,
            this.testToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_open,
            this.file_save,
            this.file_saveas,
            this.toolStripMenuItem1,
            this.file_close});
            this.file.Name = "file";
            this.file.ShortcutKeyDisplayString = "";
            this.file.Size = new System.Drawing.Size(44, 21);
            this.file.Text = "文件";
            // 
            // file_open
            // 
            this.file_open.Name = "file_open";
            this.file_open.ShortcutKeyDisplayString = "Ctrl+O";
            this.file_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.file_open.Size = new System.Drawing.Size(180, 22);
            this.file_open.Text = "打开";
            this.file_open.Click += new System.EventHandler(this.File_open_Click);
            // 
            // file_save
            // 
            this.file_save.Name = "file_save";
            this.file_save.ShortcutKeyDisplayString = "Ctrl+S";
            this.file_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.file_save.Size = new System.Drawing.Size(180, 22);
            this.file_save.Text = "保存";
            this.file_save.Click += new System.EventHandler(this.File_save_Click);
            // 
            // file_saveas
            // 
            this.file_saveas.Name = "file_saveas";
            this.file_saveas.ShortcutKeyDisplayString = "Ctrl+Alt+S";
            this.file_saveas.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.file_saveas.Size = new System.Drawing.Size(180, 22);
            this.file_saveas.Text = "另存为";
            this.file_saveas.Click += new System.EventHandler(this.File_saveas_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // file_close
            // 
            this.file_close.Name = "file_close";
            this.file_close.ShortcutKeyDisplayString = "Alt+X";
            this.file_close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.file_close.Size = new System.Drawing.Size(180, 22);
            this.file_close.Text = "关闭";
            this.file_close.Click += new System.EventHandler(this.File_close_Click);
            // 
            // im_export
            // 
            this.im_export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editor_import,
            this.toolStripMenuItem2,
            this.editor_export});
            this.im_export.Name = "im_export";
            this.im_export.Size = new System.Drawing.Size(73, 21);
            this.im_export.Text = "导入/导出";
            // 
            // editor_import
            // 
            this.editor_import.Name = "editor_import";
            this.editor_import.ShortcutKeyDisplayString = "Ctrl+Alt+I";
            this.editor_import.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.I)));
            this.editor_import.Size = new System.Drawing.Size(168, 22);
            this.editor_import.Text = "导入";
            this.editor_import.Click += new System.EventHandler(this.Editor_import_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
            // 
            // editor_export
            // 
            this.editor_export.Name = "editor_export";
            this.editor_export.ShortcutKeyDisplayString = "Ctrl+Alt+E";
            this.editor_export.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.E)));
            this.editor_export.Size = new System.Drawing.Size(168, 22);
            this.editor_export.Text = "导出";
            this.editor_export.Click += new System.EventHandler(this.Editor_export_Click);
            // 
            // edit
            // 
            this.edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.up,
            this.down,
            this.toolStripMenuItem3,
            this.copyjp,
            this.savecn,
            this.toolStripMenuItem4,
            this.checkempty,
            this.moveaotosave,
            this.autocopyjp});
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(44, 21);
            this.edit.Text = "编辑";
            // 
            // up
            // 
            this.up.Name = "up";
            this.up.ShortcutKeyDisplayString = "Ctrl+↑";
            this.up.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.up.Size = new System.Drawing.Size(173, 22);
            this.up.Text = "上一条";
            this.up.Click += new System.EventHandler(this.Up_Click);
            // 
            // down
            // 
            this.down.Name = "down";
            this.down.ShortcutKeyDisplayString = "Ctrl+↓";
            this.down.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.down.Size = new System.Drawing.Size(173, 22);
            this.down.Text = "下一条";
            this.down.Click += new System.EventHandler(this.Down_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(170, 6);
            // 
            // copyjp
            // 
            this.copyjp.Name = "copyjp";
            this.copyjp.ShortcutKeyDisplayString = "Ctrl+←";
            this.copyjp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.copyjp.Size = new System.Drawing.Size(173, 22);
            this.copyjp.Text = "复制原文";
            this.copyjp.Click += new System.EventHandler(this.Copyjp_Click);
            // 
            // savecn
            // 
            this.savecn.Name = "savecn";
            this.savecn.ShortcutKeyDisplayString = "Ctrl+→";
            this.savecn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.savecn.Size = new System.Drawing.Size(173, 22);
            this.savecn.Text = "保存译文";
            this.savecn.Click += new System.EventHandler(this.Savecn_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(170, 6);
            // 
            // checkempty
            // 
            this.checkempty.CheckOnClick = true;
            this.checkempty.Name = "checkempty";
            this.checkempty.Size = new System.Drawing.Size(173, 22);
            this.checkempty.Text = "允许保存空译文";
            // 
            // moveaotosave
            // 
            this.moveaotosave.CheckOnClick = true;
            this.moveaotosave.Name = "moveaotosave";
            this.moveaotosave.Size = new System.Drawing.Size(173, 22);
            this.moveaotosave.Text = "移动自动保存";
            // 
            // autocopyjp
            // 
            this.autocopyjp.CheckOnClick = true;
            this.autocopyjp.Name = "autocopyjp";
            this.autocopyjp.Size = new System.Drawing.Size(173, 22);
            this.autocopyjp.Text = "自动复制原文";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.TestToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "太鼓文本文件|*.dat|所有文件|*.*";
            this.openFileDialog.Title = "打开";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_Addr, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_Hex, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_utf8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 346);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F";
            // 
            // txt_Addr
            // 
            this.txt_Addr.AllowDrop = true;
            this.txt_Addr.BackColor = System.Drawing.Color.White;
            this.txt_Addr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Addr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Addr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Addr.HideSelection = false;
            this.txt_Addr.Location = new System.Drawing.Point(4, 25);
            this.txt_Addr.Multiline = true;
            this.txt_Addr.Name = "txt_Addr";
            this.txt_Addr.ReadOnly = true;
            this.txt_Addr.Size = new System.Drawing.Size(49, 317);
            this.txt_Addr.TabIndex = 2;
            this.txt_Addr.TabStop = false;
            this.txt_Addr.Text = resources.GetString("txt_Addr.Text");
            this.txt_Addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Addr.WordWrap = false;
            this.txt_Addr.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.txt_Addr.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // txt_Hex
            // 
            this.txt_Hex.AllowDrop = true;
            this.txt_Hex.BackColor = System.Drawing.Color.White;
            this.txt_Hex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Hex.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Hex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Hex.HideSelection = false;
            this.txt_Hex.Location = new System.Drawing.Point(60, 25);
            this.txt_Hex.Multiline = true;
            this.txt_Hex.Name = "txt_Hex";
            this.txt_Hex.ReadOnly = true;
            this.txt_Hex.Size = new System.Drawing.Size(284, 317);
            this.txt_Hex.TabIndex = 3;
            this.txt_Hex.TabStop = false;
            this.txt_Hex.Text = resources.GetString("txt_Hex.Text");
            this.txt_Hex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Hex.WordWrap = false;
            this.txt_Hex.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.txt_Hex.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // txt_utf8
            // 
            this.txt_utf8.AllowDrop = true;
            this.txt_utf8.BackColor = System.Drawing.Color.White;
            this.txt_utf8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_utf8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_utf8.HideSelection = false;
            this.txt_utf8.Location = new System.Drawing.Point(351, 25);
            this.txt_utf8.Multiline = true;
            this.txt_utf8.Name = "txt_utf8";
            this.txt_utf8.ReadOnly = true;
            this.txt_utf8.Size = new System.Drawing.Size(115, 317);
            this.txt_utf8.TabIndex = 4;
            this.txt_utf8.TabStop = false;
            this.txt_utf8.Text = resources.GetString("txt_utf8.Text");
            this.txt_utf8.WordWrap = false;
            this.txt_utf8.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.txt_utf8.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "UTF8 文本";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标识";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "原文";
            this.columnHeader3.Width = 81;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "译文";
            this.columnHeader4.Width = 144;
            // 
            // listView
            // 
            this.listView.AllowDrop = true;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView.LabelWrap = false;
            this.listView.Location = new System.Drawing.Point(459, 28);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(321, 346);
            this.listView.TabIndex = 1;
            this.listView.TabStop = false;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemSelectionChanged);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
            this.listView.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.listView.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 377);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(784, 160);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(384, 152);
            this.textBox1.TabIndex = 0;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Enabled = false;
            this.textBox2.HideSelection = false;
            this.textBox2.Location = new System.Drawing.Point(395, 4);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(385, 152);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            this.textBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.textBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "保存";
            // 
            // FrmTaiko
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmTaiko";
            this.Text = "太鼓达人文本处理工具";
            this.Load += new System.EventHandler(this.Form_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem file_open;
        private System.Windows.Forms.ToolStripMenuItem file_save;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem file_close;
        private System.Windows.Forms.ToolStripMenuItem im_export;
        private System.Windows.Forms.ToolStripMenuItem editor_import;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem editor_export;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Addr;
        private System.Windows.Forms.TextBox txt_Hex;
        private System.Windows.Forms.TextBox txt_utf8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem edit;
        private System.Windows.Forms.ToolStripMenuItem up;
        private System.Windows.Forms.ToolStripMenuItem down;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem copyjp;
        private System.Windows.Forms.ToolStripMenuItem savecn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem moveaotosave;
        private System.Windows.Forms.ToolStripMenuItem autocopyjp;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem file_saveas;
        private System.Windows.Forms.ToolStripMenuItem checkempty;
    }
}

