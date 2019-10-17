namespace OracleImpExp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDATA_PUMP_DIR = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDATA_PUMP_DIR = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabImport = new System.Windows.Forms.TabPage();
            this.tabExport = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.uCtrlImport1 = new OracleImpExp.Import.UCtrlImport();
            this.uCtrlExport1 = new OracleImpExp.Export.UCtrlExport();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabImport.SuspendLayout();
            this.tabExport.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblVersion,
            this.toolStripStatusLabel1,
            this.lblDATA_PUMP_DIR});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblVersion
            // 
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 17);
            this.lblVersion.Text = "版本:1.0.0.0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(598, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblDATA_PUMP_DIR
            // 
            this.lblDATA_PUMP_DIR.Name = "lblDATA_PUMP_DIR";
            this.lblDATA_PUMP_DIR.Size = new System.Drawing.Size(115, 17);
            this.lblDATA_PUMP_DIR.Text = "DATA_PUMP_DIR=";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnection,
            this.btnDATA_PUMP_DIR,
            this.btnAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnConnection
            // 
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(60, 21);
            this.btnConnection.Text = "连接(&C)";
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // btnDATA_PUMP_DIR
            // 
            this.btnDATA_PUMP_DIR.Name = "btnDATA_PUMP_DIR";
            this.btnDATA_PUMP_DIR.Size = new System.Drawing.Size(135, 21);
            this.btnDATA_PUMP_DIR.Text = "DATA_PUMP_DIR(&D)";
            this.btnDATA_PUMP_DIR.Click += new System.EventHandler(this.btnDATA_PUMP_DIR_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(60, 21);
            this.btnAbout.Text = "关于(&A)";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // tabImport
            // 
            this.tabImport.BackColor = System.Drawing.SystemColors.Control;
            this.tabImport.Controls.Add(this.uCtrlExport1);
            this.tabImport.Location = new System.Drawing.Point(4, 22);
            this.tabImport.Name = "tabImport";
            this.tabImport.Padding = new System.Windows.Forms.Padding(3);
            this.tabImport.Size = new System.Drawing.Size(792, 377);
            this.tabImport.TabIndex = 1;
            this.tabImport.Text = " 导 出 ";
            // 
            // tabExport
            // 
            this.tabExport.BackColor = System.Drawing.SystemColors.Control;
            this.tabExport.Controls.Add(this.uCtrlImport1);
            this.tabExport.Location = new System.Drawing.Point(4, 22);
            this.tabExport.Name = "tabExport";
            this.tabExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabExport.Size = new System.Drawing.Size(792, 377);
            this.tabExport.TabIndex = 0;
            this.tabExport.Text = " 导 入 ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabExport);
            this.tabControl1.Controls.Add(this.tabImport);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 403);
            this.tabControl1.TabIndex = 2;
            // 
            // uCtrlImport1
            // 
            this.uCtrlImport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uCtrlImport1.Location = new System.Drawing.Point(3, 3);
            this.uCtrlImport1.Name = "uCtrlImport1";
            this.uCtrlImport1.Size = new System.Drawing.Size(786, 371);
            this.uCtrlImport1.TabIndex = 0;
            // 
            // uCtrlExport1
            // 
            this.uCtrlExport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uCtrlExport1.Location = new System.Drawing.Point(3, 3);
            this.uCtrlExport1.Name = "uCtrlExport1";
            this.uCtrlExport1.Size = new System.Drawing.Size(786, 371);
            this.uCtrlExport1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oracle数据库备份还原工具 - 未连接";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabImport.ResumeLayout(false);
            this.tabExport.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblVersion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnConnection;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripStatusLabel lblDATA_PUMP_DIR;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabPage tabImport;
        private System.Windows.Forms.TabPage tabExport;
        private System.Windows.Forms.TabControl tabControl1;
        private Export.UCtrlExport uCtrlExport1;
        private System.Windows.Forms.ToolStripMenuItem btnDATA_PUMP_DIR;
        private Import.UCtrlImport uCtrlImport1;
    }
}

