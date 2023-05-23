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


    public partial class Form4 : Form
    {

        public Form4()
        {
            InitializeComponent();
        }
        private NpgsqlConnection connection;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // FORM 1 DEN FORM 2 YE VERİ CEKTİGİMİZ  KODLAR


        }

        private void lis_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gitarih.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand(" Delete From tdestek where no=@p1", connection);
            komut2.Parameters.AddWithValue("@p1", int.Parse(no.Text));
            komut2.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Servis Formu Silindi");

        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            ad.Text = ad.Text.ToUpper();
            soyad.Text = soyad.Text.ToUpper();

            aciklamatext.Text = aciklamatext.Text.ToUpper();
            sonuctext.Text = sonuctext.Text.ToUpper();
            connection.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("update tdestek  set   ggidistarih=@p2, ggelistarih=@p3, padi=@p4,ueki=@p5,sonuc=@p6,acıklama=@p7, serinoeski=@p8 ,serinoyeni=@p9,firmaadı=@p10,aksesuar=@p11,durum=@p12,garanti=@p13 where no=@p1", connection);
            komut2.Parameters.AddWithValue("@p1", int.Parse(no.Text));
            komut2.Parameters.AddWithValue("@p2", gitarih.Text);
            komut2.Parameters.AddWithValue("@p3", getarih.Text);
            komut2.Parameters.AddWithValue("@p4", padi.Text);
            komut2.Parameters.AddWithValue("@p5", ueki.Text);
            komut2.Parameters.AddWithValue("@p6", sonuctext.Text);
            komut2.Parameters.AddWithValue("@p7", aciklamatext.Text);
            komut2.Parameters.AddWithValue("@p8", eskiseri.Text);
            komut2.Parameters.AddWithValue("@p9", yeniseri.Text);
            komut2.Parameters.AddWithValue("@p10", firma.Text);
            komut2.Parameters.AddWithValue("@p11", aksesuar.Text);
            komut2.Parameters.AddWithValue("@p12", servis.Text);
            komut2.Parameters.AddWithValue("@p13", garantitext.Text);
            komut2.ExecuteNonQuery();

            MessageBox.Show("GÜNCELLEME BAŞARILI");
            //this.Hide();

            connection.Close();
          
        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            getarih.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    
        private void kayıt_Click(object sender, EventArgs e)
        {
            ad.Text = ad.Text.ToUpper();
            soyad.Text = soyad.Text.ToUpper();
            firma.Text = firma.Text.ToUpper();
            aciklamatext.Text = aciklamatext.Text.ToUpper();
            sonuctext.Text = sonuctext.Text.ToUpper();
            connection.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into tdestek (ad,soyad,tel,urun,durum,garanti,personel,tutar,serinoeski,serinoyeni,firmaadı,acıklama,sonuc,tarih,marka,personelislem,personelteslim,aksesuar,sifre,ggidistarih,ggelistarih,padi,ueki) values (@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25)", connection);
            // komut1.Parameters.AddWithValue("@p1", int.Parse(no.Text));

            komut1.Parameters.AddWithValue("@p2", ad.Text);
            komut1.Parameters.AddWithValue("@p3", soyad.Text);
            komut1.Parameters.AddWithValue("@p4", tel.Text);
            komut1.Parameters.AddWithValue("@p5", uruntext.Text);
            komut1.Parameters.AddWithValue("@p6", servis.Text);
            komut1.Parameters.AddWithValue("@p7", garantitext.Text);
            komut1.Parameters.AddWithValue("@p8", kper.Text);
            komut1.Parameters.AddWithValue("@p9", tutar1.Text);
            komut1.Parameters.AddWithValue("@p10", eskiseri.Text);
            komut1.Parameters.AddWithValue("@p11", yeniseri.Text);
            komut1.Parameters.AddWithValue("@p12", firma.Text);
            komut1.Parameters.AddWithValue("@p13", aciklamatext.Text);
            komut1.Parameters.AddWithValue("@p14", sonuctext.Text);
            komut1.Parameters.AddWithValue("@p15", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            komut1.Parameters.AddWithValue("@p16", markatext.Text);
            komut1.Parameters.AddWithValue("@p18", iper.Text);
            komut1.Parameters.AddWithValue("@p19", tper.Text);
            komut1.Parameters.AddWithValue("@p20", aksesuar.Text);
            komut1.Parameters.AddWithValue("@p21", sifre.Text);
            komut1.Parameters.AddWithValue("@p22", gitarih.Text);
            komut1.Parameters.AddWithValue("@p23", getarih.Text);
            komut1.Parameters.AddWithValue("@p24", padi.Text);
            komut1.Parameters.AddWithValue("@p25", ueki.Text);



           

            komut1.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("KAYIT BAŞARILI");
        
        }

        private void Form4_Load(object sender, EventArgs e)
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
            uruntext.DisplayMember = "ad";
            uruntext.ValueMember = "id";
            uruntext.DataSource = dt;
            connection.Close();

            //---------------------------------------------//

            connection.Open();
            NpgsqlDataAdapter de = new NpgsqlDataAdapter("select * from personel ", connection);
            DataTable dz = new DataTable();
            de.Fill(dz);
            kper.DisplayMember = "adsoyad";
            kper.ValueMember = "id";
            kper.DataSource = dz;
            connection.Close();

            connection.Open();
            NpgsqlDataAdapter ge = new NpgsqlDataAdapter("select * from personel ", connection);
            DataTable gz = new DataTable();
            ge.Fill(gz);
            iper.DisplayMember = "adsoyad";
            iper.ValueMember = "id";
            iper.DataSource = gz;
            connection.Close();

            connection.Open();
            NpgsqlDataAdapter qe = new NpgsqlDataAdapter("select * from personel ", connection);
            DataTable qz = new DataTable();
            qe.Fill(qz);
            tper.DisplayMember = "adsoyad";
            tper.ValueMember = "id";
            tper.DataSource = qz;
            connection.Close();

            //--------------------------------------------//
            connection.Open();
            NpgsqlDataAdapter dg = new NpgsqlDataAdapter("select * from marka ", connection);
            DataTable dx = new DataTable();
            dg.Fill(dx);
            markatext.DisplayMember = "ad";
            markatext.ValueMember = "id";
            markatext.DataSource = dx;
            connection.Close();

            //--------------------------------------------//


            connection.Open();
            NpgsqlDataAdapter gez = new NpgsqlDataAdapter("select * from garanti ", connection);
            DataTable gz1 = new DataTable();
            gez.Fill(gz1);
            firma.DisplayMember = "ad";
            firma.ValueMember = "no";
            firma.DataSource = gz1;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.Owner = this;
            f8.Show();
        }

        private void sonuctext_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

