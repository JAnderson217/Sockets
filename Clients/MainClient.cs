using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public partial class MainClient
    {
        TcpClient client = new TcpClient();
        const int portNo = 500;
        byte[] data;
        public void ClientJoin()
        {
            Console.WriteLine("Welcome to the chat! Enter your nickname: ");
            //add error for blank
            string name = Console.ReadLine();
            Console.WriteLine($"Welcome {name}, type a message and press enter to send a message.");
            try
            {
                //---connect to server---
                client = new TcpClient();
                client.Connect("127.0.0.1", portNo);
                data = new byte[client.ReceiveBufferSize];
                //---read from server---
                SendMessage(name);
                client.GetStream().BeginRead(data, 0,
                System.Convert.ToInt32(
                client.ReceiveBufferSize),
                ReceiveMessage, null);
                //send message to server
                if (Console.ReadLine() != "exit")
                {
                    SendMessage(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void SendMessage(string message)
        {
            try
            {
                //---send a message to the server---
                NetworkStream ns = client.GetStream();
                byte[] data =
                System.Text.Encoding.ASCII.GetBytes(message);
                //---send the text---
                ns.Write(data, 0, data.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                int bytesRead;
                //---read the data from the server---
                bytesRead = client.GetStream().EndRead(ar);
                if (bytesRead < 1)
                {
                    return;
                }
                else
                {
                    //---invoke the delegate to display the
                    // received data---
                    object[] para = { System.Text.Encoding.ASCII.GetString(data, 0, bytesRead) };
                    Console.WriteLine(para);
                }
                //---continue reading...---
                client.GetStream().BeginRead(data, 0,
                System.Convert.ToInt32(client.ReceiveBufferSize),
                ReceiveMessage, null);
            }
            catch (Exception ex)
            {
                //---ignore the error; fired when a user signs off---
            }
        }

        public void Disconnect()
        {
            try
            {
                //---Disconnect from server---
                client.GetStream().Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
