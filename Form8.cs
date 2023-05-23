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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        private NpgsqlConnection connection;
        private void Form8_Load(object sender, EventArgs e)
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

        private void ad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ad.Text = ad.Text.ToUpper();
                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("select * from tdestek where ad like '%" + ad.Text + "%'", connection);
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

                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("select * from tdestek where tel like '%" + tel.Text + "%'", connection);
                DataSet sx = new DataSet();
                sg.Fill(sx);
                dataGridView1.DataSource = sx.Tables[0];
                connection.Close();
             
            }
        }

        private void soyad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                soyad.Text = ad.Text.ToUpper();
                connection.Open();
                NpgsqlDataAdapter sg = new NpgsqlDataAdapter("select * from tdestek where soyad like '%" + soyad.Text + "%'", connection);
                DataSet sx = new DataSet();
                sg.Fill(sx);
                dataGridView1.DataSource = sx.Tables[0];
                connection.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Show();
            f4.ad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            f4.soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            f4.tel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            f4.firma.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
        }

        private void ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tel_TextChanged(object sender, EventArgs e)
        {

        }

        private void soyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
