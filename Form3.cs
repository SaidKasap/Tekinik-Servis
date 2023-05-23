using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace TServis1
{
    public partial class Form3 : Form
    {
        private NpgsqlConnection connection;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            // ConfigurationBuilder nesnesi oluşturulur ve appsettings.json dosyasının yolu belirtilir.
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            // IConfigurationRoot nesnesi oluşturulur ve appsettings.json dosyasındaki veriler yüklenir.
            IConfigurationRoot configuration = builder.Build();

            // Verileri okuyarak NpgsqlConnection nesnesi oluşturulur.
            connection = new NpgsqlConnection($"Server={configuration["ConnectionSettings:Host"]};" +
                $"Port={configuration["ConnectionSettings:Port"]};" +
                $"Database={configuration["ConnectionSettings:Database"]};" +
                $"User Id={configuration["ConnectionSettings:Username"]};" +
                $"Password={configuration["ConnectionSettings:Password"]};");

            // katagori veri cekme 
            connection.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from urun", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "ad";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = dt;
            connection.Close();

            //---------------------------------------------//

            connection.Open();
            NpgsqlDataAdapter de = new NpgsqlDataAdapter("select * from personel ", connection);
            DataTable dz = new DataTable();
            de.Fill(dz);
            comboBox3.DisplayMember = "adsoyad";
            comboBox3.ValueMember = "id";
            comboBox3.DataSource = dz;
            connection.Close();

            //--------------------------------------------//
            connection.Open();
            NpgsqlDataAdapter dg = new NpgsqlDataAdapter("select * from sistem ", connection);
            DataTable dx = new DataTable();
            dg.Fill(dx);
            comboBox4.DisplayMember = "ad";
            comboBox4.ValueMember = "id";
            comboBox4.DataSource = dx;
            connection.Close();
            //--------------------------------------------//
            connection.Open();
            NpgsqlDataAdapter dw = new NpgsqlDataAdapter("select * from marka ", connection);
            DataTable dj = new DataTable();
            dw.Fill(dj);
            comboBox1.DisplayMember = "ad";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = dj;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into urun (ad) values (@p1)", connection);
            komut1.Parameters.AddWithValue("@p1", textBox1.Text);
            komut1.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("insert into marka (ad) values (@p1)", connection);
            komut2.Parameters.AddWithValue("@p1", textBox2.Text);
            komut2.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("insert into personel (adsoyad) values (@p1)", connection);
            komut3.Parameters.AddWithValue("@p1", textBox3.Text);
            komut3.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("insert into sistem (ad) values (@p1)", connection);
            komut4.Parameters.AddWithValue("@p1", textBox4.Text);
            komut4.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("Delete From urun where ad=@p5", connection);
            komut5.Parameters.AddWithValue("@p5", comboBox2.Text);
            komut5.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("Delete From marka where ad=@p5", connection);
            komut5.Parameters.AddWithValue("@p5", comboBox1.Text);
            komut5.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut7 = new NpgsqlCommand("Delete From sistem where ad=@p5", connection);
            komut7.Parameters.AddWithValue("@p5", comboBox4.Text);
            komut7.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut6 = new NpgsqlCommand("Delete From personel where adsoyad=@p5", connection);
            komut6.Parameters.AddWithValue("@p5", comboBox3.Text);
            komut6.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
