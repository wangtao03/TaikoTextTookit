
namespace Transcriber
{
    partial class FemMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FemMain));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpSystem = new System.Windows.Forms.GroupBox();
            this.tlpSystem = new System.Windows.Forms.TableLayoutPanel();
            this.labSystem = new System.Windows.Forms.Label();
            this.txtSysFilePath = new System.Windows.Forms.TextBox();
            this.btnSysBrower = new System.Windows.Forms.Button();
            this.btnSysClear = new System.Windows.Forms.Button();
            this.btnSysCopy = new System.Windows.Forms.Button();
            this.lstvSystem = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chPath = new System.Windows.Forms.ColumnHeader();
            this.chRsult = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.grpStory = new System.Windows.Forms.GroupBox();
            this.tlpStory = new System.Windows.Forms.TableLayoutPanel();
            this.labStory = new System.Windows.Forms.Label();
            this.txtStoryDirPath = new System.Windows.Forms.TextBox();
            this.btnStoryBrower = new System.Windows.Forms.Button();
            this.btnStoryClear = new System.Windows.Forms.Button();
            this.btnStoryCopy = new System.Windows.Forms.Button();
            this.lstvStory = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.tlpMain.SuspendLayout();
            this.grpSystem.SuspendLayout();
            this.tlpSystem.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.grpStory.SuspendLayout();
            this.tlpStory.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.grpSystem, 0, 0);
            this.tlpMain.Controls.Add(this.grpStory, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(800, 450);
            this.tlpMain.TabIndex = 0;
            // 
            // grpSystem
            // 
            this.grpSystem.Controls.Add(this.tlpSystem);
            this.grpSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSystem.Location = new System.Drawing.Point(3, 3);
            this.grpSystem.Name = "grpSystem";
            this.grpSystem.Size = new System.Drawing.Size(394, 444);
            this.grpSystem.TabIndex = 0;
            this.grpSystem.TabStop = false;
            this.grpSystem.Text = "系统文本转录";
            // 
            // tlpSystem
            // 
            this.tlpSystem.ColumnCount = 4;
            this.tlpSystem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpSystem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSystem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSystem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpSystem.Controls.Add(this.labSystem, 0, 0);
            this.tlpSystem.Controls.Add(this.txtSysFilePath, 1, 0);
            this.tlpSystem.Controls.Add(this.btnSysBrower, 3, 0);
            this.tlpSystem.Controls.Add(this.btnSysClear, 0, 2);
            this.tlpSystem.Controls.Add(this.btnSysCopy, 3, 2);
            this.tlpSystem.Controls.Add(this.lstvSystem, 0, 1);
            this.tlpSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSystem.Location = new System.Drawing.Point(3, 19);
            this.tlpSystem.Name = "tlpSystem";
            this.tlpSystem.RowCount = 3;
            this.tlpSystem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpSystem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSystem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpSystem.Size = new System.Drawing.Size(388, 422);
            this.tlpSystem.TabIndex = 0;
            // 
            // labSystem
            // 
            this.labSystem.AutoSize = true;
            this.labSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labSystem.Location = new System.Drawing.Point(3, 0);
            this.labSystem.Name = "labSystem";
            this.labSystem.Size = new System.Drawing.Size(74, 28);
            this.labSystem.TabIndex = 0;
            this.labSystem.Text = "WordData:";
            this.labSystem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSysFilePath
            // 
            this.txtSysFilePath.AllowDrop = true;
            this.tlpSystem.SetColumnSpan(this.txtSysFilePath, 2);
            this.txtSysFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSysFilePath.Location = new System.Drawing.Point(83, 3);
            this.txtSysFilePath.Name = "txtSysFilePath";
            this.txtSysFilePath.Size = new System.Drawing.Size(222, 23);
            this.txtSysFilePath.TabIndex = 1;
            this.txtSysFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.txtSysFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.Cibtrol_DragEnter);
            // 
            // btnSysBrower
            // 
            this.btnSysBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSysBrower.Location = new System.Drawing.Point(311, 3);
            this.btnSysBrower.Name = "btnSysBrower";
            this.btnSysBrower.Size = new System.Drawing.Size(74, 22);
            this.btnSysBrower.TabIndex = 2;
            this.btnSysBrower.Text = "浏览";
            this.btnSysBrower.UseVisualStyleBackColor = true;
            this.btnSysBrower.Click += new System.EventHandler(this.BtnSysBrower_Click);
            // 
            // btnSysClear
            // 
            this.btnSysClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSysClear.Location = new System.Drawing.Point(3, 380);
            this.btnSysClear.Name = "btnSysClear";
            this.btnSysClear.Size = new System.Drawing.Size(74, 39);
            this.btnSysClear.TabIndex = 3;
            this.btnSysClear.Text = "清空";
            this.btnSysClear.UseVisualStyleBackColor = true;
            this.btnSysClear.Click += new System.EventHandler(this.BtnSysClear_Click);
            // 
            // btnSysCopy
            // 
            this.btnSysCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSysCopy.Location = new System.Drawing.Point(311, 380);
            this.btnSysCopy.Name = "btnSysCopy";
            this.btnSysCopy.Size = new System.Drawing.Size(74, 39);
            this.btnSysCopy.TabIndex = 4;
            this.btnSysCopy.Text = "转录文本";
            this.btnSysCopy.UseVisualStyleBackColor = true;
            this.btnSysCopy.Click += new System.EventHandler(this.BtnSysCopy_Click);
            // 
            // lstvSystem
            // 
            this.lstvSystem.AllowDrop = true;
            this.lstvSystem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chPath,
            this.chRsult});
            this.tlpSystem.SetColumnSpan(this.lstvSystem, 4);
            this.lstvSystem.ContextMenuStrip = this.contextMenuStrip;
            this.lstvSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvSystem.FullRowSelect = true;
            this.lstvSystem.GridLines = true;
            this.lstvSystem.HideSelection = false;
            this.lstvSystem.Location = new System.Drawing.Point(3, 31);
            this.lstvSystem.MultiSelect = false;
            this.lstvSystem.Name = "lstvSystem";
            this.lstvSystem.ShowItemToolTips = true;
            this.lstvSystem.Size = new System.Drawing.Size(382, 343);
            this.lstvSystem.TabIndex = 5;
            this.lstvSystem.UseCompatibleStateImageBehavior = false;
            this.lstvSystem.View = System.Windows.Forms.View.Details;
            this.lstvSystem.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop);
            this.lstvSystem.DragEnter += new System.Windows.Forms.DragEventHandler(this.Cibtrol_DragEnter);
            // 
            // chID
            // 
            this.chID.Text = "No.";
            this.chID.Width = 40;
            // 
            // chPath
            // 
            this.chPath.Text = "文件";
            this.chPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chPath.Width = 235;
            // 
            // chRsult
            // 
            this.chRsult.Text = "结果";
            this.chRsult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chRsult.Width = 100;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOutput});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // tsmiOutput
            // 
            this.tsmiOutput.Name = "tsmiOutput";
            this.tsmiOutput.Size = new System.Drawing.Size(124, 22);
            this.tsmiOutput.Text = "导出结果";
            this.tsmiOutput.Click += new System.EventHandler(this.TsmiOutput_Click);
            // 
            // grpStory
            // 
            this.grpStory.Controls.Add(this.tlpStory);
            this.grpStory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStory.Location = new System.Drawing.Point(403, 3);
            this.grpStory.Name = "grpStory";
            this.grpStory.Size = new System.Drawing.Size(394, 444);
            this.grpStory.TabIndex = 1;
            this.grpStory.TabStop = false;
            this.grpStory.Text = "故事文本转录";
            // 
            // tlpStory
            // 
            this.tlpStory.ColumnCount = 4;
            this.tlpStory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpStory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpStory.Controls.Add(this.labStory, 0, 0);
            this.tlpStory.Controls.Add(this.txtStoryDirPath, 1, 0);
            this.tlpStory.Controls.Add(this.btnStoryBrower, 3, 0);
            this.tlpStory.Controls.Add(this.btnStoryClear, 0, 2);
            this.tlpStory.Controls.Add(this.btnStoryCopy, 3, 2);
            this.tlpStory.Controls.Add(this.lstvStory, 0, 1);
            this.tlpStory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStory.Location = new System.Drawing.Point(3, 19);
            this.tlpStory.Name = "tlpStory";
            this.tlpStory.RowCount = 3;
            this.tlpStory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpStory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpStory.Size = new System.Drawing.Size(388, 422);
            this.tlpStory.TabIndex = 1;
            // 
            // labStory
            // 
            this.labStory.AutoSize = true;
            this.labStory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labStory.Location = new System.Drawing.Point(3, 0);
            this.labStory.Name = "labStory";
            this.labStory.Size = new System.Drawing.Size(74, 28);
            this.labStory.TabIndex = 0;
            this.labStory.Text = "StoryText:";
            this.labStory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStoryDirPath
            // 
            this.txtStoryDirPath.AllowDrop = true;
            this.tlpStory.SetColumnSpan(this.txtStoryDirPath, 2);
            this.txtStoryDirPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStoryDirPath.Location = new System.Drawing.Point(83, 3);
            this.txtStoryDirPath.Name = "txtStoryDirPath";
            this.txtStoryDirPath.Size = new System.Drawing.Size(222, 23);
            this.txtStoryDirPath.TabIndex = 1;
            this.txtStoryDirPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.txtStoryDirPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.Cibtrol_DragEnter);
            // 
            // btnStoryBrower
            // 
            this.btnStoryBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStoryBrower.Location = new System.Drawing.Point(311, 3);
            this.btnStoryBrower.Name = "btnStoryBrower";
            this.btnStoryBrower.Size = new System.Drawing.Size(74, 22);
            this.btnStoryBrower.TabIndex = 2;
            this.btnStoryBrower.Text = "浏览";
            this.btnStoryBrower.UseVisualStyleBackColor = true;
            this.btnStoryBrower.Click += new System.EventHandler(this.BtnStoryBrower_Click);
            // 
            // btnStoryClear
            // 
            this.btnStoryClear.AllowDrop = true;
            this.btnStoryClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStoryClear.Location = new System.Drawing.Point(3, 380);
            this.btnStoryClear.Name = "btnStoryClear";
            this.btnStoryClear.Size = new System.Drawing.Size(74, 39);
            this.btnStoryClear.TabIndex = 3;
            this.btnStoryClear.Text = "清空";
            this.btnStoryClear.UseVisualStyleBackColor = true;
            this.btnStoryClear.Click += new System.EventHandler(this.BtnStoryClear_Click);
            // 
            // btnStoryCopy
            // 
            this.btnStoryCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStoryCopy.Location = new System.Drawing.Point(311, 380);
            this.btnStoryCopy.Name = "btnStoryCopy";
            this.btnStoryCopy.Size = new System.Drawing.Size(74, 39);
            this.btnStoryCopy.TabIndex = 4;
            this.btnStoryCopy.Text = "转录文本";
            this.btnStoryCopy.UseVisualStyleBackColor = true;
            this.btnStoryCopy.Click += new System.EventHandler(this.BtnStoryCopy_Click);
            // 
            // lstvStory
            // 
            this.lstvStory.AllowDrop = true;
            this.lstvStory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.tlpStory.SetColumnSpan(this.lstvStory, 4);
            this.lstvStory.ContextMenuStrip = this.contextMenuStrip;
            this.lstvStory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvStory.FullRowSelect = true;
            this.lstvStory.GridLines = true;
            this.lstvStory.HideSelection = false;
            this.lstvStory.Location = new System.Drawing.Point(3, 31);
            this.lstvStory.MultiSelect = false;
            this.lstvStory.Name = "lstvStory";
            this.lstvStory.ShowItemToolTips = true;
            this.lstvStory.Size = new System.Drawing.Size(382, 343);
            this.lstvStory.TabIndex = 5;
            this.lstvStory.UseCompatibleStateImageBehavior = false;
            this.lstvStory.View = System.Windows.Forms.View.Details;
            this.lstvStory.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop);
            this.lstvStory.DragEnter += new System.Windows.Forms.DragEventHandler(this.Cibtrol_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "文件";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 235;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "结果";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 100;
            // 
            // FemMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FemMain";
            this.Text = "翻译转录器";
            this.tlpMain.ResumeLayout(false);
            this.grpSystem.ResumeLayout(false);
            this.tlpSystem.ResumeLayout(false);
            this.tlpSystem.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.grpStory.ResumeLayout(false);
            this.tlpStory.ResumeLayout(false);
            this.tlpStory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox grpSystem;
        private System.Windows.Forms.TableLayoutPanel tlpSystem;
        private System.Windows.Forms.Label labSystem;
        private System.Windows.Forms.TextBox txtSysFilePath;
        private System.Windows.Forms.Button btnSysBrower;
        private System.Windows.Forms.Button btnSysClear;
        private System.Windows.Forms.ListView lstvSystem;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chPath;
        private System.Windows.Forms.ColumnHeader chRsult;
        private System.Windows.Forms.GroupBox grpStory;
        private System.Windows.Forms.TableLayoutPanel tlpStory;
        private System.Windows.Forms.Label labStory;
        private System.Windows.Forms.TextBox txtStoryDirPath;
        private System.Windows.Forms.Button btnStoryBrower;
        private System.Windows.Forms.Button btnStoryClear;
        private System.Windows.Forms.Button btnStoryCopy;
        private System.Windows.Forms.ListView lstvStory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnSysCopy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiOutput;
    }
}

