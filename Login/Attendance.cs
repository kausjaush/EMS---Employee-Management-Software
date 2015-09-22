using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.IO;
using System.Threading;


namespace Login
{
    public partial class Attendance : Form
    {
        OleDbCommand cmnd;
        OleDbConnection con;
        OleDbDataAdapter oda;
        DataTable dt = new DataTable();
        string status = "", cur = "";

        public Attendance()
        {
            InitializeComponent();
            timer1.Tick += timer1_Tick;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label11.Text = cur;
                label9.Text = status;

            }
            catch(Exception e1)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if(openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"",Path.GetDirectoryName(openFileDialog1.FileName.ToString()+"\\"));
                    string cmndstr = "Select * from [Sheet1$]";

                    con = new OleDbConnection(constr);
                    oda = new OleDbDataAdapter();
                    cmnd = new OleDbCommand(cmndstr, con);
                    dt.Clear();
                    oda.SelectCommand = cmnd;
                    con.Open();
                    dt.Clear();
                    oda.Fill(dt);
                    con.Close();
                    
                    Thread tr = new Thread(()=>upload());
                    tr.Start();
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void upload()
        {
            int i = 0;
            string empno, empname, intime, outime, wd,ot, totald,date;
            int notins = 0,ins = 0;
            try
            {
                MySqlConnection con = new MySqlConnection();
                MySqlCommand cmnd = new MySqlCommand();
                con.ConnectionString = Form1.sqlstring;
                con.Open();
                DateTime time = DateTime.Now;

                for (int k = 1; k <= dt.Rows.Count; k++)
                {

                    try
                    {
                        date = dataGridView1.Rows[i].Cells["date"].Value.ToString();
                        empno = dataGridView1.Rows[i].Cells["emp_no"].Value.ToString();
                        empname = dataGridView1.Rows[i].Cells["emp_name"].Value.ToString();
                        intime = dataGridView1.Rows[i].Cells["intime"].Value.ToString();
                        outime = dataGridView1.Rows[i].Cells["outtime"].Value.ToString();
                        wd = dataGridView1.Rows[i].Cells["workdue"].Value.ToString();
                        ot = dataGridView1.Rows[i].Cells["ot"].Value.ToString();
                        totald = dataGridView1.Rows[i].Cells["total"].Value.ToString();

                        if ((!string.IsNullOrEmpty(intime) && !string.IsNullOrWhiteSpace(intime))
                            && (!string.IsNullOrEmpty(outime) && !string.IsNullOrWhiteSpace(outime))
                            && (!string.IsNullOrEmpty(wd) && !string.IsNullOrWhiteSpace(wd))
                            && (!string.IsNullOrEmpty(ot) && !string.IsNullOrWhiteSpace(ot))
                            && (!string.IsNullOrEmpty(totald) && !string.IsNullOrWhiteSpace(totald)))
                        {
                            //cmnd1 = new MySqlCommand("Insert into demo_data (Full_Name, Mobile,Email,Company,Profession, Address,Location,Uploaded_By,Date ) Values ('" + Fname + "','" + Mobile + "','" + Email + "','" + Company + "','" + Profession + "','" + Address + "','" + Location + "','" + Uploaded_By + "', CURDATE())", con1);
                            cmnd = new MySqlCommand("Insert into attendance (date,emp_number,emp_name,in,out,wd,ot,totald) Values ('" + date + "','" + empno + "','" + empname + "','" + intime + "','" + outime + "','" + wd + "','" + ot + "','" + totald + "')", con);
                            cmnd.ExecuteNonQuery();
                            ins++;

                            rowuploadstatus(1, i);
                            i++;
                        }
                        else
                        {
                            notins++;
                            rowuploadstatus(2, i);
                            i++;
                        }

                    }

                    catch (MySqlException e1)
                    {
                        MessageBox.Show(e1.ToString());
                        notins++;
                        rowuploadstatus(0, i);
                        i++;
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.ToString());
                        notins++;
                        rowuploadstatus(2, i);
                        i++;
                    }

                }

                status = "Data Uploaded!!!";
                cur = "Disconnecting from SERVER...";
                Thread.Sleep(2000);
                con.Close();
                cur = "Disconnected.";
                Thread.Sleep(500);
                timer1.Stop();
            }
            catch (MySqlException e1)
            {
                cur = "Internet Connection Error !!!";
                MessageBox.Show(e1.ToString());
            }
        }

        void rowuploadstatus(int color, int row)
        {
            if (color.Equals(0))
            {
                dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (color.Equals(1))
            {
                dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.Green;
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.White;
            }
            else if (color.Equals(2))
            {
                dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.White;
            }

        }
    }
}
