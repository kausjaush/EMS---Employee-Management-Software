using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Accounts : Form
    {
        public Accounts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new_employee a = new new_employee();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            salary a = new salary();
            this.Hide();
            a.ShowDialog();
            this.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            attend a = new attend();
            this.Hide();
            a.ShowDialog();
            this.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Company_Documents a = new Company_Documents();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }
    }
}
