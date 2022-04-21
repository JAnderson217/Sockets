using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ChatServer
{
    class Server
    {
        const int portNo = 500;
        static void Main(string[] args)
        {
            Console.WriteLine("How many people are chatting?");
            int numClients = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < numClients; i++)
            {
                //launch clients
                Process myProcess = new Process();
                string path = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, 
                    @"..\..\..\"));
                //path = path + "ChatClient\\bin\\Debug\\ChatClient.exe";
                path = path + "ChatClientForm\\bin\\Debug\\ChatClientForm.exe";
                //Console.WriteLine(path);
                myProcess.StartInfo.FileName = path;
                myProcess.Start();
            }

            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAdd, portNo);
            listener.Start();
            while (true)
            {
                ChatClient user = new ChatClient(listener.AcceptTcpClient());
            }
        }
    }
}