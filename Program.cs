using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MUC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("                            - Multicast Tester -                           ");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("                         *** Sending Multicast ***     ");
            Console.WriteLine(Environment.NewLine);

            // Thread.Sleep(15000);

            MulticastManager mucManager = new MulticastManager();
            mucManager.runMuc();
        }
    }
}
