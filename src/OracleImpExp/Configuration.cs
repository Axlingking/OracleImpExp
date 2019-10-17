using System;
using System.IO;

namespace OracleImpExp
{
    public class Configuration
    {
        private static object locker = new object();
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json");

        private static Configuration instance;
        public static Configuration Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        if (!File.Exists(filePath))
                        {
                            instance = new Configuration();
                            instance.Save();
                        }
                        else
                            instance = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(filePath));
                    }

                    return instance;
                }
            }
        }

        public void Save()
        {
            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(Configuration.Instance, Newtonsoft.Json.Formatting.Indented));
        }

        private Configuration()
        {

        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; } = "system";

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
