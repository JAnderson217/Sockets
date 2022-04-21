/*using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Clients
{
    class MultipleClients
    {
        const int portNo = 500;
        static void Main(string[] args)
        {
            SendAMessage("Client 1", "Hello this is a message from client 1");
            SendAMessage("Client 2", "Hello this is a message from client 2");
            Console.ReadLine();
        }

        public static void SendAMessage(string name, string message)
        {
            TcpClient tcpclient = new TcpClient();
            //---connect to the server---
            tcpclient.Connect("127.0.0.1", portNo);
            //---use a NetworkStream object to send and receive
            // data---
            NetworkStream ns = tcpclient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(name);
            //---send username---
            ns.Write(data, 0, data.Length);
            Thread.Sleep(1);
            data = Encoding.ASCII.GetBytes(message);
            //---send message---
            ns.Write(data, 0, data.Length);
        }


    }
}*/