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
    public partial class Company_Documents : Form
    {
        MySqlConnection con;
        MySqlCommand cmnd;
        MySqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dthol = new DataTable();
        string querry;
        public Company_Documents()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                download("1");
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel1,1));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void attach(LinkLabel l1,int id)
        {
            byte[] temp = null;
            try
            {

                openFileDialog1.Multiselect = false;
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.Filter = "ZIP FILES|*.zip";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Invoke(new Action(() => l1.Text = "Attaching..."));
                    var stream = new FileStream(Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName.ToString()), openFileDialog1.FileName.ToString()), FileMode.Open, FileAccess.Read);
                    var reader = new BinaryReader(stream);
                    temp = reader.ReadBytes((int)stream.Length);
                    Thread.Sleep(1000);
                    Invoke(new Action(() => l1.Text = "Copy Attached"));
                    upload(temp,id,l1);
                }

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

                querry = "Select * from compdocs where did = " + doc;
                FileStream fs;
                cmnd = new MySqlCommand(querry, con);
                da = new MySqlDataAdapter(cmnd);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        byte[] data = (byte[])dt.Rows[0]["data"];
                        fs = new FileStream(Path.GetDirectoryName(saveFileDialog1.FileName.ToString()) + "\\" + doc + ".zip", FileMode.Create, FileAccess.Write);
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

        private void upload(byte[] temp,int id,LinkLabel l1)
        {
            try
            {

                Invoke(new Action(() => l1.Text = "Uploading..."));
                con = new MySqlConnection();
                con.ConnectionString = Form1.sqlstring;


                querry = "insert into compdocs (did,data) values ("+id+",@data) ON Duplicate Key Update data = @data ";

                MySqlCommand cmnd1 = new MySqlCommand(querry, con);

                cmnd1.Parameters.Add("@data", MySqlDbType.MediumBlob);
                cmnd1.Parameters["@data"].Value = temp;

                con.Open();
                int i = cmnd1.ExecuteNonQuery();
                if (i > 0)
                {
                        Invoke(new Action(() => l1.Text = "Uploaded Successfully")); 
                }
                else
                   Invoke(new Action(() => l1.Text = "Upload Failed"));
                


            }
            catch (MySqlException e1)
            {
                if (e1.Message.Contains("Duplicate entry"))
                    MessageBox.Show("The employee ID is already in use!!!");
                else
                    MessageBox.Show(e1.ToString());
                con.Close();
            }
            catch (Exception e1) { MessageBox.Show(e1.ToString());
            con.Close();
            }
        }


        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("2");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("3");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("4");
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("5");
        }

        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("6");
        }

        private void linkLabel17_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("7");
        }

        private void linkLabel18_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("8");
        }

        private void linkLabel19_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("9");
        }

        private void linkLabel20_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            download("10");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel4, 2));
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
                Thread t = new Thread(() => attach(linkLabel6, 3));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel8, 4));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel10, 5));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel11, 6));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel12, 7));
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
                Thread t = new Thread(() => attach(linkLabel14, 8));
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
                Thread t = new Thread(() => attach(linkLabel13, 9));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Thread t = new Thread(() => attach(linkLabel15, 10));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
