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
    public partial class Form7 : Form
    {
        // PG connectionSI
        private NpgsqlConnection connection;
        public Form7()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("Delete From garanti where ad=@p5", connection);
            komut5.Parameters.AddWithValue("@p5", comboBox2.Text);
            komut5.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("basarılı");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
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

            connection.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from garanti", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "ad";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = dt;
            connection.Close();
        }
    }
}
