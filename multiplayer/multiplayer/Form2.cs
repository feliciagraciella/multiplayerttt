using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace multiplayer
{
    public partial class Form2 : Form
    {
        public Form2(bool isHost, string ip)
        {
            InitializeComponent();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;

            if (isHost)
            {
                server = new TcpListener(System.Net.IPAddress.Any, 12);
                server.Start();
                soc = server.AcceptSocket();
            }
            else
            {
                try
                {
                    client = new TcpClient(ip, 12);
                    soc = client.Client;
                    MessageReceiver.RunWorkerAsync();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            freeze();
            label1.Text = "Player 2!";
            ReceiveMove();
            label1.Text = "Player 1!";
            unfreeze();
        }

        private Socket soc;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;
        private void ReceiveMove()
        {
            byte[] buffer = new byte[1];
            soc.Receive(buffer);
            //if (buffer[0] == 1)

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] num = { 1 };
            soc.Send(num);
            MessageReceiver.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] num = { 2 };
            soc.Send(num);
            MessageReceiver.RunWorkerAsync();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageReceiver.WorkerSupportsCancellation = true;
            MessageReceiver.CancelAsync();
            if (server != null)
                server.Stop();
        }

        private void freeze()
        {
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void unfreeze()
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

    }
}
