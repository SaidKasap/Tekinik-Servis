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
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Diagnostics;


namespace TServis1
{

    public partial class Form1 : Form
    {
        private NpgsqlConnection connection;

        public Form1()
        {
            InitializeComponent();
        }
        // PG connectionSI




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.Show();
            f2.no.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            f2.frm2();


            /*f2.ad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            f2.soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            f2.tel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();           
            f2.uruntext.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            f2.markatext.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            f2.sistemtext.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            f2.garantitext.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            f2.tutar1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            f2.servis.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            f2.ktarih.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            f2.bilgitext.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            f2.sonuctext.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            f2.aciklamatext.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            f2.iper.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
         
            f2.aksesuar.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
            f2.firma.Text = dataGridView1.CurrentRow.Cells[17].Value.ToString();
            f2.eskiseri.Text = dataGridView1.CurrentRow.Cells[18].Value.ToString();
            f2.yeniseri.Text = dataGridView1.CurrentRow.Cells[19].Value.ToString();
            f2.sifre.Text = dataGridView1.CurrentRow.Cells[20].Value.ToString();
            f2.teslimtarih.Text = dataGridView1.CurrentRow.Cells[22].Value.ToString();
            f2.kper.Text = dataGridView1.CurrentRow.Cells[23].Value.ToString();
            f2.tno.Text = dataGridView1.CurrentRow.Cells[24].Value.ToString();
           // f2.tper.Text = dataGridView1.CurrentRow.Cells[23].Value.ToString();*/

        }

