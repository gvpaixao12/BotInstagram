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
        private void button1_Click(object sender, EventArgs e)
        {
            //th = new Thread(Login_Instagram); th.Start();
        }
        DateTime dataFim = DateTime.Parse("22/03/2021");
        int horaini = DateTime.Now.Hour;
        int horaatu = 9;
        private void Form1_Load(object sender, EventArgs e)
        {             
            if (DateTime.Now.Date <= dataFim)
            {                
                if (horaini>horaatu)
                {                    
                    drv.Navigate().GoToUrl(url);
                    th = new Thread(Login_Instagram);
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
                    linha = leitor.ReadLine();
                }
                leitor.Close();
                entrada.Close();
            }
        }
        private void Login_Instagram()
        {
            LerArq();

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
            // Validação pós login
            drv.FindElement(By.XPath("//button[@class = 'sqdOP yWX7d    y3zKF     ']")).Click(); Thread.Sleep(6000);
            drv.FindElement(By.XPath("//button[@class = 'aOOlW   HoLwm ']")).Click(); Thread.Sleep(2000);

            //ir para a pagina certa
            drv.Navigate().GoToUrl("https://www.instagram.com/p/CLjvp9Igvmn/"); Thread.Sleep(2000);

            comentarios();

            
        }
        public string ex = null;
        private void comentarios()
        {
            string[] mensagens = new string[6];
            mensagens[0] = "na altura da parachocada";
            mensagens[1] = "bugador";
            mensagens[2] = "Do futuro";
            mensagens[3] = "Sem limites";
            mensagens[4] = "Quem é lindo é ele";
            mensagens[5] = "Esse carro vai ser meu";

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
                //teste
                cx.Show();
            
                drv.Navigate().Refresh();
                Thread.Sleep(2000);
                comentarios();
            }

        }
        
    }   

}
