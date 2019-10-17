using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OracleImpExp.Utilities;
using System.Threading.Tasks;

namespace OracleImpExp.Export
{
    public partial class UCtrlExport : UserControl
    {
        bool isWorking = false;
        RichTextBoxOutputer richTextBoxOutputer;

        /// <summary>
        /// 正在工作
        /// </summary>
        bool IsWorking
        {
            get { return isWorking; }
            set
            {
                ChangeControlEnabled(!value);
                isWorking = value;
            }
        }

        public UCtrlExport()
        {
            InitializeComponent();

            richTextBoxOutputer = new RichTextBoxOutputer(rtxtOutput);
        }

        public void Reload()
        {
            cmbUsers.Items.Clear();
            Vars.Users.ForEach(a => cmbUsers.Items.Add(a));
            if (cmbUsers.Items.Count > 0) cmbUsers.SelectedIndex = 0;
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFileName.Text = $"{cmbUsers.Text}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string schemas = cmbUsers.Text;
            if (string.IsNullOrWhiteSpace(schemas)) return;

            string fileName = txtFileName.Text.Trim();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            string command = $"EXPDP USERID={Vars.UserId}/\"\"\"{Vars.Password}\"\"\"@orcl schemas={schemas} directory=DATA_PUMP_DIR dumpfile={fileName}.dmp logfile={fileName}.log";

            this.IsWorking = true;

            Task task = new Task(() =>
            {
                CommandUtility.Execute2(command, a =>
                {
                    UpdateUIInThread(() => richTextBoxOutputer.AppendText(a));
                });
            });

            task.Start();

            task.ContinueWith((a) =>
            {
                this.IsWorking = false;

                MessageBox.Show(this, "导出完成");
            });
        }

        #region UI

        /// <summary>
        /// 更新界面
        /// </summary>
        /// <param name="action"></param>
        private void UpdateUIInThread(Action action)
        {
            if (this.Disposing || this.IsDisposed) return;

            if (this.InvokeRequired)
                this.Invoke(action);
            else
                action();
        }

        /// <summary>
        /// 启用/禁用界面操作
        /// </summary>
        /// <param name="enabled"></param>
        private void ChangeControlEnabled(bool enabled)
        {
            UpdateUIInThread(() =>
            {
                cmbUsers.Enabled = enabled;
                txtFileName.Enabled = enabled;
                btnExport.Enabled = enabled;
            });
        }

        #endregion
    }
}
