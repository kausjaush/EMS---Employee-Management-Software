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
    public partial class done_droped : Form
    {
        
        
        public done_droped(string tid)
        {
            InitializeComponent();       
            this.Text = "Why you want to Drop Ticket: " + tid +" ?";           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
