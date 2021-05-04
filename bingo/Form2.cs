using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }
        public static String konekcioniString = "Server = localhost; Port=3309; Database= projekat;" + "Uid=root; Pwd=root";
        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = Form1.username;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            this.Hide();
            Form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            this.Hide();
            Form5.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 Form6 = new Form6();
            this.Hide();
            Form6.Show();
        }
    }
}
