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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            this.Hide();
            Form2.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            prikazNarudzbe();
        }

        String konekcioniString = Form1.konekcioniString;
        public String query;

        private void prikazNarudzbe() 
        {
            query = "SELECT s.narudzbenica_id, k.kupac_id, k.ime, k.prezime, s.stavka_id, s.artikal_id, s.kolicina " +
                "FROM kupac k, narudzbenica n, stavka_narudzbenica s, artikal a " +
                "WHERE n.narudzbenica_id = s.narudzbenica_id AND k.kupac_id = n.kupac_id AND s.artikal_id = a.artikal_id";

            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataAdapter.Dispose();



 
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brisanjeNarudzbe();
        }


        private void brisanjeNarudzbe() 
        {
            query = "DELETE FROM stavka_narudzbenica " +
                "WHERE narudzbenica_id= '" + textBox1.Text + "' ";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            int obrisano = cmd.ExecuteNonQuery();

            if (obrisano > 0)
            {
                MessageBox.Show("Narudzba sa šifrom " + textBox1.Text + " je obrisan !!!");
            }
            else
                MessageBox.Show("Narudzba sa šifrom " + textBox1.Text + " ne postoji !!!");
            konekcija.Close();

            prikazNarudzbe();

        }
    }
}
