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
    public partial class Mensagens : Form
    {
        public Mensagens()
        {
            InitializeComponent();
        }
        public static void Exibir(string titulo, string texto)
        {
            var msgBox = new Mensagens();
            msgBox.label1.Text = texto;
            msgBox.label1.Dock = DockStyle.None;
            msgBox.label1.AutoSize = false;
            msgBox.Text = titulo;
            msgBox.ShowDialog();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Mensagens_Load(object sender, EventArgs e)
        {

        }
    }
}
