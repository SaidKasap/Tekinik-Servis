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
    public partial class Form5 : Form
    {
        private NpgsqlConnection connection;
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        
        }

        private void Form5_Load(object sender, EventArgs e)
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

            string sorgu = "select * from garanti order by no desc limit 50 ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            timer1.Start();
      
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Owner = this;
            f6.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from garanti order by no desc limit 50 ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
            
            
            
            
            
            // FORM 1 DEN FORM 2 YE VERİ CEKTİGİMİZ  KODLAR

            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Show();
            f4.no.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            f4.ad.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            f4.soyad.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            f4.tel.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            f4.firma.Text = dataGridView2.CurrentRow.Cells[11].Value.ToString();
            f4.uruntext.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            f4.markatext.Text = dataGridView2.CurrentRow.Cells[14].Value.ToString();
            f4.garantitext.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            f4.eskiseri.Text = dataGridView2.CurrentRow.Cells[9].Value.ToString();
            f4.yeniseri.Text = dataGridView2.CurrentRow.Cells[10].Value.ToString();
            f4.tutar1.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            f4.servis.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            f4.kper.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            f4.iper.Text = dataGridView2.CurrentRow.Cells[16].Value.ToString();
            f4.tper.Text = dataGridView2.CurrentRow.Cells[17].Value.ToString();
            f4.aksesuar.Text = dataGridView2.CurrentRow.Cells[18].Value.ToString();
            f4.sifre.Text = dataGridView2.CurrentRow.Cells[19].Value.ToString();
            f4.aciklamatext.Text = dataGridView2.CurrentRow.Cells[12].Value.ToString();
            f4.sonuctext.Text = dataGridView2.CurrentRow.Cells[13].Value.ToString();
            f4.ktarih.Text = dataGridView2.CurrentRow.Cells[21].Value.ToString();
            f4.gitarih.Text = dataGridView2.CurrentRow.Cells[22].Value.ToString();
            f4.getarih.Text = dataGridView2.CurrentRow.Cells[23].Value.ToString();
            f4.padi.Text = dataGridView2.CurrentRow.Cells[24].Value.ToString();
            f4.ueki.Text = dataGridView2.CurrentRow.Cells[25].Value.ToString();
            f4.eskiseri.Text = dataGridView2.CurrentRow.Cells[9].Value.ToString();
            f4.yeniseri.Text = dataGridView2.CurrentRow.Cells[10].Value.ToString();
            textBox2.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM tdestek WHERE durum NOT LIKE 'TESLİM EDİLDİ%' and durum NOT LIKE 'SERVİS İŞLEMDE%'and durum NOT LIKE 'HAZIR%'and durum NOT LIKE 'SERVİS İŞLEMDE%' ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void gdepo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Owner = this;
            f7.Show();
        }
        //int indexRow;
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = textBox2.Text.ToUpper();
            textBox2.Text =dataGridView1.CurrentRow.Cells[1].Value.ToString();
            connection.Open();
            NpgsqlDataAdapter sg = new NpgsqlDataAdapter("select * from tdestek where firmaadı like '%" + textBox2.Text + "%'", connection);
            DataSet sx = new DataSet();
            sg.Fill(sx);
            dataGridView2.DataSource = sx.Tables[0];
            connection.Close();


        }

      

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void grnti_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        private void grnti_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Text = textBox1.Text.ToUpper();
                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("select * from garanti where ad like '%" + textBox1.Text + "%'", connection);
                DataSet sx = new DataSet();
                sg.Fill(sx);
                dataGridView1.DataSource = sx.Tables[0];
                connection.Close();
            }
        }
    }
}
