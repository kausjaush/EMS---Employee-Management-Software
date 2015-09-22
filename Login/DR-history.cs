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

namespace Login
{
    public partial class DR_history : Form
    {
        string id,no;
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        string querry;
        
        public DR_history(string twid,string empno)
        {
            InitializeComponent();
            id = twid;
            no = empno;
            label2.Text = "Daily report History of TWID : "+ id;
            
           // dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Thread tr = new Thread(() => get());
            tr.Start();
            //get();
        }

        private void DR_history_Load(object sender, EventArgs e)
        {

        }

        private void get()
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                Thread.Sleep(30);
                dt.Rows.Clear();
                Invoke(new Action(()=>dataGridView1.Rows.Clear()));
                querry = "Select idempwork,rate,comment,date,time from empdailyreport where idempwork = " + id +" and emp_number = "+no;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt);
                con.Close();
                int x = 0;
                if (dt.Rows.Count > 0)
                {
                    Invoke(new Action(()=>label1.Visible = false));
                    Invoke(new Action(() => dataGridView1.Visible = true));
                    foreach (DataRow row in dt.Rows)
                    {
                      Invoke(new Action(()=>  dataGridView1.Rows.Add()));
                      
                      Invoke(new Action(()=>  dataGridView1.Rows[x].Cells["date"].Value = row["date"].ToString()));
                       Invoke(new Action(()=> dataGridView1.Rows[x].Cells["time"].Value = row["time"].ToString()));
                       Invoke(new Action(()=> dataGridView1.Rows[x].Cells["report"].Value = row["comment"].ToString()));
                       Invoke(new Action(()=> dataGridView1.Rows[x++].Cells["rate"].Value = row["rate"].ToString()));

                    }
                }
                else
                {
                    Invoke(new Action(()=> dataGridView1.Visible = false));
                    Invoke(new Action(()=> label1.Visible = true));
                    Invoke(new Action(()=>label1.Text = "You haven't updated any work report of this ticket or it does not belong to your account !!!"));
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }


    }
}
