using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClientForm
{
    public partial class Form1 : Form
    {
        const int portNo = 500;
        TcpClient client;
        byte[] data;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new
System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
        }


        private void btnLogIn_Click(object sender, EventArgs e)


        {
            if (btnLogIn.Text.Equals("Log In"))
            {
                if (!txtName.Text.Equals(""))
                {
                    try
                    {
                        //---connect to server---
                        client = new TcpClient();
                        client.Connect("127.0.0.1", portNo);
                        data = new byte[client.ReceiveBufferSize];
                        //---read from server---
                        SendMessage(txtName.Text);
                        client.GetStream().BeginRead(data, 0,
                        System.Convert.ToInt32(
                        client.ReceiveBufferSize),
                        ReceiveMessage, null);
                        btnLogIn.Text = "Log Out";
                        btnSend.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Enter a nickname");
                }
            }
            else
            {
                Disconnect();
                btnLogIn.Text = "Log In";
                btnLogIn.Enabled = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(txtSend.Text);
            txtSend.Text = "";
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
                    this.Invoke(new delUpdateHistory(UpdateHistory), para);
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

        //---delegate and subroutine to update the TextBox control---
        public delegate void delUpdateHistory(string str);
        public void UpdateHistory(string str)
        {
            txtMessages.AppendText(str);
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

        private void Form_Closing(object sender,FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
