using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OracleImpExp.Utilities
{
    class DatabaseUtility
    {
        /// <summary>
        /// 返回指定用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool UserExists(string userName)
        {
            userName = userName.ToUpper();

            using (OracleConnection connection = new OracleConnection(Vars.ConnectionString))
            {
                return connection.ExecuteScalar<int>("select count(0) from all_users u where u.USERNAME=:userName", new { userName }) > 0;
            }
        }

        /// <summary>
        /// 赋予指定用户权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        public static void GrantRole(string userName, string roleName)
        {
            userName = userName.ToUpper();
            roleName = roleName.ToUpper();

            using (OracleConnection connection = new OracleConnection(Vars.ConnectionString))
            {
                if (connection.ExecuteScalar<int>("select count(0) from dba_role_privs p where p.GRANTEE = :userName", new { userName }) > 0)
                    return; 

                connection.Execute($"grant {roleName} to {userName}");
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public static void UpdatePassword(string userName, string password)
        {
            userName = userName.ToUpper();

            using (OracleConnection connection = new OracleConnection(Vars.ConnectionString))
            {
                connection.Execute($"alter user {userName} identified by \"{password}\"");
            }
        }
    }
}
