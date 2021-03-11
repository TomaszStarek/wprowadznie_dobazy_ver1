using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.Timers;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        DataTable dt;
        SqlDataAdapter da;
        DataSet ds;

        private static System.Timers.Timer aTimer;

        public Form2()
        {
            InitializeComponent();
         
            start_awaria = DateTime.Now;
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
            result =  do_licznika - start_awaria;

            UpdateControl(label1, SystemColors.Control, result.Minutes.ToString(), true);
            UpdateControl(label4, SystemColors.Control, result.Seconds.ToString(), true);
        }


        public bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }
        public void UpdateControl(Control myControl, Color c, String s, bool widzialnosc)
        {
            //Check if invoke requied if so return - as i will be recalled in correct thread
            if (ControlInvokeRequired(myControl, () => UpdateControl(myControl, c, s, widzialnosc))) return;
            myControl.Text = s;
            myControl.BackColor = c;
            myControl.Visible = widzialnosc;
        }




        private void potwierdzenia_minut()
        {
            DialogResult r5 = MessageBox.Show("Czy potwierdzasz czas awarii: " + result.Minutes.ToString(), "Help Caption", MessageBoxButtons.YesNo);

            if (r5 == DialogResult.Yes)
            {
                minutes = result.Minutes.ToString();
            }
            else if (r5 == DialogResult.No)
            {
                UpdateControl(label6, SystemColors.Control, "", true);
                UpdateControl(label5, SystemColors.Control, "Podaj czas awarii w minutach:", true);
                label5.BringToFront();
                textBox1.Visible = true;
                textBox1.BringToFront();
                button8.Visible = true;
                button8.BringToFront();
                return;
            }
            dobazy("R010", opis_awarii);
        }


        private void pobieranie()
        {
            string cmdString = "SELECT ([opis]) FROM [dbo].[Table4]  GROUP BY ([opis]) ORDER BY COUNT(*) DESC OFFSET 0 ROWS FETCH NEXT 4 ROWS ONLY;";    //OFFSET 3 ROWS FETCH NEXT 3 ROWS ONLY
            string connString = @"database=PLKWIM0LME045\WINCCPLUSMIG2014; Initial Catalog = test; User ID = loginsql; Password = Jabil12345";           //server=192.168.1.10; 
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");
                    da = new SqlDataAdapter(comm.CommandText, comm.Connection);
                    ds = new DataSet();
                    da.Fill(ds, "asd");

                    dt = ds.Tables["asd"];

                    //int i;
                    //for (i = 0; i <= dt.Rows.Count - 1; i++)
                    //{
                    button2.Text = dt.Rows[0].ItemArray[0].ToString();
                    button3.Text = dt.Rows[1].ItemArray[0].ToString();
                    button4.Text = dt.Rows[2].ItemArray[0].ToString();
                    button5.Text = dt.Rows[3].ItemArray[0].ToString();
                    //    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                    //          }
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();                        
                        MessageBox.Show("Connection Open ! ");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Exception: " + ex);
                        // do something with the exception
                        // don't hide it
                    }
                }
            }
        }



        private void dobazy(string stacja, string opis)
        {
            
            
                string cmdString = "INSERT INTO [dbo].[Table4] ([sekcja],[stacja],[opis],[min],[czas_start],[czas_stop]) VALUES ( @val2, @val3, @val4, @val5, @val6, @val7)";
            //    string connString = @"Data Source=PLKWIM0LME045; Initial Catalog = test; Integrated Security = SSPI";                                   PLKWIM0LME045\WINCCPLUSMIG2014     server=192.168.1.10;
            string connString = @"database=PLKWIM0LME045\WINCCPLUSMIG2014; Initial Catalog = test; User ID = loginsql; Password = Jabil12345";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");
                    comm.Parameters.AddWithValue("@val2", "M0");
                    comm.Parameters.AddWithValue("@val3", stacja);
                    comm.Parameters.AddWithValue("@val4", opis);
                    comm.Parameters.AddWithValue("@val5", minutes);
                    comm.Parameters.AddWithValue("@val6", Program.global_czasstart.ToString());
                    comm.Parameters.AddWithValue("@val7", DateTime.Now.ToString());
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        MessageBox.Show("Connection Open ! ");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Uart exception: " + ex);
                        // do something with the exception
                        // don't hide it
                    }
                }
            }

            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }



        //public Form2(string strTextBox)
        //{
        //    InitializeComponent();
        //    label1.Text = strTextBox;
        //}

        DateTime stop_awaria, start_awaria;

        private void button7_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Show();
            this.Hide();
        }

        //private void button8_Click(object sender, EventArgs e)
        //{


        //        Application.DoEvents();
        //        Thread.Sleep(100);
        //        stop_awaria = DateTime.Now;

        //        var minutesPassed = Math.Floor((stop_awaria - start_awaria).TotalMilliseconds);
        //        string data = minutesPassed.ToString();
        //        label1.Text = data;

        //}
        string opis_awarii;

        private void button2_Click(object sender, EventArgs e)
        {
            opis_awarii = button2.Text;
            //       dobazy("R010", button2.Text);
            potwierdzenia_minut();




        }

        private void button3_Click(object sender, EventArgs e)
        {
            opis_awarii = button3.Text;
            //        dobazy("R010", button3.Text);
            potwierdzenia_minut();
            //Form1 frm = new Form1();
            //frm.Show();
            //this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            opis_awarii = button4.Text;
            //        dobazy("R010", button4.Text);
            potwierdzenia_minut();
            //Form1 frm = new Form1();
            //frm.Show();
            //this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            opis_awarii = button5.Text;
            //       dobazy("R010", button5.Text);
            potwierdzenia_minut();
            //Form1 frm = new Form1();
            //frm.Show();
            //this.Hide();
        }

        string minutes;
        private void button8_Click_1(object sender, EventArgs e)
        {
            minutes = textBox1.Text;
            textBox1.Clear();
            UpdateControl(label6, SystemColors.Control, "", false);
            UpdateControl(label5, SystemColors.Control, "", false);
            textBox1.Visible = false;
            button8.Visible = false;
            dobazy("R010", opis_awarii);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Bardzo proszę wpisywać jednak cyfry...");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pobieranie();
        }

        private void button6_Click(object sender, EventArgs e)
        {           
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

    }
}
