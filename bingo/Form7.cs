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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            pregledArtikla();
        }

        String konekcioniString = Form1.konekcioniString;
        String query;
        private void pregledArtikla()
        {
            query = "SELECT a.artikal_id, a.naziv_artikla, a.vrsta_artikla, a.cijena, s.kolicina_stanje " +
                "FROM artikal a, skladiste s WHERE a.artikal_id = s.artikal_id";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataAdapter.Dispose();
        }
        
        
        private void pregledKorpe()
        {
            query = "SELECT s.stavka_id, s.narudzbenica_id, s.artikal_id, s.kolicina, a.naziv_artikla " +
                "FROM artikal a, stavka_narudzbenica s, narudzbenica n WHERE a.artikal_id = s.artikal_id AND s.narudzbenica_id = n.narudzbenica_id AND n.kupac_id = (SELECT kupac_id FROM kupac WHERE user = '" + Form1.username + "') " +
                "AND n.narudzbenica_id= (SELECT MAX(narudzbenica_id) FROM narudzbenica) ";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, konekcija);
            DataTable tabela = new DataTable();
            dataAdapter.Fill(tabela);
            dataGridView2.DataSource = tabela;
            dataAdapter.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dodaj();
        }

        private void dodaj()
        {
            if (n == 0)
            {
                MessageBox.Show("MOLIMO VAS KREIRAJTE NARUDZBU");
            }
            else
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (Convert.ToInt32(textBox1.Text) <= 0 || Convert.ToInt32(textBox2.Text) <= 0)
                    {

                        MessageBox.Show("Unesite pozitivne vrijednosti!");
                    }
                    else
                    {
                        String query1 = "SELECT kolicina_stanje from skladiste where artikal_id='"+ textBox1.Text+ "' ";
                        
                        MySqlConnection konekcija1 = new MySqlConnection(konekcioniString);
                        konekcija1.Open();
                        MySqlCommand cmd1 = new MySqlCommand(query1, konekcija1);
                        MySqlDataReader reader;
                        reader = cmd1.ExecuteReader();
                        reader.Read();
                        String kolicina = reader[0].ToString();
                        int br1 = Convert.ToInt32(kolicina);
                        int br2 = Convert.ToInt32(textBox2.Text);
                        if (br2 > br1) 
                        { if (kolicina == null) { MessageBox.Show("Nema na stanju"); } 
                        else { MessageBox.Show("Nema na stanju"); } }
                        else
                        {


                            query =
                                "INSERT INTO stavka_narudzbenica(narudzbenica_id, artikal_id)" +
                                " VALUES ((SELECT MAX(narudzbenica_id) FROM narudzbenica), (SELECT artikal_id FROM artikal WHERE artikal_id =  '" +
                                textBox1.Text + "')); UPDATE stavka_narudzbenica SET kolicina= '" +
                                textBox2.Text + "' WHERE artikal_id ='" + textBox1.Text + "' " +
                                "AND narudzbenica_id = (SELECT MAX(narudzbenica_id) FROM narudzbenica);" +
                                "UPDATE skladiste SET kolicina_stanje = kolicina_stanje - '" +
                                textBox2.Text + "' WHERE artikal_id= '" + textBox1.Text + "' ";


                            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                            konekcija.Open();
                            MySqlCommand cmd = new MySqlCommand(query, konekcija);
                            cmd.ExecuteReader();



                            MessageBox.Show("Uspješno dodano u korpu");

                            konekcija.Close();


                            cijena();
                            pregledKorpe();
                        }
                        reader.Close();
                        konekcija1.Close();
                        
                    }
                }
                else { MessageBox.Show("molimo vas unesite ID narudzbe/kolicinu"); }
            }
            pregledArtikla();
           
            
            
        }

        private void cijena()
        {
           MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = konekcija;
            cmd.CommandText = "SELECT ('" + textBox3.Text + "' + '" + textBox2.Text + 
                "'*a.cijena) FROM stavka_narudzbenica s, artikal a " +
                    "WHERE a.artikal_id = '" + textBox1.Text + "' ";
           
           var broj = cmd.ExecuteScalar().ToString();
              textBox3.Text = (broj).ToString();
           konekcija.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }
        int n=0;
        private void kreirajNarudzbu()
        {
            if (n == 1) { MessageBox.Show("Vec ste kreirali narudzbu"); }
            else
            {
                query = "INSERT INTO narudzbenica(kupac_id) VALUES((SELECT(kupac_id) FROM kupac WHERE user='" + Form1.username + "'))";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(query, konekcija);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Uspješno kreirana NARUDZBA");
                konekcija.Close();
                n = 1;
            }
            pregledKorpe();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kreirajNarudzbu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obrisi();
        }
        private void obrisi()
        {  if(n==1){
            query =
                " UPDATE skladiste SET kolicina_stanje = kolicina_stanje + (SELECT kolicina FROM stavka_narudzbenica WHERE artikal_id='" +
                textBox1.Text + "' AND narudzbenica_id = (SELECT MAX(narudzbenica_id) FROM narudzbenica)) WHERE artikal_id = '" +
                textBox1.Text + "' " ;
            isprazniKorpu();
            oduzimanjeCijene();
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd = new MySqlCommand(query, konekcija);
            cmd.ExecuteNonQuery();
            konekcija.Close();
            pregledKorpe();
            pregledArtikla();}
        else { MessageBox.Show("Niste jos kreirali narudzbu");
        }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void isprazniKorpu()
        {
            String query1 = "DELETE  FROM stavka_narudzbenica   WHERE artikal_id = '" + textBox1.Text +
          "' AND narudzbenica_id IN (SELECT MAX(narudzbenica_id) FROM narudzbenica) ";
            MySqlConnection konekcija = new MySqlConnection(konekcioniString);
            konekcija.Open();
            MySqlCommand cmd1 = new MySqlCommand(query1, konekcija);
            cmd1.ExecuteNonQuery();
            konekcija.Close();
           
        }
        private void oduzimanjeCijene()
        {
           
            if (textBox3.Text == "") { MessageBox.Show("Korpa je prazna"); }
            else
            {
                String lol = "SELECT SUM(s.kolicina*a.cijena) from stavka_narudzbenica s, artikal a " +
                     "WHERE s.artikal_id=a.artikal_id AND s.kolicina>0 AND s.narudzbenica_id= (SELECT MAX(narudzbenica_id) from narudzbenica)";
                MySqlConnection konekcija = new MySqlConnection(konekcioniString);
                konekcija.Open();
                MySqlCommand cmd = new MySqlCommand(lol, konekcija);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                reader.Read();
                String cijena = reader[0].ToString();
                reader.Dispose();
                konekcija.Dispose();
                textBox3.Text = cijena;
            }
        }


    }
}
