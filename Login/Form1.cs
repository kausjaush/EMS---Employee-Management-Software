using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Login
{
    public partial class Form1 : Form
    {
        public readonly static string sqlstring = "server=127.0.0.1;Port=3306;UID=root;Password =root;database=ems_data";
        
        public static string empno,empname;
        public static string user;
        MySqlConnection con;
        MySqlDataAdapter da;
        MySqlCommand cmnd;
        string querry;
        public static DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if(!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if(!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        con = new MySqlConnection();
                        con.ConnectionString = sqlstring;
                        querry = "select * from users where username ='" + textBox1.Text + "'";
                        cmnd = new MySqlCommand(querry,con);
                        da= new MySqlDataAdapter(cmnd);
                        dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        con.Close();

                        if(dt.Rows.Count>0)
                        {
                            byte[] hpass = new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(textBox2.Text));
                            
                            StringBuilder sb = new StringBuilder(hpass.Length);

                            for (int i = 0; i < hpass.Length; i++)
                            {
                                sb.Append(hpass[i].ToString("X2"));                               
                            }
                            string pass = sb.ToString();

                            if (pass.Equals(dt.Rows[0]["password"].ToString()))
                            {
                                if ("Active".Equals(dt.Rows[0]["Status"].ToString()))
                                {
                                    if ("admin".Equals(dt.Rows[0]["credentials"].ToString()))
                                    {
                                        empname = dt.Rows[0]["empname"].ToString();
                                        user = "admin";
                                        empno = dt.Rows[0]["empnumber"].ToString();
                                        textBox1.Text = textBox2.Text = "";
                                        Employer home = new Employer();
                                        this.Hide();
                                        home.ShowDialog();
                                        this.Show();
                                    }
                                    else if ("padmin".Equals(dt.Rows[0]["credentials"].ToString()))
                                    {
                                        empname = dt.Rows[0]["empname"].ToString();
                                        user = "padmin";
                                        empno = dt.Rows[0]["empnumber"].ToString();
                                        textBox1.Text = textBox2.Text = "";
                                        TL home = new TL();
                                        this.Hide();
                                        home.ShowDialog();
                                        this.Show();
                                    }
                                    else if ("accounts".Equals(dt.Rows[0]["credentials"].ToString()))
                                    {
                                        empname = dt.Rows[0]["empname"].ToString();
                                        user = "accounts";
                                        empno = dt.Rows[0]["empnumber"].ToString();
                                        textBox1.Text = textBox2.Text = "";
                                        Accounts home = new Accounts();                                      
                                        this.Hide();
                                        home.ShowDialog();
                                        this.Show();
                                    }
                                     else if ("employee".Equals(dt.Rows[0]["credentials"].ToString()))
                                    {
                                        empname = dt.Rows[0]["empname"].ToString();
                                        user = "employee";
                                        empno = dt.Rows[0]["empnumber"].ToString();
                                        textBox1.Text = textBox2.Text = "";
                                        Employee home = new Employee();
                                        this.Hide();
                                        home.ShowDialog();
                                        this.Show();
                                    }
                                
                                }
                            else
                            {
                                //load.Close();
                                MessageBox.Show("Your account is Not-Active!!!.\n Please contact to your Administrator.", "Login Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            else
                                MessageBox.Show("Icorrect Password !!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                        }
                        else
                            MessageBox.Show("The entered username not found !!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Hand);

                    }
                    else
                    {
                        textBox2.BackColor = Color.Red;
                    }
                }
                else
                {
                    textBox1.BackColor = Color.Red;
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int i = 355;
            int j = 0;

            while (i>59)
            {
                j += 1;
                i -= 60;
            }
            MessageBox.Show(j.ToString()+":"+i.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
