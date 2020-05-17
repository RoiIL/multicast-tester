using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace MUC
{
    class Config
    {
        public Config()
        {
        }

        public Connection readConfiguration(string configFilePath)
        {
            Connection connection = null;
            if (String.IsNullOrEmpty(configFilePath))
            {
                configFilePath = "./Config.json";
            }

            try
            {
                using (StreamReader readConfig = new StreamReader(configFilePath))
                {
                    string json = readConfig.ReadToEnd();
                    connection = JsonConvert.DeserializeObject<Connection>(json);
                }
            }
            catch
            (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(@"ERROR: The 'Config.json' file was not found");
                Console.ResetColor();
            }

            return connection;            
        }
    }

    class Connection
    {
        public string remoteAddress = string.Empty;
        public string localAddress = string.Empty;
        public List<Peer> peers = new List<Peer>();
    }

    class Peer
    {
        public string multicast = string.Empty;
        public int port = 0;
        public bool commandPeer = false;
    }
}
