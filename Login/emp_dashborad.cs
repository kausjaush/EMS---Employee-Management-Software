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
    public partial class emp_dashboard : Form
    {

        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dthol = new DataTable();
        byte[] doc, emedoc, meddoc;
        string querry;
        int l;
        public emp_dashboard()
        {
            InitializeComponent();
            timer1.Tick += timer1_Tick;
            timer1.Start();
            label106.Text = Form1.empno;
            label92.Text = Form1.empname;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
        public Form reffer
        {
            get;
            set;
        }
        #region Personal Details
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from empdetails where emp_number = " + Form1.dt.Rows[0]["empnumber"].ToString();
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
        #endregion

        #region Leave
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(richTextBox2.Text) && !string.IsNullOrWhiteSpace(richTextBox2.Text))
                {
                    if ((radioButton1.Checked == true && !string.IsNullOrEmpty(textBox23.Text)) || ((radioButton2.Checked == true && !string.IsNullOrEmpty(textBox23.Text) && !string.IsNullOrEmpty(textBox24.Text))))
                    {
                        int total_days = 0;
                        con = new MySqlConnection();
                        con.ConnectionString = Form1.sqlstring;
                        string submit = string.Empty;

                        if (radioButton2.Checked == true)
                        {
                            total_days = (int)(dateTimePicker2.Value - dateTimePicker1.Value).TotalDays;
                        }
                        else
                            total_days = 1;


                        if (doc != null)
                            submit = "Submitted";
                        querry = "Insert into emp_leave (emp_number,reason,from_date,till_date,date,time,doc,doc_submit,no_days) values(" + Form1.empno + ",'" + richTextBox2.Text + "','" + textBox23.Text + "','" + textBox24.Text + "',CURDATE(),CURTIME(),@do,@doc_submit,@no_days)";
                        cmnd = new MySqlCommand(querry, con);
                        cmnd.Parameters.Add("@do", MySqlDbType.MediumBlob);
                        cmnd.Parameters["@do"].Value = doc;
                        cmnd.Parameters.Add("@doc_submit", MySqlDbType.VarChar).Value = submit;
                        cmnd.Parameters.Add("@no_days", MySqlDbType.VarChar).Value = total_days * 9 + ":00";

                        con.Open();
                        if (cmnd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Your Leave Ticket has been submitted Successfully. Please check later for status!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox23.Text = textBox24.Text = richTextBox2.Text = string.Empty;
                            linkLabel12.Text = "Attach Copy";
                            radioButton1.Checked = radioButton2.Checked = false;
                        }
                        con.Close();
                    }
                    else
                        MessageBox.Show("Kindly provide date properly !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Kindly provide the reason of taking leave !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private int days(int d, int m, int y, int dd, int mm, int yy)
        {
            int day = 0;
            try
            {

                return day;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return day;
            }
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox23.Text = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox24.Text = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;

                querry = "Select idleave,reason,from_date,till_date,date,time,admin_approval,admin_reason,admin_date,admin_time,doc_submit,half_day from emp_leave where emp_number = " + Form1.empno;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);

                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells["ticket_no"].Value = row["idleave"].ToString();
                        dataGridView1.Rows[i].Cells["reason"].Value = row["reason"].ToString();
                        dataGridView1.Rows[i].Cells["from"].Value = row["from_date"].ToString();
                        dataGridView1.Rows[i].Cells["till"].Value = row["till_date"].ToString();
                        dataGridView1.Rows[i].Cells["date"].Value = row["date"].ToString();
                        dataGridView1.Rows[i].Cells["time"].Value = row["time"].ToString();
                        dataGridView1.Rows[i].Cells["admin_approval"].Value = row["admin_approval"].ToString();
                        dataGridView1.Rows[i].Cells["admin_reason"].Value = row["admin_reason"].ToString();
                        dataGridView1.Rows[i].Cells["admin_date"].Value = row["admin_date"].ToString();
                        dataGridView1.Rows[i].Cells["admin_time"].Value = row["admin_time"].ToString();
                        dataGridView1.Rows[i].Cells["half"].Value = row["half_day"].ToString();

                        if (!string.IsNullOrEmpty(row["doc_submit"].ToString()))
                            dataGridView1.Rows[i].Cells["file"].Value = "Attached";
                        i++;
                    }
                }
                else
                    MessageBox.Show("No Leave Tickets found !!!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }


        private void load()
        {
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                string querry = "Select * from empdailyreport where idempwork = " + Convert.ToInt32(Form1.empno);
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
                    Invoke(new Action(() => dataGridView1.Visible = true));
                    foreach (DataRow row in dt.Rows)
                    {
                        Invoke(new Action(() => dataGridView1.Rows.Add()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells[""].Value = row["emp_number"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["workid"].Value = row["idempwork"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["comment"].Value = row["comment"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["Date"].Value = row["date"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["Time"].Value = row["time"].ToString()));
                        Invoke(new Action(() => dataGridView1.Rows[i++].Cells["Reason"].Value = row["comment"].ToString()));
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
                    Invoke(new Action(() => label1.Text = "No Record Found for Ticket TW-ID:" + Form1.empno));
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

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

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

                            querry = "Insert into emp_leave (emp_number,reason,from_date,till_date,date,time,doc,no_days,doc_submit) values(" + Form1.empno + ",'" + richTextBox4.Text + "','" + textBox25.Text + "','" + textBox27.Text + "',CURDATE(),CURTIME(),@do,@no_days,@doc_submit)";
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

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            textBox26.Text = dateTimePicker4.Value.Year + "-" + dateTimePicker4.Value.Month + "-" + dateTimePicker4.Value.Day;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox25.Text = dateTimePicker3.Value.Year + "-" + dateTimePicker3.Value.Month + "-" + dateTimePicker3.Value.Day;
            textBox27.Enabled = true;
            dateTimePicker5.Enabled = true;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            textBox27.Text = dateTimePicker5.Value.Year + "-" + dateTimePicker5.Value.Month + "-" + dateTimePicker5.Value.Day;

        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        #endregion

        #region edudetails


        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                try
                {

                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;
                    querry = "Select * from edudetails where emp_number = " + Form1.dt.Rows[0]["empnumber"].ToString();
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

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                download("ssc");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                download("hsc");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }


        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                download("ug");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                download("pg");
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
                download("other");
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
                download("idcopy");
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
                download("ppcopy");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private void download(string doc)
        {
            try
            {

                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;

                querry = "Select " + doc + " from edudetails where emp_number = " + Form1.empno;
                FileStream fs;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        byte[] data = (byte[])dt.Rows[0][doc];
                        fs = new FileStream(Path.GetDirectoryName(saveFileDialog1.FileName.ToString()) + "\\" + doc + ".pdf", FileMode.Create, FileAccess.Write);
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
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        #endregion

        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            password a = new password();
            a.ShowDialog();
        }

        #region attendance

        private void linkLabel17_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                label112.Text = label108.Text = label111.Text = label114.Text = label105.Text = label116.Text = label88.Text = label89.Text = label118.Text = label120.Text = "-------------";
                Thread tr = new Thread(() => attendance());
                tr.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        #endregion

        private void dateTimePicker6_CloseUp(object sender, EventArgs e)
        {
            textBox28.Text = dateTimePicker6.Value.Month + "-" + dateTimePicker6.Value.Year;
            linkLabel17.Enabled = true;
        }

        private void attendance()
        {
            try
            {

                int[] month = { 31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int i = 0;
                int ii = 0, jj = 0;


                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;
                querry = "Select * from attendance where emp_number = " + label106.Text + " and month = " + dateTimePicker6.Value.Month + " and year = " + dateTimePicker6.Value.Year + " Order by day ASC";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);

                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
                Invoke(new Action(() => dataGridView2.Rows.Clear()));
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
                        Invoke(new Action(() => dataGridView2.Rows[i++].Cells["tdcal"].Value = row["cal_hours"].ToString()));

                        ii += Convert.ToInt32(row["cal_hours"].ToString().Split(':')[0]);
                        jj += Convert.ToInt32(row["cal_hours"].ToString().Split(':')[1]);
                    }
                    Invoke(new Action(() => dataGridView2.Enabled = true));
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
                int attend_day = 0, attend_set = 0;
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



                querry = "Select * from emp_leave where emp_number = " + label106.Text + " and from_date between '" + dateTimePicker6.Value.Year + "-" + dateTimePicker6.Value.Month + "-01' AND '" + dateTimePicker6.Value.Year + "-" + dateTimePicker6.Value.Month + "-31'";
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();

                int leave_hours = 0, leave_mins = 0;
                if (dthol.Rows.Count > 0)
                {
                    foreach (DataRow row in dthol.Rows)
                    {
                        if (string.Compare(row["admin_approval"].ToString(), "Approved") == 0)
                        {

                            leave_hours += Convert.ToInt32(row["no_days"].ToString().Split(':')[0]);
                            leave_mins += Convert.ToInt32(row["no_days"].ToString().Split(':')[1]);
                        }
                    }

                    int days = 0, set = 0;
                    while (leave_mins > 59)
                    {
                        leave_hours += 1;
                        leave_mins -= 60;
                    }
                    while (leave_hours > 8)
                    {
                        days += 1;
                        leave_hours -= 9;
                        set = 1;
                    }
                    if (set == 1)
                    {
                        Invoke(new Action(() => label88.Text = days + " Day/s & " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"));
                    }
                    else
                    {
                        Invoke(new Action(() => label88.Text = days + " Day/s & " + leave_hours.ToString() + ":" + leave_mins.ToString() + " Hours"));
                    }

                    Invoke(new Action(() => label88.Enabled = true));

                }
                else
                {
                    Invoke(new Action(() => label88.Text = "No Leave Taken !!!"));
                }

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

                int holiday_day = 0, holiday_set = 0;



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
                    holiday_set = 1;
                }
                Invoke(new Action(() => label111.Text = holiday_day + " Day/s & " + holiday_temphour.ToString() + ":" + holiday_tempmins.ToString() + " Hours"));
                Invoke(new Action(() => label111.Enabled = true));


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

                querry = "Select * from leaves where emp_number = " + label106.Text;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
                int left_hours = 0, left_mins = 0;
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

                int leave_days = 0;

                while (left_hours > 8)
                {
                    leave_days += 1;
                    left_hours -= 9;
                }

                if (leave_set == 0)
                {
                    Invoke(new Action(() => label89.Text = leave_days + " Day/s & " + left_hours.ToString() + ":" + left_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));
                }
                else
                {
                    Invoke(new Action(() => label89.Text = "-" + leave_days + " Day/s & " + left_hours.ToString() + ":" + left_mins.ToString() + " Hours"));
                    Invoke(new Action(() => label89.Enabled = true));
                }


                querry = "Select * from leave_log where emp_num = " + label106.Text + " and month = " + dateTimePicker6.Value.Month + " and year = " + dateTimePicker6.Value.Year;

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


                int dif_day = 0, dif_hour = 0, dif_min = 0;

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

                Invoke(new Action(() => label116.Text = dif_day + " Day/s & " + dif_hour.ToString() + ":" + dif_min.ToString() + " Hours"));
                Invoke(new Action(() => label116.Enabled = true));

                querry = "Select salary from empdetails where emp_number = " + label106.Text;
                DataTable salary = new DataTable();

                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                salary.Clear();
                con.Open();
                da.Fill(salary);
                con.Close();
                int cal_salary = 0;

                if (salary.Rows.Count > 0)
                {
                    foreach (DataRow row in salary.Rows)
                    {
                        cal_salary = Convert.ToInt32(row["salary"].ToString());
                        if (cal_salary > 0)
                        {
                            Invoke(new Action(() => label120.Text = "Rs. " + row["salary"].ToString()));
                            Invoke(new Action(() => label120.Enabled = true));
                        }

                        else
                        {
                            Invoke(new Action(() => label120.Text = "No Salary Information. Please Update bank and salary details."));
                            Invoke(new Action(() => label120.Enabled = true));
                        }
                    }
                }

                if (cal_salary > 0)
                {
                    float total_days = float.Parse(label112.Text.Split(' ')[0]);
                    total_days = total_days * 9;

                    float per_hour = 0, per_day = 0;

                    per_hour = (cal_salary / total_days);
                    per_day = (cal_salary / (total_days / 9));

                    int pay_day = Convert.ToInt32(per_day * attend_day);

                    int pay_hour = 0;

                    int pay_min = 0;

                    if (ii > 0)
                    {
                        float temp = per_hour * ii;
                        pay_hour = Convert.ToInt32(temp);
                    }

                    if (jj >= 45)
                    {
                        pay_hour = Convert.ToInt32(pay_hour + per_hour);
                    }

                    Invoke(new Action(() => label118.Text = "Rs. " + (pay_day + pay_hour).ToString()));
                    Invoke(new Action(() => label118.Enabled = true));
                }


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                querry = "Insert into ";

                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                dthol.Clear();
                con.Open();
                da.Fill(dthol);
                con.Close();
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
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    MessageBox.Show(row.Cells["ticket_no"].ToString());
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
    }
}