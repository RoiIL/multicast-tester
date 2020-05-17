using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MUC
{
    class MulticastSender
    {
        protected UdpClient sender;

        private const int bindPort = 9191;
        private int multicastPort = 0;

        protected IPEndPoint sendToEp;

        private IPAddress multicastAddress;
        private IPAddress remote;
        private IPAddress local;

        public MulticastSender(string remoteAddress, string localAddress, Peer peer)
        {
            bool parsed = IPAddress.TryParse(peer.multicast, out multicastAddress);
            parsed &= IPAddress.TryParse(remoteAddress, out remote);
            if (string.IsNullOrEmpty(localAddress))
            {
                local = IPAddress.Any;
            }
            else
            {
                parsed &= IPAddress.TryParse(localAddress, out local);
            }
            
            if (!parsed)
            {
                Console.WriteLine("Got an error while parsing the Config.json file");
                Environment.Exit(1);
            }         

            sender = new UdpClient();
            sender.Ttl = 64;
            sender.JoinMulticastGroup(multicastAddress);
            sender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);                      

            try
            {
                sender.Client.Bind(new IPEndPoint(local, bindPort));
            }
            catch (SocketException)
            {
                Console.WriteLine("ERROR! The given local IP is not valid, please check your machine's IP address and correct it in the Config.json file.");
                Console.ReadLine();
                Environment.Exit(-1);
            }

            multicastPort = peer.port;
            sendToEp = new IPEndPoint(multicastAddress, multicastPort);
        }

        public void Send()
        {
            Thread myNewThread = new Thread(() => SendMulticast());
            myNewThread.Start();
        }

        public void SendMulticast()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("Hello");
                    sender.Send(bytes, bytes.Length, sendToEp);
                    Thread.Sleep(1000);
                    Console.WriteLine("Sending Multicast to {0} on port {1} : length {2}\n", multicastAddress.ToString(), multicastPort, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sender.Close();
                sender.Dispose();
                Console.ResetColor();
            }
        }
    }
}
