using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Login
{
    public partial class password : Form
    {
        MySqlCommand cmnd;
        MySqlConnection con;
        string empid =  string.Empty;
        
        public password()
        {
            InitializeComponent();
        }

        public password(string id)
        {
            InitializeComponent();
            empid = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "" && textBox3.Text != "")
                {
                    if (textBox2.Text.Equals(textBox3.Text))
                    {
                        byte[] b = new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(textBox3.Text));
                        StringBuilder fhpass = new StringBuilder(b.Length);

                        for (int i = 0; i < b.Length; i++)
                        {
                            fhpass.Append(b[i].ToString("X2"));
                        }
                        con = new MySqlConnection();
                        con.ConnectionString = Form1.sqlstring;
                        string q;

                        if(string.IsNullOrEmpty(empid))
                         q = "update users set password = '" + fhpass + "' where username = '" + Form1.empno + "'";
                        else
                         q = "update users set password = '" + fhpass + "' where username = '" + empid+ "'";

                        cmnd = new MySqlCommand(q, con);
                        con.Open();
                        if (cmnd.ExecuteNonQuery() > 0)
                        {
                            con.Close();
                            MessageBox.Show("Password has been changed successfully !!!", "Password Change Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Server is not reponding. Kindly try again after some time !!!", "Password Change Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Passwords do not match !!!", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else

                    MessageBox.Show("Please enter a password and reconfirm it!!!", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }
    }
}
