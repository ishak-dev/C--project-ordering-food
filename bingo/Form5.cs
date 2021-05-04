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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            this.Hide();
            Form2.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            prikazZadataka();
        }

        String konekcioniString = Form1.konekcioniString;
        public String query;
        private void prikazZadataka()
        {
            query = "SELECT a.artikal_id, a.naziv_artikla, a.vrsta_artikla, a.cijena, s.kolicina_stanje  FROM artikal a, skladiste s WHERE a.artikal_id = s.artikal_id ";
            pretrazivanje();
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataAdapter.Dispose();
             
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            prikazZadataka();
        }

        private void pretrazivanje()
        {

            if (textBox1.Text != "")
            {
                query += "AND a.artikal_id= '" + textBox1.Text.ToString() + "' ";
            }
            if (textBox2.Text != "")
            {
                query += "AND a.naziv_artikla LIKE '" + textBox2.Text + "%' ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dodajArtikal();
        }

        private void dodajArtikal()
        {


            String dodaj = "INSERT INTO artikal(naziv_artikla, vrsta_artikla, cijena) " +
                "VALUES ('" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "'); " + 
                "INSERT INTO skladiste(artikal_id, kolicina_stanje) " + "VALUES ((SELECT MAX(artikal_id) FROM artikal), '" + textBox6.Text + "') ";
             
            
            
                
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(dodaj, konekcija);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Ažurirano");
            konekcija.Close();

            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            azuriranjeArtikla();

        }

        private void azuriranjeArtikla()
        {
            int broj = Convert.ToInt32(numericUpDown1.Value);
            if (broj != 0)
            {
                query = "UPDATE skladiste SET kolicina_stanje= kolicina_stanje +'" + broj + "' " + " WHERE artikal_id = '" + textBox7.Text + "' ";
            }
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Uspjesno ažurirana količina");
            konekcija.Close();
            
            prikazZadataka();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            obrisiArtikal();
        }

        private void obrisiArtikal()
        {
            query = " DELETE  FROM skladiste WHERE artikal_id = '" + textBox7.Text + "'; "
                + "DELETE  FROM artikal WHERE artikal_id = '" + textBox7.Text + "' ";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Uspjesno obrisan artikal");
            konekcija.Close();



            prikazZadataka();
        }

    }
}
