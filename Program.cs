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

            bool isCorrectInput = false;
            string userInput = string.Empty;
            do
            {
                Console.WriteLine("Enter 'S' for Sending Multicast");
                Console.WriteLine("Enter 'R' for Receiving Multicast");
                Console.WriteLine("Enter 'M' for Sending and Receiving Multicast");
                userInput = Console.ReadLine();
                if (userInput.ToLower().Equals("s") ||
                    userInput.ToLower().Equals("r") ||
                    userInput.ToLower().Equals("m"))
                {
                    isCorrectInput = true;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Come on! Really??? Ok, let's try again...");
                    Console.WriteLine(Environment.NewLine);
                }
            } while (!isCorrectInput);
            

            MulticastManager mucManager = new MulticastManager();
            mucManager.runMuc(userInput.ToLower());
        }
    }
}
