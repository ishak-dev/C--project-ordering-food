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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            this.Hide();
            Form2.Show();
        }

         


        

        String konekcioniString = Form1.konekcioniString;
        private void Form4_Load(object sender, EventArgs e)
        {
            prikazKupca();
            
        }

       

        public String query;
        private void prikazKupca()
        {
            query = "SELECT kupac_id, ime, prezime, grad, adresa, telefon, user, pass " +
                "FROM kupac WHERE kupac_id = kupac_id      ";

            prikazi();
            

            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataAdapter.Dispose();

        }
        private void prikazi()
        {
            

            if (textBox1.Text != "")
            {
                query += " and ime LIKE '" + textBox1.Text + "%' ";
            }
            else {
              
            }
            if (textBox2.Text != "")
            {
                query += " and prezime LIKE '" + textBox2.Text + "%' ";
            }
            else {
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            prikazKupca();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            unesiKupca();

        }

        private void unesiKupca() 
        {
            String dodaj = "INSERT INTO kupac(ime, prezime, grad, adresa, telefon, user, pass) " +
                    "VALUES ('" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" +
                    textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "') ";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();

            MySqlCommand cmd = new MySqlCommand(dodaj, konekcija);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Dodan novi korisnik !!!");

            konekcija.Close();

            textBox9.Text = "";
            textBox8.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            brisanjePodataka();
        }

        private void brisanjePodataka()
        {
            query = "DELETE FROM kupac WHERE kupac_id = '" + textBox10.Text + "' ";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            int obrisano = cmd.ExecuteNonQuery();

            if (obrisano > 0)
                MessageBox.Show("Korisnik sa šifrom " + textBox10.Text + " je obrisan !!!");
            else
                MessageBox.Show("Korisnik sa šifrom " + textBox10.Text + " ne postoji !!!");

            konekcija.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            azurirajKupca();
        }

        private void azurirajKupca()
        {
            if (textBox3.Text != "") { query = "UPDATE kupac SET ime= '" + textBox3.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' ";}
            if (textBox4.Text != "") { query = "UPDATE kupac SET prezime= '" + textBox4.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' "; }
            if (textBox5.Text != "") { query = "UPDATE kupac SET grad= '" + textBox5.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' "; }
            if (textBox6.Text != "") { query = "UPDATE kupac SET adresa= '" + textBox6.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' "; }
            if (textBox7.Text != "") { query = "UPDATE kupac SET telefon= '" + textBox7.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' "; }
            if (textBox8.Text != "") { query = "UPDATE kupac SET user= '" + textBox8.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' "; }
            if (textBox9.Text != "") { query = "UPDATE kupac SET pass= '" + textBox9.Text + "' " + "WHERE kupac_id = '" + textBox10.Text + "' "; }

            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            
            cmd.ExecuteNonQuery();
            MessageBox.Show("Uspjesno ažuriran korsnik");
            konekcija.Close();


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
   
    }
}
