using HenReader.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HenReader.Views
{
    public partial class DashForm : Form
    {
        public Selenium selenium { get; set; }


        public DashForm()
        {

            InitializeComponent();
            LoadSettings();
        }

        private void AlterTab()
        {
            this.panel1.Visible = !this.panel1.Visible;
            this.panel2.Visible = !this.panel2.Visible;
        }
        private void LoadSettings()
        {
            this.radioButton1.Checked = Properties.Settings.Default.isFavorito == 1;
            this.radioButton2.Checked = !this.radioButton1.Checked;
            this.textBox3.Text = Properties.Settings.Default.tempo.ToString();
            this.textBox1.Text = !string.IsNullOrEmpty(Properties.Settings.Default.username) ? Properties.Settings.Default.username : "";
            this.textBox2.Text = !string.IsNullOrEmpty(Properties.Settings.Default.password) ? Properties.Settings.Default.password : "";

        }
        private void SaveSettings()
        {
            Properties.Settings.Default.isFavorito = (byte)(this.radioButton1.Checked ? 1 : 0);
            Properties.Settings.Default.username = this.textBox1.Text;
            Properties.Settings.Default.password = this.textBox2.Text;
            Properties.Settings.Default.tempo = !string.IsNullOrEmpty(this.textBox3.Text) ? Int32.Parse(this.textBox3.Text) : 500;
            Properties.Settings.Default.Save();
        }
        private void DashForm_Load(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                selenium = new Selenium(Int32.Parse(this.textBox3.Text));


                if (this.radioButton1.Checked)
                {
                    this.selenium.FavoritoRead(this.textBox1.Text, this.textBox2.Text);
                }
                else
                {
                    this.selenium.RandomRead();
                }

            }
            catch (Exception ex)
            {
                Mensagens.Exibir("Alerta!", ex.Message);
              //  MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.selenium != null)
                    this.selenium.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.AlterTab();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //TODO:
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked && (string.IsNullOrEmpty(this.textBox1.Text) || string.IsNullOrEmpty(this.textBox2.Text)))
            {
                MessageBox.Show("É Necessário preencher username e password, para iniciar a aplicação pelos Favoritos.");

                return;
            }
            this.SaveSettings();
            this.AlterTab();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
