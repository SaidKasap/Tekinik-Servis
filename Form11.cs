using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TServis1
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        private NpgsqlConnection connection;
        private void ad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ad.Text = ad.Text.ToUpper();
                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("SELECT no, ad, soyad,tel FROM musteri WHERE ad like '%" + ad.Text + "%'  ORDER BY no DESC LIMIT 50", connection);
                DataSet sx = new DataSet();
                sg.Fill(sx);
                dataGridView1.DataSource = sx.Tables[0];
                connection.Close();
            }
        }

        private void Form11_Load(object sender, EventArgs e)
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
            string sorgu = "select * from tdestek order by no desc limit 50 ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Form2 f2 = (Form2)this.Owner;
            f2.SetData(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[3].Value.ToString());
            f2.BringToFront();
            this.Close();

            Form11 f11 = new Form11();
            f11.Owner = this;
            f11.Close();
        }

        private void soyad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ad.Text = ad.Text.ToUpper();
                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("SELECT no, ad, soyad,tel FROM musteri WHERE soyad like '%" + soyad.Text + "%'  ORDER BY no DESC LIMIT 50", connection);
                DataSet sx = new DataSet();
                sg.Fill(sx);
                dataGridView1.DataSource = sx.Tables[0];
                connection.Close();
            }
        }

        private void tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ad.Text = ad.Text.ToUpper();
                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("SELECT no, ad, soyad,tel FROM musteri WHERE tel like '%" + tel.Text + "%'  ORDER BY no DESC LIMIT 50", connection);
                DataSet sx = new DataSet();
                sg.Fill(sx);
                dataGridView1.DataSource = sx.Tables[0];
                connection.Close();
            }
        }
    }
}
