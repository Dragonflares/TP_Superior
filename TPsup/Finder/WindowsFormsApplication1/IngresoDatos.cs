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
        private int[][] matriz;
        private Boolean pasosmostrados = false;

        public IngresoDatos(String _metodo)
        {
            InitializeComponent();
            metodo = _metodo;
            textBox1.Text = metodo;
            label3.Visible = false;
            button3.Visible = false;
            listView1.Visible = false;
            textBox2.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button5.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
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
            if (x == "" || y == "")
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
                if (match)
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
            if (table.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataGridViewRow row in table.Rows)
                {

                    int[] auxiliar = new int[2];
                    auxiliar[0] = (int)Convert.ToInt64(row.Cells["X"].Value);
                    auxiliar[1] = (int)Convert.ToInt64(row.Cells["Y"].Value);
                    matrizXY[i] = auxiliar;
                    i++;
                }
                int[][] matrizOrdenada = ordenarMatriz(matrizXY);
                matriz = matrizOrdenada;
                if (esEquidistante(matrizOrdenada))
                    textBox5.Text = "Si";
                else
                    textBox5.Text = "No";
                label3.Visible = true;
                button3.Visible = true;
                textBox2.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button5.Visible = true;
                label8.Visible = true;
                textBox5.Visible = true;
                Lagrange lagrangecalculator = new Lagrange();
                string resultado = lagrangecalculator.Calcular(matrizOrdenada);
                if (button2.Text == "Recalcular")
                {
                    textBox6.Visible = true;
                    label9.Visible = true;
                    if (textBox2.Text == resultado)
                    {
                        textBox6.Text = "No";
                    }
                    else
                    {
                        textBox6.Text = "Si";
                    }
                }
                textBox2.Text = resultado;
                button2.Text = "Recalcular";
                listView1.Visible = false;
                pasosmostrados = false;
            }
            else
                MessageBox.Show("Tabla sin puntos", "ERROR",
           MessageBoxButtons.OK, MessageBoxIcon.Error);
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pasosmostrados)
            {
                listView1.Visible = false;
                foreach (ListViewItem item in listView1.Items)
                {
                    listView1.Items.Remove(item);
                }
                pasosmostrados = false;
            }
            else
            {
                listView1.Visible = true;
                Lagrange lagrangecalculator = new Lagrange();
                List<string> lis = lagrangecalculator.ListaLis(matriz);
                foreach (string li in lis)
                {
                    ListViewItem some = new ListViewItem(li);
                    listView1.Items.Add(some);
                }
                pasosmostrados = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Lagrange lagrangecalculator = new Lagrange();
            if (textBox4.Text == "")
            {
                new AdvertenciaXinvalido("Usted ingresó no ingresó un valor de k.").Show(this);
            }
            else
            {
                int k = (int)Convert.ToInt64(textBox4.Text);
                textBox3.Text = lagrangecalculator.CalcularEnPunto(k, matriz).ToString();
            }

        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == table.Columns["Borrar"].Index)
            {

                if (table.Rows.Count  > 0)
                    table.Rows.RemoveAt(e.RowIndex);
                else
                    MessageBox.Show("Tabla sin puntos", "ERROR",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private int[][] ordenarMatriz(int[][] matrix)
        {
            int[] aux = new int[2];
            int length = matrix.Length;
            int a = 0;
            while(a != length)
            {
                if(a == 0)
                {
                    a++;
                }
                else if(matrix[a][0] < matrix[a-1][0])
                {
                    aux = matrix[a - 1];
                    matrix[a - 1] = matrix[a];
                    matrix[a] = aux;
                    a--;
                }
                else
                {
                    a++;
                }
            }
            return matrix;
        }
        private Boolean esEquidistante(int[][] matrix)
        {
            Boolean result = true;
            int a = 1;
            int distance = 0;
            for(a = 1; a < matrix.Length; a++)
            {
                if(distance == 0)
                {
                    distance = matrix[a][0] - matrix[a - 1][0];
                }
                else
                {
                    int newDistance = matrix[a][0] - matrix[a - 1][0];
                    if(newDistance != distance)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
