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
    public partial class IngresoDatos : Form
    {
        private String metodo;
          

        public IngresoDatos(String _metodo)
        {
            InitializeComponent();
            metodo = _metodo;
            textBox1.Text = metodo;
        }

        private void IngresoDatos_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String x = puntoX.Text;
            String y = puntoY.Text;
            Boolean match = false;
            if(x == "" || y == "")
            {
                new AdvertenciaXinvalido("Usted ingresó un valor vacío.").Show(this);
            }
            else
            {
                foreach (DataGridViewRow row in table.Rows)
                {
                    if (Convert.ToDouble(row.Cells["X"].Value) == Convert.ToDouble(x))
                    {
                        match = true;
                        break;
                    }
                }
                if(match)
                {
                    new AdvertenciaXinvalido("Usted ingresó un valor de x repetido.").Show(this);
                    puntoY.Text = "";
                    puntoX.Text = "";
                }
                else
                {
                    table.Rows.Add(x, y);
                    puntoY.Text = "";
                    puntoX.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numRows = table.Rows.Count;
            int[][] matrizXY = new int[numRows][];

            int i = 0;
            foreach (DataGridViewRow row in table.Rows)
            {
               
                int[] auxiliar = new int[2];
                auxiliar[0] = (int)Convert.ToInt64(row.Cells["X"].Value);
                auxiliar[1] = (int)Convert.ToInt64(row.Cells["Y"].Value);
                matrizXY[i] = auxiliar;
                i++; 
            }
            new Procesamiento(matrizXY).Show(this);

            
        }
    }
}
