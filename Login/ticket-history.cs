using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using System.IO;

namespace Login
{
    public partial class ticket_history : Form
    {
        Thread t;
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt;
        string id,stat;
        int i = 0;
        public ticket_history(string stat, string id)
        {
            InitializeComponent();
            this.id = id;
            this.stat = stat;
            i = 1;
        }
        public ticket_history(string id)
        {
            InitializeComponent();
            this.id = id;
            i = 2;
        }

        private void ticket_history_Load(object sender, EventArgs e)
        {
            if (i == 1)
            {

                t = new Thread(new ThreadStart(load1));
                t.Start();
            }
            else if (i==2)
            {
                t = new Thread(new ThreadStart(load2));
                t.Start();
            }
        }

        private void load1()
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                string querry = "Select * from empdailyreport where idempwork = "+Convert.ToInt32(id);
                cmnd = new MySqlCommand(querry,con);
                da = new MySqlDataAdapter(cmnd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                Invoke(new Action(() => label1.Visible = false));
                if(dt.Rows.Count>0)
                {
                    int i=0;
                    Invoke(new Action(() => dataGridView1.Visible = true));
                    foreach(DataRow row in dt.Rows)
                    {
                        Invoke(new Action(() => dataGridView1.Rows.Add()));
                        Invoke(new Action(()=>dataGridView1.Rows[i].Cells["emp_number"].Value = row["emp_number"].ToString()));
                        Invoke(new Action(()=>dataGridView1.Rows[i].Cells["workid"].Value = row["idempwork"].ToString()));
                        Invoke(new Action(()=>dataGridView1.Rows[i].Cells["comment"].Value = row["comment"].ToString()));
                        Invoke(new Action(()=>dataGridView1.Rows[i].Cells["Date"].Value = row["date"].ToString()));
                        Invoke(new Action(()=>dataGridView1.Rows[i].Cells["Time"].Value = row["time"].ToString()));
                        Invoke(new Action(()=>dataGridView1.Rows[i++].Cells["Reason"].Value = row["comment"].ToString()));
                        
                        if(string.Compare( row["comment"].ToString(),"Completed")==0)
                            Invoke(new Action(() => dataGridView1.Rows[i-1].DefaultCellStyle.BackColor = Color.LawnGreen));
                        else if (string.Compare(row["comment"].ToString(), "Droped") == 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i - 1].DefaultCellStyle.BackColor = Color.Red));
                            Invoke(new Action(() => dataGridView1.Rows[i - 1].DefaultCellStyle.ForeColor= Color.White));
                        }
                    }
                }
                else
                {
                    Invoke(new Action(() => label1.Visible = true));
                    Invoke(new Action(() => label1.Text = "No Record Found for Ticket TW-ID:"+id));                    
                }
            }
            catch (ArgumentException e1)
            {
                MessageBox.Show(e1.ToString());
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            
        }
        private void load2()
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                string querry = "Select * from emp_leave where emp_number = " + Convert.ToInt32(id);
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                Invoke(new Action(() => label1.Visible = false));
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    Invoke(new Action(() => dataGridView2.Visible = true));
                    foreach (DataRow row in dt.Rows)
                    {
                        Invoke(new Action(() => dataGridView2.Rows.Add()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["empid"].Value = row["emp_number"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["tickid"].Value = row["idleave"].ToString()));
                        
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["lreason"].Value = row["reason"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["from"].Value = row["from_date"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["till"].Value = row["till_date"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["appldate"].Value = row["date"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["appltime"].Value = row["time"].ToString()));

                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["file"].Value = row["doc_submit"].ToString()));

                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["aapproval"].Value = row["admin_approval"].ToString()));

                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["areason"].Value = row["admin_reason"].ToString()));

                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["aadate"].Value = row["admin_date"].ToString()));

                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["aatime"].Value = row["admin_time"].ToString()));

                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["nodays"].Value = row["no_days"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["half"].Value = row["half_day"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i++].Cells["type"].Value = row["type"].ToString()));


                        if (string.Compare(row["admin_approval"].ToString(), "Approved") == 0)
                            Invoke(new Action(() => dataGridView2.Rows[i - 1].DefaultCellStyle.BackColor = Color.LawnGreen));
                        else if (string.Compare(row["admin_approval"].ToString(), "Not Approved") == 0)
                        {
                            Invoke(new Action(() => dataGridView2.Rows[i - 1].DefaultCellStyle.BackColor = Color.Red));
                            Invoke(new Action(() => dataGridView2.Rows[i - 1].DefaultCellStyle.ForeColor = Color.White));
                        }
                        else if (string.Compare(row["admin_approval"].ToString(), "Approved & Canceled") == 0)
                        {
                            Invoke(new Action(() => dataGridView2.Rows[i - 1].DefaultCellStyle.BackColor = Color.Blue));
                            Invoke(new Action(() => dataGridView2.Rows[i - 1].DefaultCellStyle.ForeColor = Color.White));
                        }

                    }
                }
                else
                {
                    Invoke(new Action(() => label1.Visible = true));
                    Invoke(new Action(() => label1.Text = "No Leave Record Found For Emp. ID : " + id));
                }
            }
            catch (ArgumentException e1)
            {
                MessageBox.Show(e1.ToString());
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    try
                    {
                        if (string.Compare(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ToString(), "0") != 0)
                        {
                            con = new MySqlConnection();
                            con.ConnectionString = Form1.sqlstring;
                            string querry = "Select doc, emp_number, date from emp_leave where idleave = " + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();

                            cmnd = new MySqlCommand(querry, con);
                            da = new MySqlDataAdapter(cmnd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            FileStream fs;
                            if (dt.Rows.Count > 0)
                            {
                                saveFileDialog1.Filter = "PDF | *.pdf";
                                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                                {
                                    byte[] data = (byte[])dt.Rows[0]["doc"];
                                    fs = new FileStream(Path.GetDirectoryName(saveFileDialog1.FileName.ToString()) + "\\" + dt.Rows[0]["emp_number"].ToString() + "-" + dt.Rows[0]["date"].ToString() + ".pdf", FileMode.Create, FileAccess.Write);
                                    fs.Write(data, 0, data.Length);
                                    fs.Close();
                                    MessageBox.Show("Downloaded Successfully !!! ");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No Document Found !!!");
                            }
                        }

                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.ToString());
                    }

                }
                else
                {

                }
            }
            catch (Exception e1)
            {

            }
        }
    }
}
