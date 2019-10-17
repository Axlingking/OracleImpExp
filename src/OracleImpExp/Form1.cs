using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OracleImpExp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblVersion.Text = $"版本:{Assembly.GetEntryAssembly().GetName().Version.ToString()}";
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            FrmConnection frmConnection = new FrmConnection();
            if (frmConnection.ShowDialog() == DialogResult.OK)
            {
                this.Text = $"Oracle数据库备份还原工具 - {Vars.UserId}";
                lblDATA_PUMP_DIR.Text = $"DATA_PUMP_DIR={Vars.DATA_PUMP_DIR}";

                uCtrlImport1.Reload();
                uCtrlExport1.Reload();
            }
        }

        private void btnDATA_PUMP_DIR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Vars.DATA_PUMP_DIR)) return;

            if (Directory.Exists(Vars.DATA_PUMP_DIR))
                Process.Start(Vars.DATA_PUMP_DIR);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提供对Oracle数据库的数据泵模式导入导出（IMPDP/EXPDP）功能。\r需要在服务端环境运行。基于Oracle 12c版本进行测试。\r\r作者：曾子凌", "关于");
        }
    }
}
