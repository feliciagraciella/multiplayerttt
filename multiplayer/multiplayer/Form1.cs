using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multiplayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(false, textBox1.Text);
            Visible = false;
            if (!form.IsDisposed)
                form.ShowDialog();
            Visible = true;
        }

        private void buttonHost_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(true, textBox1.Text);
            Visible = false;
            if (!form.IsDisposed)
                form.ShowDialog();
            Visible = true;
        }
    }
}
