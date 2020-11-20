using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OracleImpExp
{
    public static class Vars
    {
        /// <summary>
        /// 用户默认密码
        /// </summary>
        public const string DefaultPassword = "1234"; 

        /// <summary>
        /// 当前登录Server
        /// </summary>
        public static string Server = string.Empty;

        /// <summary>
        /// 当前登录用户Id
        /// </summary>
        public static string UserId = string.Empty;

        /// <summary>
        /// 当前登录用户密码
        /// </summary>
        public static string Password = string.Empty;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get { return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={Server})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id={UserId};Password={Password};"; } }
        
        /// <summary>
        /// DATA_PUMP_DIR 指定的目录
        /// </summary>
        public static string DATA_PUMP_DIR = string.Empty;

        /// <summary>
        /// 用户清单
        /// </summary>
        public static List<string> Users = new List<string>();
    }
}
