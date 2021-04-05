using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotInstagram
{
    public partial class CaixaMsg : Form
    {
        public CaixaMsg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string mensagem;

        private void CaixaMsg_Load(object sender, EventArgs e)
        {
            textBox1.Text = mensagem;
            Thread.Sleep(4000);            
           // button1.PerformClick();
        }
    }
}
