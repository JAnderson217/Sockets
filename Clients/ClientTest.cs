/*using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
namespace Clients
{
    class ClientTest
    {
        const int portNo = 500;
        static void Main(string[] args)
        {
            TcpClient tcpclient = new TcpClient();
            //---connect to the server---
            tcpclient.Connect("127.0.0.1", portNo);
            //---use a NetworkStream object to send and receive
            // data---
            NetworkStream ns = tcpclient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("Client A");
            //---send the text---
            ns.Write(data, 0, data.Length);
        }
    }
}*/