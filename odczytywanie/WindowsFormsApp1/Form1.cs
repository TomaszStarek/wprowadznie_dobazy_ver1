using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer aTimer;

        public Form1()
        {
            InitializeComponent();
            SetTimer();
            WindowState = FormWindowState.Maximized;
        }


        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(5);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        // bool flaga; 

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

        }



        DateTime stop_awaria, start_awaria;
        bool flaga = true;


        private void button2_Click(object sender, EventArgs e)
        {

            Program.global_czasstart = DateTime.Now;

            //start_awaria = DateTime.Now;
            //var R010 = new Form2();
            //R010.Show();

            Form2 frm = new Form2();
            frm.Show();
            this.Hide();


            //Thread.Sleep(1000);
            //stop_awaria = DateTime.Now;

            //var minutesPassed = Math.Floor((stop_awaria - start_awaria).TotalMilliseconds);
            //string data = minutesPassed.ToString();
            //Form2 frm = new Form2(data);
            //    frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.global_czasstart = DateTime.Now;

            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.global_czasstart = DateTime.Now;

            Form4 frm = new Form4();
            frm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.global_czasstart = DateTime.Now;

            Form5 frm = new Form5();
            frm.Show();
            this.Hide();
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    //string connetionString = null;
        //    //SqlConnection cnn;
        //    //connetionString = @"Data Source=PLKWIM0LME045; Initial Catalog = test; Integrated Security = SSPI";          
        //    //cnn = new SqlConnection(connetionString);
        //    //    cnn.Open();
        //    //    MessageBox.Show("Connection Open ! ");
        //    //    cnn.Close();


        //    //       Server = hostname; Database = databaseName; User ID = sqlServerUserid; Password = sqlServerPassword   User ID=sqlServerUserid;Password=sqlServerPassword  server=192.168.1.10;database=A

        //    string cmdString = "INSERT INTO [dbo].[Table] ([ID],[stacja],[opis],[czas_start],[czas_stop]) VALUES (@val1, @val2, @val3, @val4, @val5)";
        //    string connString = @"server=192.168.1.10; database=PLKWIM0LME045; Initial Catalog = test; User ID = loginsql; Password = Jabil12345";
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        using (SqlCommand comm = new SqlCommand())
        //        {
                    
        //            comm.Connection = conn;
        //            comm.CommandText = cmdString;
        //            comm.Parameters.AddWithValue("@val1", textBox1.Text);
        //            comm.Parameters.AddWithValue("@val2", textBox2.Text);
        //            comm.Parameters.AddWithValue("@val3", textBox3.Text);
        //            comm.Parameters.AddWithValue("@val4", textBox3.Text);
        //            comm.Parameters.AddWithValue("@val5", textBox3.Text);
        //            try
        //            {
        //                conn.Open();
        //                comm.ExecuteNonQuery();
        //                MessageBox.Show("Connection Open ! ");
        //            }
        //            catch(SqlException ex)
        //            {
        //                MessageBox.Show("Uart exception: " + ex);
        //                // do something with the exception
        //                // don't hide it
        //            }
        //        }
        //    }




       // }
    }
}
