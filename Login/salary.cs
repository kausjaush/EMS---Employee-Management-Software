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
    public partial class salary : Form
    {
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dtupdate = new DataTable();
        DataTable dtbank = new DataTable();
        DataTable dtpay = new DataTable();
        DataTable dtdown = new DataTable();
        DataTable dthol = new DataTable();
        DataTable emplist = new DataTable();

        int pay_day =0;
        int pay_hour = 0,pay_min = 0;
        int leave_hours = 0, leave_mins =  0, leave_days = 0, set = 0;
        int left_hours = 0, left_mins = 0, left_days = 0; 
        string querry,oldcur = "0",newcur = "0";
        int[] id= new Int32[500];
        int old =0;
        float per_hour = 0, per_day = 0, per_min = 0, extra = 0, net = 0, calculated_salary = 0, cal_salary = 0, gross = 0;
        int pt = 0;
        int dif_day = 0, dif_hour = 0, dif_min = 0;
        int temp_expected_days =0, temp_holiday_temphour = 0, temp_holiday_tempmins = 0;
        int attend_day = 0;
        int newleft_mins = 0, temp_leavemins = 0, temp_acruedmins = 0 ,newleft_hours = 0,newleft_days =0;

        public salary()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM, yyyy";

            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "MMMM, yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "MMMM, yyyy";
            timer1.Tick+=timer1_Tick;
            timer1.Start();           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select emp_number,emp_fname,emp_mname,emp_lname,bank_name,ifsc_code,acc_number,bank_address,acc_name,salary from empdetails order by emp_number ASC";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dtdown = new DataTable();
                con.Open();
                da.Fill(dtdown);
                if(dtdown.Rows.Count > 0)
                {         
                    foreach(DataRow dr in dtdown.Rows)
                    {
                        listBox1.Items.Add( dr["emp_number"].ToString()+"-"+dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No employee data found !!!");
                }
                con.Close();
                
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                       
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    if (!string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrWhiteSpace(richTextBox1.Text))
                    {
                        button2.Enabled = false;
                        textBox2.ReadOnly = true;
                        con = new MySqlConnection();
                        con.ConnectionString = Form1.sqlstring;
                        querry = "Insert into salupdate (emp_number,emp_oldsal,emp_newsal,emp_reason,updateon,time,updatedby,pt) values (" + Convert.ToInt32(label23.Text) + "," + old + "," + textBox2.Text + ",'" + richTextBox1.Text + "', CURDATE(),CURTIME(),'" + Form1.empname + "',@pt)";
                        cmnd = new MySqlCommand(querry, con);
                        cmnd.Parameters.AddWithValue("@pt", Convert.ToInt32(textBox11.Text));
                        con.Open();
                        if (cmnd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Salary Updated !!!", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.None);
                            label28.Text = label25.Text = label6.Text = label5.Text = "-----------";
                            label23.Text = "-----------";
                            textBox2.Text = textBox11.Text = string.Empty;
                            panel6.Enabled = false;
                        }
                        else
                        {

                            MessageBox.Show("Error in connectivity !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        con.Close();
                    }
                    else
                        MessageBox.Show("Kinldy enter a specifc reason !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Kinldy enter the new Salary of the Employee !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel6.Enabled = false;
            label5.Text = label6.Text = "-----------";
            button2.Enabled = true;
            textBox2.ReadOnly = false;
            textBox2.Text = string.Empty;
            richTextBox1.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    Excel.ExtractDataToCSV(dataGridView1,"Salary Month \n From: "+textBox10.Text +" To: "+textBox3.Text);
                }
                else if (!string.IsNullOrEmpty(textBox10.Text) && string.IsNullOrEmpty(textBox3.Text))
                {
                    Excel.ExtractDataToCSV(dataGridView1, "Salary Month :" + textBox10.Text);
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int set = 0;
                DataTable dttemp = new DataTable();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    querry = "Select * from empsalary where emp_number = " + label12.Text + " AND salmon_yr between '" + textBox10.Text + "' and '" + textBox3.Text + "' Order by salmon_yr ASC";
                    set = 1;
                }
                else if (!string.IsNullOrEmpty(textBox10.Text) && string.IsNullOrEmpty(textBox3.Text))
                {
                    querry = "Select * from empsalary where emp_number = " + label12.Text + " AND salmon_yr = '" + textBox10.Text + "'";
                    set = 1;
                }
                else if (string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Please select From Month as well !!!");
                }
                else
                    MessageBox.Show("Please select month of the salary or provide range of months !!!");

                if (set == 1)
                {
                    cmnd = new MySqlCommand(querry, con);
                    da = new MySqlDataAdapter(cmnd);
                    con.Open();
                    da.Fill(dttemp);

                    if (dttemp.Rows.Count > 0)
                    {
                        Thread tr = new Thread(() => fillsal(dttemp));
                        tr.Start();
                    }
                    else
                        MessageBox.Show("The decision for the payment of this employee is pending.", "No Record Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private void fillsal(DataTable temp)
        {
            int i = 0;
            Invoke(new Action(() => dataGridView1.Rows.Clear()));
            try
            {
                foreach (DataRow dr in temp.Rows)
                {
                    Invoke(new  Action(()=>  dataGridView1.Rows.Add()));
                    Invoke(new  Action(()=>dataGridView1.Rows[i].Cells["empname"].Value = dr["empname"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["emp_number"].Value = dr["emp_number"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["acc_number"].Value = dr["acc_number"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["acc_name"].Value = dr["acc_name"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["nbank"].Value = dr["nbank"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["ifsc_code"].Value = dr["ifsc_code"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["bank_address"].Value = dr["bank_address"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["apaid"].Value = dr["salamount"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["pdate"].Value = dr["date"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["ptime"].Value = dr["time"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["salmon"].Value = dr["salmon_yr"].ToString()));
                    Invoke(new Action(() => dataGridView1.Rows[i].Cells["paid_by"].Value = dr["paid_by"].ToString()));
                   
                    if (string.Compare(dr["salamount"].ToString(), "0") == 0)
                    {
                        Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red));
                        Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));
                    }
                    i++;
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int [] id = new int[500];
            try
            {
                dtupdate.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select emp_number,emp_fname,emp_mname,emp_lname,bank_name,ifsc_code,acc_number,bank_address,acc_name,salary from empdetails Order by emp_number ASC";
                
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                
                con.Open();
                da.Fill(dtupdate);
                int i = 0;
                listBox2.Items.Clear();
                if (dtupdate.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtupdate.Rows)
                    {
                        listBox2.Items.Add(dr["emp_number"].ToString() +" - "+ dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
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

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int[] id = new int[500];
            try
            {
                dtbank.Clear();
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select emp_number,emp_fname,emp_mname,emp_lname,bank_name,ifsc_code,acc_number,bank_address,acc_name from empdetails Order by emp_number ASC";

                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);

                con.Open();
                da.Fill(dtbank);
                int i = 0;
                listBox3.Items.Clear();
                if (dtbank.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbank.Rows)
                    {
                        listBox3.Items.Add(dr["emp_number"].ToString() + " - " + dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
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

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                DataRow[] rows = dtupdate.Select("emp_number = "+listBox2.SelectedItem.ToString().Split('-')[0]);
                    if (rows.Length > 0)
                    {
                        foreach (DataRow dr in rows)
                        {
                            label5.Text = dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString();
                            label23.Text = dr["emp_number"].ToString();
                            if (!string.IsNullOrEmpty(dr["bank_name"].ToString()))
                            {
                                label6.Text = dr["salary"].ToString();
                                
                                label28.Text = dr["acc_number"].ToString();
                                label25.Text = dr["bank_name"].ToString();
                                
                                old = Convert.ToInt32(dr["salary"].ToString());
                                if(old == 0)
                                {
                                    label6.Text = "No salary is yet offered to this employee.";
                                }
                                panel6.Enabled = true;
                                textBox2.ReadOnly = false;
                                button2.Enabled = true;
                                
                            }
                            else
                            {
                                panel6.Enabled = false;
                                richTextBox1.Text = string.Empty;
                                textBox2.Text = string.Empty;
                                label6.Text = label28.Text = label25.Text = "---------------";
                                MessageBox.Show("Bank details of this employee not found. Kindly update the bank details first !!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                
                            }
                        }
                    }
                    else
                    {
                        label6.Text = "-----------";
                        label5.Text = "-----------";
                        panel6.Enabled = false;
                        MessageBox.Show("No record found with this Employee ID. Kinldy enter valid Employee ID.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }


            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = dateTimePicker3.Value.Month.ToString() + " " + dateTimePicker3.Value.Year.ToString();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                DataRow[] rows = dtbank.Select("emp_number = " + listBox3.SelectedItem.ToString().Split('-')[0]);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        
                        label35.Text = dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString();
                        label32.Text = dr["emp_number"].ToString();
                        
                        textBox4.Text = dr["acc_name"].ToString();
                        textBox5.Text = dr["acc_number"].ToString();
                        textBox6.Text = dr["bank_name"].ToString();
                        textBox7.Text = dr["ifsc_code"].ToString();
                        richTextBox2.Text = dr["bank_address"].ToString();                       
                        panel6.Enabled = false;
                        richTextBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        button1.Enabled = true;
                        if(!string.IsNullOrEmpty(textBox4.Text))
                        {

                        }
                    }
                }
                else
                {
                    label6.Text = "-----------";
                    label5.Text = "-----------";
                    panel6.Enabled = false;
                    MessageBox.Show("No record found with this Employee ID. Kinldy enter valid Employee ID.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
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
                if ((!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                    && (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
                    && (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
                    && (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
                    && (!string.IsNullOrEmpty(richTextBox2.Text) && !string.IsNullOrWhiteSpace(richTextBox2.Text)))
                {

                    button1.Enabled = false;
                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;
                    querry = "Update empdetails set acc_name = '" + textBox4.Text + "', acc_number = '" + textBox5.Text + "', ifsc_code = '" + textBox7.Text + "', bank_name ='" + textBox6.Text + "',bank_address = '" + richTextBox2.Text + "' where emp_number = " + label32.Text;
                    cmnd = new MySqlCommand(querry, con);
                    con.Open();

                    if (cmnd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Salary Updated !!!", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.None);
                        linkLabel6.Text = "Refresh";
                    }
                    else
                    {

                        MessageBox.Show("Error in connectivity !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Kinldly fill all the bank details !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int[] id = new int[500];
                try
                {
                    dtpay.Clear();
                    
                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;
                    querry = "Select t1.emp_number,emp_fname,emp_mname,emp_lname,bank_name,ifsc_code,acc_number,bank_address,acc_name,t1.salary from empdetails as t1 left join emp_calsal as t2 on t1.emp_number = t2.emp_number order by t1.emp_number";

                    cmnd = new MySqlCommand(querry, con);
                    da = new MySqlDataAdapter(cmnd);

                    con.Open();
                    da.Fill(dtpay);
                    int i = 0;
                    listBox4.Items.Clear();
                    if (dtpay.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtpay.Rows)
                        {
                            listBox4.Items.Add(dr["emp_number"].ToString() + " - " + dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
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
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dateTimePicker4_CloseUp(object sender, EventArgs e)
        {
            textBox9.Text = dateTimePicker4.Value.Month + " - "+ dateTimePicker4.Value.Year.ToString();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            textBox9.Text = dateTimePicker4.Value.Month + " - " + dateTimePicker4.Value.Year.ToString();
        }

        private void listBox4_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtpay.Select("emp_number = " + listBox4.SelectedItem.ToString().Split('-')[0]);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {                        
                        label51.Text = dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString();
                        label53.Text = dr["emp_number"].ToString();
                        label47.Text = dr["acc_name"].ToString();
                        label38.Text = dr["acc_number"].ToString();
                        label48.Text = dr["bank_name"].ToString();
                        label49.Text = dr["ifsc_code"].ToString();
                        label50.Text = dr["bank_address"].ToString();
                        label40.Text = dr["salary"].ToString();
                        label15.Text = "-----------";
                        panel9.Enabled = true;
                    }
                }
                else{
                    label51.Text = 
                    label53.Text = 
                    label47.Text = 
                    label38.Text = 
                    label48.Text = 
                    label49.Text = 
                    label50.Text = 
                    label40.Text = 
                    label15.Text = "-----------";
                }
               
                con.Close();
            }


            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void calsal ()
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "select * from emp_calsal where emp_number = " + label53.Text + " and salmon_yr  = '" + format_date.dateformat(dateTimePicker4.Value.Month.ToString(),dateTimePicker4.Value.Year.ToString())+"'";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                DataTable temp = new DataTable();
                con.Open();
                
                da.Fill(temp);
                con.Close();
                if(temp.Rows.Count>0)
                {
                    foreach(DataRow dr in temp.Rows)
                        Invoke(new Action (()=> label15.Text = "Rs. "+ dr["cal_salary"]));
                }
                else
                    Invoke(new Action(() => label15.Text = "Please Update the Calculated Salary from Salary Calculation tab for this employee."));

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker4_CloseUp_1(object sender, EventArgs e)
        {
            textBox9.Text = format_date.dateformat(dateTimePicker4.Value.Month.ToString(), dateTimePicker4.Value.Year.ToString());
            Thread tr = new Thread(()=>calsal());
            tr.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                pay("Paid",textBox8.Text);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void pay(string str, string amount)
        {
            try
            {
                if ((string.Compare(label38.Text, "") != 0
                    && (string.Compare(label47.Text, "") != 0)
                    && (string.Compare(label48.Text, "") != 0)
                    && (string.Compare(label49.Text, "") != 0)
                    && (string.Compare(label50.Text, "") != 0)))
                {
                    if (string.Compare(str, "Paid") == 0)
                    {
                        if ((!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)))
                        {

                            button1.Enabled = false;
                            con = new MySqlConnection();
                            con.ConnectionString = Form1.sqlstring;
                            querry = "Insert into empsalary (emp_number,empname,salmon_yr,salstatus,salamount,date,time,paid_by,nbank,ifsc_code,bank_address,acc_name,acc_number) values (@emp_number,@name,@month,@salstatus,@salamount,CURDATE(),CURTIME(),@user,@nbank,@ifsc,@bank_address,@acc_name,@acc_number)";
                            cmnd = new MySqlCommand(querry, con);
                            cmnd.Parameters.Add("@emp_number", MySqlDbType.VarChar).Value = label53.Text;
                            cmnd.Parameters.Add("@name", MySqlDbType.VarChar).Value = label51.Text;
                            cmnd.Parameters.Add("@month", MySqlDbType.VarChar).Value = textBox9.Text;
                            cmnd.Parameters.Add("@salamount", MySqlDbType.VarChar).Value = amount;
                            cmnd.Parameters.Add("@user", MySqlDbType.VarChar).Value = Form1.empname;
                            cmnd.Parameters.Add("@salstatus", MySqlDbType.VarChar).Value = str;
                            cmnd.Parameters.Add("@nbank", MySqlDbType.VarChar).Value = label48.Text;
                            cmnd.Parameters.Add("@ifsc", MySqlDbType.VarChar).Value = label49.Text;
                            cmnd.Parameters.Add("@bank_address", MySqlDbType.VarChar).Value = label50.Text;
                            cmnd.Parameters.Add("@acc_name", MySqlDbType.VarChar).Value = label47.Text;
                            cmnd.Parameters.Add("@acc_number", MySqlDbType.VarChar).Value = label38.Text;

                            con.Open();
                            if (cmnd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Successfull !!!", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.None);
                                label40.Text = label38.Text = label47.Text = label48.Text = label49.Text = label50.Text = label51.Text = label53.Text = "-----------";
                                textBox8.Text = string.Empty;
                                textBox9.Text = string.Empty;
                                textBox8.Enabled = false;
                                button4.Enabled = false;
                                linkLabel4.Enabled = false;
                                panel9.Enabled = false;
                            }
                            else
                            {

                                MessageBox.Show("Error in connectivity !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kinldly fill all the bank details !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        button1.Enabled = false;
                        con = new MySqlConnection();
                        con.ConnectionString = Form1.sqlstring;
                        querry = "Insert into empsalary (emp_number,empname,salmon_yr,salstatus,salamount,date,time,paid_by,nbank,ifsc_code,bank_address,acc_name,acc_number) values (@emp_number,@name,@month,@salstatus,@salamount,CURDATE(),CURTIME(),@user,@nbank,@ifsc,@bank_address,@acc_name,@acc_number)";
                        cmnd = new MySqlCommand(querry, con);
                        cmnd.Parameters.Add("@emp_number", MySqlDbType.VarChar).Value = label53.Text;
                        cmnd.Parameters.Add("@name", MySqlDbType.VarChar).Value = label51.Text;
                        cmnd.Parameters.Add("@month", MySqlDbType.VarChar).Value = textBox9.Text;
                        cmnd.Parameters.Add("@salamount", MySqlDbType.VarChar).Value = amount;
                        cmnd.Parameters.Add("@user", MySqlDbType.VarChar).Value = Form1.empname;
                        cmnd.Parameters.Add("@salstatus", MySqlDbType.VarChar).Value = str;
                        cmnd.Parameters.Add("@nbank", MySqlDbType.VarChar).Value = label48.Text;
                        cmnd.Parameters.Add("@ifsc", MySqlDbType.VarChar).Value = label49.Text;
                        cmnd.Parameters.Add("@bank_address", MySqlDbType.VarChar).Value = label50.Text;
                        cmnd.Parameters.Add("@acc_name", MySqlDbType.VarChar).Value = label47.Text;
                        cmnd.Parameters.Add("@acc_number", MySqlDbType.VarChar).Value = label38.Text;

                        con.Open();
                        if (cmnd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Successfull !!!", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.None);
                            label40.Text = label38.Text = label47.Text = label48.Text = label49.Text = label50.Text = label51.Text = label53.Text = "-----------";
                            textBox8.Text = string.Empty;
                            textBox8.Enabled = false;
                            textBox9.Text = string.Empty;
                            button4.Enabled = false;
                            linkLabel4.Enabled = false;
                        }
                        else
                        {

                            MessageBox.Show("Error in connectivity !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        con.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Kindly Update the Bank Details of this Employee First !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                pay("NotPaid","0");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox9.Text))
            {
                textBox8.Enabled = true;
                button4.Enabled = true;
                linkLabel4.Enabled = true;
            }
            else
            {
                textBox8.Enabled = false;
                button4.Enabled = false;
                linkLabel4.Enabled = false;
            }
            if (dataGridView1.Rows.Count > 0)
                button3.Enabled = true;
            else
                button3.Enabled = false;
            if (textBox1.TextLength > 0)
                linkLabel10.Enabled = true;
            else
                linkLabel10.Enabled = false;

            if (string.Compare(label21.Text, "-------------") != 0)
                button5.Enabled = true;
            else
                button5.Enabled = false;

            if (string.Compare(label15.Text, "-----------") != 0 && string.Compare(label15.Text,"Please Update the Calculated Salary from Salary Calculation tab for this employee.")!=0)
                panel10.Enabled = true;
            else
                panel10.Enabled = false;

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow[] rows = dtdown.Select("emp_number = " + listBox1.SelectedItem.ToString().Split('-')[0]);
                
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {

                        label14.Text = dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString();
                        label12.Text = dr["emp_number"].ToString();
                        panel5.Enabled = true;
                    }
                }

                con.Close();
            }


            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            textBox10.Text = format_date.dateformat(dateTimePicker1.Value.Month.ToString(),dateTimePicker1.Value.Year.ToString());
        }

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            textBox3.Text = format_date.dateformat(dateTimePicker3.Value.Month.ToString(), dateTimePicker3.Value.Year.ToString());
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox10.Text = textBox3.Text = string.Empty;
            dataGridView1.Rows.Clear();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int holiday_hours = 0;
                if (!string.IsNullOrEmpty(textBox9.Text))
                {
                    int[][] empdur;
                    int ii = 0, jj = 0;
                    int[] month = {31,0,31,30,31,30,31,31,30,31,30,31};
                    List<int> emp_wd = new List<int>();
                    List<int> date = new List<int>();
                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;
                    querry = "select * from holiday where month = " + dateTimePicker4.Value.Month +"and year  = "+dateTimePicker4.Value.Year;
                    cmnd = new MySqlCommand(querry, con);
                    da = new MySqlDataAdapter(cmnd);
                    con.Open();
                    dthol.Clear();
                    da.Fill(dthol);
                    con.Close();
                    int i = 1;
                    if (dthol.Rows.Count > 0)
                    {
                        foreach (DataRow row in dthol.Rows)
                        {
                            date.Add(Convert.ToInt32(row["day"].ToString()));
                            i++;
                        }

                        holiday_hours = date.Count * 9;
                    }  
                        
                        int total_hours = 0;
                       
                        if(dateTimePicker4.Value.Month == 2)
                        {
                            if ((dateTimePicker4.Value.Year % 4 == 0) && (dateTimePicker4.Value.Year % 100 != 0) || dateTimePicker4.Value.Year % 400 == 0)
                            {
                                total_hours = 29 * 9;
                                empdur = new int[29][];
                            }
                            else
                            {
                                total_hours = 28 * 9;
                                empdur = new int[28][];
                            }
                        }
                        else
                        {
                            total_hours = month[dateTimePicker1.Value.Month] * 9;
                            empdur = new int[month[dateTimePicker1.Value.Month]][];
                        }
                        querry = "Select * from attendance as t1 left join empdetails as t2 on t1.emp_number = t2.emp_number where t1.emp_number = " + label53.Text + " and t1.month = " + dateTimePicker4.Value.Month + "and t1.year = " + dateTimePicker4.Value.Year;
                        cmnd = new MySqlCommand(querry,con);
                        da = new MySqlDataAdapter(cmnd);
                        dthol.Clear();
                        con.Open();
                        da.Fill(dthol);
                        con.Close();
                        if (dthol.Rows.Count > 0)
                        {
                            
                            foreach(DataRow row in dthol.Rows)
                            {
                                ii += Convert.ToInt32(row["totald"].ToString().Split(':')[0]);
                                jj += Convert.ToInt32(row["totald"].ToString().Split(':')[1]);
                            }
                        }
                        else
                        {
                            ii = 0;
                            jj = 0;
                        }
                    
                    while(jj>59)
                    {
                        jj -= 60;
                        ii += 1;
                    }

                    ii += holiday_hours;
                    int left_hours = total_hours - ii;
                    
                    

                    //List<DateTime> dates = new List<DateTime>();
                    //int year = 2015;
                    //int month = 4;
                    //DayOfWeek day = DayOfWeek.Sunday;
                    //System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                    //for (int i = 1; i <= currentCulture.Calendar.GetDaysInMonth(year, memonth); i++)
                    //{
                    //    DateTime d = new DateTime(year, month, i);
                    //    if (d.DayOfWeek == day)
                    //    {
                    //        listBox4.Items.Add(d);
                    //        dates.Add(d);
                    //    }
                    //}
                }
                else
                    MessageBox.Show("Please select date first","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int[] id = new int[500];
                try
                {
                    emplist.Clear();

                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;
                    querry = "Select t1.emp_number,emp_fname,emp_mname,emp_lname from empdetails as t1 left join leaves as t2 on  t1.emp_number = t2.emp_number Order by t1.emp_number";

                    cmnd = new MySqlCommand(querry, con);
                    da = new MySqlDataAdapter(cmnd);

                    con.Open();
                    da.Fill(emplist);
                    con.Close();
                    
                    listBox5.Items.Clear();
                    if (emplist.Rows.Count > 0)
                    {
                        foreach (DataRow dr in emplist.Rows)
                        {
                            listBox5.Items.Add(dr["emp_number"].ToString() + "-" + dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
                        }
                    }

                    else
                    {
                        MessageBox.Show("No employee data found !!!");
                    }
                    
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listBox5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox5.SelectedItems.Count == 1)
                {
                    DataRow[] rows = emplist.Select("emp_number = " + listBox5.SelectedItem.ToString().Split('-')[0]);
                    if (rows.Length > 0)
                    {
                        foreach (DataRow dr in rows)
                        {

                            label64.Text = dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString();
                            label62.Text = dr["emp_number"].ToString();
                            dataGridView2.Rows.Clear();
                            label69.Text = label60.Text = label58.Text = label56.Text = label112.Text = label108.Text = label111.Text = label114.Text = label105.Text = label116.Text = label88.Text = label89.Text = label18.Text = label21.Text = "-------------";
                        }
                    }
                    panel4.Enabled = true;
                }
                else
                    panel4.Enabled = false;
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
                richTextBox3.Text = string.Empty;
                Thread tr = new Thread(() => attendance());
                tr.Start();
                
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void attendance()
        {
            try
            {
                Invoke(new Action(() => linkLabel10.Enabled = false));
                int[] month = { 31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int i = 0;
                int ii = 0, jj = 0;

                int[] hour = new int [200];
                int[] mins = new int [200];

                textappend("Getting Attendance..."+Environment.NewLine,Color.Red);
                textappend(Environment.NewLine, Color.Black);  
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from attendance where emp_number = " + label62.Text + " and month = " + dateTimePicker2.Value.Month + " and year = " + dateTimePicker2.Value.Year + " Order by day ASC";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);

                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();

                Invoke(new Action(() => dataGridView2.Rows.Clear()));
                Invoke(new Action(() => dataGridView2.Enabled = true));

                if (dthol.Rows.Count > 0)
                {

                    foreach (DataRow row in dthol.Rows)
                    {
                        Invoke(new Action(() => dataGridView2.Rows.Add()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["intime"].Value = row["intime"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["date"].Value = row["date"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["outtime"].Value = row["outtime"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["totald"].Value = row["totald"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["incal"].Value = row["cal_in"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["outcal"].Value = row["cal_out"].ToString()));
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["tdcal"].Value = row["cal_hours"].ToString()));

                        if (String.Compare(dataGridView2.Rows[i].Cells["tdcal"].Value.ToString(), "00:00") != 0)
                        {
                            Invoke(new Action(() => dataGridView2.Rows[i].Cells["Leave"].Value = "Present"));
                        }
                        else
                            Invoke(new Action(() => dataGridView2.Rows[i].Cells["Leave"].Value = "Absent"));

                        ii += Convert.ToInt32(row["cal_hours"].ToString().Split(':')[0]);
                        jj += Convert.ToInt32(row["cal_hours"].ToString().Split(':')[1]);
                        i++;

                    }

                }


                else
                {
                    ii = 0;
                    jj = 0;
                }

                

                
                while (jj > 59)
                {
                    jj -= 60;
                    ii += 1;
                }

                attend_day = 0; int attend_set = 0;


                Invoke(new Action(() => dataGridView2.Rows.Add()));

                Invoke(new Action(() => dataGridView2.Rows[i].Cells["intime"].Value = string.Empty));
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["date"].Value = "Total Hours :"));
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["outtime"].Value = string.Empty));
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["totald"].Value = string.Empty));
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["incal"].Value = string.Empty));
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["outcal"].Value = string.Empty));
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["tdcal"].Value = ii.ToString() + ":" + jj.ToString()));
                Invoke(new Action(() => dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Brown));
                Invoke(new Action(() => dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White));
                i = 0;

                while (ii > 8)
                {
                    attend_day += 1;
                    ii -= 9;
                    attend_set = 1;
                }

                if (attend_set == 1)
                {
                    Invoke(new Action(() => label105.Text = attend_day + " Day/s & " + ii.ToString() + ":" + jj.ToString() + " Hours"));
                    Invoke(new Action(() => label105.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label105.Text = attend_day + " Day/s & " + ii.ToString() + ":" + jj.ToString() + " Hours"));
                    Invoke(new Action(() => label105.Enabled = true));
                }

                textappend("Attendance Calculated as :" + label105.Text + Environment.NewLine, Color.Brown);
                textappend(Environment.NewLine, Color.Black);  
                label105.ForeColor = Color.Brown;

                textappend("Calculating number of Leave Taken" + Environment.NewLine, Color.Blue);
                textappend(Environment.NewLine, Color.Black);  

                string temp_month;

                if (dateTimePicker2.Value.Month < 9)
                    temp_month = "0" + dateTimePicker2.Value.Month.ToString();
                else
                    temp_month = dateTimePicker2.Value.Month.ToString();

                
                
                querry = "Select * from emp_leave where emp_number = " + label62.Text + " and from_date between '" + dateTimePicker2.Value.Year + "-" + temp_month + "-01' AND '" + dateTimePicker2.Value.Year + "-" + temp_month + "-31'";

                
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
                leave_days = leave_hours = leave_mins = 0;
                
                List<string> leave_dates = new List<string>();
                
                List<string> leave_type = new List<string>();
                if (dthol.Rows.Count > 0)
                {
                    
                    foreach (DataRow row in dthol.Rows)
                    {
                        if (string.Compare(row["admin_approval"].ToString().TrimEnd(), "Approved") == 0
                            || string.Compare(row["admin_approval"].ToString().Trim(), "Approved & Cancelation Pending") == 0)
                        {
                            leave_hours += Convert.ToInt32(row["no_days"].ToString().Split(':')[0]);
                            leave_mins += Convert.ToInt32(row["no_days"].ToString().Split(':')[1]);
                            leave_type.Add(row["half_day"].ToString());
                            leave_dates.Add(row["from_date"].ToString().Split('-')[2]);
                             if(!string.IsNullOrEmpty(row["till_date"].ToString().Trim()))
                                 leave_dates.Add(row["till_date"].ToString().Split('-')[2]);
                             else
                                 leave_dates.Add("NA");
                             
                        }
                     
                    }

                    string[] from_till_dates = leave_dates.ToArray();
                    string[] leave_types = leave_type.ToArray();
                    int from;
                    int typ = 0;
                    for (int counter = 0; counter < from_till_dates.Length;counter++ )
                    {
                        from = Convert.ToInt32(from_till_dates[counter]);
                       
                        if (string.Compare(from_till_dates[counter+1],"NA")!=0)
                        {
                            int till = Convert.ToInt32(from_till_dates[counter + 1]);

                            for (int row = 0; row < dataGridView2.Rows.Count - 2; row++)
                            {
                                int temp_date = Convert.ToInt32(dataGridView2.Rows[row].Cells["date"].Value.ToString().Split('-')[2]);
                                if (temp_date >= from && temp_date <= till)
                                {                                   
                                    Invoke(new Action(() => dataGridView2.Rows[row].DefaultCellStyle.BackColor = Color.Orange));
                                    Invoke(new Action(() => dataGridView2.Rows[row].Cells["Leave"].Value = "Leave - " + leave_type[typ]));
                                }
                            }
                            
                        }
                        else
                        {
                            for (int row = 0; row < dataGridView2.Rows.Count - 2; row++)
                            {
                                
                                int temp_date = Convert.ToInt32(dataGridView2.Rows[row].Cells["date"].Value.ToString().Split('-')[2]);
                                if (temp_date == from)
                                {
                                    Invoke(new Action(() => dataGridView2.Rows[row].DefaultCellStyle.BackColor = Color.Orange));
                                    Invoke(new Action(() => dataGridView2.Rows[row].Cells["Leave"].Value = "Leave - " + leave_type[typ]));
                                    break;
                                }
                            }
                        }
                        counter++;
                        typ++;
                    }



                    
                    while (leave_mins > 59)
                    {
                        leave_hours += 1;
                        leave_mins -= 60;
                    }
                    while (leave_hours > 8)
                    {
                        leave_days += 1;
                        leave_hours -= 9;
                        set = 1;
                    }
                    if (set == 1)
                    {
                        Invoke(new Action(() => label88.Text = leave_days + " Day/s & " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"));
                    }
                    else
                    {
                        Invoke(new Action(() => label88.Text = leave_days + " Day/s & " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"));
                    }

                    Invoke(new Action(() => label88.Enabled = true));

                    textappend("Leave Taken Calculated as :  "+leave_days + " Day/s & " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"+Environment.NewLine,Color.Blue);
                    textappend(Environment.NewLine, Color.Black);  
                    label88.ForeColor = Color.Blue;
                }

                else
                {
                    Invoke(new Action(() => label88.Text = "No Leave Taken !!!"));
                    
                    textappend("No Leave Taken !!!"+Environment.NewLine,Color.Blue);
                    label105.ForeColor = Color.Blue;
                }

                textappend("Checking the number of holidays this month." + Environment.NewLine,Color.Red);
                textappend(Environment.NewLine, Color.Black);  
                querry = "Select * from holiday where month = " + dateTimePicker2.Value.Month + " AND year =  " + dateTimePicker2.Value.Year + " Order by day";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
                int holiday_hours = 0, holiday_mins = 0;

                if (dthol.Rows.Count > 0)
                {
                    foreach (DataRow row in dthol.Rows)
                    {
                        holiday_hours += Convert.ToInt32(row["type"].ToString().Split(':')[0]);
                        holiday_mins += Convert.ToInt32(row["type"].ToString().Split(':')[1]);
                    }
                }

                int holiday_day = 0;

                int count_leave = 0;
                int chk_date = 0;
                for (int row = 0; row < dataGridView2.Rows.Count - 1; row++)
                {
                    foreach (DataRow row1 in dthol.Rows)
                    {
                        chk_date = Convert.ToInt16(row1["day"]);

                        string[] dgvdate = dataGridView2.Rows[row].Cells["date"].Value.ToString().Split('-');
                        int dgv_day = Convert.ToInt32(dgvdate[2]);
                        if (dgv_day == chk_date)
                        {

                            Invoke(new Action(() => dataGridView2.Rows[row].Cells["Holiday"].Value = row1["name"].ToString()));
                            Invoke(new Action(() => dataGridView2.Rows[row].DefaultCellStyle.BackColor = Color.Red));
                            Invoke(new Action(() => dataGridView2.Rows[row].DefaultCellStyle.ForeColor = Color.White));
                            break;
                        }
                        else
                        {
                            Invoke(new Action(() => dataGridView2.Rows[row].Cells["Holiday"].Value = "-NA-"));
                        }

                    }
                }


                int cut=0;
                for (int row = 0; row < dataGridView2.Rows.Count - 2;row++ )
                {
                    if (string.Compare(dataGridView2.Rows[row].Cells["Leave"].Value.ToString(),"Present")!=0)
                    {
                        int lvdate = Convert.ToInt32(dataGridView2.Rows[row].Cells["date"].Value.ToString().Split('-')[2]);
                        int lvmonth = dateTimePicker2.Value.Month;
                        int lvyear = dateTimePicker2.Value.Year;

                        DateTime datetime = new DateTime(lvyear,lvmonth,lvdate);
                        if(string.Compare(datetime.DayOfWeek.ToString(),"Saturday")==0)
                        {
                            if (string.Compare(dataGridView2.Rows[row].Cells["Holiday"].Value.ToString(), "-NA-") != 0)
                            {
                                int temp_row = row-1;
                                if ((row - 1) >= 0)
                                {
                                    if (string.Compare(dataGridView2.Rows[row - 1].Cells["Leave"].Value.ToString(), "Present") != 0)
                                    {
                                        if (string.Compare(dataGridView2.Rows[row + 2].Cells["Leave"].Value.ToString(), "Present") != 0)
                                        {
                                            cut += 1;
                                        }
                                    }
                                }
                                else
                                {
                                    querry = "Select * from attendance where emp_number = " + label62.Text + " and date= '"+format_date.previous_day(dateTimePicker2)+"'";
                                    cmnd = new MySqlCommand(querry, con);
                                    da = new MySqlDataAdapter(cmnd);

                                    DataTable temp = new DataTable();
                                    con.Open();
                                    da.Fill(temp);
                                    con.Close();

                                    if(temp.Rows.Count>0)
                                    {
                                        if(string.Compare(temp.Rows[0]["wd"].ToString(), "00:00") == 0);
                                        {
                                            if (string.Compare(dataGridView2.Rows[row + 2].Cells["Leave"].Value.ToString(), "Present") != 0)
                                            {
                                                cut += 1;
                                            }
                                        }
                                    }                                 
                                }
                            }
                            else if ((row + 3) <= dataGridView2.Rows.Count - 1)
                            {
                                if (string.Compare(dataGridView2.Rows[row + 2].Cells["Leave"].Value.ToString(), "Present") != 0)
                                {
                                    cut += 1;
                                    row += 2;
                                }
                            }
                        }

                    }
                    
                }



                if (dataGridView2.Rows.Count > 0)
                {
                    int start_date_row = 0, end_date_row = 0;
                    int rows = 0, set = 0;
                    while (rows < dataGridView2.Rows.Count)
                    {
                        if (string.IsNullOrEmpty(dataGridView2.Rows[rows].Cells["outtime"].Value.ToString()))
                        {
                            start_date_row = rows;
                        }
                        else
                            break;
                        rows += 1;
                    }
                    rows = dataGridView2.Rows.Count - 1;
                    while (rows >= 0)
                    {
                        if (!string.IsNullOrEmpty(dataGridView2.Rows[rows].Cells["outtime"].Value.ToString()))
                        {
                            end_date_row = rows;
                            break;
                        }
                        rows -= 1;

                    }

                    //if (dataGridView2.Rows.Count > 1)
                    //{
                    //    string start_date = dataGridView2.Rows[start_date_row].Cells["date"].Value.ToString();
                    //    string end_date = dataGridView2.Rows[end_date_row].Cells["date"].Value.ToString();
                    //    DataRow[] leave = dthol.Select("date >= #" + start_date + "# AND date <= #" + end_date + "#");
                    //    count_leave = leave.Length;
                    //}

                    for (int zz = 0; zz < dataGridView2.Rows.Count-1;zz++ )
                    {
                        if (string.Compare(dataGridView2.Rows[zz].Cells["holiday"].Value.ToString(), "-NA-") != 0)
                        {
                            count_leave += 1;
                        }

                    }

                        //if (start_date_row <= 10)
                        //{
                        //    for (int left_holiday = 0; left_holiday < start_date_row; left_holiday++)
                        //    {
                        //        if (string.Compare(dataGridView2.Rows[left_holiday].Cells["holiday"].Value.ToString(), "Sunday") != 0
                        //            && string.Compare(dataGridView2.Rows[left_holiday].Cells["holiday"].Value.ToString(), "-NA-") != 0)
                        //        {
                        //            count_leave += 1;
                        //        }
                        //    }
                        //}
                    
                    string[] splitdate = dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells["date"].Value.ToString().Split('-');
                    int lvdate = Convert.ToInt32(splitdate[2]);
                    int lvmonth = Convert.ToInt32(splitdate[1]);
                    int lvyear = Convert.ToInt32(splitdate[0]);

                    DateTime datetime = new DateTime(lvyear, lvmonth, lvdate);

                    if ((string.Compare(datetime.DayOfWeek.ToString(), "Sunday") == 0))
                    {
                        querry = "select * from attendance where emp_number = " + label62.Text + " and day = " + datetime.AddDays(1).Day.ToString() + " and month = " + datetime.AddDays(1).Month.ToString() + " and year =" + datetime.AddDays(1).Year.ToString();

                        cmnd = new MySqlCommand(querry, con);
                        MySqlDataAdapter chk_da = new MySqlDataAdapter(cmnd);
                        DataTable attend_chk = new DataTable();
                        con.Open();
                        chk_da.Fill(attend_chk);
                        con.Close();
                        if (attend_chk.Rows.Count > 0)
                        {
                            if (string.Compare(attend_chk.Rows[0]["wd"].ToString(), "00:00") == 0
                                && string.Compare(dataGridView2.Rows[dataGridView2.Rows.Count - 3].Cells["totald"].Value.ToString(), "00:00") == 0)
                            {
                                cut += 1;
                            }
                        }


                    }

                    if (MessageBox.Show("Is this a new employee ? (Yes / No)", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (; start_date_row >= 0; start_date_row--)
                        {
                            if (string.Compare(dataGridView2.Rows[start_date_row].Cells["holiday"].Value.ToString(), "-NA-") != 0)
                            {
                                cut += 1;
                            }
                        }
                    }


                }

                    Invoke(new Action(() => label69.Text = cut.ToString()));
                    Invoke(new Action(() => label69.Enabled = true));
                while (holiday_mins > 59)
                {
                    holiday_hours += 1;
                    holiday_mins -= 60;
                }

                int holiday_temphour = holiday_hours;
                int holiday_tempmins = holiday_mins;

                while (holiday_temphour > 8)
                {
                    holiday_day += 1;
                    holiday_temphour -= 9;
                }

                Invoke(new Action(() => label111.Text = holiday_day + " Day/s  " + holiday_temphour.ToString() + ":" + holiday_tempmins.ToString() + " Hours"));
                Invoke(new Action(() => label111.Enabled = true));
                
                label111.ForeColor = Color.Red;
                textappend("Holidays :" + label111.Text + Environment.NewLine, Color.Red);
                textappend(Environment.NewLine, Color.Black);  

                textappend("Calculating number of days this month ?" + Environment.NewLine, Color.RoyalBlue);
                textappend(Environment.NewLine, Color.Black);  
                int total_hours = 0;
                if (dateTimePicker2.Value.Month == 2)
                {
                    if ((dateTimePicker2.Value.Year % 4 == 0) && (dateTimePicker2.Value.Year % 100 != 0) || dateTimePicker2.Value.Year % 400 == 0)
                    {
                        total_hours = (29 - holiday_day) * 9;
                        Invoke(new Action(() => label112.Text = " 29 Days"));
                        Invoke(new Action(() => label112.Enabled = true));
                    }
                    else
                    {
                        total_hours = (28 - holiday_day) * 9;
                        Invoke(new Action(() => label112.Text = " 28 Days"));
                        Invoke(new Action(() => label112.Enabled = true));
                    }
                }
                else
                {
                    total_hours = (month[dateTimePicker2.Value.Month - 1] - holiday_day) * 9;
                    Invoke(new Action(() => label112.Text = month[dateTimePicker2.Value.Month - 1].ToString() + " Days"));
                    Invoke(new Action(() => label112.Enabled = true));
                }

                label112.ForeColor = Color.RoyalBlue;
                textappend("Number of days this month :" + Environment.NewLine, Color.RoyalBlue);
                textappend(Environment.NewLine, Color.Black);  


                textappend("Calculating number of days to attend ?" + Environment.NewLine, Color.DarkRed);
                textappend(Environment.NewLine, Color.Black);  
                int expected_hours = total_hours;

                int expected_days = 0;

                while (expected_hours > 8)
                {
                    expected_days += 1;
                    expected_hours -= 9;
                }



                if (holiday_temphour > 00)
                {
                    expected_days -= 1;
                    Invoke(new Action(() => label108.Text = expected_days + " Day/s & " + holiday_temphour.ToString() + ":" + holiday_tempmins.ToString() + " Hours"));
                    Invoke(new Action(() => label108.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label108.Text = expected_days + " Day/s & " + holiday_temphour.ToString() + ":" + holiday_tempmins.ToString() + " Hours"));
                    Invoke(new Action(() => label108.Enabled = true));
                }

                label108.ForeColor = Color.DarkRed;
                textappend("Total Days to attend :"+Environment.NewLine,Color.DarkRed);
                textappend(Environment.NewLine, Color.Black);  


                textappend("Calculating the number Accrued Leaves ?" + Environment.NewLine, Color.DarkMagenta);
                textappend(Environment.NewLine, Color.Black);  
                temp_expected_days = expected_days; temp_holiday_temphour = holiday_temphour; temp_holiday_tempmins = holiday_tempmins;


                querry = "Select * from leaves where emp_number = " + label62.Text;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
                left_days = left_hours = left_mins = 0;
                int leave_set = 0;
                if (dthol.Rows.Count > 0)
                {
                    foreach (DataRow row in dthol.Rows)
                    {
                        left_hours = Convert.ToInt32(row["emp_leaves"].ToString().Split(':')[0]);
                        left_mins = Convert.ToInt32(row["emp_leaves"].ToString().Split(':')[1]);
                    }
                }
                if (left_hours < 0)
                {
                    left_hours = Convert.ToInt32(left_hours.ToString().Split('-')[1]);
                    leave_set = 1;
                }

                


                while (left_hours > 8)
                {
                    left_days += 1;
                    left_hours -= 9;
                }

                if (leave_set == 0)
                {
                    Invoke(new Action(() => label89.Text = left_days + " Day/s  " + left_hours.ToString() + ":" + left_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label89.Text = left_days + " Day/s  " + left_hours.ToString() + ":" + left_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));
                }
                label89.ForeColor = Color.DarkMagenta;
                textappend("Number of Accrued Leaves :" + label89.Text + Environment.NewLine, Color.DarkMagenta);
                textappend(Environment.NewLine, Color.Black);  

                textappend("Checking if Leaves Credited this month ?", Color.DarkBlue);
                textappend(Environment.NewLine, Color.Black);  
                querry = "Select * from leave_log where emp_num = " + label62.Text + " and month = " + dateTimePicker2.Value.Month + " and year = " + dateTimePicker2.Value.Year + " and isleave = 'True' ";

                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
                int accrued_hours = 0, accrued_mins = 0, accrued_days = 0;
                if (dthol.Rows.Count > 0)
                {
                    foreach (DataRow row in dthol.Rows)
                    {
                        accrued_hours += Convert.ToInt32(row["credits"].ToString().Split(':')[0]);
                        accrued_mins += Convert.ToInt32(row["credits"].ToString().Split(':')[1]);
                    }
                }

                while (accrued_mins > 59)
                {
                    accrued_hours += 1;
                    accrued_mins -= 60;
                }

                while (accrued_hours > 8)
                {
                    accrued_days += 1;
                    accrued_hours -= 9;
                }

                Invoke(new Action(() => label114.Text = accrued_days + " Day/s & " + accrued_hours.ToString() + ":" + accrued_mins.ToString() + " Hours"));
                Invoke(new Action(() => label114.Enabled = true));

                label114.ForeColor = Color.CornflowerBlue;
                textappend("Credited Leave : " + label114.Text + Environment.NewLine, Color.DarkBlue);
                textappend(Environment.NewLine, Color.Black);  


                textappend("Calculating the days left to Attend ?" + Environment.NewLine, Color.DarkBlue);
                textappend(Environment.NewLine, Color.Black);  
                if (holiday_tempmins > jj)
                {
                    dif_min = holiday_tempmins - jj;
                }
                else
                {
                    holiday_tempmins += 60;
                    dif_min = holiday_tempmins - jj;

                    if (holiday_temphour > 0)
                        holiday_temphour -= 1;
                    else
                    {
                        expected_days -= 1;
                        holiday_temphour += 8;
                    }

                }
                if (holiday_temphour > ii)
                    dif_hour = holiday_temphour - ii;
                else
                {
                    holiday_temphour += 9;
                    dif_hour = holiday_temphour - ii;
                    expected_days -= 1;
                }

                dif_day = expected_days - attend_day;

                if (dif_day >= 0 )
                {


                    Invoke(new Action(() => label116.Text = dif_day + " Day/s & " + dif_hour.ToString() + ":" + dif_min.ToString() + " Hours"));
                    Invoke(new Action(() => label116.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label116.Text = 0 + " Day/s & " + 0 + ":" + 0 + " Hours"));
                    Invoke(new Action(() => label116.Enabled = true));
                }

                label116.ForeColor = Color.DarkGreen;
                textappend("Days left to Attend : " + label116.Text + Environment.NewLine, Color.DarkGreen);

                textappend(Environment.NewLine, Color.Black);  

                textappend("Checking Salary per month ?"+Environment.NewLine,Color.DarkOrange);
                textappend(Environment.NewLine, Color.Black);  
                querry = "Select salary,pt from empdetails where emp_number = " + label62.Text ;
                DataTable salary = new DataTable();
                
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                salary.Clear();
                con.Open();
                da.Fill(salary);
                con.Close();
                cal_salary = 0;
                if (salary.Rows.Count > 0)
                {
                    foreach (DataRow row in salary.Rows)
                    {
                        cal_salary = Convert.ToInt32(row["salary"].ToString());
                        if (cal_salary > 0)
                        {
                            Invoke(new Action(() => label18.Text = "Rs. " + row["salary"].ToString()));
                            pt = Convert.ToInt32(row["pt"].ToString());

                            Invoke(new Action(() => label58.Text = pt.ToString()));

                            Invoke(new Action(() => label58.Enabled = true));

                            Invoke(new Action(() => label18.Enabled = true));
                        }

                        else
                        {
                            Invoke(new Action(() => label18.Text = "No Salary Information. Please Update bank and salary details."));
                            Invoke(new Action(() => label18.Enabled = true));
                            Invoke(new Action(() => label58.Enabled = true));
                        }
                    }
                }

                label18.ForeColor = label58.ForeColor = Color.DarkOrange;
                textappend("Salary per month : "+label18.Text+ "and PT :"+label58.Text + Environment.NewLine, Color.DarkOrange);
                textappend(Environment.NewLine, Color.Black);  

                textappend("Calculating Salary as per attended days" + Environment.NewLine, Color.SeaGreen);
                textappend(Environment.NewLine, Color.Black);  
                if (cal_salary > 0 )
                {
                    float total_days = float.Parse(label112.Text.Split(' ')[0]);
                    total_days = total_days * 9;



                    per_hour = (cal_salary / total_days);
                    per_day = (cal_salary / (total_days / 9));
                    per_min = per_hour / 60;
                    pay_day = pay_hour = 0;

                     pay_hour = 0;
                     pay_min = 0;

                     if (attend_day < temp_expected_days)
                     {
                         if (ii > 0)
                         {
                             float temp = per_hour * ii;
                             pay_hour = Convert.ToInt32(temp);
                         }


                         pay_min = Convert.ToInt32(jj * per_min);
                         pay_day = Convert.ToInt32(per_day * attend_day);
                         extra = (count_leave - cut) * per_day;

                         calculated_salary = pay_day + pay_hour+pay_min;
                         Invoke(new Action(() => label21.Text = "Rs. " + calculated_salary.ToString()));
                         Invoke(new Action(() => label21.Enabled = true));

                         label21.ForeColor = Color.SeaGreen;
                         textappend("Calculated Salary :" + label21.Text+Environment.NewLine, Color.SeaGreen);
                         textappend(Environment.NewLine, Color.Black);  

                         Invoke(new Action(() => label56.Text = "Rs. " + (calculated_salary + extra).ToString() + " (Calculated Salary + " + extra + " )"));
                         Invoke(new Action(() => label56.Enabled = true));

                         label56.ForeColor = Color.RosyBrown;
                         textappend("Gross Salary :" + label56.Text+Environment.NewLine, Color.RosyBrown);
                         textappend(Environment.NewLine, Color.Black);  

                         net = (calculated_salary + extra) - pt;

                     }
                     else
                     {
                         calculated_salary = per_day * temp_expected_days;
                         extra = (count_leave - cut) * per_day;
                         Invoke(new Action(() => label21.Text = "Rs. " + calculated_salary.ToString()));
                         Invoke(new Action(() => label21.Enabled = true));

                         label21.ForeColor = Color.SeaGreen;
                         textappend("Calculated Salary :" + label21.Text + Environment.NewLine, Color.SeaGreen);
                         textappend(Environment.NewLine, Color.Black);  

                         Invoke(new Action(() => label56.Text = "Rs. " + (calculated_salary + extra).ToString() + " (Full Salary)"));
                         Invoke(new Action(() => label56.Enabled = true));

                         label56.ForeColor = Color.RosyBrown;
                         textappend("Gross Salary :" + label56.Text + Environment.NewLine, Color.RosyBrown);
                         textappend(Environment.NewLine, Color.Black);

                         net = (calculated_salary + extra) - pt;
                     }


                     label60.ForeColor = Color.Navy;
                     textappend("Net Salary :" + net + Environment.NewLine, Color.Navy);
                     textappend(Environment.NewLine, Color.Black);  
                    Invoke(new Action(() => label60.Text = "Rs. "+ net.ToString()));
                    Invoke(new Action(() => label60.Enabled = true));                    
                }
                Invoke(new Action(() => linkLabel10.Enabled = true));

                Invoke(new Action (()=> button7.Enabled = true));

                Invoke(new Action(() => adjst_leave()));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Invoke(new Action(() => linkLabel10.Enabled = true));
            }
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            textBox1.Text = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month;
        }

        private void listBox5_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable salary = new DataTable();

                querry = "select * from emp_calsal where emp_number = " + label62.Text + " and salmon_yr  = '" + format_date.dateformat(dateTimePicker2.Value.Month.ToString(), dateTimePicker2.Value.Year.ToString()) + "'";

                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                DataTable temp = new DataTable();
                con.Open();

                da.Fill(temp);
                con.Close();
                if (temp.Rows.Count <= 0)
                {
                querry = "Insert into leave_log (emp_num,date,assign_by,month,year,adjust,leave_adjust,isleave) values (@emp_num,date_format(CURDATE(),'%d-%m-%Y'),@assign_by,@month,@yr,@adjust,@leave_adjust,'False')";
                
                textappend("Adjustment of left over Paid Leaves is being Updated..." + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);
                cmnd = new MySqlCommand(querry, con);
                cmnd.Parameters.AddWithValue("@emp_num", label62.Text);
                cmnd.Parameters.AddWithValue("@assign_by", Form1.empname);
                cmnd.Parameters.AddWithValue("@month", dateTimePicker2.Value.Month);
                cmnd.Parameters.AddWithValue("@yr", dateTimePicker2.Value.Year);
                cmnd.Parameters.AddWithValue("@adjust", 1);

                newleft_hours = newleft_hours + (newleft_days * 9);

                cmnd.Parameters.AddWithValue("@leave_adjust", newleft_hours + ":" + newleft_mins);
                con.Open();
                int insert = cmnd.ExecuteNonQuery();
                con.Close();
                if (insert > 0)
                    MessageBox.Show("The updation of Leave Adjustment has been reflected Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textappend("Adjustment of left over Paid Leaves Updated back to the Employee Emp.ID : " + label62.Text + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);
                


                
                    querry = "Insert into emp_calsal (emp_number,salary,cal_salary,salmon_yr,sal_calculation) values (@emp_num,@sal,@cal,@salmon_yr,@calculation)";

                    salary.Clear();

                    cmnd = new MySqlCommand(querry, con);
                    cmnd.Parameters.AddWithValue("@emp_num", label62.Text);
                    cmnd.Parameters.AddWithValue("@sal", label18.Text.Split('.')[1].TrimStart());
                    cmnd.Parameters.AddWithValue("@cal", label60.Text.Split(' ')[1].TrimStart());
                    cmnd.Parameters.AddWithValue("@calculation", richTextBox3.Text);
                    cmnd.Parameters.AddWithValue("@salmon_yr", format_date.dateformat(dateTimePicker2.Value.Month.ToString(), dateTimePicker2.Value.Year.ToString()));
                    con.Open();
                    insert = cmnd.ExecuteNonQuery();
                    con.Close();
                    if (insert > 0)
                        MessageBox.Show("Calculated Salary Updated !!!");
                    else
                        MessageBox.Show("Couldn't Update due to server problem !!!. Please try after some time.");
                }
                else
                {
                    MessageBox.Show("The salary for this month is already updated for this employee."+Environment.NewLine+" It can not be updated again."+Environment.NewLine+Environment.NewLine+"Contact to Administrator to make changes.","Salary Updation Not Done !!!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }

                

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                con.Close();
            }
        }

        private void adjst_leave()
        {
            try
            {
                button7.Enabled = false;

                float remaining_salary = 0;
                float amount = 0;
                 newleft_mins = temp_leavemins = temp_acruedmins = newleft_hours =newleft_days =0;
                textappend("Calculating the adjustment of Leave Taken and Paid Leaves left...." + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);



                temp_leavemins = (leave_days * 9 * 60) + (leave_hours * 60) + leave_mins;

                temp_acruedmins = (left_days * 9 * 60) + (left_hours * 60) + left_mins;

                newleft_mins = temp_acruedmins - temp_leavemins;

                if (newleft_mins >= 0)
                {

                    while (newleft_mins > 59)
                    {
                        newleft_hours += 1;
                        newleft_mins -= 60;
                    }


                    while (newleft_hours > 8)
                    {
                        newleft_days += 1;
                        newleft_hours -= 9;
                    }


                    amount = (leave_days * per_day) + (leave_hours * per_hour) + (leave_mins * per_min);



                    Invoke(new Action(() => label89.Text = newleft_days + " Day/s & " + newleft_hours.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));

                }
                else
                {

                    newleft_mins = newleft_hours = 0;
                    amount = (left_days * per_day) + (left_hours * per_hour);

                    if (left_mins >= 30)
                        amount = amount + per_hour;

                    Invoke(new Action(() => label89.Text = newleft_days + " Day/s & " + newleft_hours.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));
                }

                textappend("Paid Leave after adjustment :" + label89.Text + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);
                textappend("Extra Amount Calculated : " + amount + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);
                gross = (calculated_salary + extra + amount);


                if (cal_salary >= gross)
                {
                    Invoke(new Action(() => label56.Text = "Rs. " + gross.ToString() + " (Calculated Salary + " + extra + " + Paid Leaves)"));
                    Invoke(new Action(() => label56.Enabled = true));
                    textappend("After adjustment the Gross Salary :" + label56.Text + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    net = net + amount;
                    textappend("After adjustment the final Net Salary :" + net + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label60.Text = "Rs. " + net.ToString()));
                    Invoke(new Action(() => label60.Enabled = true));
                }

                else
                {
                    textappend("The left over Paid leavs are being calculated" + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    remaining_salary = (gross - cal_salary);


                    float save_remaining_salary = amount - remaining_salary;

                    int temp_hour = 0, temp_mins = 0;
                    while (remaining_salary >= per_hour)
                    {
                        temp_hour += 1;
                        remaining_salary -= per_hour;
                    }
                    if (remaining_salary >= per_min)
                    {
                        temp_mins += 1;
                        remaining_salary -= per_min;
                    }

                    newleft_hours += temp_hour;
                    newleft_mins += temp_mins;


                    while (newleft_mins > 59)
                    {
                        newleft_hours += 1;
                        newleft_mins -= 60;
                    }



                    while (newleft_hours > 8)
                    {
                        newleft_days += 1;
                        newleft_hours -= 9;
                    }

                    Invoke(new Action(() => label89.Text = newleft_days + " Day/s & " + newleft_hours.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));


                    textappend("Left over Paid leaves are ready to save." + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);

                    gross = calculated_salary + extra + save_remaining_salary;
                    Invoke(new Action(() => label56.Text = "Rs. " + Convert.ToInt32(gross).ToString() + " (Calculated Salary + " + extra + " Paid Leaves)"));
                    textappend("After adjustment the new Gross Salary : " + label56.Text + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label56.Enabled = true));

                    net = net + save_remaining_salary;
                    textappend("After adjustment the new Net Salary : " + Convert.ToInt32(net).ToString() + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label60.Text = "Rs. " + net.ToString()));
                    Invoke(new Action(() => label60.Enabled = true));
                    textappend("The left over Paid Leaves is :" + newleft_hours + ":" + newleft_mins + " Hours" + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                }



                if (gross < cal_salary && (newleft_hours > 0 || newleft_mins > 0))
                {
                    if (MessageBox.Show("The Employee is still liable for the benefit of the Paid Leaves which is left over with him.\r\n" + Environment.NewLine + "Would you like to adjust them as well ? ", "Paid Leave Adjustment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        textappend("Further adjustment of Paid Leave as Calculated Salary is less than Current Salary ..." + Environment.NewLine, Color.Black);
                        float per_min = per_hour / 60;
                        int temp_total_mins = (newleft_hours * 60) + newleft_mins;

                        while (gross < cal_salary && temp_total_mins > 0)
                        {
                            gross = gross + (per_min);
                            temp_total_mins -= 1;
                        }

                        newleft_hours = 0;
                        newleft_days = 0;
                        if (temp_total_mins > 0)
                        {
                            while (temp_total_mins > 59)
                            {
                                newleft_hours += 1;
                                temp_total_mins -= 60;
                            }

                            while (newleft_hours > 8)
                            {
                                newleft_days += 1;
                                newleft_hours -= 9;
                            }
                            gross = cal_salary;
                            net = gross - pt;
                            newleft_mins = temp_total_mins;
                        }

                        else
                        {
                            newleft_mins = 0;

                            net = gross - pt;
                        }
                        textappend(Environment.NewLine, Color.Black);
                        textappend("Further adjustment of Paid Leaves is done." + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        textappend("The final Calculated Gross Salary : " + gross + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        textappend("The final Calculated Net Salary : " + net + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        textappend("The Left Over Paid Leaves : " + newleft_hours + ":" + newleft_mins + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);

                        Invoke(new Action(() => label56.Text = "Rs. " + Convert.ToInt32(gross).ToString() + " (Calculated Salary + " + extra + " Paid Leaves)"));
                        textappend("After adjustment the new Gross Salary : " + label56.Text + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        Invoke(new Action(() => label56.Enabled = true));

                        textappend("After adjustment the new Net Salary : " + Convert.ToInt32(net).ToString() + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        Invoke(new Action(() => label60.Text = "Rs. " + net.ToString()));
                        Invoke(new Action(() => label60.Enabled = true));

                        Invoke(new Action(() => label89.Text = newleft_days + " Day/s  " + newleft_hours.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                        Invoke(new Action(() => label89.Enabled = true));


                        textappend("The left over Paid Leaves are :" + newleft_days + " Day/s & " + newleft_hours + ":" + newleft_mins + " Hours" + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                    }

                }


               

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }



        public void textappend(string msg,Color c)
        {
            try
            {
                int length = 0;
             Invoke(new Action(()=>   length = richTextBox3.TextLength));
               Invoke(new Action(()=> richTextBox3.AppendText(msg)));
                Invoke(new Action(()=> richTextBox3.SelectionStart = length));
                Invoke(new Action(()=> richTextBox3.SelectionLength = msg.Length));
               Invoke(new Action(()=>  richTextBox3.SelectionColor = c));

            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }


       
    }
}