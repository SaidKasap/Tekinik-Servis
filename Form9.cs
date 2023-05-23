using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Diagnostics;

namespace TServis1
{
    public partial class Form9 : Form
    {
        private NpgsqlConnection connection;
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
       

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TextBox'lar dolu mu diye kontrol etmek için kullanılacak bool değişkeni tanımlanır.
            bool hata = false;

            // TextBox'lar boş ise hata mesajı gösterilir.
            if (string.IsNullOrEmpty(txtServer.Text))
            {
                MessageBox.Show("Sunucu adı gerekli.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hata = true;
            }

            if (string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show("Port numarası gerekli.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hata = true;
            }

            if (string.IsNullOrEmpty(txtDatabase.Text))
            {
                MessageBox.Show("Veritabanı adı gerekli.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hata = true;
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Kullanıcı adı gerekli.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hata = true;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Parola gerekli.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hata = true;
            }

            // Hata yoksa yapılandırma dosyası kaydedilir.
            if (!hata)
            {
                // TextBox'lardan verileri okuyun
                var host = txtServer.Text;
                var port = int.Parse(txtPort.Text);
                var database = txtDatabase.Text;
                var username = txtUserName.Text;
                var password = txtPassword.Text;

                var jsonObject = new JObject(
                  new JProperty("ConnectionSettings", new JObject(
                  new JProperty("Host", txtServer.Text),
                  new JProperty("Port", txtPort.Text),
                     new JProperty("Database", txtDatabase.Text),
                     new JProperty("Username", txtUserName.Text),
                     new JProperty("Password", txtPassword.Text)
                           ))
                           );

                using (var writer = new StreamWriter("appsettings.json"))
                {
                    writer.Write(jsonObject.ToString());
                }

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
                try
                {
                    // Veritabanı bağlantısı açılır.
                    connection.Open();
                    MessageBox.Show("Bağlantı başarılı!");
                    MessageBox.Show("Kayıt Başarılı");
                    Application.Restart();
                }
                catch (Exception)
                {
                    // Bağlantı hatası mesajı gösterilir.
                    MessageBox.Show("LÜTFEN DOĞRU BİLGİ GİRDİGİNİZDEN EMİN OLUN");
                }

               
            }

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
         
        }
    }
}
