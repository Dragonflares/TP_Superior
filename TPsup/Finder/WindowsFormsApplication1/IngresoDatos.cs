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
            table.Rows.Add(x, y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numRows = table.Rows.Count;
            double[] arrayX = new double[numRows];
            double[,] matrizY = new double[numRows, numRows];
            foreach (DataGridViewRow row in table.Rows)
            {

                
                int i = 0;
                matrizY[i, 0] = Convert.ToDouble(row.Cells["Y"].Value);
                arrayX[i] = Convert.ToDouble(row.Cells["X"].Value);
                i++;

                 
            }
            new Procesamiento(arrayX, matrizY).Show(this);

            
        }
    }
}
