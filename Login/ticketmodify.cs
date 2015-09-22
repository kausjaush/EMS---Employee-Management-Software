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
    public partial class ticketmodify : Form
    {
        
        MySqlConnection con;
        MySqlDataAdapter da;
        MySqlCommand cmnd;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        string querry;
        int a1, a2;
        int[] index = new int[300];
        int xx=0;
        int set = 0;

        public ticketmodify()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            timer1.Tick +=timer1_Tick;
            timer1.Start();

            if (string.Compare(Form1.user, "admin") == 0)
            {
                set = 1;
                linkLabel8.Enabled = true;
            }
            else if (string.Compare(Form1.user, "padmin") == 0)
            {
                set = 2;
                linkLabel8.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int [] id = new int[500];
            try
            {
                dt.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select emp_number,emp_fname,emp_mname,emp_lname from empdetails Order by emp_number ASC";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                
                con.Open();
                da.Fill(dt);
                int i = 0;
                listBox1.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["emp_number"].ToString() +"-"+ dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
                        id[i++] = Convert.ToInt32(dr["emp_number"]);
                    }
                }
                else
                {
                    MessageBox.Show("No employee data found !!!");
                }
                con.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string user = listBox1.SelectedItem.ToString(); 
                label6.Text = user.Split('-')[1].Trim();
                label7.Text = user.Split('-')[0].Trim();
 
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mesg = "";
                if((!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)) && (!string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrWhiteSpace(richTextBox1.Text)) && (!string.IsNullOrEmpty(richTextBox2.Text) && !string.IsNullOrWhiteSpace(richTextBox2.Text)))
                {
                    mesg = "Please Confirm the Work : \r\n \r\n Type of work:\r\n" + textBox1.Text + "\r\n Work Detail:\r\n" + richTextBox1.Text + "\r\n Target to Achieve:\r\n" + richTextBox2.Text + "\r\n Start Date: " + textBox2.Text + "\r\n End Date : " + textBox3.Text + "\r\n \r\n Are you sure to Issue this work to :" + label6.Text + "?";
                    if(MessageBox.Show(mesg,"Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.ConnectionString = Form1.sqlstring;
                        querry = "Insert into empwork (emp_number,emp_name,tow,wd,tta,sd,ed,assign,ticket_status) Values ("+label7.Text+",'"+label6.Text.Trim()+"','"+textBox1.Text.Trim()+"','"+richTextBox1.Text+"','"+richTextBox2.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+Form1.empname+"','Active')";
                        richTextBox1.Text = querry;
                        cmnd = new MySqlCommand(querry,con);
                        con.Open();
                        if(cmnd.ExecuteNonQuery()>0)
                        {
                            label6.Text = label7.Text = "----------";
                            textBox1.Text = textBox2.Text = textBox3.Text = richTextBox2.Text = richTextBox1.Text = string.Empty;
                            MessageBox.Show("Work has been Assigned Successfully !!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        con.Close();
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Please fill all details !!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                con.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker1.Value.Year + "-"+dateTimePicker1.Value.Month+"-"+dateTimePicker1.Value.Day;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int[] id = new int[500];
            List<string> name =  new List<string>();
            List<string>idwork = new List<string>();;
            try
            {
                dt2.Clear();

                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                if (string.Compare(Form1.user, "padmin") == 0)
                    querry = "Select * from empwork where approve2=11";
                else
                    querry = "Select * from empwork where approve1=11";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                
                con.Open();
                da.Fill(dt2);
                listBox2.Items.Clear();
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {

                        listBox2.Items.Add(dr["emp_name"].ToString().Trim() + "-" + dr["idempwork"].ToString().Trim());
                    }


                }
                else
                {
                    MessageBox.Show("No ticket found for Approval!!!");
                }
                con.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                label13.Text = (listBox2.SelectedItem.ToString()).Split('-')[0];
                label11.Text = (listBox2.SelectedItem.ToString()).Split('-')[1];
                var rows = from row in dt2.AsEnumerable() where row.Field<int>("idempwork") == Convert.ToInt32(label11.Text) select row;
                DataRow[] outrow = rows.ToArray();
                foreach(DataRow row in outrow)
                {
                    textBox6.Text = row["tow"].ToString();
                    richTextBox4.Text = row["wd"].ToString();
                    richTextBox3.Text = row["tta"].ToString();
                    textBox5.Text = row["sd"].ToString();
                    textBox4.Text = row["ed"].ToString();
                    label19.Text = row["a"].ToString();
                    richTextBox6.Text = row["sodd"].ToString();
                    a1 = Convert.ToInt32(row["approve1"].ToString());
                    a2 = Convert.ToInt32(row["approve2"].ToString());
                    //Thread t = new Thread(()=> getreason(label11.Text,label19.Text));
                    //t.Start();
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        
        // to show reason on the form directly on double click in liast box. 
        //private void getreason(string id,string status)
        //{
        //    try
        //    {
        //        MySqlConnection con = new MySqlConnection();
        //        con.ConnectionString = Form1.sqlstring;
        //        querry = "Select comreason,dropreason, idempwork, date,time from empdailyreport where idempwork ="+Convert.ToInt32(id)+" and comment ='"+status+"'";
        //        MySqlCommand cmnd = new MySqlCommand(querry,con);
        //        DataTable dt = new DataTable();
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmnd);
        //        con.Open();
        //        da.Fill(dt);
        //        con.Close();
                
        //    }catch(Exception e1)
        //    {
        //        MessageBox.Show(e1.ToString());
        //    }
        //}
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ticket_history a = new ticket_history(label19.Text,label11.Text);
                a.ShowDialog();

            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int rate = 0;
                if(!string.IsNullOrEmpty(richTextBox5.Text)&& !string.IsNullOrWhiteSpace(richTextBox5.Text) )
                {
                    if ((!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text)))
                    {
                        rate = Convert.ToInt32(textBox7.Text);
                        if (rate < 11 && rate >=0)
                        {
                            con.ConnectionString = Form1.sqlstring;


                            if (string.Compare(Form1.user, "padmin") == 0)
                            {
                                if (a1 > 10)
                                {
                                    querry = "update empwork set approve2=" + rate + " ,ticket_status = 'De-Active',approve2_date =CURDATE(),approve2_time=CURTIME(),approve2_reason='" + richTextBox5.Text + "' where idempwork =" + label11.Text;
                                }
                                else if (a1 <= 10)
                                {
                                    querry = "update empwork set approve2=" + rate + " ,ticket_status = '" + label19.Text + "',approve2_date =CURDATE(),approve2_time=CURTIME(),approve2_reason='" + richTextBox5.Text + "' where idempwork =" + label11.Text;
                                }


                            }
                            else
                            {
                                if (a1 > 10)
                                {
                                    querry = "update empwork set approve1=" + rate + " ,ticket_status = 'De-Active',approve1_date =CURDATE(),approve1_time=CURTIME(),approve1_reason='" + richTextBox5.Text + "' where idempwork =" + label11.Text;
                                }
                                else if (a1 <= 10)
                                {
                                    querry = "update empwork set approve1=" + rate + " ,ticket_status = '" + label19.Text + "',approve1_date =CURDATE(),approve1_time=CURTIME(),approve1_reason='" + richTextBox5.Text + "' where idempwork =" + label11.Text;
                                }
                            }

                            cmnd = new MySqlCommand(querry, con);
                            con.Open();
                            if (cmnd.ExecuteNonQuery() > 0)
                            {
                                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                                label13.Text = label11.Text = label19.Text = "----------";
                                textBox6.Text = textBox5.Text = textBox4.Text = textBox7.Text = richTextBox4.Text = richTextBox3.Text = richTextBox5.Text = richTextBox6.Text = string.Empty;
                                MessageBox.Show("Thank you for your approval and rating.");
                            }
                            con.Close();

                        }
                        else
                            MessageBox.Show("Kindly rate from 0 to 10 only !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MessageBox.Show("Kindly rate the work done under this ticket","Error",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                    }

                }
                else
                {
                    MessageBox.Show("Please enter a valid reason for approval");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
         
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int[] id = new int[500];
            List<string> name = new List<string>();
            List<string> idwork = new List<string>();
            try
            {
                dt3.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;                
                querry = "Select * from empwork where ticket_status = 'Active' Order by idempwork";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt3.Rows)
                    {
                       TreeNode node = new TreeNode();
                        node.Text = dr["emp_name"].ToString().Trim();
                        if (!listBox3.Items.Contains(dr["emp_name"].ToString().Trim()))
                        {
                            listBox3.Items.Add(dr["emp_name"].ToString());
                            treeView1.Nodes.Add(dr["emp_name"].ToString().Trim());
                            treeView1.EndUpdate();                           
                        }                      
                        
                    }
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        treeView1.Nodes[node.Index].Nodes.Clear();
                        foreach (DataRow r in dt3.Rows)
                        {
                            if(string.Compare(node.Text,r["emp_name"].ToString().Trim())==0)
                            {
                                treeView1.Nodes[node.Index].Nodes.Add(r["idempwork"].ToString().Trim());
                            }
                        }
                        treeView1.EndUpdate();
                    }

                }
                else
                {
                    MessageBox.Show("No Active tickets found !!!");
                }
                con.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label6.Text = label7.Text = "----------";
            textBox1.Text = textBox2.Text = textBox3.Text = richTextBox2.Text = richTextBox1.Text = string.Empty;
        }

        

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                label27.Text = (listBox3.SelectedItem.ToString()).Split('-')[0];
                label32.Text = (listBox3.SelectedItem.ToString()).Split('-')[1];
                
                
                var rows = from row in dt3.AsEnumerable() where row.Field<int>("idempwork") == Convert.ToInt32(label32.Text) select row;
                DataRow[] outrow = rows.ToArray();
                foreach (DataRow row in outrow)
                {
                    richTextBox8.Text = row["wd"].ToString();
                    richTextBox7.Text = row["tta"].ToString();
                    textBox10.Text = row["sd"].ToString();
                    textBox9.Text = row["ed"].ToString();
                }

                Thread t = new Thread(() => gethistory());
                t.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void gethistory()
        {
            try
            {
                
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from empdailyreport where idempwork = " + label32.Text;
                cmnd = new MySqlCommand(querry,con);
                da = new MySqlDataAdapter(cmnd);                
                dt4.Clear();
                da.Fill(dt4);
                Invoke(new Action(() => dataGridView1.Rows.Clear()));
                if(dt4.Rows.Count>0)
                {
                    Invoke(new Action(() => dataGridView1.Rows.Clear()));
                    int i = 0;
                    foreach(DataRow row in dt4.Rows)
                    {
                        Invoke(new Action(() => dataGridView1.Rows.Add()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["dailyworkid"].Value = row["idemp_dailyreport"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["id"].Value = row["idempwork"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["work"].Value = row["comment"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["date"].Value = row["date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["time"].Value = row["time"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate"].Value = row["rate"].ToString()));

                        if (set == 1)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate1"].Value = row["seen1"].ToString()));
                            if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["rate1"].Value.ToString()))
                            {
                                Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate1"].ReadOnly = false));
                                Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate1"].Style.BackColor = Color.Orange));
                            }
                        }
                        else if (set == 2)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate2"].Value = row["seen2"].ToString()));
                            if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["rate2"].Value.ToString()))
                            {
                                Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate2"].ReadOnly = false));
                                Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate2"].Style.BackColor = Color.CadetBlue));
                            }
                        }

                        i++;
                                                
                    }
                                        
                    xx = 0;
                }
                else
                {
                    MessageBox.Show("No record found of this ticket TW-ID : " + label32.Text);
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ticket_history a = new ticket_history("-----", label32.Text);
                a.ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (string.Compare(label7.Text, "----------") != 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;

            if (string.Compare(label11.Text, "----------") != 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;

            if (string.Compare(label32.Text, "----------") != 0)
               panel5.Enabled = true;
            else
               panel5.Enabled = false;
            if(string.Compare(label42.Text,"----------")!=0)
                panel6.Enabled = true;
            else
                panel6.Enabled = false;
            if (string.Compare(label13.Text, "----------") != 0)
                panel3.Enabled = true;
            else
                panel3.Enabled = false;

            if (dataGridView1.Rows.Count > 0)
                linkLabel6.Enabled = true;
            else
                linkLabel6.Enabled = false;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Thread t = new Thread(()=>rateupdate());
                t.Start();
                
                //if ((!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)))
                //{
                    
                //    if (rate < 11 && rate >= 0)
                //    {
                //        if (string.Compare(Form1.user, "padmin") == 0)
                //        {
                //            querry = "update empdailyreport set seen2=" + rate + " where idempwork =" + label32.Text;

                //        }
                //        else
                //        {
                //            querry = "update empdailyreport set seen1=" + rate + " where idempwork =" + label32.Text;
                //        }
                //    }
                //    else
                //        MessageBox.Show("Kindly rate the work from 0 to 10 only !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //}
                //else
                //    MessageBox.Show("Kindly rate the work done under this ticket !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //con = new MySqlConnection();
                //con.ConnectionString = Form1.sqlstring;
                //cmnd = new MySqlCommand(querry, con);
                //con.Open();
                //if (cmnd.ExecuteNonQuery() > 0)
                //    MessageBox.Show("Thank you for rating the work !!!");
                //con.Close();
               
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void rateupdate()
        {
            try
            {
                int i = 0;
                int r;
                for (int q = 0; q < xx; q+=2)
                {
                    con = new MySqlConnection();
                    r = Convert.ToInt32(dataGridView1.Rows[index[i]].Cells[index[i+1]].Value.ToString());
                    
                    if (set == 1)
                    {
                        querry = "update empdailyreport set seen1=" + r + " where idempwork =" + label32.Text + " AND idemp_dailyreport = " + dataGridView1.Rows[index[i]].Cells["dailyworkid"].Value.ToString();                                             
                    }
                    else
                    {
                        querry = "update empdailyreport set seen2=" + r + " where idempwork =" + label32.Text + " AND idemp_dailyreport = " + dataGridView1.Rows[index[i]].Cells["dailyworkid"].Value.ToString();
                    }
                    con.ConnectionString = Form1.sqlstring;
                    cmnd = new MySqlCommand(querry, con);
                    con.Open();
                    if (cmnd.ExecuteNonQuery() > 0)
                    {
                      Invoke(new Action(()=>  dataGridView1.Rows[index[i]].Cells[index[i + 1]].Style.BackColor = Color.White));
                      Invoke(new Action(()=>   dataGridView1.Rows[index[i]].Cells[index[i + 1]].ReadOnly = true));
                    }
                    con.Close();
                    i += 2;
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void load(int id)
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                string querry = "Select * from empdailyreport where idempwork = " + id;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    Invoke(new Action(() => dataGridView1.Visible = true));
                    foreach (DataRow row in dt.Rows)
                    {
                        Invoke(new Action(() => dataGridView1.Rows.Add()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["id"].Value = row["idempwork"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["work"].Value = row["comment"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["date"].Value = row["date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["time"].Value = row["time"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["rate2"].Value = row["seen2"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i++].Cells["rate1"].Value = row["seen1"].ToString()));
                        
                        if (string.Compare(row["comment"].ToString(), "Completed") == 0)
                            Invoke(new Action(() => dataGridView1.Rows[i - 1].DefaultCellStyle.BackColor = Color.LawnGreen));
                        else if (string.Compare(row["comment"].ToString(), "Droped") == 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i - 1].DefaultCellStyle.BackColor = Color.Red));
                            Invoke(new Action(() => dataGridView1.Rows[i - 1].DefaultCellStyle.ForeColor = Color.White));
                        }
                    }
                }
                else
                {
                    Invoke(new Action(() => label1.Visible = true));
                    Invoke(new Action(() => label1.Text = "No Record Found for Ticket TW-ID:" + id));
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                    && !string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    int check = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    if (check < 11 && check >= 0)
                    {
                        index[xx] = e.RowIndex;
                        index[++xx] = e.ColumnIndex;
                        xx++;
                        button3.Enabled = true;
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                        button3.Enabled = false;
                        MessageBox.Show("You can rate from 0 to 10 only !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                    button3.Enabled = false;
                }
            }
            catch(Exception e1)
            {
                if(e1.Message.Contains("Object reference not set"))
                MessageBox.Show("Please enter a valid number !!!");
                index = shift(e.RowIndex,e.ColumnIndex,index);
            }
        }

        private int[] shift(int row, int col, int[] temp1)
        {
            try
            {
                int[] temp = temp1;
                int found = 0;
                if (xx > 0)
                {
                    for (int cc = 0; cc < xx; cc++)
                    {
                        if (temp[cc] == row && temp[cc + 1] == col)
                        {
                            found = 1;
                        }
                        else
                            cc += 1;
                        if (found == 1)
                        {
                            temp[cc] = temp[cc + 2];
                        }
                    }
                    if(found == 1)
                     xx -= 2;                  
                }
                return temp;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return temp1;
            }
        }

        #region leave approval

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread tr = new Thread(()=> approval());
                tr.Start();

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            
        }

        private void approval()
        {
            int[] id = new int[500];
            List<string> name = new List<string>();
            List<string> idwork = new List<string>(); ;
            try
            {
                dt5.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select t1.*,t2.emp_fname from emp_leave as t1 left join empdetails as t2 on t1.emp_number = t2.emp_number where t1.admin_approval = '0';";

                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);

                con.Open();
                da.Fill(dt5);
                con.Close();
                 Invoke(new Action(()=>listBox4.Items.Clear()));
                con.Close();

                if (dt5.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt5.Rows)
                    {
                       Invoke(new Action(()=> listBox4.Items.Add(dr["emp_fname"].ToString().Trim() + "-" + dr["idleave"].ToString().Trim())));
                    }

                }
                else
                {
                    MessageBox.Show("No ticket found for Approval!!!");
                }
                con.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void cancel_approval()
        {
            int[] id = new int[500];
            List<string> name = new List<string>();
            List<string> idwork = new List<string>();
            try
            {
                dt6.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select t1.*,t2.emp_fname from emp_leave as t1 left join empdetails as t2 on t1.emp_number = t2.emp_number where t1.cancel_id != '0' and (t1.cancel_status = '0' OR t1.cancel_status ='1')";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(dt6);
                con.Close();
                 Invoke(new Action(()=>listBox5.Items.Clear()));
                con.Close();

                if (dt6.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt6.Rows)
                    {
                        Invoke(new Action(()=> listBox5.Items.Add(dr["emp_fname"].ToString().Trim() + "-" + dr["idleave"].ToString().Trim())));
                    }

                }
                else
                {
                    MessageBox.Show("No ticket found for Approval!!!");
                }
                con.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                label42.Text = (listBox4.SelectedItem.ToString()).Split('-')[0];
                label40.Text = (listBox4.SelectedItem.ToString()).Split('-')[1];

                var rows = from row in dt5.AsEnumerable() where row.Field<int>("idleave") == Convert.ToInt32(label40.Text) select row;
                DataRow[] outrow = rows.ToArray();

                foreach (DataRow row in outrow)
                {
                    richTextBox12.Text = row["reason"].ToString();                    
                    textBox11.Text = row["from_date"].ToString();
                    textBox12.Text = row["till_date"].ToString();
                    textBox8.Text = row["date"].ToString();
                    textBox13.Text = row["time"].ToString();
                    label29.Text = row["half_day"].ToString();
                    label35.Text = row["emp_number"].ToString();
                    panel6.Enabled = true;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ticket_history a = new ticket_history(label35.Text);
                a.ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                
                if (!string.IsNullOrEmpty(richTextBox10.Text))
                {
                    
                        con = new MySqlConnection();
                        


                        querry = "update emp_leave set admin_approval= 'Approved' , admin_reason = '"
                            + richTextBox10.Text
                            + "',admin_date = CURDATE()"
                            + ", admin_time = CURTIME()"
                            + " where idleave = " + label40.Text;                        

                        con.ConnectionString = Form1.sqlstring;
                        cmnd = new MySqlCommand(querry, con);
                        con.Open();

                        if (cmnd.ExecuteNonQuery() > 0)
                        {
                            listBox4.Items.RemoveAt(listBox4.SelectedIndex);
                            panel6.Enabled = false;
                            richTextBox10.Text = string.Empty;
                            MessageBox.Show("This tikect has been approved sucessfully !!!");
                        }

                        con.Close();
                        i += 2;
                    
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            } 
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                if(treeView1.SelectedNode.Parent != null)
                {
                    try
                    {

                        label27.Text = treeView1.SelectedNode.Parent.Text;
                        label32.Text = treeView1.SelectedNode.Text;

                        var rows = from row in dt3.AsEnumerable() where row.Field<int>("idempwork") == Convert.ToInt32(label32.Text) select row;
                        DataRow[] outrow = rows.ToArray();
                        foreach (DataRow row in outrow)
                        {
                            richTextBox8.Text = row["wd"].ToString();
                            richTextBox7.Text = row["tta"].ToString();
                            textBox10.Text = row["sd"].ToString();
                            textBox9.Text = row["ed"].ToString();
                        }

                        Thread t = new Thread(() => gethistory());
                        t.Start();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.ToString());
                    }
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                if (!string.IsNullOrEmpty(richTextBox10.Text))
                {

                    con = new MySqlConnection();

                    querry = "update emp_leave set admin_approval= 'Not Approved' , admin_reason = '"
                        + richTextBox10.Text
                        + "',admin_date = CURDATE()"
                        + ", admin_time = CURTIME()"
                        + " where idleave = " + label40.Text;

                    con.ConnectionString = Form1.sqlstring;
                    cmnd = new MySqlCommand(querry, con);
                    con.Open();

                    if (cmnd.ExecuteNonQuery() > 0)
                    {
                        listBox4.Items.RemoveAt(listBox4.SelectedIndex);
                        panel6.Enabled = false;
                        richTextBox10.Text = string.Empty;
                        MessageBox.Show("This tikect has been disapproved sucessfully !!!");
                    }

                    con.Close();
                    i += 2;

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            } 
        }

        private void listBox5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                label55.Text = (listBox5.SelectedItem.ToString()).Split('-')[0];
                label53.Text = (listBox5.SelectedItem.ToString()).Split('-')[1];

                var rows = from row in dt6.AsEnumerable() where row.Field<int>("idleave") == Convert.ToInt32(label53.Text) select row;
                DataRow[] outrow = rows.ToArray();

                foreach (DataRow row in outrow)
                {
                    label52.Text = row["cancel_id"].ToString();
                    richTextBox11.Text = row["reason"].ToString();
                    textBox16.Text = row["from_date"].ToString();
                    textBox17.Text = row["till_date"].ToString();
                    textBox14.Text = row["date"].ToString();
                    textBox15.Text = row["time"].ToString();
                    label50.Text = "Cancellation Of Leave";
                    label44.Text = row["emp_number"].ToString();

                    panel8.Enabled = true;

                    Thread tr = new Thread(() => cancel_date_time());
                    tr.Start();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void cancel_date_time  ()
        {
            try
            {
                DataTable temp = new DataTable();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from cancel_leave where idcancel_leave = "+label52.Text;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                con.Open();
                da.Fill(temp);
                con.Close();


                if (temp.Rows.Count > 0)
                {
                    Invoke(new Action( ()=> label62.Text = temp.Rows[0]["cancel_date"].ToString()));
                    Invoke(new Action(() => label64.Text = temp.Rows[0]["cancel_time"].ToString()));

                }
                else
                {
                    MessageBox.Show("No ticket found for Approval!!!");
                } 
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                panel8.Enabled = false;
                    con = new MySqlConnection();
                    querry = "update cancel_leave set approval= 'Approved'"
                        + ",ap_date = CURDATE()"
                        + ", ap_time = CURTIME()"
                        + " where idcancel_leave = " + label52.Text;

                    con.ConnectionString = Form1.sqlstring;
                    cmnd = new MySqlCommand(querry, con);
                    con.Open();

                    if (cmnd.ExecuteNonQuery() > 0)
                    {
                        listBox5.Items.RemoveAt(listBox5.SelectedIndex);
                        MessageBox.Show("This tikect has been approved sucessfully !!!");
                    }
                    else
                    {
                        MessageBox.Show("Updation could not be done !!! Kindly try again");
                    }

                    con.Close();
                                   
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            } 
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread tr = new Thread(() => cancel_approval());
                tr.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ticket_history a = new ticket_history(label35.Text);
                a.ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
    }
}
