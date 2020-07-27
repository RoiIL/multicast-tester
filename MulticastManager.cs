using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MUC
{
    class MulticastManager
    {
        private List<MulticastSender> uniToMultiList;

        public MulticastManager()
        {
            uniToMultiList = new List<MulticastSender>();
        }

        public void runMuc(string userChoice)
        {
            Config config = new Config();
            Console.WriteLine("Reading configuration");
            Connection connection = config.readConfiguration("");
            Console.WriteLine("Read configuration successfully");

            foreach (Peer peer in connection.peers)
            {
                switch(userChoice)
                {
                    case "r":
                        new MulticastReceiver(connection.remoteAddress, connection.localAddress, peer).Receive();
                        break;
                    case "s":
                        new MulticastSender(connection.remoteAddress, connection.localAddress, peer).Send();
                        break;
                    default:
                        new MulticastSender(connection.remoteAddress, connection.localAddress, peer).Send();
                        new MulticastReceiver(connection.remoteAddress, connection.localAddress, peer).Receive();
                        break;
                }
            }
        }
    }
}
