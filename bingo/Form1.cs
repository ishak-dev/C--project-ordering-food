using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;




namespace WindowsFormsApplication1

   
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public static String konekcioniString = "Server = localhost; Port=3309; Database= projekt;" + "Uid=root; Pwd=user";

        public static String username;
        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            username = textBox1.Text;
            
            
            String password = textBox2.Text;
            
            String query = "SELECT k.pass, k.kupac_id FROM kupac k WHERE user ='" + username + "' ";
            
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
            {
                errorProvider1.SetError(textBox1, "Pogrešno korisničko ime !!!");
            }
            else
            {
                String pass = reader[0].ToString();
                String kupac_id = reader[1].ToString();
                if (password == pass)
                {
                    if (kupac_id == "1")
                    {
                        MessageBox.Show("Uspješno ste logovani!!!");
                        Form2 forma2 = new Form2();
                        this.Hide();
                        forma2.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Uspješno ste logovani!!!");
                        Form3 forma3 = new Form3();
                        this.Hide();
                        forma3.Show();

                    }
                }
                else
                {
                    errorProvider1.SetError(textBox2, "Pogrešan password !!!");
                }

            }
            
            reader.Close();
            konekcija.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
