using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.InteropServices;
using System.Diagnostics; 

namespace BotInstagram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        [DllImport("kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        CaixaMsg cx = new CaixaMsg();
        ChromeDriver drv = new ChromeDriver(); Thread th;
        string url = "https://www.instagram.com";
        string Caminhoinstrucoes = @"Instrucoes.txt";
        private void button1_Click(object sender, EventArgs e)
        {
            //th = new Thread(Login_Instagram); th.Start();            
        } 
        int horaini = DateTime.Now.Hour;
        int horaatu = 9;
        private void Form1_Load(object sender, EventArgs e)
        {
            Process.Start("notepad", Caminhoinstrucoes);
            LerArq();
            drv.Navigate().GoToUrl(url);
            th = new Thread(Login_Instagram);
            DateTime dataFim = DateTime.Parse(dataSorteio);
            if (DateTime.Now.Date <= dataFim)
            {
                if (horaini > horaatu)
                {
                    WindowState = FormWindowState.Minimized;
                    th.Start();
                }
            }
            else
            {
                MessageBox.Show("Data limite do Sorteio!!!");
                Close();
                drv.Close();
            }
        }
        public string CaminhoArquivo = @"senha.txt";

        public string username;
        public string password;
        public string dataSorteio;
        public string linkPost;
        private void LerArq()
        {
            if (File.Exists("senhaBot.txt"))
            {
                Stream entrada = File.Open("senhaBot.txt", FileMode.Open);
                StreamReader leitor = new StreamReader(entrada);
                string linha = leitor.ReadLine();
                while (linha != null)
                {
                    string[] colunas = linha.Split(';');
                    username = colunas[0];
                    password = colunas[1];
                    dataSorteio = colunas[2];
                    linkPost = colunas[3];
                    linha = leitor.ReadLine();
                }
                leitor.Close();
                entrada.Close();
            }
        }
        private void Login_Instagram()
        {
            Thread.Sleep(3000);
            try
            {
                drv.FindElement(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']")).SendKeys(username); Thread.Sleep(3000);
                drv.FindElement(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']")).SendKeys(password); Thread.Sleep(3000);

                //Login

                drv.FindElement(By.XPath("//button[@class = 'sqdOP  L3NKy   y3zKF     ']")).Click(); Thread.Sleep(5000);

            }
            catch(Exception erro)
            {
                MessageBox.Show("Algo de errado aconteceu! " + erro);
            }

            string erroSenha = drv.FindElementByClassName("eiCW-").Text;

            if (erroSenha != null)
            {
                //string aux = "Sua senha está inválida, tente alterar no arquivo";
                //cx.mensagem = aux;
                //cx.Show();
                MessageBox.Show("Sua senha está inválida, tente alterar no arquivo");
                drv.Close();
                Close();          
            }
            else
            {
                // Validação pós login
                drv.FindElement(By.XPath("//button[@class = 'sqdOP yWX7d    y3zKF     ']")).Click(); Thread.Sleep(6000);
                drv.FindElement(By.XPath("//button[@class = 'aOOlW   HoLwm ']")).Click(); Thread.Sleep(2000);

                // Ir para a pagina solicitada
                try
                {
                    drv.Navigate().GoToUrl(linkPost); Thread.Sleep(2000);
                    // Método sem retorno para escolha aleatória do comentário a ser digitado
                    comentarios();
                }
                catch(Exception ex)
                {

                }
                
            }
            
        }
        public string ex = null;

        #region Método de comentários
        private void comentarios()
        {
            string[] mensagens = new string[6];
            mensagens[0] = "vou ganhar esse sorteio";
            mensagens[1] = "já ta no papo";
            mensagens[2] = "esse sorteio é meu";
            mensagens[3] = "vou ganhar";
            mensagens[4] = "vou ganhar =D";
            mensagens[5] = "vou ganhar isso :D";

            string msgatual;
            Random r = new Random();
            Thread.Sleep(2000);

            
            Beep(1000, 300);
            try
            {

                while (ex == null)
                {
                    int posicao = r.Next(mensagens.Length);
                    msgatual = mensagens[posicao];
                    Thread.Sleep(2000);
                    //comentar
                    drv.FindElement(By.XPath("//textarea[@class='Ypffh']")).Click(); Thread.Sleep(4000);
                    drv.FindElement(By.XPath("//textarea[@class='Ypffh focus-visible']")).SendKeys(msgatual); Thread.Sleep(4000);
                    drv.FindElement(By.XPath("//button[@class = 'sqdOP yWX7d    y3zKF     ']")).Click(); Thread.Sleep(4500);


                }
                while (ex != null)
                {
                    Thread.Sleep(60000);
                    Beep(1000, 300);
                    drv.Navigate().Refresh();
                    ex = null;
                    comentarios();
                }
            }
            catch (Exception erro)
            {
                
                string aux = "Aguarde um momento para comentar novamente";
                cx.mensagem = aux;
                cx.Show();            
                drv.Navigate().Refresh();
                Thread.Sleep(2000);
                comentarios();
            }
        }
        #endregion

    }   

}
