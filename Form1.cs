using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        Socket server, client;
        int port;
        byte[] data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint gui = new IPEndPoint(IPAddress.Parse(textBox1.Text), 8000);
            UdpClient udp = new UdpClient();
            
            udp.Connect(gui);
            var timeToWait = TimeSpan.FromSeconds(1);
            var asyncResult = udp.BeginReceive(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(timeToWait);
            if (asyncResult.IsCompleted)
            {
                try
                {
                    byte[] data = Encoding.ASCII.GetBytes("abc");
                    udp.Send(data, data.Length);
                    listBox1.Items.Add(gui.ToString() + " port mo");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(gui.ToString() + " port dong");
                }
            }
            else
            {
                listBox1.Items.Add("Time Out");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(textBox1.Text), 80);
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(ip);
                listBox1.Items.Add(ip.ToString() + " port mo");
            }
            catch {
                listBox1.Items.Add(ip.ToString() + " port dong");
            }
        }
    }
}
