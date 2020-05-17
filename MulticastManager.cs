using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUC
{
    class MulticastManager
    {
        private List<MulticastSender> uniToMultiList;

        public MulticastManager()
        {
            uniToMultiList = new List<MulticastSender>();
        }

        public void runMuc()
        {
            Config config = new Config();
            Console.WriteLine("Reading configuration");
            Connection connection = config.readConfiguration("");
            Console.WriteLine("Read configuration successfully");

            foreach (Peer peer in connection.peers)
            {
                MulticastSender uniToMulti = new MulticastSender(connection.remoteAddress, connection.localAddress, peer);
                uniToMulti.Send();
                uniToMultiList.Add(uniToMulti);
            }
        }
    }
}
