using Newtonsoft.Json;

using System.IO;

namespace hostinpanda.daemon
{
    public class ConfigObject
    {
        public string DatabaseConnectionString { get; set; }

        public string SMTPHostName { get; set; }

        public string SMTPUsername { get; set; }

        public string SMTPPassword { get; set; }

        public int SMTPPort { get; set; }

        public static ConfigObject Load(string configFile)
        {
            if (!File.Exists(configFile))
            {
                return default(ConfigObject);
            }

            return JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(configFile));
        }
    }
}