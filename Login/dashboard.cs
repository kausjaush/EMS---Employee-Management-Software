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
    public partial class dashboard : Form
    {
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dthol = new DataTable();
        byte[] doc, emedoc, meddoc;
        string querry;
        int[] submit = new int[50];
        string emp;
        int sub;


        int pay_day = 0;
        int pay_hour = 0;
        int attend_day = 0;
        float per_hour = 0, per_day = 0, extra = 0, net = 0, calculated_salary = 0, cal_salary = 0, gross = 0;
        int leave_hours = 0, leave_mins = 0, leave_days = 0, set = 0;
        int temp_expected_days = 0, temp_holiday_temphour = 0, temp_holiday_tempmins = 0, pt = 0;
        int dif_day = 0, dif_hour = 0, dif_min = 0;
        int left_hours = 0, left_mins = 0, left_days = 0; 

        DataTable dtbank = new DataTable();

        public dashboard()
        {
            InitializeComponent();
            timer1.Tick+=timer1_Tick;
            timer1.Start();
            linkLabel12.Enabled = false;
            emp = Form1.empno;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                try
                {

                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;
                    querry = "Select * from edudetails where emp_number = " + label133.Text;
                    cmnd = new MySqlCommand(querry, con);

                    da = new MySqlDataAdapter(cmnd);
                    dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        string data = dt.Rows[0]["score"].ToString();
                        string[] scores = data.Split('#');

                        string per = null;
                        string sn = null;
                        string yr = null;
                        string med = null;
                        seperate(scores[0], per, sn, yr, med);

                        label62.Text = scores[0].Split(',')[1];
                        label66.Text = scores[0].Split(',')[2];
                        label65.Text = scores[0].Split(',')[3];
                        label64.Text = scores[0].Split(',')[4];

                        seperate(scores[1], per, sn, yr, med);

                        label52.Text = scores[1].Split(',')[1];
                        label56.Text = scores[1].Split(',')[2];
                        label55.Text = scores[1].Split(',')[3];
                        label54.Text = scores[1].Split(',')[4];

                        seperate(scores[2], per, sn, yr, med);

                        label53.Text = scores[2].Split(',')[1];
                        label71.Text = scores[2].Split(',')[2];
                        label63.Text = scores[2].Split(',')[3];
                        label61.Text = scores[2].Split(',')[4];

                        seperate(scores[3], per, sn, yr, med);

                        label96.Text = scores[3].Split(',')[1];
                        label99.Text = scores[3].Split(',')[2];
                        label98.Text = scores[3].Split(',')[3];
                        label97.Text = scores[3].Split(',')[4];

                        richTextBox1.Text = scores[4].Split(',')[1];

                    }
                    else
                        MessageBox.Show("No Record Found !!!");


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

        private void seperate(string input, string per, string sn, string yr, string medium)
        {
            try
            {
                string[] output = input.Split(',');
                per = output[1];
                sn = output[2];
                yr = output[3];
                medium = output[4];
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from empdetails where emp_number = " + label133.Text;
                cmnd = new MySqlCommand(querry, con);

                da = new MySqlDataAdapter(cmnd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        textBox1.Text = row["emp_number"].ToString();
                        textBox20.Text = row["status"].ToString();
                        label15.Text = row["doj"].ToString();
                        label25.Text = row["leave_date"].ToString();
                        textBox2.Text = row["emp_fname"].ToString();
                        textBox3.Text = row["emp_mname"].ToString();
                        textBox4.Text = row["emp_lname"].ToString();
                        textBox21.Text = row["dob"].ToString();
                        textBox22.Text = row["gender"].ToString();
                        textBox5.Text = row["pancard"].ToString();
                        textBox18.Text = row["passport"].ToString();
                        textBox17.Text = row["job_title1"].ToString();
                        textBox19.Text = row["job_title2"].ToString();
                        textBox6.Text = row["department"].ToString();
                        textBox7.Text = row["cur_adds"].ToString();
                        textBox8.Text = row["per_adds"].ToString();
                        textBox9.Text = row["mobile"].ToString();
                        textBox10.Text = row["hmobile"].ToString();
                        textBox11.Text = row["emailid"].ToString();
                        textBox12.Text = row["r_cur_adds"].ToString();
                        textBox13.Text = row["r_per_adds"].ToString();
                        textBox14.Text = row["r_mobile"].ToString();
                        textBox15.Text = row["r_hmobile"].ToString();
                        textBox16.Text = row["r_emailid"].ToString();
                    }
                }
                else
                    MessageBox.Show("No Record Found !!!");


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
                listBox1.Items.Clear();
                if (dtbank.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbank.Rows)
                    {
                        listBox1.Items.Add(dr["emp_number"].ToString() + " - " + dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString());
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                linkLabel12.Enabled = true;
                DataRow[] rows = dtbank.Select("emp_number = " + listBox1.SelectedItem.ToString().Split('-')[0]);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        
                        label132.Text = dr["emp_fname"].ToString() + " " + dr["emp_mname"].ToString() + " " + dr["emp_lname"].ToString();
                        Form1.empno = label133.Text = dr["emp_number"].ToString();                       
                    }
                    textBox1.Text =
                    textBox2.Text =
                    textBox3.Text =
                    textBox4.Text =
                    textBox5.Text =
                    textBox6.Text =
                    textBox7.Text =
                    textBox8.Text =
                    textBox9.Text =
                    textBox10.Text =
                    textBox11.Text =
                    textBox12.Text =
                    textBox13.Text =
                    textBox14.Text =
                    textBox15.Text =
                    textBox16.Text =
                    textBox17.Text =
                    textBox18.Text =
                    textBox19.Text =
                    textBox20.Text =
                    textBox21.Text =
                    textBox22.Text = 
                    richTextBox1.Text = string.Empty;

                    label52.Text =
                    label53.Text =
                    label54.Text =
                    label55.Text =
                    label56.Text =
                    label61.Text =
                    label62.Text =
                    label63.Text =
                    label64.Text =
                    label65.Text =
                    label66.Text =
                    label71.Text =
                    label96.Text =
                    label97.Text =
                    label98.Text =
                    label99.Text =
                    label15.Text =
                    label25.Text = "--------------";

                        
                }
                else
                {
                    label132.Text = "-----------";
                    label133.Text = "-----------";
                    MessageBox.Show("No record found with this Employee ID. Kinldy enter valid Employee ID.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(string.Compare(label132.Text,"--------------") ==0 || string.Compare(label133.Text,"--------------") ==0)
                {
                    tabControl1.Enabled = false;
                }
                else
                {
                    tabControl1.Enabled = true;
                }

                if (radioButton1.Checked == true)
                {
                    textBox24.Text = string.Empty;
                    dateTimePicker2.Enabled = false;
                }
                else
                {
                    dateTimePicker2.Enabled = true;
                }

                if (!string.IsNullOrEmpty(textBox25.Text) && !string.IsNullOrEmpty(textBox27.Text))
                    label83.Text = Convert.ToString((dateTimePicker5.Value - dateTimePicker3.Value).Days + 1);

                if (!string.IsNullOrEmpty(textBox28.Text) && !string.IsNullOrEmpty(textBox28.Text))
                {
                    linkLabel17.Enabled = true;
                }
                else
                {
                    linkLabel17.Enabled = false;
                }

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }


            
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ticket_history a = new ticket_history(label133.Text);
                a.ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                password a = new password(label133.Text);
                a.ShowDialog();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread tr = new Thread(() => getleaves());
                tr.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private void getleaves()
        {
            try
            {
                Invoke(new Action(() => linkLabel15.Enabled = false));
                sub = 0;
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;

                querry = "Select idleave,reason,from_date,till_date,date,time,admin_approval,admin_reason,admin_date,admin_time,doc_submit,half_day,cancel_id,cancel_status from emp_leave where emp_number = " + Form1.empno;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);

                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    Invoke(new Action(() => dataGridView1.Rows.Clear()));
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        Invoke(new Action(() => dataGridView1.Rows.Add()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["ticket_no"].Value = row["idleave"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["reason"].Value = row["reason"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["from"].Value = row["from_date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["till"].Value = row["till_date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["date"].Value = row["date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["time"].Value = row["time"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["admin_approval"].Value = row["admin_approval"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["admin_reason"].Value = row["admin_reason"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["admin_date"].Value = row["admin_date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["admin_time"].Value = row["admin_time"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["half"].Value = row["half_day"].ToString()));

                        if (!string.IsNullOrEmpty(row["doc_submit"].ToString()))
                            Invoke(new Action(() => dataGridView1.Rows[i].Cells["file"].Value = "Attached"));


                        if (string.Compare(row["cancel_id"].ToString(), '0'.ToString()) == 0
                           && string.Compare(row["admin_approval"].ToString(), "0") == 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow));
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black));

                        }

                        else if (string.Compare(row["cancel_id"].ToString(), '0'.ToString()) == 0
                           && string.Compare(row["admin_approval"].ToString(), "Approved") == 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen));
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black));
                        }
                        else if (string.Compare(row["cancel_id"].ToString(), '0'.ToString()) == 0
                            && string.Compare(row["admin_approval"].ToString(), "Not Approved") == 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red));
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));

                        }
                        else if (string.Compare(row["cancel_id"].ToString(), '0'.ToString()) != 0
                            && string.Compare(row["cancel_status"].ToString(), "Approved") != 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Brown));
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));

                        }
                        else if (string.Compare(row["cancel_id"].ToString(), '0'.ToString()) != 0
                            && string.Compare(row["cancel_status"].ToString(), "Approved") == 0)
                        {
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Blue));
                            Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));

                        }


                        i++;


                    }
                }
                else
                {
                   Invoke(new Action(() => dataGridView1.Rows.Clear()));
                    MessageBox.Show("No Leave Tickets found !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Invoke(new Action(() => linkLabel15.Enabled = true));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Invoke(new Action(() => linkLabel15.Enabled = true));
            }
        }

        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                Thread t = new Thread(() => doc = attach(linkLabel12));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                Thread t = new Thread(() => emedoc = attach(linkLabel13));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => meddoc = attach(linkLabel14));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] attach(LinkLabel l1)
        {
            byte[] temp = null;
            try
            {

                openFileDialog1.Multiselect = false;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Invoke(new Action(() => l1.Text = "Attaching..."));
                    var stream = new FileStream(Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName.ToString()), openFileDialog1.FileName.ToString()), FileMode.Open, FileAccess.Read);
                    var reader = new BinaryReader(stream);
                    temp = reader.ReadBytes((int)stream.Length);
                    Thread.Sleep(1000);
                    Invoke(new Action(() => l1.Text = "Copy Attached"));
                    return temp;
                }
                return temp;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return temp;
            }
        }


        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            textBox23.Text = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            textBox24.Text = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;
        }

        private void dateTimePicker4_CloseUp(object sender, EventArgs e)
        {
            textBox26.Text = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
        }

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            textBox25.Text = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            textBox27.Enabled = true;
            dateTimePicker5.Enabled = true;           
        }

        private void dateTimePicker5_CloseUp(object sender, EventArgs e)
        {
            textBox27.Text = dateTimePicker5.Value.Year + "-" + dateTimePicker5.Value.Month + "-" + dateTimePicker5.Value.Day;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string half = string.Empty;
                if (!string.IsNullOrEmpty(richTextBox3.Text) && !string.IsNullOrWhiteSpace(richTextBox3.Text))
                {
                    if (radioButton3.Checked == true)
                        half = radioButton3.Text;
                    else if (radioButton4.Checked == true)
                        half = radioButton4.Text;
                    if (!string.IsNullOrEmpty(half))
                    {
                        if (!string.IsNullOrEmpty(textBox26.Text))
                        {
                            con = new MySqlConnection();
                            con.ConnectionString = Form1.sqlstring;
                            string submit = string.Empty;
                            if (emedoc != null)
                                submit = "Submitted";
                            querry = "Insert into emp_leave (emp_number,reason,from_date,half_day,date,time,doc,doc_submit,no_days) values("
                                + Form1.empno + ",'"
                                + richTextBox3.Text + "','"
                                + textBox26.Text + "','"
                                + half
                                + "',CURDATE(),CURTIME(),@do,@doc_submit,@no_days)";
                            cmnd = new MySqlCommand(querry, con);
                            cmnd.Parameters.Add("@do", MySqlDbType.MediumBlob);
                            cmnd.Parameters["@do"].Value = emedoc;
                            cmnd.Parameters.Add("@doc_submit", MySqlDbType.VarChar).Value = submit;
                            cmnd.Parameters.Add("@no_days", MySqlDbType.VarChar).Value = "4:30";
                            con.Open();
                            if (cmnd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Your Leave Ticket has been submitted Successfully. Please check later for status!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox26.Text = richTextBox3.Text = string.Empty;
                                linkLabel13.Text = "Attach Copy";
                                radioButton3.Checked = radioButton4.Checked = false;
                            }
                            con.Close();
                        }
                        else
                            MessageBox.Show("Please select the date!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Please select when you wish to have half day!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Kindly provide the reason of taking leave !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(richTextBox4.Text) && !string.IsNullOrWhiteSpace(richTextBox4.Text))
                {
                    if (label83.Text.CompareTo("----------") != 0)
                    {
                        if (meddoc != null)
                        {
                            con = new MySqlConnection();
                            con.ConnectionString = Form1.sqlstring;
                            string submit = string.Empty;
                            if (meddoc != null)
                                submit = "Submitted";

                            querry = "Insert into emp_leave (emp_number,reason,from_date,till_date,date,time,doc,no_days,doc_submit,half_day) values(" + Form1.empno + ",'" + richTextBox4.Text + "','" + textBox25.Text + "','" + textBox27.Text + "',CURDATE(),CURTIME(),@do,@no_days,@doc_submit,'Full_Day')";
                            cmnd = new MySqlCommand(querry, con);
                            cmnd.Parameters.Add("@do", MySqlDbType.MediumBlob);
                            cmnd.Parameters["@do"].Value = meddoc;
                            cmnd.Parameters.Add("@no_days", MySqlDbType.VarChar).Value = Convert.ToInt32(label83.Text) * 9 + ":00";
                            cmnd.Parameters.Add("@doc_submit", MySqlDbType.VarChar).Value = submit;

                            con.Open();
                            if (cmnd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Your Leave Ticket has been submitted Successfully. Please check later for status!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                label83.Text = "----------";
                                textBox25.Text = textBox27.Text = richTextBox4.Text = string.Empty;
                                linkLabel14.Text = "Attach Copy";
                            }
                            con.Close();
                        }
                        else
                            MessageBox.Show("Kindly attach a proper medical document !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Kindly provide Date(From) & Date(To) !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Kindly provide the reason of taking leave !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel17_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                label93.Text = label121.Text = label119.Text = label108.Text = label137.Text = label91.Text = label87.Text = label112.Text = label108.Text = label111.Text = label114.Text = label105.Text = label116.Text = label89.Text = label118.Text = "-------------";
                Thread tr = new Thread(() => attendance());
                tr.Start();

            }
            catch (Exception e1)
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

                int[] hour = new int[200];
                int[] mins = new int[200];

                textappend("Getting Attendance..." + Environment.NewLine, Color.Red);
                textappend(Environment.NewLine, Color.Black);
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from attendance where emp_number = " + label133.Text + " and month = " + dateTimePicker6.Value.Month + " and year = " + dateTimePicker6.Value.Year + " Order by day ASC";
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
                        Invoke(new Action(() => dataGridView2.Rows[i].Cells["date1"].Value = row["date"].ToString()));
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
                Invoke(new Action(() => dataGridView2.Rows[i].Cells["date1"].Value = "Total Hours :"));
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
                    Invoke(new Action(() => label121.Text = attend_day + " Day/s  " + ii.ToString() + ":" + jj.ToString() + " Hours"));
                    Invoke(new Action(() => label121.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label121.Text = attend_day + " Day/s  " + ii.ToString() + ":" + jj.ToString() + " Hours"));
                    Invoke(new Action(() => label121.Enabled = true));
                }

                textappend("Attendance Calculated as :" + label121.Text + Environment.NewLine, Color.Brown);
                textappend(Environment.NewLine, Color.Black);
                label121.ForeColor = Color.Brown;

                textappend("Calculating number of Leave Taken" + Environment.NewLine, Color.Blue);
                textappend(Environment.NewLine, Color.Black);








                string temp_month;

                if (dateTimePicker6.Value.Month < 9)
                    temp_month = "0" + dateTimePicker6.Value.Month.ToString();
                else
                    temp_month = dateTimePicker6.Value.Month.ToString();



                querry = "Select * from emp_leave where emp_number = " + label133.Text + " and from_date between '" + dateTimePicker6.Value.Year + "-" + temp_month + "-01' AND '" + dateTimePicker6.Value.Year + "-" + temp_month + "-31'";


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
                            if (!string.IsNullOrEmpty(row["till_date"].ToString().Trim()))
                                leave_dates.Add(row["till_date"].ToString().Split('-')[2]);
                            else
                                leave_dates.Add("NA");

                        }

                    }

                    string[] from_till_dates = leave_dates.ToArray();
                    string[] leave_types = leave_type.ToArray();
                    int from;
                    int typ = 0;
                    for (int counter = 0; counter < from_till_dates.Length; counter++)
                    {
                        from = Convert.ToInt32(from_till_dates[counter]);

                        if (string.Compare(from_till_dates[counter + 1], "NA") != 0)
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

                                int temp_date = Convert.ToInt32(dataGridView2.Rows[row].Cells["date1"].Value.ToString().Split('-')[2]);
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
                        Invoke(new Action(() => label108.Text = leave_days + " Day/s  " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"));
                    }
                    else
                    {
                        Invoke(new Action(() => label108.Text = leave_days + " Day/s  " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"));
                    }

                    Invoke(new Action(() => label108.Enabled = true));

                    textappend("Leave Taken Calculated as :  " + leave_days + " Day/s  " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours" + Environment.NewLine, Color.Blue);
                    textappend(Environment.NewLine, Color.Black);
                    label108.ForeColor = Color.Blue;
                }

                else
                {
                    Invoke(new Action(() => label108.Text = "No Leave Taken !!!"));
                    textappend("No Leave Taken !!!" + Environment.NewLine, Color.Blue);
                    label105.ForeColor = Color.Blue;
                }

                textappend("Checking the number of holidays this month." + Environment.NewLine, Color.Red);
                textappend(Environment.NewLine, Color.Black);
                querry = "Select * from holiday where month = " + dateTimePicker6.Value.Month + " AND year =  " + dateTimePicker6.Value.Year;
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

                        string[] dgvdate = dataGridView2.Rows[row].Cells["date1"].Value.ToString().Split('-');
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


                int cut = 0;
                for (int row = 0; row < dataGridView2.Rows.Count - 2; row++)
                {
                    if (string.Compare(dataGridView2.Rows[row].Cells["Leave"].Value.ToString(), "Present") != 0)
                    {
                        int lvdate = Convert.ToInt32(dataGridView2.Rows[row].Cells["date1"].Value.ToString().Split('-')[2]);
                        int lvmonth = dateTimePicker6.Value.Month;
                        int lvyear = dateTimePicker6.Value.Year;

                        DateTime datetime = new DateTime(lvyear, lvmonth, lvdate);
                        if (string.Compare(datetime.DayOfWeek.ToString(), "Saturday") == 0)
                        {
                            if (string.Compare(dataGridView2.Rows[row].Cells["Holiday"].Value.ToString(), "-NA-") != 0)
                            {
                                int temp_row = row - 1;
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
                                    querry = "Select * from attendance where emp_number = " + label62.Text + " and date= '" + format_date.previous_day(dateTimePicker6) + "'";
                                    cmnd = new MySqlCommand(querry, con);
                                    da = new MySqlDataAdapter(cmnd);

                                    DataTable temp = new DataTable();
                                    con.Open();
                                    da.Fill(temp);
                                    con.Close();

                                    if (temp.Rows.Count > 0)
                                    {
                                        if (string.Compare(temp.Rows[0]["wd"].ToString(), "00:00") == 0) ;
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

                Invoke(new Action(() => label137.Text = cut.ToString()));

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

                    if (dataGridView2.Rows.Count > 1)
                    {
                        string start_date = dataGridView2.Rows[start_date_row + 1].Cells["date1"].Value.ToString();
                        string end_date = dataGridView2.Rows[end_date_row - 1].Cells["date1"].Value.ToString();
                        DataRow[] leave = dthol.Select("date >= #" + start_date + "# AND date <= #" + end_date + "#");
                        count_leave = leave.Length;
                    }
                }


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
                if (dateTimePicker6.Value.Month == 2)
                {
                    if ((dateTimePicker6.Value.Year % 4 == 0) && (dateTimePicker6.Value.Year % 100 != 0) || dateTimePicker6.Value.Year % 400 == 0)
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
                    total_hours = (month[dateTimePicker6.Value.Month - 1] - holiday_day) * 9;
                    Invoke(new Action(() => label112.Text = month[dateTimePicker6.Value.Month - 1].ToString() + " Days"));
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
                    Invoke(new Action(() => label118.Text = expected_days + " Day/s & " + holiday_temphour.ToString() + ":" + holiday_tempmins.ToString() + " Hours"));
                    Invoke(new Action(() => label118.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label118.Text = expected_days + " Day/s & " + holiday_temphour.ToString() + ":" + holiday_tempmins.ToString() + " Hours"));
                    Invoke(new Action(() => label118.Enabled = true));
                }

                label118.ForeColor = Color.DarkRed;
                textappend("Total Days to attend :" + Environment.NewLine, Color.DarkRed);
                textappend(Environment.NewLine, Color.Black);


                textappend("Calculating the number Accrued Leaves ?" + Environment.NewLine, Color.DarkMagenta);
                textappend(Environment.NewLine, Color.Black);
                temp_expected_days = expected_days; temp_holiday_temphour = holiday_temphour; temp_holiday_tempmins = holiday_tempmins;


                querry = "Select * from leaves where emp_number = " + label133.Text;
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
                    Invoke(new Action(() => label119.Text = left_days + " Day/s  " + left_hours.ToString() + ":" + left_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label119.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label119.Text = left_days + " Day/s  " + left_hours.ToString() + ":" + left_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label119.Enabled = true));
                }
                label119.ForeColor = Color.DarkMagenta;
                textappend("Number of Accrued Leaves :" + label119.Text + Environment.NewLine, Color.DarkMagenta);
                textappend(Environment.NewLine, Color.Black);

                textappend("Checking if Leaves Credited this month ?", Color.DarkBlue);
                textappend(Environment.NewLine, Color.Black);
                querry = "Select * from leave_log where emp_num = " + label133.Text + " and month = " + dateTimePicker6.Value.Month + " and year = " + dateTimePicker6.Value.Year + " and isleave = 'True'";

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

                if (dif_day > 0)
                {
                    Invoke(new Action(() => label116.Text = dif_day + " Day/s & " + dif_hour.ToString() + ":" + dif_min.ToString() + " Hours"));
                    Invoke(new Action(() => label116.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label116.Text = 0 + " Day/s & " + 0 + ":" + 0 + " Hours"));
                    Invoke(new Action(() => label116.Enabled = true));
                }

                label116.ForeColor = Color.MediumSeaGreen;
                textappend("Days left to Attend : " + label116.Text + Environment.NewLine, Color.MediumSeaGreen);
                textappend(Environment.NewLine, Color.Black);

                textappend("Checking Salary per month ?" + Environment.NewLine, Color.DarkOrange);
                textappend(Environment.NewLine, Color.Black);
                querry = "Select salary,pt from empdetails where emp_number = " + label133.Text;
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
                            Invoke(new Action(() => label105.Text = "Rs. " + row["salary"].ToString()));
                            pt = Convert.ToInt32(row["pt"].ToString());

                            Invoke(new Action(() => label89.Text = pt.ToString()));

                            Invoke(new Action(() => label89.Enabled = true));

                            Invoke(new Action(() => label105.Enabled = true));
                        }

                        else
                        {
                            Invoke(new Action(() => label105.Text = "No Salary Information. Please Update bank and salary details."));
                            Invoke(new Action(() => label105.Enabled = true));
                            Invoke(new Action(() => label89.Enabled = true));
                        }
                    }
                }

                label105.ForeColor = label89.ForeColor = Color.DarkOrange;
                textappend("Salary per month : " + label105.Text + "and PT :" + label58.Text + Environment.NewLine, Color.DarkOrange);
                textappend(Environment.NewLine, Color.Black);

                textappend("Calculating Salary as per attended days" + Environment.NewLine, Color.SeaGreen);
                textappend(Environment.NewLine, Color.Black);
                if (cal_salary > 0)
                {
                    float total_days = float.Parse(label112.Text.Split(' ')[0]);
                    total_days = total_days * 9;



                    per_hour = (cal_salary / total_days);
                    per_day = (cal_salary / (total_days / 9));

                    pay_day = pay_hour = 0;

                    pay_hour = 0;

                    if (attend_day < temp_expected_days)
                    {
                        if (ii > 0)
                        {
                            float temp = per_hour * ii;
                            pay_hour = Convert.ToInt32(temp);
                        }

                        if (jj >= 30)
                        {
                            pay_hour = Convert.ToInt32(pay_hour + per_hour);
                        }

                        pay_day = Convert.ToInt32(per_day * attend_day);
                        extra = (count_leave - cut) * per_day;

                        calculated_salary = pay_day + pay_hour;
                        Invoke(new Action(() => label93.Text = "Rs. " + calculated_salary.ToString()));
                        Invoke(new Action(() => label93.Enabled = true));

                        label93.ForeColor = Color.SeaGreen;
                        textappend("Calculated Salary :" + label93.Text + Environment.NewLine, Color.SeaGreen);
                        textappend(Environment.NewLine, Color.Black);

                        Invoke(new Action(() => label91.Text = "Rs. " + (calculated_salary + extra).ToString() + " (Calculated Salary + " + extra + " )"));
                        Invoke(new Action(() => label91.Enabled = true));

                        label91.ForeColor = Color.RosyBrown;
                        textappend("Gross Salary :" + label91.Text + Environment.NewLine, Color.RosyBrown);
                        textappend(Environment.NewLine, Color.Black);

                        net = (calculated_salary + extra) - pt;

                    }
                    else
                    {
                        calculated_salary = per_day * temp_expected_days;
                        extra = (count_leave - cut) * per_day;
                        Invoke(new Action(() => label93.Text = "Rs. " + calculated_salary.ToString()));
                        Invoke(new Action(() => label93.Enabled = true));

                        label93.ForeColor = Color.SeaGreen;
                        textappend("Calculated Salary :" + label93.Text + Environment.NewLine, Color.SeaGreen);
                        textappend(Environment.NewLine, Color.Black);

                        Invoke(new Action(() => label91.Text = "Rs. " + (calculated_salary + extra).ToString() + " (Full Salary)"));
                        Invoke(new Action(() => label91.Enabled = true));

                        label91.ForeColor = Color.RosyBrown;
                        textappend("Gross Salary :" + label91.Text + Environment.NewLine, Color.RosyBrown);
                        textappend(Environment.NewLine, Color.Black);

                        net = (calculated_salary + extra) - pt;
                    }


                    label87.ForeColor = Color.Navy;
                    textappend("Net Salary : " + net + Environment.NewLine, Color.Navy);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label87.Text = "Rs. " + net.ToString()));
                    Invoke(new Action(() => label87.Enabled = true));


                }
                Invoke(new Action(() => linkLabel18.Enabled = true));
                Invoke(new Action(() => button5.Enabled = true));



            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Invoke(new Action(() => linkLabel18.Enabled = true));
                Invoke(new Action(() => button5.Enabled = true));
            }
        }


        public void textappend(string msg, Color c)
        {
            try
            {
                int length = 0;
                Invoke(new Action(() => length = richTextBox6.TextLength));
                Invoke(new Action(() => richTextBox6.AppendText(msg)));
                Invoke(new Action(() => richTextBox6.SelectionStart = length));
                Invoke(new Action(() => richTextBox6.SelectionLength = msg.Length));
                Invoke(new Action(() => richTextBox6.SelectionColor = c));

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel_Reason a = new Cancel_Reason();
                if (a.ShowDialog() == DialogResult.OK)
                {
                    string reason = a.richTextBox1.Text;
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        querry = "Insert into cancel_leave (emp_number,leave_tid,cancel_reason,cancel_date,cancel_time,approval) values("
                            + Form1.empno
                            + ","
                            + row.Cells["ticket_no"].Value.ToString()
                            + ",'"
                            + reason
                            + "','"
                            + dateTimePicker7.Value.Year + "-" + dateTimePicker7.Value.Month + "-" + dateTimePicker7.Value.Day
                            + "','"
                            + dateTimePicker7.Value.ToShortTimeString() + "','Pending')";

                        cmnd = new MySqlCommand(querry, con);

                        con.Open();

                        if (cmnd.ExecuteNonQuery() == 1)
                            MessageBox.Show("Cancellation of ticket is submitted !!!");
                        con.Close();

                        richTextBox5.Text = string.Empty;
                        label126.Text = label127.Text = label128.Text = label129.Text = "-----------";

                        try
                        {
                            Thread tr = new Thread(() => getleaves());
                            tr.Start();
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.ToString());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                con.Close();
            }
        }

        private void linkLabel11_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Daily_Reporting a = new Daily_Reporting();
                this.Hide();
                a.ShowDialog();
                this.Show();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                DataGridViewRow row1;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    row1 = row;
                    index = row.Index;
                }



                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {

                    if (string.Compare(dt.Rows[index]["cancel_id"].ToString(), '0'.ToString()) == 0 && string.Compare(dt.Rows[index]["admin_approval"].ToString(), '0'.ToString()) != 0 && string.Compare(dt.Rows[index]["admin_approval"].ToString(), "Dis-Approved") != 0)
                    {

                        label126.Text = row.Cells["ticket_no"].Value.ToString();
                        label127.Text = row.Cells["from"].Value.ToString();
                        label128.Text = row.Cells["till"].Value.ToString();
                        label129.Text = row.Cells["admin_approval"].Value.ToString();
                        richTextBox5.Text = row.Cells["reason"].Value.ToString();
                        button4.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show("Cancellation for this ticket can not be done !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        richTextBox5.Text = string.Empty;
                        label126.Text = label127.Text = label128.Text = label129.Text = "-----------";
                        button4.Enabled = false;
                    }


                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dateTimePicker6_CloseUp(object sender, EventArgs e)
        {
            textBox28.Text = format_date.dateformat(dateTimePicker6.Value.Month.ToString(), dateTimePicker6.Value.Year.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = false;
                int newleft_hours = 0, newleft_days = 0;
                float remaining_salary = 0;

                float amount = 0;


                textappend("Calculating the adjustment of Leave Taken and Paid Leaves left...." + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);

                int newleft_mins = 0, temp_leavemins = 0, temp_acruedmins = 0;

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

                    int temp = newleft_hours;

                    while (temp > 8)
                    {
                        newleft_days += 1;
                        temp -= 9;
                    }


                    amount = (leave_days * per_day) + (leave_hours * per_hour);

                    if (leave_mins >= 30)
                        amount = amount + per_hour;

                    Invoke(new Action(() => label119.Text = newleft_days + " Day/s & " + temp.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label119.Enabled = true));

                }
                else
                {

                    newleft_mins = newleft_hours = 0;

                    amount = (left_days * per_day) + (left_hours * per_hour);

                    if (left_mins >= 30)
                        amount = amount + per_hour;

                    Invoke(new Action(() => label119.Text = newleft_days + " Day/s & " + newleft_hours.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label119.Enabled = true));
                }

                textappend("Paid Leave after adjustment :" + label119.Text + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);
                textappend("Extra Amount Calculated : " + amount + Environment.NewLine, Color.Black);
                textappend(Environment.NewLine, Color.Black);

                gross = (calculated_salary + extra + amount);
                if (cal_salary >= gross)
                {

                    Invoke(new Action(() => label91.Text = "Rs. " + gross.ToString() + " (Calculated Salary + " + extra + " + Paid Leaves)"));
                    Invoke(new Action(() => label91.Enabled = true));
                    textappend("After adjustment the Gross Salary :" + label91.Text + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    net = net + amount;
                    textappend("After adjustment the final Net Salary :" + net + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label87.Text = "Rs. " + net.ToString()));
                    Invoke(new Action(() => label87.Enabled = true));
                }
                else
                {
                    textappend("The left over Paid leavs are being calculated" + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    remaining_salary = (gross - cal_salary);


                    float save_remaining_salary = amount - remaining_salary;

                    int temp_hour = 0, temp_mins = 0;
                    while (remaining_salary > per_hour)
                    {
                        temp_hour += 1;
                        remaining_salary -= per_hour;
                    }
                    if (remaining_salary >= (per_hour / 2))
                    {
                        temp_hour += 1;
                        remaining_salary -= (per_hour / 2);
                    }

                    newleft_hours += temp_hour;

                    textappend("Left over Paid leaves are ready to save." + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);

                    gross = calculated_salary + extra + save_remaining_salary;
                    Invoke(new Action(() => label91.Text = "Rs. " + Convert.ToInt32(gross).ToString() + " (Calculated Salary + " + extra + " Paid Leaves)"));
                    textappend("After adjustment the new Gross Salary : " + label91.Text + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label91.Enabled = true));

                    net = net + save_remaining_salary;
                    textappend("After adjustment the new Net Salary : " + Convert.ToInt32(net).ToString() + Environment.NewLine, Color.Black);
                    textappend(Environment.NewLine, Color.Black);
                    Invoke(new Action(() => label87.Text = "Rs. " + net.ToString()));
                    Invoke(new Action(() => label87.Enabled = true));
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

                        Invoke(new Action(() => label91.Text = "Rs. " + Convert.ToInt32(gross).ToString() + " (Calculated Salary + " + extra + " Paid Leaves)"));
                        textappend("After adjustment the new Gross Salary : " + label91.Text + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        Invoke(new Action(() => label91.Enabled = true));

                        textappend("After adjustment the new Net Salary : " + Convert.ToInt32(net).ToString() + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                        Invoke(new Action(() => label87.Text = "Rs. " + net.ToString()));
                        Invoke(new Action(() => label87.Enabled = true));

                        Invoke(new Action(() => label119.Text = newleft_days + " Day/s  " + newleft_hours.ToString() + ":" + newleft_mins.ToString() + " Hours"));
                        Invoke(new Action(() => label119.Enabled = true));


                        textappend("The left over Paid Leaves are :" + newleft_days + " Day/s & " + newleft_hours + ":" + newleft_mins + " Hours" + Environment.NewLine, Color.Black);
                        textappend(Environment.NewLine, Color.Black);
                    }

                }


                //if (MessageBox.Show("Do you want the adjustment to be reflected for Emp.ID :" + label62.Text, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    if (MessageBox.Show("Are you Sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {

                //        querry = "Insert into leave_log (emp_num,date,assign_by,month,year,adjust,leave_adjust,isleave) values (@emp_num,date_format(CURDATE(),'%d-%m-%Y'),@assign_by,@month,@yr,@adjust,@leave_adjust,'False')";
                //        DataTable salary = new DataTable();
                //        textappend("Adjustment of left over Paid Leaves is being Updated..." + Environment.NewLine, Color.Black);
                //        textappend(Environment.NewLine, Color.Black);
                //        cmnd = new MySqlCommand(querry, con);
                //        cmnd.Parameters.AddWithValue("@emp_num", label62.Text);
                //        cmnd.Parameters.AddWithValue("@assign_by", Form1.empname);
                //        cmnd.Parameters.AddWithValue("@month", dateTimePicker2.Value.Month);
                //        cmnd.Parameters.AddWithValue("@yr", dateTimePicker2.Value.Year);
                //        cmnd.Parameters.AddWithValue("@adjust", 1);
                //        cmnd.Parameters.AddWithValue("@leave_adjust", newleft_hours + ":" + newleft_mins);
                //        con.Open();
                //        int insert = cmnd.ExecuteNonQuery();
                //        con.Close();
                //        if (insert > 0)
                //            MessageBox.Show("The updation of Leave Adjustment has been reflected Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //        textappend("Adjustment of left over Paid Leaves Updated back to the Employee Emp.ID : " + label62.Text + Environment.NewLine, Color.Black);
                //        textappend(Environment.NewLine, Color.Black);
                //    }
                //}

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        
    }
}