        private void lis_Click(object sender, EventArgs e)
        {
            //FORM1  ANA EKRANA VERİ CEKTİGİMİZ  zzzz

            string sorgu = "SELECT * FROM tdestek WHERE durum NOT LIKE 'TESLİM EDİLDİ%' and durum NOT LIKE 'GARANTİDEPO%' and ad NOT LIKE 'ŞİRKET%' and durum NOT LIKE 'GARANTİ FİRMA%' order by no desc limit 50 ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }


        private void button1_Click(object sender, EventArgs e)
        {


            //YENİ KAYIT EKRAN KODU
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.Show();
            string query = ("localhost/aa/sorgulama.php?tel=" + f2.tel.Text + "&tno =" + f2.tno.Text + "");
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(query, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(1);
            f2.qrcode.Image = qrCodeImage;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 1300;
            this.Height = 800;
            WindowState = FormWindowState.Normal;
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                IConfigurationRoot configuration = builder.Build();

                connection = new NpgsqlConnection($"Server={configuration["ConnectionSettings:Host"]};" +
                    $"Port={configuration["ConnectionSettings:Port"]};" +
                    $"Database={configuration["ConnectionSettings:Database"]};" +
                    $"User Id={configuration["ConnectionSettings:Username"]};" +
                    $"Password={configuration["ConnectionSettings:Password"]};");
            }
            catch (Exception)
            {
                MessageBox.Show("DATABASE AYARLI DEGİL LÜTFEN ÖNCE AYARLARI YAPIN");
                Form9 f9 = new Form9();
                f9.Owner = this;
                f9.Show();
            }


        }

        private void gARANTİDEPOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Owner = this;
            f5.Show();
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kATAGORİVEPERSONELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Owner = this;
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            string query = @"SELECT no, ad, soyad, tel, urun, marka, sistem, garanti, tutar, durum, tarih, bilgi, sonuc, açıklama, personelislem, personelteslim, aksesuar, firma, serinoeski, serinoyeni, sifre, teslimtarih, personel, tno 
                     FROM tdestek 
                     WHERE no LIKE @tel";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@tel", "%" + tel.Text + "%");
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet data = new DataSet();
                adapter.Fill(data);
                dataGridView1.DataSource = data.Tables[0];
            }
            connection.Close();
        }

        private void adara_Click(object sender, EventArgs e)
        {

        }

        private void soyadara_Click(object sender, EventArgs e)
        {
            connection.Open();
            string query = @"SELECT no, ad, soyad, tel, urun, marka, sistem, garanti, tutar, durum, tarih, bilgi, sonuc, açıklama, personelislem, personelteslim, aksesuar, firma, serinoeski, serinoyeni, sifre, teslimtarih, personel, tno 
                     FROM tdestek 
                     WHERE no LIKE @soyad";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@soyad", "%" + soyad.Text + "%");
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet data = new DataSet();
                adapter.Fill(data);
                dataGridView1.DataSource = data.Tables[0];
            }
            ad.Clear();
            tel.Clear();

            connection.Close();
        }



        private void ad_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ad_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void ad_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                string add = ad.Text.ToUpper();
                connection.Open();
                string query = @"SELECT no, ad, soyad, tel, urun, marka, sistem, garanti, tutar,durum,tarih,bilgi,sonuc,acıklama,personel,personelislem,aksesuar,firmaadı,serinoeski,serinoyeni,sifre,odmtur,teslimtarih,personel,tno
                     FROM tdestek 
                     WHERE ad LIKE '%' || @ad || '%'
                            ORDER BY no DESC
                            LIMIT 50";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ad", add);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet data = new DataSet();
                    adapter.Fill(data);
                    dataGridView1.DataSource = data.Tables[0];
                }
                ad.Clear();
                soyad.Clear();
                tel.Clear();

                connection.Close();
            }
        }

        private void soyad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string soyadd = soyad.Text.ToUpper();
                connection.Open();
                string query = @"SELECT no, ad, soyad, tel, urun, marka, sistem, garanti, tutar,durum,tarih,bilgi,sonuc,acıklama,personel,personelislem,aksesuar,firmaadı,serinoeski,serinoyeni,sifre,odmtur,teslimtarih,personel,tno
                     FROM tdestek 
                     WHERE soyad LIKE '%' || @soyad || '%'

                             ORDER BY no DESC
                            LIMIT 50";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@soyad", soyadd);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet data = new DataSet();
                    adapter.Fill(data);
                    dataGridView1.DataSource = data.Tables[0];
                }
                ad.Clear();
                soyad.Clear();
                tel.Clear();

                connection.Close();
            }
        }

        private void tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                connection.Open();

                string query = @"SELECT no, ad, soyad, tel, urun, marka, sistem, garanti, tutar,durum,tarih,bilgi,sonuc,acıklama,personel,personelislem,aksesuar,firmaadı,serinoeski,serinoyeni,sifre,odmtur,teslimtarih,personel,tno
                     FROM tdestek
                     WHERE CAST(tel AS TEXT) LIKE @tel
                             ORDER BY no DESC
                            LIMIT 50";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tel", "%" + tel.Text + "%");
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet data = new DataSet();
                    adapter.Fill(data);

                    // "tel" alanını integer'a dönüştürme işlemi
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        int tel;
                        if (Int32.TryParse(row["tel"].ToString(), out tel))
                        {
                            row["tel"] = tel;
                        }
                        else
                        {
                            // Dönüştürme işlemi başarısız oldu
                            // Burada hata yönetimi işlemleri yapılabilir
                        }
                    }

                    dataGridView1.DataSource = data.Tables[0];
                }
                connection.Close();
                soyad.Clear();
                tel.Clear();
                ad.Clear();

            }
        }



        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                connection.Open();
                string sorgu = "SELECT no, ad, soyad, marka,tutar,tel,urun,durum,garanti,personel,serinoeski,serinoyeni,firmaadı,acıklama,sonuc,sistem,personelislem,personelteslim,aksesuar,sifre,bilgi,tarih,ggidistarih,ggelistarih,padi,ueki,tno,teslimtarih,odmtur,tno FROM tdestek WHERE durum NOT LIKE 'TESLİM EDİLDİ%' AND durum NOT LIKE 'GARANTİDEPO%' AND ad NOT LIKE 'ŞİRKET%' AND durum NOT LIKE 'GARANTİ FİRMA%' ORDER BY no DESC";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
                da.Fill(ds);
                dataGridView1.AutoGenerateColumns = false;
                DataGridViewTextBoxColumn colno = new DataGridViewTextBoxColumn();
                colno.Name = "colno";
                colno.HeaderText = "NO";
                colno.DataPropertyName = "no";

                DataGridViewTextBoxColumn colAd = new DataGridViewTextBoxColumn();
                colAd.Name = "colAd";
                colAd.HeaderText = "AD";
                colAd.DataPropertyName = "ad";

                DataGridViewTextBoxColumn colSoyad = new DataGridViewTextBoxColumn();
                colSoyad.Name = "colSoyad";
                colSoyad.HeaderText = "SOYAD";
                colSoyad.DataPropertyName = "soyad";

                DataGridViewTextBoxColumn colMarka = new DataGridViewTextBoxColumn();
                colMarka.Name = "colMarka";
                colMarka.HeaderText = "MARKA";
                colMarka.DataPropertyName = "marka";

                DataGridViewTextBoxColumn colTutar = new DataGridViewTextBoxColumn();
                colTutar.Name = "colTutar";
                colTutar.HeaderText = "Tutar";
                colTutar.DataPropertyName = "Tutar";

                DataGridViewTextBoxColumn odmtur = new DataGridViewTextBoxColumn();
                odmtur.Name = "odmtur";
                odmtur.HeaderText = "OdemeTuru";
                odmtur.DataPropertyName = "odmtur";

                DataGridViewTextBoxColumn colTel = new DataGridViewTextBoxColumn();
                colTel.Name = "colTel";
                colTel.HeaderText = "Tel";
                colTel.DataPropertyName = "tel";


                DataGridViewTextBoxColumn colUrun = new DataGridViewTextBoxColumn();
                colUrun.Name = "colUrun";
                colUrun.HeaderText = "Urun";
                colUrun.DataPropertyName = "urun";

                DataGridViewTextBoxColumn colDurum = new DataGridViewTextBoxColumn();
                colDurum.Name = "colUrun";
                colDurum.HeaderText = "Durum";
                colDurum.DataPropertyName = "durum";

                DataGridViewTextBoxColumn colgaranti = new DataGridViewTextBoxColumn();
                colgaranti.Name = "colgaranti";
                colgaranti.HeaderText = "Garanti";
                colgaranti.DataPropertyName = "garanti";

                DataGridViewTextBoxColumn colpersonel = new DataGridViewTextBoxColumn();
                colpersonel.Name = "colpersonel";
                colpersonel.HeaderText = "Personel";
                colpersonel.DataPropertyName = "personel";


                DataGridViewTextBoxColumn coltarih = new DataGridViewTextBoxColumn();
                coltarih.Name = "coltarih";
                coltarih.HeaderText = "Tarih";
                coltarih.DataPropertyName = "tarih";

                DataGridViewTextBoxColumn colFirma = new DataGridViewTextBoxColumn();
                colFirma.Name = "colFirma";
                colFirma.HeaderText = "Firma";
                colFirma.DataPropertyName = "firmaadı";

                DataGridViewTextBoxColumn colsistem = new DataGridViewTextBoxColumn();
                colsistem.Name = "colsistem";
                colsistem.HeaderText = "Sistem";
                colsistem.DataPropertyName = "sistem";



                DataGridViewTextBoxColumn colbilgi = new DataGridViewTextBoxColumn();
                colbilgi.Name = "colbilgi";
                colbilgi.HeaderText = "Bilgi";
                colbilgi.DataPropertyName = "bilgi";


                DataGridViewTextBoxColumn colserino = new DataGridViewTextBoxColumn();
                colserino.Name = "colserino";
                colserino.HeaderText = "Serino Eski";
                colserino.DataPropertyName = "serinoeski";

                DataGridViewTextBoxColumn colserinonew = new DataGridViewTextBoxColumn();
                colserinonew.Name = "colserinonew";
                colserinonew.HeaderText = "Serino Yeni";
                colserinonew.DataPropertyName = "serinoyeni";

                DataGridViewTextBoxColumn colacıklama = new DataGridViewTextBoxColumn();
                colacıklama.Name = "colacıklama";
                colacıklama.HeaderText = "Açıklama";
                colacıklama.DataPropertyName = "acıklama";

                DataGridViewTextBoxColumn colsonuc = new DataGridViewTextBoxColumn();
                colsonuc.Name = "colacıklama";
                colsonuc.HeaderText = "Sonuc";
                colsonuc.DataPropertyName = "sonuc";


                DataGridViewTextBoxColumn coliper = new DataGridViewTextBoxColumn();
                coliper.Name = "coliper";
                coliper.HeaderText = "Personel İslem";
                coliper.DataPropertyName = "personelislem";

                DataGridViewTextBoxColumn colpersonelteslim = new DataGridViewTextBoxColumn();
                colpersonelteslim.Name = "colpersonelteslim";
                colpersonelteslim.HeaderText = "Personel Teslim";
                colpersonelteslim.DataPropertyName = "personelteslim";

                DataGridViewTextBoxColumn colaksesuar = new DataGridViewTextBoxColumn();
                colaksesuar.Name = "colaksesuar";
                colaksesuar.HeaderText = "Aksesuar";
                colaksesuar.DataPropertyName = "aksesuar";


                DataGridViewTextBoxColumn colpass = new DataGridViewTextBoxColumn();
                colpass.Name = "colpass";
                colpass.HeaderText = "Şifre";
                colpass.DataPropertyName = "sifre";

                DataGridViewTextBoxColumn ttarih = new DataGridViewTextBoxColumn();
                ttarih.Name = "ttarih";
                ttarih.HeaderText = "Teslim Tarih";
                ttarih.DataPropertyName = "teslimtarih";

                DataGridViewTextBoxColumn TNO = new DataGridViewTextBoxColumn();
                TNO.Name = "TNO";
                TNO.HeaderText = "TAKİP NO";
                TNO.DataPropertyName = "tno";

                dataGridView1.Columns.Add(colno);  //0        
                dataGridView1.Columns.Add(colAd); //1
                dataGridView1.Columns.Add(colSoyad); //2
                dataGridView1.Columns.Add(colTel);//3
                dataGridView1.Columns.Add(colUrun);//5
                dataGridView1.Columns.Add(colMarka);//6
                dataGridView1.Columns.Add(colsistem);//7
                dataGridView1.Columns.Add(colgaranti);//8
                dataGridView1.Columns.Add(colTutar);//   9     
                dataGridView1.Columns.Add(colDurum);//10
                dataGridView1.Columns.Add(coltarih);//11
                dataGridView1.Columns.Add(colbilgi);//   12  
                dataGridView1.Columns.Add(colsonuc);//13
                dataGridView1.Columns.Add(colacıklama);//14
                dataGridView1.Columns.Add(coliper);//15
                dataGridView1.Columns.Add(colpersonelteslim);//16
                dataGridView1.Columns.Add(colaksesuar);//17
                dataGridView1.Columns.Add(colFirma);//18
                dataGridView1.Columns.Add(colserino);//19
                dataGridView1.Columns.Add(colserinonew);//20
                dataGridView1.Columns.Add(colpass);//21
                dataGridView1.Columns.Add(odmtur);//21
                dataGridView1.Columns.Add(ttarih);//22
                dataGridView1.Columns.Add(colpersonel);//23
                dataGridView1.Columns.Add(TNO);//23
                                               // Verileri yükleyin
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya oluşturma hatası: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void dATABASEAYARLARIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.Owner = this;
            f9.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                // Verileri içeren bir DataSet nesnesi oluşturun
                DataSet ds = new DataSet();

                // Verileri çekin ve DataSet nesnesine ekleyin

                connection.Open();

                string sorgu = "SELECT no, ad, soyad, marka,tutar,tel,urun,durum,garanti,personel,serinoeski,serinoyeni,firmaadı,acıklama,sonuc,sistem,personelislem,personelteslim,aksesuar,sifre,bilgi,tarih,ggidistarih,ggelistarih,padi,ueki,tno,teslimtarih,odmtur,tno FROM tdestek WHERE durum NOT LIKE 'TESLİM EDİLDİ%' AND durum NOT LIKE 'GARANTİDEPO%' AND ad NOT LIKE 'ŞİRKET%' AND durum NOT LIKE 'GARANTİ FİRMA%' ORDER BY no DESC LIMIT 50";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
                da.Fill(ds);



                dataGridView1.AutoGenerateColumns = false;
                DataGridViewTextBoxColumn colno = new DataGridViewTextBoxColumn();
                colno.Name = "colno";
                colno.HeaderText = "NO";
                colno.DataPropertyName = "no";

                DataGridViewTextBoxColumn colAd = new DataGridViewTextBoxColumn();
                colAd.Name = "colAd";
                colAd.HeaderText = "AD";
                colAd.DataPropertyName = "ad";

                DataGridViewTextBoxColumn colSoyad = new DataGridViewTextBoxColumn();
                colSoyad.Name = "colSoyad";
                colSoyad.HeaderText = "SOYAD";
                colSoyad.DataPropertyName = "soyad";

                DataGridViewTextBoxColumn colMarka = new DataGridViewTextBoxColumn();
                colMarka.Name = "colMarka";
                colMarka.HeaderText = "MARKA";
                colMarka.DataPropertyName = "marka";

                DataGridViewTextBoxColumn colTutar = new DataGridViewTextBoxColumn();
                colTutar.Name = "colTutar";
                colTutar.HeaderText = "TUTAR";
                colTutar.DataPropertyName = "Tutar";

                DataGridViewTextBoxColumn odmtur = new DataGridViewTextBoxColumn();
                odmtur.Name = "odmtur";
                odmtur.HeaderText = "ODEMETURU";
                odmtur.DataPropertyName = "odmtur";

                DataGridViewTextBoxColumn colTel = new DataGridViewTextBoxColumn();
                colTel.Name = "colTel";
                colTel.HeaderText = "TEL";
                colTel.DataPropertyName = "tel";


                DataGridViewTextBoxColumn colUrun = new DataGridViewTextBoxColumn();
                colUrun.Name = "colUrun";
                colUrun.HeaderText = "URUN";
                colUrun.DataPropertyName = "urun";

                DataGridViewTextBoxColumn colDurum = new DataGridViewTextBoxColumn();
                colDurum.Name = "colUrun";
                colDurum.HeaderText = "DURUM";
                colDurum.DataPropertyName = "durum";

                DataGridViewTextBoxColumn colgaranti = new DataGridViewTextBoxColumn();
                colgaranti.Name = "colgaranti";
                colgaranti.HeaderText = "GARANTİ";
                colgaranti.DataPropertyName = "garanti";

                DataGridViewTextBoxColumn colpersonel = new DataGridViewTextBoxColumn();
                colpersonel.Name = "colpersonel";
                colpersonel.HeaderText = "PERSONEL";
                colpersonel.DataPropertyName = "personel";


                DataGridViewTextBoxColumn coltarih = new DataGridViewTextBoxColumn();
                coltarih.Name = "coltarih";
                coltarih.HeaderText = "TARİH";
                coltarih.DataPropertyName = "tarih";

                DataGridViewTextBoxColumn colFirma = new DataGridViewTextBoxColumn();
                colFirma.Name = "colFirma";
                colFirma.HeaderText = "FİRMA";
                colFirma.DataPropertyName = "firmaadı";

                DataGridViewTextBoxColumn colsistem = new DataGridViewTextBoxColumn();
                colsistem.Name = "colsistem";
                colsistem.HeaderText = "SİSTEM";
                colsistem.DataPropertyName = "sistem";



                DataGridViewTextBoxColumn colbilgi = new DataGridViewTextBoxColumn();
                colbilgi.Name = "colbilgi";
                colbilgi.HeaderText = "BİLGİ";
                colbilgi.DataPropertyName = "bilgi";


                DataGridViewTextBoxColumn colserino = new DataGridViewTextBoxColumn();
                colserino.Name = "colserino";
                colserino.HeaderText = "Serino Eski";
                colserino.DataPropertyName = "serinoeski";

                DataGridViewTextBoxColumn colserinonew = new DataGridViewTextBoxColumn();
                colserinonew.Name = "colserinonew";
                colserinonew.HeaderText = "Serino Yeni";
                colserinonew.DataPropertyName = "serinoyeni";

                DataGridViewTextBoxColumn colacıklama = new DataGridViewTextBoxColumn();
                colacıklama.Name = "colacıklama";
                colacıklama.HeaderText = "AÇIKLAMA";
                colacıklama.DataPropertyName = "acıklama";

                DataGridViewTextBoxColumn colsonuc = new DataGridViewTextBoxColumn();
                colsonuc.Name = "colacıklama";
                colsonuc.HeaderText = "SONUC";
                colsonuc.DataPropertyName = "sonuc";


                DataGridViewTextBoxColumn coliper = new DataGridViewTextBoxColumn();
                coliper.Name = "coliper";
                coliper.HeaderText = "Personel İslem";
                coliper.DataPropertyName = "personelislem";

                DataGridViewTextBoxColumn colpersonelteslim = new DataGridViewTextBoxColumn();
                colpersonelteslim.Name = "colpersonelteslim";
                colpersonelteslim.HeaderText = "Personel Teslim";
                colpersonelteslim.DataPropertyName = "personelteslim";

                DataGridViewTextBoxColumn colaksesuar = new DataGridViewTextBoxColumn();
                colaksesuar.Name = "colaksesuar";
                colaksesuar.HeaderText = "Aksesuar";
                colaksesuar.DataPropertyName = "aksesuar";


                DataGridViewTextBoxColumn colpass = new DataGridViewTextBoxColumn();
                colpass.Name = "colpass";
                colpass.HeaderText = "Şifre";
                colpass.DataPropertyName = "sifre";

                DataGridViewTextBoxColumn ttarih = new DataGridViewTextBoxColumn();
                ttarih.Name = "ttarih";
                ttarih.HeaderText = "Teslim Tarih";
                ttarih.DataPropertyName = "teslimtarih";

                DataGridViewTextBoxColumn TNO = new DataGridViewTextBoxColumn();
                TNO.Name = "TNO";
                TNO.HeaderText = "TAKİP NO";
                TNO.DataPropertyName = "tno";

                dataGridView1.Columns.Add(colno);  //0        
                dataGridView1.Columns.Add(colAd); //1
                dataGridView1.Columns.Add(colSoyad); //2
                dataGridView1.Columns.Add(colTel);//3
                dataGridView1.Columns.Add(colUrun);//5
                dataGridView1.Columns.Add(colMarka);//6
                dataGridView1.Columns.Add(colsistem);//7
                dataGridView1.Columns.Add(colgaranti);//8
                dataGridView1.Columns.Add(colTutar);//   9     
                dataGridView1.Columns.Add(colDurum);//10
                dataGridView1.Columns.Add(coltarih);//11
                dataGridView1.Columns.Add(colbilgi);//   12  
                dataGridView1.Columns.Add(colsonuc);//13
                dataGridView1.Columns.Add(colacıklama);//14
                dataGridView1.Columns.Add(coliper);//15
                dataGridView1.Columns.Add(colpersonelteslim);//16
                dataGridView1.Columns.Add(colaksesuar);//17
                dataGridView1.Columns.Add(colFirma);//18
                dataGridView1.Columns.Add(colserino);//19
                dataGridView1.Columns.Add(colserinonew);//20
                dataGridView1.Columns.Add(colpass);//21
               // dataGridView1.Columns.Add(odmtur);//21
                dataGridView1.Columns.Add(ttarih);//22
                dataGridView1.Columns.Add(colpersonel);//23
               // dataGridView1.Columns.Add(TNO);//23
                                               // Verileri yükleyin
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya oluşturma hatası: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {



            using (var connection = new NpgsqlConnection())
            {
                // Yedekleme işlemi için NpgsqlCommand nesnesi oluşturulur.
                var command = new NpgsqlCommand();
                command.Connection = connection;

                // Yedekleme işlemi için NpgsqlCommand nesnesine komut atanır.
                command.CommandText = $"pg_dump --format=custom --file=backup_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.backup {connection.Database}";

                // ...
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pROGRAMFİRMAAYARLARIToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void servno_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
