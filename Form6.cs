using System;
using Npgsql;
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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private NpgsqlConnection connection;
        private void button1_Click(object sender, EventArgs e)
        {
            ad.Text = ad.Text.ToUpper();
            tel.Text = tel.Text.ToUpper();
            adres.Text = adres.Text.ToUpper();

            connection.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into garanti (ad,ftel,fadres,sacıklama) values (@p2,@p3,@p4,@p5)", connection);
            // komut1.Parameters.AddWithValue("@p1", int.Parse(no.Text));

            komut1.Parameters.AddWithValue("@p2", ad.Text);
            komut1.Parameters.AddWithValue("@p3", tel.Text);
            komut1.Parameters.AddWithValue("@p4", adres.Text);
            komut1.Parameters.AddWithValue("@p5", srmlu.Text);

            komut1.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("KAYIT BAŞARILI");
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
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

        }
    }
}
