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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            pregledNarudzbi();
            stavkeNarduzbenice();
            total();
        }
        String query1;
        String konekcioniString = Form1.konekcioniString;
        private void pregledNarudzbi()
        {
            query1 = "SELECT n.narudzbenica_id, n.kupac_id, n.datum_narudzbe " +
                "FROM narudzbenica n, stavka_narudzbenica s " +
                "WHERE kupac_id = (SELECT kupac_id FROM kupac where user = '" + Form1.username + "') "+
                "AND n.narudzbenica_id=s.narudzbenica_id order by narudzbenica_id ASC";
                
                
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query1, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataAdapter.Dispose();
            konekcija.Close();
        }
        String query;
        private void stavkeNarduzbenice()
        {
            query = "SELECT s.stavka_id, s.narudzbenica_id, a.naziv_artikla, s.artikal_id, s.kolicina, a.cijena " +
               "FROM stavka_narudzbenica s, artikal a " +
               "WHERE s.narudzbenica_id IN (SELECT narudzbenica_id FROM narudzbenica WHERE " +
               "kupac_id = (SELECT kupac_id FROM kupac where user = '" + Form1.username + "')) " +
               " AND s.artikal_id = a.artikal_id  ";
             pretrazivanje(); 
                
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
            DataTable tabela2 = new DataTable();
            dataAdapter.Fill(tabela2);
            dataGridView2.DataSource = tabela2;
            dataAdapter.Dispose();
            konekcija.Close();
        }
        private void pretrazivanje()
        {

            if (textBox1.Text != "")
            {
                query += "AND s.narudzbenica_id= '" + textBox1.Text.ToString() + "' order by s.stavka_id ASC ";
            }
            else { query += "order by s.stavka_id ASC"; }
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stavkeNarduzbenice();
            
        }
        private void total()
        {
            


            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = konekcija;
            //cmd.ExecuteReader();
            cmd.CommandText = "SELECT SUM(a.cijena) " +
               "FROM stavka_narudzbenica s, artikal a " +
               "WHERE s.narudzbenica_id IN (SELECT narudzbenica_id FROM narudzbenica WHERE " +
               "kupac_id = (SELECT kupac_id FROM kupac where user = '" + Form1.username + "')) " +
               "AND s.artikal_id = a.artikal_id ";
            
            var broj = cmd.ExecuteScalar().ToString();


            textBox2.Text = (broj).ToString();

            //MessageBox.Show("Uspješno dodano u korpu");
            konekcija.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }
    }
}
