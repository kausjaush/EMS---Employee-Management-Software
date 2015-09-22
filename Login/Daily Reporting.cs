using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login
{
    public partial class Daily_Reporting : Form
    {
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        string querry;
        string empnumber = Form1.empno;

        public Daily_Reporting()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                dt.Rows.Clear();
                listView1.Items.Clear();
                listView2.Items.Clear();
                listView3.Items.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select idempwork,ticket_status from empwork where emp_number="+empnumber;
                cmnd = new MySqlCommand(querry,con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach(DataRow row in dt.Rows)
                {
                    if(string.Compare(row["ticket_status"].ToString(),"Active")==0)
                        listView1.Items.Add(row["idempwork"].ToString());
                    else if (string.Compare(row["ticket_status"].ToString(),"De-Active") == 0)
                        listView2.Items.Add(row["idempwork"].ToString());
                   else if (string.Compare(row["ticket_status"].ToString(),"Droped") == 0)
                        listView3.Items.Add(row["idempwork"].ToString());
                    else if (string.Compare(row["ticket_status"].ToString(), "Completed") == 0)
                        listView4.Items.Add(row["idempwork"].ToString());
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ticket(Convert.ToInt32(listView1.SelectedItems[0].Text));
            label2.Text = listView1.SelectedItems[0].Text;
            panel3.Enabled = true;
            
        }
        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            //ticket(Convert.ToInt32(listView2.SelectedItems[0].Text));
            //label2.Text = listView2.SelectedItems[0].Text;
            //panel3.Enabled = true;
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            //ticket(Convert.ToInt32(listView3.SelectedItems[0].Text));
            //label2.Text = listView3.SelectedItems[0].Text;
            //panel3.Enabled = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fetch("De-Active");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fetch("Active");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fetch("Droped");
        }
        
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fetch("Completed");
        }
        private void ticket(int tno)
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                dt.Rows.Clear();
                dataGridView1.Rows.Clear();
                querry = "Select * from empwork where idempwork = "+tno;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt);
                con.Close();
                int x = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[x].Cells["TWID"].Value = row["idempwork"].ToString();
                        dataGridView1.Rows[x].Cells["TOW"].Value = row["tow"].ToString();
                        dataGridView1.Rows[x].Cells["WD"].Value = row["wd"].ToString();
                        dataGridView1.Rows[x].Cells["TTA"].Value = row["tta"].ToString();
                        dataGridView1.Rows[x].Cells["SD"].Value = row["sd"].ToString();
                        dataGridView1.Rows[x].Cells["ED"].Value = row["ed"].ToString();
                        dataGridView1.Rows[x].Cells["assign"].Value = row["assign"].ToString();
                        dataGridView1.Rows[x].Cells["SODD"].Value = row["sodd"].ToString();
                        dataGridView1.Rows[x].Cells["DIE"].Value = row["die"].ToString();
                        dataGridView1.Rows[x].Cells["R"].Value = row["r"].ToString();
                        dataGridView1.Rows[x].Cells["CP"].Value = row["cp"].ToString();
                        dataGridView1.Rows[x++].Cells["A"].Value = row["a"].ToString();
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void fetch(string ts)
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                dt.Rows.Clear();
                dataGridView1.Rows.Clear();
                querry = "Select * from empwork where emp_number = "+empnumber+" AND ticket_status ='"+ts+"'";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt);
                con.Close();
                int x = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[x].Cells["TWID"].Value = row["idempwork"].ToString();
                        dataGridView1.Rows[x].Cells["TOW"].Value = row["tow"].ToString();
                        dataGridView1.Rows[x].Cells["WD"].Value = row["wd"].ToString();
                        dataGridView1.Rows[x].Cells["TTA"].Value = row["tta"].ToString();
                        dataGridView1.Rows[x].Cells["SD"].Value = row["sd"].ToString();
                        dataGridView1.Rows[x].Cells["ED"].Value = row["ed"].ToString();
                        dataGridView1.Rows[x].Cells["assign"].Value = row["assign"].ToString();
                        dataGridView1.Rows[x].Cells["SODD"].Value = row["sodd"].ToString();
                        dataGridView1.Rows[x].Cells["DIE"].Value = row["die"].ToString();
                        dataGridView1.Rows[x].Cells["R"].Value = row["r"].ToString();
                        dataGridView1.Rows[x].Cells["CP"].Value = row["cp"].ToString();
                        dataGridView1.Rows[x++].Cells["A"].Value = row["a"].ToString();
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                if (Convert.ToInt32(textBox1.Text) <= 10 && Convert.ToInt32(textBox1.Text) >= 0)
                {
                    querry = "insert into  empdailyreport (emp_number,idempwork,rate,comment,date,time) value(" + empnumber + "," + Convert.ToInt32(label2.Text) + "," + Convert.ToInt32(textBox1.Text) + ",'" + richTextBox1.Text + "',CURDATE(),CURTIME())";
                    cmnd = new MySqlCommand(querry, con);
                    con.Open();
                    cmnd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Daily report of this ticket is submitted successfully.");
                    richTextBox1.Text = string.Empty;
                    label2.Text = "------";
                    textBox1.Text = string.Empty;
                    panel3.Enabled = false;
                }
                else
                    MessageBox.Show("Kindly Rate from 0 - 10 Only!!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Kindly Rate your work todays work !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }catch(MySqlException e1)
            {
                con.Close();
                MessageBox.Show(e1.ToString());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox1, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string tid=string.Empty;
                if(radioButton2.Checked == true)
                { 
                int rows = 0;
                if (dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["check"].Value))
                        {
                            tid = row.Cells["TWID"].Value.ToString();
                            rows++;
                        }
                    }
                    if (rows ==1)
                    {
                        
                        radioButton2.Checked = false;

                        if (MessageBox.Show("Are you sure you would like to DROP TW-ID :" + tid + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                done_droped a = new done_droped(tid);
                                if (a.ShowDialog() == DialogResult.OK)
                                {

                                    if (!string.IsNullOrEmpty(a.richTextBox1.Text) && !string.IsNullOrWhiteSpace(a.richTextBox1.Text))
                                    {
                                        string message = a.richTextBox1.Text;
                                        con.ConnectionString = Form1.sqlstring;
                                        querry = "select rate from empdailyreport where idempwork = " + tid;
                                        cmnd = new MySqlCommand(querry, con);
                                        DataTable dt = new DataTable();
                                        da = new MySqlDataAdapter(cmnd);

                                        con.Open();
                                        da.Fill(dt);
                                        con.Close();

                                        int avg = (int)average(dt);
                                        querry = "insert into empdailyreport (emp_number,idempwork,rate,comment,date,time,dropreason) values(" + empnumber + "," + tid + "," + avg + "," + "'Droped',CURDATE(),CURTIME(),'"+message+"')";
                                        cmnd = new MySqlCommand(querry, con);
                                        con.Open();
                                        if (cmnd.ExecuteNonQuery() > 0)
                                        {
                                            MessageBox.Show("The TW-ID :" + tid + " has been successfully submitted as Ticket Droped. \r\n It has been moved to De-Active section.\r\n After approval it will be marked as Droped Ticket and will display in Droped Section.");
                                        }
                                        con.Close();
                                        radioButton1.Checked = false;
                                        int i = 0;
                                        
                                        foreach(DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            if(string.Compare(row.Cells["TWID"].Value.ToString(),tid)==0)
                                            {
                                                dataGridView1.Rows.Remove(row);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("You can not drop a ticket with out any reason. Kindly provide a valid reason!!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("You can not drop a ticket with out any reason. Kindly provide a valid reason!!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                    }
                    else if(rows >1)
                    {
                        MessageBox.Show("Please select only one ticket at a time !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        radioButton2.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("Please select a ticket first !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        radioButton2.Checked = false;
                    }
                }

                }
                
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    List<string> temp = new List<string>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["check"].Value))
                            temp.Add(row.Cells["TWID"].Value.ToString());
                    }

                    if (temp.Count > 0)
                    {
                        foreach (string s in temp)
                            if (MessageBox.Show("Are you sure that you have completed work of TW-ID :" + s + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                con.ConnectionString = Form1.sqlstring;
                                querry = "select rate from empdailyreport where idempwork = " + s;
                                cmnd = new MySqlCommand(querry, con);
                                DataTable dt = new DataTable();
                                da = new MySqlDataAdapter(cmnd);

                                con.Open();
                                da.Fill(dt);
                                con.Close();

                                int avg = (int)average(dt);
                                
                                    querry = "insert into empdailyreport (emp_number,idempwork,rate,comment,date,time) values(" + empnumber + "," + s + "," + avg + "," + "'Completed',CURDATE(),CURTIME())";
                                cmnd = new MySqlCommand(querry, con);
                                con.Open();
                                if (cmnd.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show("The TW-ID : " + s + " has been successfully submitted as Target Achieved. \r\n It has been moved to De-Active section.\r\n After Approval it will be marked as Target Achieved and will display in Completed Section.");
                                }
                                con.Close();
                                radioButton1.Checked = false;
                            }
                    }
                    radioButton1.Checked = false;
                }
            }
            catch(MySqlException e1)
            {
                MessageBox.Show(e1.ToString());
                radioButton1.Checked = false;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                radioButton1.Checked = false;
            }
        }

        private float average(DataTable dt)
        {
            float i = 0;
            float x =0;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {                   
                    i = i + Convert.ToInt32(row["rate"].ToString());
                }
                x = i / dt.Rows.Count;
            }

            return (x);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                DR_history a = new DR_history(textBox2.Text,Form1.empno);
                a.ShowDialog();

                
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                linkLabel6.Enabled = true;
            }
            else
                linkLabel6.Enabled = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox2,e);
        }

        
    }
}
