using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String valor = comboBox1.Text;

            if (valor == "Lagrange")
            {
                new IngresoDatos(comboBox1.Text).Show(this);
            }
            else if (valor == "Newton-Gregory")
            {
                new Newton(comboBox1.Text).Show(this);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un metodo");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}
