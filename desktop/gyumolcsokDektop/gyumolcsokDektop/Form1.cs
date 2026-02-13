using gyumolcsokDektop.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace gyumolcsokDektop
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();            
        }
        HttpClient client = new HttpClient();
        private async void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSize = true;
            panel1.Visible = false;
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            await Betoltes();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Frissites();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
        }

        private async Task Betoltes()
        {
            try
            {
                var response = await client.GetAsync("http://localhost:3000/fruits");
                var content = await response.Content.ReadAsStringAsync();

                var gyumolcsok = JsonConvert.DeserializeObject<List<Gyumolcsok>>(content);
                dataGridView1.DataSource = gyumolcsok;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba betöltés közben: " + ex.Message);
            }
        }
        private async Task Hozzaad()
        {
            try
            {
                var ujGyumolcs = new
                {
                    nev = textBox1.Text,
                    megjegyzes = textBox2.Text,
                    nev_eng = textBox3.Text,
                    alt_szoveg = textBox4.Text,
                    src = textBox6.Text
                };

                var json = JsonConvert.SerializeObject(ujGyumolcs);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:3000/fruits", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sikeres hozz��adás!");
                    await Betoltes();
                }
                else
                {
                    MessageBox.Show("Hiba történt!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async Task Frissites()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Kérlek válassz ki egy gyümölcsöt!");
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];
                var id = selectedRow.Cells["Gyumolcsid"].Value;

                var frissitettGyumolcs = new
                {
                    nev = textBox1.Text,
                    megjegyzes = textBox2.Text,
                    nev_eng = textBox3.Text,
                    alt_szoveg = textBox4.Text,
                    src = textBox6.Text
                };

                var json = JsonConvert.SerializeObject(frissitettGyumolcs);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"http://localhost:3000/fruits/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sikeres frissítés!");
                    panel1.Visible = false;
                    await Betoltes();
                }
                else
                {
                    MessageBox.Show("Hiba történt a frissítés során!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            Hozzaad();
        }


    }
}
