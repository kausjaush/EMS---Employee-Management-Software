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
    public partial class TL : Form
    {
        public TL()
        {
            InitializeComponent();
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

    }
}
