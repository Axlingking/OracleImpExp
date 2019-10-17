using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OracleImpExp.Utilities;
using System.IO;
using System.Threading.Tasks;

namespace OracleImpExp.Import
{
    public partial class UCtrlImport : UserControl
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

        public UCtrlImport()
        {
            InitializeComponent();

            richTextBoxOutputer = new RichTextBoxOutputer(rtxtOutput);
        }

        public void Reload()
        {
            cmbDmps.Items.Clear();
            GetDmpFiles(Vars.DATA_PUMP_DIR).ForEach(a => cmbDmps.Items.Add(a));
            if (cmbDmps.Items.Count > 0) cmbDmps.SelectedIndex = 0;

            cmbUsers.Items.Clear();
            Vars.Users.ForEach(a => cmbUsers.Items.Add(a));
            if (cmbUsers.Items.Count > 0) cmbUsers.SelectedIndex = 0;
        }

        /// <summary>
        /// 获取 DATA_PUMP_DIR 下的数据库文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<DmpFile> GetDmpFiles(string path)
        {
            return Directory.GetFiles(path, "*.dmp").Select(a => new DmpFile(a)).ToList();
        }

        private void cmbDmps_SelectedIndexChanged(object sender, EventArgs e)
        {
            DmpFile dmpFile = cmbDmps.SelectedItem as DmpFile;
            if (dmpFile == null) return;

            if (string.IsNullOrEmpty(dmpFile.Schema))
                dmpFile.Schema = DmpSchemaReader.Read(dmpFile.FilePath);

            txtDestUser.Text = dmpFile.Schema;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string destSchema = txtDestUser.Text.Trim();
            if (string.IsNullOrWhiteSpace(destSchema)) return;

            string targetSchema = cmbUsers.Text;
            if (string.IsNullOrWhiteSpace(targetSchema)) return;

            string dumpfile = cmbDmps.Text;
            if (string.IsNullOrWhiteSpace(dumpfile)) return;

            // 确定导入到现有用户还是创建新用户
            bool userExists = DatabaseUtility.UserExists(targetSchema);

            string command = string.Empty;

            if (destSchema.Equals(targetSchema, StringComparison.CurrentCultureIgnoreCase))
                command = $"IMPDP USERID={Vars.UserId}/\"\"\"{Vars.Password}\"\"\"@orcl directory=DATA_PUMP_DIR dumpfile={dumpfile}.dmp logfile={dumpfile}.log";
            else
                command = $"IMPDP USERID={Vars.UserId}/\"\"\"{Vars.Password}\"\"\"@orcl directory=DATA_PUMP_DIR dumpfile={dumpfile}.dmp logfile={dumpfile}.log remap_schema={destSchema}:{targetSchema}";

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
                // 如果是创建新用户，则在导入后赋予权限以及设置默认密码
                if (!userExists && DatabaseUtility.UserExists(targetSchema))
                {
                    DatabaseUtility.GrantRole(targetSchema, "DBA");
                    DatabaseUtility.GrantRole(targetSchema, "CONNECT");
                    DatabaseUtility.GrantRole(targetSchema, "RESOURCE");
                    DatabaseUtility.GrantRole(targetSchema, "UNLIMITED TABLESPACE");

                    DatabaseUtility.UpdatePassword(targetSchema, Vars.DefaultPassword);
                }

                this.IsWorking = false;

                MessageBox.Show(this, "导入完成");
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
                cmbDmps.Enabled = enabled;
                txtDestUser.Enabled = enabled;
                cmbUsers.Enabled = enabled;
                btnImport.Enabled = enabled;
            });
        }

        #endregion
    }
}
