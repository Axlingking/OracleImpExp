﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using OracleImpExp.Utilities;

namespace OracleImpExp
{
    public partial class FrmConnection : Form
    {
        public FrmConnection()
        {
            InitializeComponent();
        }

        private void FrmConnection_Load(object sender, EventArgs e)
        {
            txtServer.Text = Configuration.Instance.Server;
            txtUserId.Text = Configuration.Instance.UserId;
            txtPassword.Text = Configuration.Instance.Password;
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();

            string userId = txtUserId.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password))
                return;

            if (userId.Equals("sys", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"不支持 sys 登录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (OracleConnection connection = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={server})));User Id={userId};Password={password};"))
                {
                    connection.Open();

                    Vars.Server = server;
                    Vars.UserId = userId;
                    Vars.Password = password;

                    Configuration.Instance.Server = server;
                    Configuration.Instance.UserId = userId;
                    Configuration.Instance.Password = password;
                    Configuration.Instance.Save();

                    // 获取 DATA_PUMP_DIR 指定的目录
                    Vars.DATA_PUMP_DIR = connection.ExecuteScalar<string>("select t.DIRECTORY_PATH from dba_directories t where t.OWNER='SYS' and t.DIRECTORY_NAME='DATA_PUMP_DIR'");
                    // 获取所有可用用户
                    Vars.Users = connection.Query<string>("select t.USERNAME from dba_users t where t.account_status='OPEN' and t.DEFAULT_TABLESPACE!='SYSTEM' order by t.USERNAME asc").ToList();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败。{ex.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
