using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class Employer : Form
    {
        public Employer()
        {
            InitializeComponent();
        }

        private void Employer_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reffer.Show();
            this.Dispose();
        }

        public Form reffer
        {
            get;
            set;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ticketmodify a = new ticketmodify();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            salary a = new salary();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new_employee a = new new_employee();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                password a = new password();
                a.ShowDialog();
                
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            attend a = new attend();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Company_Documents a = new Company_Documents();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            company_policy a = new company_policy();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }
    }
}
