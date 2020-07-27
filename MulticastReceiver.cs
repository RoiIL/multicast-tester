using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MUC
{
    class MulticastReceiver : IDisposable
    {
        protected UdpClient listener;

        protected int listenPort = 0;
        protected int outPort = 0;

        protected IPEndPoint inPoint;
        protected IPAddress multicastAddress;
        protected IPAddress localAddress;

        public MulticastReceiver(string remoteAddress, string local, Peer peer)
        {
            bool parsed = IPAddress.TryParse(peer.multicast, out multicastAddress);
            if (string.IsNullOrEmpty(local))
            {
                localAddress = IPAddress.Any;
            }
            else
            {
                parsed &= IPAddress.TryParse(local, out localAddress);
            }

            listenPort = peer.port;
            listener = new UdpClient();                      
            inPoint = new IPEndPoint(localAddress, listenPort);            

            try
            {
                listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                listener.MulticastLoopback = true;
                listener.JoinMulticastGroup(multicastAddress);                
                listener.Client.Bind(inPoint);
            }
            catch (SocketException)
            {
                Console.WriteLine("ERROR! The given local IP is not valid, please check your machine's IP address and correct it in the Config.json file.");
                Console.ReadLine();
                Environment.Exit(-1);
            }            
        }

        public void Receive()
        {
            Thread myNewThread = new Thread(() => ReceiveMulticast());
            myNewThread.Start();
        }

        public void ReceiveMulticast()
        {
            try
            {                
                Console.WriteLine("Listening on Multicast: " + multicastAddress.ToString() + ", Port: " + listenPort.ToString());
                while (true)
                {
                    Byte[] data = listener.Receive(ref inPoint);
                    string strData = Encoding.ASCII.GetString(data);
                    Console.WriteLine(strData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
                Console.ResetColor();
            }
        }

        public void Dispose()
        {
            listener.Close();
        }
    }
}
