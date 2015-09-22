using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace Login
{
    
    public partial class new_employee : Form
    {
        
        string eduscore;
        string errmsg,gen;
        string dob;
        string cmndstring,querry;
        byte[] f1,f2,ssc,hsc,ug,pg,other;        
        MySqlConnection con;
        MySqlCommand cmnd,cmnd1;
        
        
        public new_employee()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    eduscore = string.Empty;
                    score();
                    con = new MySqlConnection();
                    con.ConnectionString = Form1.sqlstring;                   
                    
                    cmndstring = "Insert into empdetails (emp_number,status,doj,emp_fname,emp_mname,emp_lname,dob,gender,pancard,passport,job_title1,job_title2,company_name,location,department,cur_adds,per_adds,mobile,hmobile,emailid,r_cur_adds,r_per_adds,r_mobile,r_hmobile,r_emailid) values ('" + textBox1.Text + "','" + comboBox1.SelectedItem + "','" + textBox20.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox21.Text + "','" + gen + "','" + textBox5.Text + "','" + textBox18.Text + "','" + textBox17.Text + "','" + textBox19.Text + "','"+label21.Text+"','"+label22.Text+"','"+ comboBox2.SelectedItem + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox16.Text + "','" + textBox15.Text + "','" + textBox14.Text + "','" + textBox13.Text + "','" + textBox12.Text+"')";
                    querry = "insert into edudetails (emp_number,score,ssc,hsc,ug,pg,other,idcopy,ppcopy) values ("+textBox1.Text+",'"+eduscore+"',@ssc,@hsc,@ug,@pg,@other,@id,@pp)";

                    cmnd = new MySqlCommand(cmndstring,con);
                    cmnd1 = new MySqlCommand(querry,con);

                    cmnd1.Parameters.Add("@ssc", MySqlDbType.MediumBlob);
                    cmnd1.Parameters.Add("@hsc", MySqlDbType.MediumBlob);
                    cmnd1.Parameters.Add("@ug", MySqlDbType.MediumBlob);
                    cmnd1.Parameters.Add("@pg", MySqlDbType.MediumBlob);
                    cmnd1.Parameters.Add("@other", MySqlDbType.MediumBlob);
                    cmnd1.Parameters.Add("@id", MySqlDbType.MediumBlob);
                    cmnd1.Parameters.Add("@pp", MySqlDbType.MediumBlob);

                    cmnd1.Parameters.Add("@score",MySqlDbType.VarChar).Value = eduscore;
                    cmnd1.Parameters["@ssc"].Value = ssc;
                    cmnd1.Parameters["@hsc"].Value = hsc;
                    cmnd1.Parameters["@ug"].Value = ug;
                    cmnd1.Parameters["@pg"].Value = pg;
                    cmnd1.Parameters["@other"].Value = other;
                    cmnd1.Parameters["@id"].Value = f1;
                    cmnd1.Parameters["@pp"].Value = f2;

                    con.Open();
                    int i = cmnd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        if(cmnd1.ExecuteNonQuery() > 0)
                            MessageBox.Show("Account added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Account added with error due to internet issue !!!", "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Not added", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
                else
                {
                    MessageBox.Show(errmsg, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (MySqlException e1)
            {
                if (e1.Message.Contains("Duplicate entry"))
                    MessageBox.Show("The employee ID is already in use!!!");
                else
                    MessageBox.Show(e1.ToString());
            }
            catch (Exception e1) { MessageBox.Show(e1.ToString()); }
        }


        private void score()
        {
            try
            {
                eduscore = 
                    "SSC," 
                    + textBox25.Text + "," 
                    + textBox24.Text + "," 
                    + textBox23.Text + "," 
                    + comboBox3.Text 
                    + "#HSC," 
                    + textBox26.Text + "," 
                    + textBox22.Text + "," 
                    + textBox6.Text + ","
                    + comboBox4.Text 
                    + "#UG," 
                    + textBox29.Text + "," 
                    + textBox28.Text + ","
                    + textBox27.Text + ","
                    + comboBox5.Text 
                    + "#PG," 
                    + textBox32.Text + "," 
                    + textBox31.Text + "," 
                    + textBox30.Text + ","
                    + comboBox6.Text 
                    + "#OTHER,"
                    + richTextBox1.Text;

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private bool check()
        {
            if ((!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)) )
            {
                if(comboBox1.SelectedIndex!=0)
                {
                    if(!string.IsNullOrEmpty(textBox20.Text))
                    {
                        if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox4.Text))
                        {
                            if (!string.IsNullOrEmpty(textBox21.Text))
                            {
                                if (age())
                                {
                                    if (radioButton1.Checked == true || radioButton2.Checked == true)
                                    {
                                        if (radioButton1.Checked == true)
                                            gen = radioButton1.Text;
                                        else
                                            gen = radioButton2.Text;

                                        if (!string.IsNullOrEmpty(textBox5.Text))
                                        {
                                            if (!string.IsNullOrEmpty(textBox18.Text))
                                            {
                                                if (!string.IsNullOrEmpty(textBox17.Text))
                                                {
                                                    if (comboBox2.SelectedIndex!=0)
                                                    {
                                                        if ((!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text)) 
                                                            && (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
                                                            && (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
                                                            && (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))
                                                            && (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text)) 
                                                            )
                                                        {
                                                            return true;
                                                        }
                                                        else
                                                            errmsg = "Please enter \"Employee's All Address Details\" !!!";
                                                    }
                                                    else
                                                        errmsg = "Please enter \"Employee's Department\" !!!";
                                                }
                                                else
                                                    errmsg = "Please enter \"Employee's Job Title\" !!!";
                                            }
                                            else
                                                errmsg = "Please enter \"Employee's Passport Number\" !!!";
                                        }
                                        else
                                            errmsg = "Please enter \"Employee's PanCard Number\" !!!";
                                    }
                                    else
                                        errmsg = "Please select \"Employee's Gender\" !!!";
                                }
                                else
                                    errmsg = "Employee's age is less than min. required as per \"Company Policy \" !!!";
                            }
                            else
                                errmsg = "Please enter \"Employee's Date of Birth\" !!!";
                        }
                        else
                            errmsg = "Please enter \"Employee's Name Properly\" !!!";
                    }
                    else
                        errmsg = "Please enter \"Employee's Date of Joining\" !!!";
                }
                else
                   errmsg= "Please enter \"Status of Employee\" !!!";
            }
            else
                errmsg="Please enter \"Employee Number\" !!!";

            return false;
        }

        private bool age()
        {
            string date = dateTimePicker3.Value.ToString();
            string[] dt = date.Split(' ');
            dob = dt[0];

            if (DateTime.Today.Month < dateTimePicker3.Value.Month)
            {
                int dob_year = (DateTime.Today.Year - 1) - dateTimePicker3.Value.Year;
                if (dob_year >= 19)
                    return (true);
            }
            else
            {
                int dob_year = DateTime.Today.Year - dateTimePicker3.Value.Year;
                if (dob_year >= 19)
                    return true;
            }

            return false;
        }
        private void dateTimePicker1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox20.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox21.Text = dateTimePicker3.Value.ToShortDateString();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
               Thread t = new Thread(()=> f1= attach(linkLabel1));
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
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.Filter = "PDF FILES|*.pdf";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Invoke(new Action(() => l1.Text = "Attaching..."));
                    var stream = new FileStream(Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName.ToString()), openFileDialog1.FileName.ToString()), FileMode.Open, FileAccess.Read);
                    var reader = new BinaryReader(stream);
                    temp= reader.ReadBytes((int)stream.Length);
                    Thread.Sleep(1000);
                    Invoke(new Action(() => l1.Text = "Copy Attached"));
                    return temp;
                }
                return temp;
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return temp;
            }
        }
        

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => f2= attach(linkLabel2));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => ssc=attach(linkLabel3));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => hsc= attach(linkLabel4));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => ug= attach(linkLabel5));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() =>pg= attach(linkLabel6));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() =>other= attach(linkLabel7));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox25,e);
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox23, e);
        }

        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox26, e);
        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox9, e);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox10, e);
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox14, e);
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            intvalid.validate(textBox13, e);
        }

      
    }
}