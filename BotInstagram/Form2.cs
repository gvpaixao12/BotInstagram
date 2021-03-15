using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotInstagram
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void AdicionarAplicacaoAoIniciar()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue(caminho, "\"" + Application.ExecutablePath + "\"");
                }
            }
            catch
            {
                throw;
            }
        }
        public void RemoverAplicacaoAoIniciar()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue(caminho, false);
                }
            }
            catch
            {
                throw;
            }
        }
        string caminho = @"D:\VS\BotInstagram\BotInstagram\bin\Debug\BotInstagram.exe";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionarAplicacaoAoIniciar();
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                RemoverAplicacaoAoIniciar();
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
