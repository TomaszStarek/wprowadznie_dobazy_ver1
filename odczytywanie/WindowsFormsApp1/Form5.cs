using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Timers;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        private static System.Timers.Timer aTimer;

        public Form5()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            SetTimer();
        }


        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        // bool flaga; 
        TimeSpan result;
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            DateTime do_licznika = DateTime.Now;
            result = do_licznika - Program.global_czasstart;
            //   textBox3.Text = result.Minutes.ToString();

            ////////////////////////if (textBox3.InvokeRequired == true)
            ////////////////////////    textBox3.Invoke((MethodInvoker)delegate { result.Minutes.ToString(); });

            ////////////////////////else
            ////////////////////////    textBox3.Text = result.Minutes.ToString();
            ///
            textBox3.BeginInvoke(new InvokeDelegate(InvokeMethod));

            //          UpdateControl(label1, SystemColors.Control, result.Minutes.ToString(), true);
            //          UpdateControl(label4, SystemColors.Control, result.Seconds.ToString(), true);

        }

        public delegate void InvokeDelegate();

        //private void Invoke_Click(object sender, EventArgs e)
        //{
        //    myTextBox.BeginInvoke(new InvokeDelegate(InvokeMethod));
        //}
        public void InvokeMethod()
        {
            textBox3.Text = result.Minutes.ToString();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox2.Text;
            
        }
        private void stacjaBox_TextChanged(object sender, EventArgs e)
        {
            label10.Text = stacjaBox.Text;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string minutes = textBox3.Text;
            string opis = label4.Text;
            string stacja = label1.Text;
            string sekcja = label10.Text;
            string cmdString = "INSERT INTO [dbo].[awarieGM3] ([sekcja],[stacja],[opis],[min],[czas_start],[czas_stop]) VALUES ( @val2, @val3, @val4, @val5, @val6, @val7)";
            //    string connString = @"Data Source=PLKWIM0LME045; Initial Catalog = test; Integrated Security = SSPI";                                   PLKWIM0LME045\WINCCPLUSMIG2014     server=192.168.1.10;
            // string connString = @"database=PLKWIM0P21B1GM3\PLKWIM0P21B1GM3; Initial Catalog = test; User ID = loginsql; Password = Jabil12345";
            string connString = @"database=PLKWIM0P21B1GM3; Initial Catalog = test; User ID = loginsql; Password = Jabil12345";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");
                    comm.Parameters.AddWithValue("@val2", sekcja);
                    comm.Parameters.AddWithValue("@val3", stacja);
                    comm.Parameters.AddWithValue("@val4", opis);
                    comm.Parameters.AddWithValue("@val5", minutes);
                    comm.Parameters.AddWithValue("@val6", Program.global_czasstart.ToString("yyyy/MM/dd HH:mm:ss"));
                    comm.Parameters.AddWithValue("@val7", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
        //                MessageBox.Show("Connection Open ! ");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("exception: " + ex);
                        // do something with the exception
                        // don't hide it
                    }
                }
            }


            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            aTimer.Stop();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if(textBox3.Text.Length == 0)
                aTimer.Start();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
