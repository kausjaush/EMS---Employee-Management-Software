using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;

namespace Login
{
    public partial class company_policy : Form
    {
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        string querry,st;
        public company_policy()
        {
            InitializeComponent();
            Thread tr = new Thread(() => webload());
            tr.Start();
        }

        private void company_policy_Load(object sender, EventArgs e)
        {

        }


        private void webload()
        {
            try
            {
                string info = "";
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;


                querry = "select code from docs";
                DataTable dt = new DataTable();
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt);
                con.Close();
                string toppage = string.Empty;

                toppage = dt.Rows[0]["code"].ToString();
                info = dt.Rows[1]["code"].ToString();
                st = dt.Rows[2]["code"].ToString(); 
                if (!string.IsNullOrEmpty(toppage))
                    Invoke(new Action(() => webBrowser1.DocumentText = toppage));
                else
                    Invoke(new Action(() => webBrowser1.DocumentText = st));

                if (!string.IsNullOrEmpty(info))
                    Invoke(new Action(() => webBrowser2.DocumentText = info));
                else
                    Invoke(new Action(() => webBrowser2.DocumentText = st));


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
    }
}
