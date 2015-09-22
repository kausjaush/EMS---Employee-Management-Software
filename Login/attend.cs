using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;

namespace Login
{
    public partial class attend : Form
    {
        OleDbCommand cmnd;
        OleDbConnection con;
        OleDbDataAdapter oda;
        DataTable dt = new DataTable();
        string date_selected;
        int day, month, year;

        string status = "", cur = "";

        public attend()
        {
            InitializeComponent();
        }


        private void import()
        {


            int i = 0;

            


            date_selected = textBox9.Text;
            try
            {
                Invoke(new Action(() => dateTimePicker4.Enabled = false));
                foreach (DataRow row in dt.Rows)
                {
                    Invoke(new Action(() => dataGridView1.Rows.Add()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["serial"].Value = row["Sno"].ToString()));
                    
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["emp_name"].Value = row["Name"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["intime"].Value = row["InTime"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["outime"].Value = row["OutTime"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["wd"].Value = row["wd"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["ot"].Value = row["OT"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["td"].Value = row["td"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["stat"].Value = row["Status"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["emp_number"].Value = "360"+row["EmpNo"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i++].Cells["date"].Value = date_selected));
                }


            }
            catch (MySqlException e1)
            {
                cur = "Internet Connection Error !!!";
                MessageBox.Show(e1.ToString());
            }
            catch (Exception e1)
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

        private void upload()
        {
            int i = 0;

            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmnd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dttemp = new DataTable();
            con.ConnectionString = Form1.sqlstring;

            string selected_month = "", selected_day = "";
            if (dateTimePicker4.Value.Month <= 9)
                selected_month = "0" + dateTimePicker4.Value.Month.ToString();
            else
                selected_month = dateTimePicker4.Value.Month.ToString();

            if (dateTimePicker4.Value.Day <= 9)
                selected_day = "0" + dateTimePicker4.Value.Day.ToString();
            else
                selected_day = dateTimePicker4.Value.Day.ToString();


            i = 0;
            string empno, empname, intime, outime, wd, ot, totald, date, pstats, date_selected;
            int notins = 0, ins = 0;
            con.Open();
            for (int k = 1; k <= dt.Rows.Count; k++)
            {

                try
                {
                    date = dataGridView1.Rows[i].Cells["date"].Value.ToString();
                    empno = dataGridView1.Rows[i].Cells["emp_number"].Value.ToString();
                    empname = dataGridView1.Rows[i].Cells["emp_name"].Value.ToString();
                    intime = dataGridView1.Rows[i].Cells["intime"].Value.ToString();
                    outime = dataGridView1.Rows[i].Cells["outime"].Value.ToString();
                    wd = dataGridView1.Rows[i].Cells["wd"].Value.ToString();
                    ot = dataGridView1.Rows[i].Cells["ot"].Value.ToString();
                    totald = dataGridView1.Rows[i].Cells["td"].Value.ToString();
                    pstats = dataGridView1.Rows[i].Cells["stat"].Value.ToString();

                    cmnd = new MySqlCommand("Select * from attendance where emp_number = 360" + empno + " and date = '" + date + "'", con);
                    dttemp.Clear();
                    da = new MySqlDataAdapter(cmnd);
                    da.Fill(dttemp);
                    if (dttemp.Rows.Count == 0)
                    {
                        if ((!string.IsNullOrEmpty(wd) && !string.IsNullOrWhiteSpace(wd))
                            && (!string.IsNullOrEmpty(ot) && !string.IsNullOrWhiteSpace(ot))
                            && (!string.IsNullOrEmpty(totald) && !string.IsNullOrWhiteSpace(totald)))
                        {
                            //cmnd1 = new MySqlCommand("Insert into demo_data (Full_Name, Mobile,Email,Company,Profession, Address,Location,Uploaded_By,Date ) Values ('" + Fname + "','" + Mobile + "','" + Email + "','" + Company + "','" + Profession + "','" + Address + "','" + Location + "','" + Uploaded_By + "', CURDATE())", con1);
                            cmnd = new MySqlCommand("Insert into attend_log (emp_number,emp_name,intime,outtime,wd,ot,totald,status,date,day,month,year) Values (@empno,@empname,@intime,@out,@wd,@ot,@total,@status,@date,@day,@month,@year)", con);
                            cmnd.Parameters.Add("@empno", MySqlDbType.VarChar).Value = empno;
                            cmnd.Parameters.Add("@empname", MySqlDbType.VarChar).Value = empname;
                            cmnd.Parameters.Add("@intime", MySqlDbType.VarChar).Value = intime;
                            cmnd.Parameters.Add("@out", MySqlDbType.VarChar).Value = outime;
                            cmnd.Parameters.Add("@wd", MySqlDbType.VarChar).Value = wd;
                            cmnd.Parameters.Add("@ot", MySqlDbType.VarChar).Value = ot;
                            cmnd.Parameters.Add("@total", MySqlDbType.VarChar).Value = totald;
                            cmnd.Parameters.Add("@status", MySqlDbType.VarChar).Value = pstats;
                            cmnd.Parameters.Add("@date", MySqlDbType.VarChar).Value = date;
                            cmnd.Parameters.Add("@day", MySqlDbType.VarChar).Value = selected_day;
                            cmnd.Parameters.Add("@month", MySqlDbType.VarChar).Value = selected_month;
                            cmnd.Parameters.Add("@year", MySqlDbType.VarChar).Value = dateTimePicker4.Value.Year;

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
                    else
                    {
                        notins++;
                        rowuploadstatus(0, i);
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

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    int inserted = 0;
                    cmnd = new MySqlCommand();
                    cmnd.Connection = con;
                    cmnd.CommandText = "split;";
                    cmnd.CommandType = CommandType.StoredProcedure;

                    cmnd.Parameters.AddWithValue("?empno", Convert.ToInt32(row.Cells["emp_number"].Value.ToString().Trim()));
                    cmnd.Parameters.AddWithValue("?mon", Convert.ToInt32(dateTimePicker4.Value.Month.ToString()));
                    cmnd.Parameters.AddWithValue("?yr", Convert.ToInt32(dateTimePicker4.Value.Year.ToString()));

                    inserted = cmnd.ExecuteNonQuery();

                    if (inserted > 0)
                    {
                        Invoke(new Action(() => dataGridView1.Rows[row.Index].DefaultCellStyle.BackColor = Color.Purple));
                        Invoke(new Action(() => dataGridView1.Rows[row.Index].DefaultCellStyle.ForeColor = Color.White));
                        inserted = 0;
                    }
                    else
                    {
                        Invoke(new Action(() => dataGridView1.Rows[row.Index].DefaultCellStyle.BackColor = Color.Yellow));
                        Invoke(new Action(() => dataGridView1.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Black));
                        inserted = 0;
                    }

                }
                catch (MySqlException e1)
                {
                    MessageBox.Show(e1.ToString());
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }


            }


            //status = "Data Uploaded!!!";
            //cur = "Disconnecting from SERVER...";
            //Thread.Sleep(2000);
            //con.Close();
            //cur = "Disconnected.";
            //Thread.Sleep(500);
        }

        private void dateTimePicker4_CloseUp(object sender, EventArgs e)
        {
            string selected_month = "", selected_day = "";
            if (dateTimePicker4.Value.Month <= 9)
                selected_month = "0" + dateTimePicker4.Value.Month.ToString();
            else
                selected_month = dateTimePicker4.Value.Month.ToString();

            if (dateTimePicker4.Value.Day <= 9)
                selected_day = "0" + dateTimePicker4.Value.Day.ToString();
            else
                selected_day = dateTimePicker4.Value.Day.ToString();
            textBox9.Text = dateTimePicker4.Value.Year + "-" + selected_month + "-" + selected_day;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"", Path.GetDirectoryName(openFileDialog1.FileName.ToString() + "\\"));
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
                    if (dt.Rows.Count > 0)
                    {
                        Thread tr = new Thread(() => import());
                        tr.Start();
                        button1.Enabled = false;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(dateTimePicker4.Value.Month + "," + dateTimePicker4.Value.Year);
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Thread tr = new Thread(()=> upload());
                tr.IsBackground = true;
                tr.Start();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                textBox9.Text = string.Empty;
                dateTimePicker4.Enabled = true;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
    }
}
