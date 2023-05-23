using Npgsql;
using System;
using QRCoder;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TServis1
{
    public partial class Form2 : Form
    {

        private NpgsqlConnection connection;

        public Form2()
        {
            InitializeComponent();
            tel.KeyPress += new KeyPressEventHandler(tel_KeyPress);
        }

        Random rasgele = new Random();
        int sayi1, sayi2, sayi3, sayi4, sayi5, sayi6, sayi7, sayi8;

        private void button1_Click(object sender, EventArgs e)

        {
            if (ad.Text == "" || String.IsNullOrEmpty(soyad.Text) || String.IsNullOrEmpty(tel.Text))
            {
                MessageBox.Show("BOŞ ALANLARI DOLDURUNUZ");

            }
            else
            {

                ad.Text = ad.Text.ToUpper();
                soyad.Text = soyad.Text.ToUpper();
                firma.Text = firma.Text.ToUpper();
                aciklamatext.Text = aciklamatext.Text.ToUpper();
                sonuctext.Text = sonuctext.Text.ToUpper();

                try
                {
                    connection.Open();
                    string query1 = "SELECT MAX(no) FROM tdestek";
                    NpgsqlCommand command = new NpgsqlCommand(query1, connection);
                    object result2 = command.ExecuteScalar();
                    int newNo = result2 == DBNull.Value ? 1 : Convert.ToInt32(result2) + 1;




                    NpgsqlCommand komut1 = new NpgsqlCommand("insert into tdestek (ad,soyad,tel,urun,durum,garanti,personel,tutar,serinoeski,serinoyeni,firmaadı,acıklama,sonuc,tarih,marka,sistem,personelislem,personelteslim,aksesuar,sifre,bilgi) values (@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22)", connection);
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
                    komut1.Parameters.AddWithValue("@p17", sistemtext.Text);
                    komut1.Parameters.AddWithValue("@p18", iper.Text);
                    komut1.Parameters.AddWithValue("@p19", tper.Text);
                    komut1.Parameters.AddWithValue("@p20", aksesuar.Text);
                    komut1.Parameters.AddWithValue("@p21", sifre.Text);
                    komut1.Parameters.AddWithValue("@p22", bilgitext.Text);

                    komut1.ExecuteNonQuery();
                    string checkQuery = "SELECT COUNT(*) FROM musteri WHERE tel = @p4";
                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@p4", tel.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    NpgsqlCommand komut2 = new NpgsqlCommand("insert into musteri (no, ad, soyad, tel) values (@p1, @p2, @p3, @p4)", connection);
                    komut2.Parameters.AddWithValue("@p1", newNo);
                    komut2.Parameters.AddWithValue("@p2", ad.Text);
                    komut2.Parameters.AddWithValue("@p3", soyad.Text);
                    komut2.Parameters.AddWithValue("@p4", tel.Text);
                    komut2.ExecuteNonQuery();


                    this.Hide();
                    Form1 f1 = new Form1();
                    f1.Owner = this;

                    connection.Close();
                    MessageBox.Show("KAYIT BAŞARILI");
                }
                catch
                {
                    connection.Close();
                    MessageBox.Show("Lütfen geçerli bir sayı girin.");
                }

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            tel.KeyPress += new KeyPressEventHandler(tel_KeyPress);
            if (ad.Text == "" || String.IsNullOrEmpty(soyad.Text) || String.IsNullOrEmpty(tel.Text))
            {
                MessageBox.Show("BOŞ ALANLARI DOLDURUNUZ");

            }
            else if (no.Text == "" || String.IsNullOrEmpty(ktarih.Text))
            {


                string Veri = (tel.Text + tno.Text);

                MD5 md5 = new MD5CryptoServiceProvider();

                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Veri));

                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }
                string cikti = strBuilder.ToString();

                string query = ("http://10.1.1.36/aa/sorgula.php?sorgu=" + cikti + "");
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(query, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(3);

                qrcode.Image = qrCodeImage;


                ad.Text = ad.Text.ToUpper();
                soyad.Text = soyad.Text.ToUpper();
                firma.Text = firma.Text.ToUpper();
                aciklamatext.Text = aciklamatext.Text.ToUpper();
                sonuctext.Text = sonuctext.Text.ToUpper();
                try
                {
                    connection.Open();

                    NpgsqlCommand komut1 = new NpgsqlCommand("insert into tdestek ( ad, soyad, tel, urun, durum, garanti, personel, tutar, serinoeski, serinoyeni, firmaadı, acıklama, sonuc, tarih, marka, sistem, personelislem, personelteslim, aksesuar, sifre, bilgi, odmtur, veri) values ( @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23, @p24)", connection);

                    //komut1.Parameters.AddWithValue("@p1", newNo);
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
                    komut1.Parameters.AddWithValue("@p15", DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss"));
                    komut1.Parameters.AddWithValue("@p16", markatext.Text);
                    komut1.Parameters.AddWithValue("@p17", sistemtext.Text);
                    komut1.Parameters.AddWithValue("@p18", iper.Text);
                    komut1.Parameters.AddWithValue("@p19", tper.Text);
                    komut1.Parameters.AddWithValue("@p20", aksesuar.Text);
                    komut1.Parameters.AddWithValue("@p21", sifre.Text);
                    komut1.Parameters.AddWithValue("@p22", bilgitext.Text);
                    komut1.Parameters.AddWithValue("@p23", otur.Text);
                    komut1.Parameters.AddWithValue("@p24", cikti);
                    komut1.ExecuteNonQuery();

                    bool musteriKaydiVar = false;
                    NpgsqlCommand kontrolKomutu = new NpgsqlCommand("select count(*) from musteri where tel = @p1", connection);
                    kontrolKomutu.Parameters.AddWithValue("@p1", tel.Text);
                    int kayitSayisi = Convert.ToInt32(kontrolKomutu.ExecuteScalar());
                    if (kayitSayisi > 0)
                    {
                        musteriKaydiVar = true;
                    }
                    string checkQuery = "SELECT COUNT(*) FROM musteri WHERE tel = @p4";
                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@p4", tel.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (!musteriKaydiVar)
                    {
                        NpgsqlCommand komut3 = new NpgsqlCommand("insert into musteri ( ad, soyad, tel) values ( @p2, @p3, @p4)", connection);

                        komut3.Parameters.AddWithValue("@p2", ad.Text);
                        komut3.Parameters.AddWithValue("@p3", soyad.Text);
                        komut3.Parameters.AddWithValue("@p4", tel.Text);
                        komut3.ExecuteNonQuery();
                    }


                    teslimtarih.Text = DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss");
                    this.Hide();
                    Form1 f1 = new Form1();
                    f1.Owner = this;

                    connection.Close();
                    MessageBox.Show("KAYIT BAŞARILI");



                }
                catch (FormatException)
                {
                    connection.Close();
                    MessageBox.Show("Lütfen geçerli bir sayı girin.");
                }
            }




            else
            {
                try
                {
                    string Veri = (tel.Text + tno.Text);

                    MD5 md5 = new MD5CryptoServiceProvider();

                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Veri));

                    byte[] result = md5.Hash;
                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        strBuilder.Append(result[i].ToString("x2"));
                    }
                    string cikti = strBuilder.ToString();


                    string query = ("http://10.1.1.36/aa/sorgula.php?sorgu=" + cikti + "");


                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(query, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(3);
                    qrcode.Image = qrCodeImage;

                    ad.Text = ad.Text.ToUpper();
                    soyad.Text = soyad.Text.ToUpper();
                    firma.Text = firma.Text.ToUpper();
                    aciklamatext.Text = aciklamatext.Text.ToUpper();
                    sonuctext.Text = sonuctext.Text.ToUpper();


                    connection.Open();
                    string query1 = "SELECT MAX(no) FROM tdestek";
                    NpgsqlCommand command = new NpgsqlCommand(query1, connection);
                    object result2 = command.ExecuteScalar();
                    int newNo = result2 == DBNull.Value ? 1 : Convert.ToInt32(result2) + 1;


                    NpgsqlCommand komut2 = new NpgsqlCommand("update tdestek set  ad=@p2, soyad=@p3, tel=@p4, acıklama=@p5,sonuc=@p6, serinoeski=@p7,serinoyeni=@p8,firmaadı=@p9, tutar=@p10,durum=@p11,personel=@p12,personelislem=@p13,personelteslim=@p14, aksesuar=@p15,sifre=@p16,urun=@p17,marka=@p18,sistem=@p19,garanti=@p20,bilgi=@p21,odmtur=@p22,teslimtarih=@p23,veri=@p24 where no=@p1 ", connection);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(no.Text));
                    komut2.Parameters.AddWithValue("@p2", ad.Text);
                    komut2.Parameters.AddWithValue("@p3", soyad.Text);
                    komut2.Parameters.AddWithValue("@p4", tel.Text);
                    komut2.Parameters.AddWithValue("@p5", aciklamatext.Text);
                    komut2.Parameters.AddWithValue("@p6", sonuctext.Text);
                    komut2.Parameters.AddWithValue("@p7", eskiseri.Text);
                    komut2.Parameters.AddWithValue("@p8", yeniseri.Text);
                    komut2.Parameters.AddWithValue("@p9", firma.Text);
                    komut2.Parameters.AddWithValue("@p10", tutar1.Text);
                    komut2.Parameters.AddWithValue("@p11", servis.Text);
                    komut2.Parameters.AddWithValue("@p12", kper.Text);
                    komut2.Parameters.AddWithValue("@p13", iper.Text);
                    komut2.Parameters.AddWithValue("@p14", tper.Text);
                    komut2.Parameters.AddWithValue("@p15", aksesuar.Text);
                    komut2.Parameters.AddWithValue("@p16", sifre.Text);
                    komut2.Parameters.AddWithValue("@p17", uruntext.Text);
                    komut2.Parameters.AddWithValue("@p18", markatext.Text);
                    komut2.Parameters.AddWithValue("@p19", sistemtext.Text);
                    komut2.Parameters.AddWithValue("@p20", garantitext.Text);
                    komut2.Parameters.AddWithValue("@p21", bilgitext.Text);
                    komut2.Parameters.AddWithValue("@p22", otur.Text);
                    komut2.Parameters.AddWithValue("@p23", DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss"));
                    komut2.Parameters.AddWithValue("@p24", cikti);
                    komut2.ExecuteNonQuery();
                    teslimtarih.Text = DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss");

                    bool musteriKaydiVar = false;
                    NpgsqlCommand kontrolKomutu = new NpgsqlCommand("select count(*) from musteri where tel = @p1", connection);
                    kontrolKomutu.Parameters.AddWithValue("@p1", tel.Text);
                    int kayitSayisi = Convert.ToInt32(kontrolKomutu.ExecuteScalar());
                    if (kayitSayisi > 0)
                    {
                        musteriKaydiVar = true;
                    }
                    string checkQuery = "SELECT COUNT(*) FROM musteri WHERE tel = @p4";
                    NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@p4", tel.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (!musteriKaydiVar)
                    {
                        NpgsqlCommand komut3 = new NpgsqlCommand("insert into musteri (no, ad, soyad, tel) values (@p1, @p2, @p3, @p4)", connection);
                        komut3.Parameters.AddWithValue("@p1", newNo);
                        komut3.Parameters.AddWithValue("@p2", ad.Text);
                        komut3.Parameters.AddWithValue("@p3", soyad.Text);
                        komut3.Parameters.AddWithValue("@p4", tel.Text);
                        komut3.ExecuteNonQuery();
                    }

                    this.Hide();
                    Form1 f1 = new Form1();
                    f1.Owner = this;
                    connection.Close();
                    MessageBox.Show("GÜNCELLEME BAŞARILI");
                }
                catch (FormatException)
                {
                    connection.Close();
                    MessageBox.Show("Lütfen geçerli bir kayıt oluşturun.");
                }

            }

        }



        public void SetData(string no, string ad, string soyad, string tel)
        {
            this.no.Text = no;
            this.ad.Text = ad;
            this.soyad.Text = soyad;
            this.tel.Text = tel;
        }

        public void frm2()
        {
            try
            {
                connection.Open();
                int noValue = int.Parse(no.Text);
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM tdestek WHERE no = @noValue", connection);
                cmd.Parameters.AddWithValue("noValue", noValue);
                NpgsqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ad.Text = read["ad"].ToString();
                    soyad.Text = read["soyad"].ToString();
                    tel.Text = read["tel"].ToString();
                    uruntext.Text = read["urun"].ToString();
                    servis.Text = read["durum"].ToString();
                    garantitext.Text = read["garanti"].ToString();
                    kper.Text = read["personel"].ToString();
                    tutar1.Text = read["tutar"].ToString();
                    eskiseri.Text = read["serinoeski"].ToString();
                    yeniseri.Text = read["serinoyeni"].ToString();
                    firma.Text = read["firmaadı"].ToString();
                    aciklamatext.Text = read["acıklama"].ToString();
                    sonuctext.Text = read["sonuc"].ToString();
                    markatext.Text = read["marka"].ToString();
                    sistemtext.Text = read["sistem"].ToString();
                    iper.Text = read["personelislem"].ToString();
                    tper.Text = read["personelteslim"].ToString();
                    aksesuar.Text = read["aksesuar"].ToString();
                    sifre.Text = read["sifre"].ToString();
                    bilgitext.Text = read["bilgi"].ToString();
                    ktarih.Text = read["tarih"].ToString();
                    otur.Text = read["odmtur"].ToString();

                }
                connection.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("PostgreSQL Hatası: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }




        }

        private void Form2_Load(object sender, EventArgs e)
        {







            no.ReadOnly = true;
            ad.Select();
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

            if (tno.Text == "label26")
            {
                sayi1 = rasgele.Next(1, 9);
                sayi2 = rasgele.Next(1, 9);
                sayi3 = rasgele.Next(1, 9);
                sayi4 = rasgele.Next(1, 9);
                sayi5 = rasgele.Next(1, 9);
                sayi6 = rasgele.Next(1, 9);
                sayi7 = rasgele.Next(1, 9);
                sayi8 = rasgele.Next(1, 9);

                tno.Text = sayi1.ToString() + sayi2.ToString() + sayi3.ToString() + sayi4.ToString() + sayi5.ToString() + sayi6.ToString() + sayi7.ToString() + sayi8.ToString();

            }


            // katagori veri cekme 
            connection.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from urun ORDER BY id ASC", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            uruntext.DisplayMember = "ad";
            uruntext.ValueMember = "id";
            uruntext.DataSource = dt;
            connection.Close();

            //---------------------------------------------//

            connection.Open();
            NpgsqlDataAdapter de = new NpgsqlDataAdapter("select * from personel ORDER BY id ASC ", connection);
            DataTable dz = new DataTable();
            de.Fill(dz);
            kper.DisplayMember = "adsoyad";
            kper.ValueMember = "id";
            kper.DataSource = dz;
            connection.Close();

            connection.Open();
            NpgsqlDataAdapter ge = new NpgsqlDataAdapter("select * from personel ORDER BY id ASC ", connection);
            DataTable gz = new DataTable();
            ge.Fill(gz);
            iper.DisplayMember = "adsoyad";
            iper.ValueMember = "id";
            iper.DataSource = gz;
            connection.Close();

            connection.Open();
            NpgsqlDataAdapter qe = new NpgsqlDataAdapter("select * from personel  ORDER BY id ASC ", connection);
            DataTable qz = new DataTable();
            qe.Fill(qz);
            tper.DisplayMember = "adsoyad";
            tper.ValueMember = "id";
            tper.DataSource = qz;
            connection.Close();

            //--------------------------------------------//
            connection.Open();
            NpgsqlDataAdapter dg = new NpgsqlDataAdapter("select * from marka ORDER BY id ASC ", connection);
            DataTable dx = new DataTable();
            dg.Fill(dx);
            markatext.DisplayMember = "ad";
            markatext.ValueMember = "id";
            markatext.DataSource = dx;
            connection.Close();

            //--------------------------------------------//
            connection.Open();
            NpgsqlDataAdapter sg = new NpgsqlDataAdapter("select * from sistem ORDER BY id ASC", connection);
            DataTable sx = new DataTable();
            sg.Fill(sx);
            sistemtext.DisplayMember = "ad";
            sistemtext.ValueMember = "id";
            sistemtext.DataSource = sx;
            connection.Close();

            connection.Open();
            NpgsqlDataAdapter gez = new NpgsqlDataAdapter("select * from garanti ORDER BY no ASC ", connection);
            DataTable gz1 = new DataTable();
            gez.Fill(gz1);
            firma.DisplayMember = "ad";
            firma.ValueMember = "no";
            firma.DataSource = gz1;
            connection.Close();



        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sistem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void marka_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void urun_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void urun_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {


        }

        private void tel_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            tel.KeyPress += new KeyPressEventHandler(tel_KeyPress);
            if (ad.Text == "" || String.IsNullOrEmpty(soyad.Text) || String.IsNullOrEmpty(tel.Text))
            {
                MessageBox.Show("BOŞ ALANLARI DOLDURUNUZ");

            }
            else if (no.Text == "" || String.IsNullOrEmpty(ktarih.Text))
            {
                string Veri = (tel.Text + tno.Text);

                MD5 md5 = new MD5CryptoServiceProvider();

                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Veri));

                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }
                string cikti = strBuilder.ToString();

                /*string query = ("http://10.1.1.36/aa/sorgula.php?sorgu=" + cikti + "");
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(query, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(3);
               
                qrcode.Image = qrCodeImage;

             */

                ad.Text = ad.Text.ToUpper();
                soyad.Text = soyad.Text.ToUpper();
                firma.Text = firma.Text.ToUpper();
                aciklamatext.Text = aciklamatext.Text.ToUpper();
                sonuctext.Text = sonuctext.Text.ToUpper();
                connection.Open();
                string query1 = "SELECT MAX(no) FROM tdestek";
                NpgsqlCommand command = new NpgsqlCommand(query1, connection);
                object result2 = command.ExecuteScalar();
                int newNo = result2 == DBNull.Value ? 1 : Convert.ToInt32(result2) + 1;
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into tdestek (ad,soyad,tel,urun,durum,garanti,personel,tutar,serinoeski,serinoyeni,firmaadı,acıklama,sonuc,tarih,marka,sistem,personelislem,personelteslim,aksesuar,sifre,bilgi,tno,odmtur,veri) values (@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25)", connection);
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
                komut1.Parameters.AddWithValue("@p15", DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss"));
                komut1.Parameters.AddWithValue("@p16", markatext.Text);
                komut1.Parameters.AddWithValue("@p17", sistemtext.Text);
                komut1.Parameters.AddWithValue("@p18", iper.Text);
                komut1.Parameters.AddWithValue("@p19", tper.Text);
                komut1.Parameters.AddWithValue("@p20", aksesuar.Text);
                komut1.Parameters.AddWithValue("@p21", sifre.Text);
                komut1.Parameters.AddWithValue("@p22", bilgitext.Text);
                komut1.Parameters.AddWithValue("@p23", tno.Text);
                komut1.Parameters.AddWithValue("@p24", otur.Text);
                komut1.Parameters.AddWithValue("@p25", cikti);
                komut1.ExecuteNonQuery();
                bool musteriKaydiVar = false;
                NpgsqlCommand kontrolKomutu = new NpgsqlCommand("select count(*) from musteri where tel = @p1", connection);
                kontrolKomutu.Parameters.AddWithValue("@p1", tel.Text);
                int kayitSayisi = Convert.ToInt32(kontrolKomutu.ExecuteScalar());
                if (kayitSayisi > 0)
                {
                    musteriKaydiVar = true;
                }
                string checkQuery = "SELECT COUNT(*) FROM musteri WHERE tel = @p4";
                NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@p4", tel.Text);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (!musteriKaydiVar)
                {
                    NpgsqlCommand komut3 = new NpgsqlCommand("insert into musteri (no, ad, soyad, tel) values (@p1, @p2, @p3, @p4)", connection);
                    komut3.Parameters.AddWithValue("@p1", newNo);
                    komut3.Parameters.AddWithValue("@p2", ad.Text);
                    komut3.Parameters.AddWithValue("@p3", soyad.Text);
                    komut3.Parameters.AddWithValue("@p4", tel.Text);
                    komut3.ExecuteNonQuery();
                }


                this.Hide();
                Form1 f1 = new Form1();
                f1.Owner = this;
                connection.Close();
                MessageBox.Show("KAYIT BAŞARILI");
                printPreviewDialog1.ShowDialog();






            }


            else
            {
                string Veri = (tel.Text + tno.Text);

                MD5 md5 = new MD5CryptoServiceProvider();

                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Veri));

                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }
                string cikti = strBuilder.ToString();



                string query = ("http://10.1.1.36/aa/sorgula.php?sorgu=" + cikti + "");
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(query, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(3);
                qrcode.Image = qrCodeImage;

                ad.Text = ad.Text.ToUpper();
                soyad.Text = soyad.Text.ToUpper();
                firma.Text = firma.Text.ToUpper();
                aciklamatext.Text = aciklamatext.Text.ToUpper();
                sonuctext.Text = sonuctext.Text.ToUpper();
                connection.Open();
                string query1 = "SELECT MAX(no) FROM tdestek";
                NpgsqlCommand command = new NpgsqlCommand(query1, connection);
                object result2 = command.ExecuteScalar();
                int newNo = result2 == DBNull.Value ? 1 : Convert.ToInt32(result2) + 1;
                NpgsqlCommand komut2 = new NpgsqlCommand("update tdestek set  ad=@p2, soyad=@p3, tel=@p4, acıklama=@p5,sonuc=@p6, serinoeski=@p7,serinoyeni=@p8,firmaadı=@p9, tutar=@p10,durum=@p11,personel=@p12,personelislem=@p13,personelteslim=@p14, aksesuar=@p15,sifre=@p16,urun=@p17,marka=@p18,sistem=@p19,garanti=@p20,bilgi=@p21,odmtur=@p22,teslimtarih=@p23,veri=@p24 where no=@p1 ", connection);
                komut2.Parameters.AddWithValue("@p1", int.Parse(no.Text));
                komut2.Parameters.AddWithValue("@p2", ad.Text);
                komut2.Parameters.AddWithValue("@p3", soyad.Text);
                komut2.Parameters.AddWithValue("@p4", tel.Text);
                komut2.Parameters.AddWithValue("@p5", aciklamatext.Text);
                komut2.Parameters.AddWithValue("@p6", sonuctext.Text);
                komut2.Parameters.AddWithValue("@p7", eskiseri.Text);
                komut2.Parameters.AddWithValue("@p8", yeniseri.Text);
                komut2.Parameters.AddWithValue("@p9", firma.Text);
                komut2.Parameters.AddWithValue("@p10", tutar1.Text);
                komut2.Parameters.AddWithValue("@p11", servis.Text);
                komut2.Parameters.AddWithValue("@p12", kper.Text);
                komut2.Parameters.AddWithValue("@p13", iper.Text);
                komut2.Parameters.AddWithValue("@p14", tper.Text);
                komut2.Parameters.AddWithValue("@p15", aksesuar.Text);
                komut2.Parameters.AddWithValue("@p16", sifre.Text);
                komut2.Parameters.AddWithValue("@p17", uruntext.Text);
                komut2.Parameters.AddWithValue("@p18", markatext.Text);
                komut2.Parameters.AddWithValue("@p19", sistemtext.Text);
                komut2.Parameters.AddWithValue("@p20", garantitext.Text);
                komut2.Parameters.AddWithValue("@p21", bilgitext.Text);
                komut2.Parameters.AddWithValue("@p22", otur.Text);
                komut2.Parameters.AddWithValue("@p23", DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss"));
                komut2.Parameters.AddWithValue("@p24", cikti);
                komut2.ExecuteNonQuery();
                teslimtarih.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                bool musteriKaydiVar = false;
                NpgsqlCommand kontrolKomutu = new NpgsqlCommand("select count(*) from musteri where tel = @p1", connection);
                kontrolKomutu.Parameters.AddWithValue("@p1", tel.Text);
                int kayitSayisi = Convert.ToInt32(kontrolKomutu.ExecuteScalar());
                if (kayitSayisi > 0)
                {
                    musteriKaydiVar = true;
                }
                string checkQuery = "SELECT COUNT(*) FROM musteri WHERE tel = @p4";
                NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@p4", tel.Text);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (!musteriKaydiVar)
                {
                    NpgsqlCommand komut3 = new NpgsqlCommand("insert into musteri (no, ad, soyad, tel) values (@p1, @p2, @p3, @p4)", connection);
                    komut3.Parameters.AddWithValue("@p1", newNo);
                    komut3.Parameters.AddWithValue("@p2", ad.Text);
                    komut3.Parameters.AddWithValue("@p3", soyad.Text);
                    komut3.Parameters.AddWithValue("@p4", tel.Text);
                    komut3.ExecuteNonQuery();
                }

                this.Hide();

                Form1 f1 = new Form1();
                f1.Owner = this;

                connection.Close();
                MessageBox.Show("GÜNCELLEME BAŞARILI");
                printPreviewDialog1.ShowDialog();



            }







        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                this.qrcode.Size = new System.Drawing.Size(140, 140);

                ad.Text = ad.Text.ToUpper();
                soyad.Text = soyad.Text.ToUpper();
                firma.Text = firma.Text.ToUpper();
                aciklamatext.Text = aciklamatext.Text.ToUpper();
                sonuctext.Text = sonuctext.Text.ToUpper();
                Font font1 = new Font("Calibri", 6, FontStyle.Bold);
                SolidBrush firca1 = new SolidBrush(Color.Black);
                Pen kalem = new Pen(Color.Black);
                SolidBrush firca = new SolidBrush(Color.Black);
                e.Graphics.DrawImage(pictureBox1.Image, 210, 5);//logo

                e.Graphics.DrawImage(qrcode.Image, 350, 400);//QR CODE
                e.Graphics.DrawString("Dilara Bilgisayar Elektronik Reklamcılık İnşaat Turizm...", font1, firca1, 210, 30);
                e.Graphics.DrawString("Kutlubey Mh. 6 Mart Atatürk Cd. No:12/C Merkez/ISPARTA", font1, firca1, 210, 40);
                e.Graphics.DrawString("www.dilarabilgisayar.com", font1, firca1, 275, 10);
                e.Graphics.DrawString("TEL : 0246 232 61 99", font1, firca1, 500, 10);
                e.Graphics.DrawString("info@dilarabilgisayar.com", font1, firca1, 275, 20);
                e.Graphics.DrawLine(kalem, 205, 600, 205, 5); // SOL DİK ÇİZGİ
                e.Graphics.DrawLine(kalem, 600, 600, 600, 5); // SAĞ DİK ÇİZGİ
                e.Graphics.DrawLine(kalem, 205, 5, 600, 5); // LOGO ÜSTÜ yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 50, 600, 50); // ADRES ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 70, 600, 70); // FORM TAKIP NO ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 90, 600, 90); // AD SOYAD ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 150, 600, 150); // ACIKLAMA ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 230, 600, 230); // SONUC ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 400, 150, 400, 50); // ORTA DİK ÇİZGİ
                e.Graphics.DrawLine(kalem, 205, 5, 600, 5); // SAĞ SOL DİK CİZGİ
                e.Graphics.DrawLine(kalem, 200, 600, 600, 600); // SAĞ SOL DİK CİZGİ
                e.Graphics.DrawLine(kalem, 205, 110, 600, 110); // LOGO ÜSTÜ yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 130, 600, 130); // LOGO ÜSTÜ yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 320, 600, 320); // SONUC ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 170, 600, 170); // ACIKLAMA ALTI yatay düz cizgi 
                e.Graphics.DrawLine(kalem, 205, 350, 600, 350); // SONUC ALTI yatay düz cizgi 

                e.Graphics.DrawString("TEL NO:", font1, firca, 210, 55);
                e.Graphics.DrawString("TAKİP NO:", font1, firca, 405, 55);
                e.Graphics.DrawString("AD:", font1, firca, 210, 75);
                e.Graphics.DrawString("ÜRÜN:", font1, firca, 405, 75); //ÜRÜN
                e.Graphics.DrawString("SOYAD:", font1, firca, 210, 95); //SOYAD
                e.Graphics.DrawString("MARKA:", font1, firca, 405, 95);
                e.Graphics.DrawString("ÖNEMLİ BİLGİ:", font1, firca, 210, 115);
                e.Graphics.DrawString("AKSESUAR:", font1, firca, 210, 135);
                e.Graphics.DrawString("SİSTEM:", font1, firca, 405, 115);
                e.Graphics.DrawString("ŞİFRE:", font1, firca, 405, 135);
                e.Graphics.DrawString("PERSONEL:", font1, firca, 210, 155);
                e.Graphics.DrawString("ARIZA:", font1, firca, 210, 175);
                e.Graphics.DrawString("SONUÇ:", font1, firca, 210, 235);
                e.Graphics.DrawString("GARANTİ:", font1, firca, 210, 330);
                e.Graphics.DrawString("ÜCRET:", font1, firca, 405, 330);
                e.Graphics.DrawString("KAYIT TARİHİ:", font1, firca, 400, 155);
                Font font = new Font("Bahnschrift", 8, FontStyle.Bold);


                // SORGU CIKTISI
                e.Graphics.DrawString(ad.Text, font, firca, 248, 75);
                e.Graphics.DrawString(kper.Text, font, firca, 248, 155);
                e.Graphics.DrawString(uruntext.Text, font, firca, 445, 75);//uruntext
                e.Graphics.DrawString(soyad.Text, font, firca, 248, 95);//soyad
                e.Graphics.DrawString(tel.Text, font, firca, 248, 55);
                e.Graphics.DrawString(markatext.Text, font, firca, 445, 95);
                e.Graphics.DrawString(bilgitext.Text, font, firca, 260, 115);
                e.Graphics.DrawString(sistemtext.Text, font, firca, 445, 115);
                e.Graphics.DrawString(sifre.Text, font, firca, 445, 135);

                e.Graphics.DrawString(tno.Text, font, firca, 450, 55);
                e.Graphics.DrawString(aciklamatext.Text, font, firca, 247, 180); // acıklama
                e.Graphics.DrawString(sonuctext.Text, font, firca, 248, 230); // sonuc
                e.Graphics.DrawString(aksesuar.Text, font, firca, 248, 135); // aksesuar

                e.Graphics.DrawString(garantitext.Text, font, firca, 330, 330); //garanti
                Font font4 = new Font("Arial", 8, FontStyle.Bold);                                                            //
                e.Graphics.DrawString(tutar1.Text + "  " + "₺", font4, firca, 485, 330); //ÜCRET

                // e.Graphics.DrawString(ktarih.Text, font, firca, 450, 155);
                e.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font1, firca, 450, 155);


                Font font3 = new Font("Times New Roman", 6, FontStyle.Bold);

                e.Graphics.DrawString("- 15 Gün içinde alınmayan ürünlerden firmamız sorumlu değildir.", font3, firca, 220, 355);
                e.Graphics.DrawString("- Verdiğiniz ürünlerdeki kişisel bilgileriniz yedelenmiş sayılır. Veri kaybında firmamız sorunlu değildir.", font3, firca, 220, 365);
                e.Graphics.DrawString("- Ürününüzün garantiye gitmesi esnasında kargoda oluşabilecek hasarlardan firmamız sorumlu değildir. ", font3, firca, 220, 375);
                e.Graphics.DrawString("QR KODU İLE  ÜRÜNÜNÜZÜN DURUMU ÖĞRENEBİLİRSİNİZ ", font3, firca, 300, 400);


            }
            catch (Exception)
            {

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tel_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tel_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from tdestek where tel like '%" + tel.Text + "%'", connection);
                NpgsqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ad.Text = read["ad"].ToString();
                    soyad.Text = read["soyad"].ToString();
                    tel.Text = read["tel"].ToString();
                }


                connection.Close();
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            tutar1.Text = "100";
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            tutar1.Text = "200";
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            tutar1.Text = "300";
        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void ad_KeyDown(object sender, KeyEventArgs e)
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            Form11 f8 = new Form11();
            f8.Owner = this;
            f8.Show();
        }

        private void tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '(') && (e.KeyChar != ')') && (e.KeyChar != '-') && (e.KeyChar != ' '))
            {
                e.Handled = true;
            }

            // Sadece bir kapanış parantezi veya tire girilmesini önler
            if ((e.KeyChar == ')') && (tel.Text.IndexOf('(') == -1))
            {
                e.Handled = true;
            }
            else if ((e.KeyChar == '-') && (tel.Text.IndexOf('(') == -1 || tel.Text.IndexOf(')') == -1))
            {
                e.Handled = true;
            }

            // En fazla 14 karaktere izin verir
            if (tel.Text.Length >= 14 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tel_Enter(object sender, EventArgs e)
        {

        }

        private void tel_Leave(object sender, EventArgs e)
        {

        }

        private void tel_Enter_1(object sender, EventArgs e)
        {

        }

        private void Form2_Leave(object sender, EventArgs e)
        {

        }

        private void tutar_TextChanged(object sender, EventArgs e)
        {

        }

        private void tutar1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MaskedTextBox mt = new MaskedTextBox();
            mt.Mask = "999.990,00 ₺";
            mt.RightToLeft = RightToLeft.Yes;
            this.Controls.Add(mt);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_2(object sender, EventArgs e)
        {

            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("SİLMEK İSTEDİGİNNİZDEN EMİNMİSİNİZ?", "ÇIKIŞ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                connection.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand(" Delete From tdestek where no=@p1", connection);
                komut2.Parameters.AddWithValue("@p1", int.Parse(no.Text));
                komut2.ExecuteNonQuery();

                MessageBox.Show("Servis Formu Silindi");

                this.Hide();
                connection.Close();
            }
            if (secim == DialogResult.No)
            {
                this.Hide();
            }




        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void sonuctext_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
