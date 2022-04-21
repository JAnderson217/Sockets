using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    internal class GetClients 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many clients would you like?");
            int numClients = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < numClients; i++)
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "Clients.exe";
                myProcess.Start();
                //MainClient client = new MainClient();
                //client.ClientJoin();
            }
        }
    }
}
