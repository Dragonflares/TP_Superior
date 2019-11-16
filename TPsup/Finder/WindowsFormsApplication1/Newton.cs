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
    public partial class Newton : Form
    {

        private String metodo;
        private int[][] matriz;
        private Boolean pasosmostrados = false;
        private NewtonGregory newtonGregory;
        private NewtonGregoryRegresivo newtonGregoryRegre;
        int cantidadDeElementos = 0;

        public Newton(String _metodo)
        {
            InitializeComponent();
            metodo = _metodo;
            listView1.Visible = false;
            textBox1.Text = metodo;
            label3.Visible = false;
            button3.Visible = false;
            txtPolinomioResultado.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button5.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            coef.Visible = false;
            textBox2.Visible = false;
            textBox5.Visible = false;

            txtgradoPolinomio.Visible = false;
            lblgradoPolinomio.Visible = false;

        }

        private void Newton_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e) { }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (pasosmostrados)
            {
                listView1.Visible = false;

            }
            else
            {
                listView1.Visible = true;
                coef.Visible = true;
                textBox2.Visible = true;

            }
            double[,] MatrizDeValores;
            if (comboBox1.Text == "Progresivo")
            {
                MatrizDeValores = newtonGregory.obtenerValoresProgresivos();
            }
            else
            {
                MatrizDeValores = newtonGregoryRegre.obtenerValoresRegresivos();
            }
            listView1.Items.Clear();

            for (int i = 0; i < cantidadDeElementos; i++)
            {
                for (int j = 0; j < cantidadDeElementos - i; j++)
                {
                    ListViewItem listitem = new ListViewItem(Convert.ToString(MatrizDeValores[i, j]));
                    listView1.Items.Add(listitem);
                }

            }


        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            String valorViejo;
            if (comboBox1.Text != "" && table.Rows.Count > 1)
            {
                int numRows = table.Rows.Count;
                int[][] matrizXY = new int[numRows][];
                valorViejo = txtPolinomioResultado.Text;
                txtPolinomioResultado.Text = "";
                int i = 0;
                foreach (DataGridViewRow row in table.Rows)
                {

                    int[] auxiliar = new int[2];
                    auxiliar[0] = (int)Convert.ToInt64(row.Cells["X"].Value);
                    auxiliar[1] = (int)Convert.ToInt64(row.Cells["Y"].Value);
                    matrizXY[i] = auxiliar;
                    i++;
                }
                /* if (button2.Text == "Calcular")
                 {
                     button2.Text = "Recalcular";
                 }
                 else if (button2.Text == "Recalcular")
                 {
                     label9.Visible = true;
                     textBox6.Visible = true;
                     if (valorViejo == txtPolinomioResultado.Text)
                     {
                         textBox6.Text = "Si";
                     }
                     else
                     {
                         textBox6.Text = "No";
                     }
                 } */
                matriz = matrizXY;


                cantidadDeElementos = table.Rows.Count;
                double[] x = new double[cantidadDeElementos];
                double[,] y = new double[cantidadDeElementos, cantidadDeElementos];
                for (int j = 0; j < cantidadDeElementos; j++)
                {
                    x[j] = Convert.ToDouble(matriz[j][0]);
                    y[j, 0] = Convert.ToDouble(matriz[j][1]);

                }

                //Double[] x; con valores de x
                //Double[,] y con los valores de y en la columna 0

                txtPolinomioResultado.Visible = true;
                label3.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button5.Visible = true;
                label5.Visible = true;
                button3.Visible = true;
                label7.Visible = true;
                textBox5.Visible = true;
                label10.Visible = true;
                txtPolinomioResultado.Visible = true;
                int[][] matrizOrdenada = new int[matriz.Length][];
                matrizOrdenada = ordenarMatriz(matriz);
                if (esEquidistante(matrizOrdenada))
                {
                    textBox5.Text = "Si";
                }
                else
                {
                    textBox5.Text = "No";
                }
                if (comboBox1.Text == "Progresivo")
                {
                    newtonGregory = new NewtonGregory(x, y);
                    newtonGregory.calcularValoresProgresivos();
                    List<Polinomio> listaPolinomios = newtonGregory.obtenerListaDePolinomios();
                    int gradoPolinomio = 0;
                    for (int j = 0; j < cantidadDeElementos; j++)
                    {
                        if ((j + 1) < cantidadDeElementos)
                        {
                            txtPolinomioResultado.Text += listaPolinomios[j] + " + ";
                        }
                        else
                        {
                            txtPolinomioResultado.Text += listaPolinomios[j];
                        }
                        if (listaPolinomios[j].grado > gradoPolinomio)
                        {
                            gradoPolinomio = listaPolinomios[j].grado;
                        }
                        
                    }
                    txtgradoPolinomio.Text = Convert.ToString(gradoPolinomio);
                    txtgradoPolinomio.Visible = true;
                    lblgradoPolinomio.Visible = true;

                    double[] coe = newtonGregory.obtenerCoeficientes();
                    textBox2.Text = "";
                    for (i = 0; i < coe.Length; i++)
                    {
                        textBox2.Text += "A" + i + ": " + Convert.ToString(coe[i]) + "  ";
                    }
                }
                else if (comboBox1.Text == "Regresivo")
                {
                    newtonGregoryRegre = new NewtonGregoryRegresivo(x, y);
                    newtonGregoryRegre.calcularValoresRegresivos();
                    List<Polinomio> listaPolinomios = newtonGregoryRegre.obtenerListaDePolinomios();
                    int gradoPolinomio = 0;
                    for (int j = 0; j < cantidadDeElementos; j++)
                    {
                        if ((j + 1) < cantidadDeElementos)
                        {
                            txtPolinomioResultado.Text += listaPolinomios[j] + " + ";
                        }
                        else
                        {
                            txtPolinomioResultado.Text += listaPolinomios[j];
                        }
                        if (listaPolinomios[j].grado > gradoPolinomio)
                        {
                            gradoPolinomio = listaPolinomios[j].grado;
                        }
                    }
                    txtgradoPolinomio.Text = Convert.ToString(gradoPolinomio);
                    txtgradoPolinomio.Visible = true;
                    lblgradoPolinomio.Visible = true;
                    double[] coe = newtonGregoryRegre.obtenerCoeficientesRegre();
                    textBox2.Text = "";
                    for (i = 0; i < coe.Length; i++)
                    {
                        textBox2.Text += "A" + i + ": " + Convert.ToString(coe[i]) + "  ";
                    }
                }

                if (button2.Text == "Calcular")
                {
                    button2.Text = "Recalcular";
                }
                else if (button2.Text == "Recalcular")
                {
                    label9.Visible = true;
                    textBox6.Visible = true;
                    if (valorViejo == txtPolinomioResultado.Text)
                    {
                        textBox6.Text = "No";
                    }
                    else
                    {
                        textBox6.Text = "Si";
                    }


                }
                else
                {

                    MessageBox.Show("Elija una opcion o agregue puntos a la tabla");
                }


            }
            else
            {
                MessageBox.Show("Faltan datos para empezar a calcular");
            }


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
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Debe seleccionar una opcion");
            }
            else
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
                matriz = matrizXY;
                label3.Visible = true;
                button3.Visible = true;
                txtPolinomioResultado.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button5.Visible = true;
                label6.Visible = true;
                label9.Visible = true;

                //  textBox2.Text = lagrangecalculator.Calcular(matrizXY);
                button2.Text = "Recalcular";
                listView1.Visible = true;
                pasosmostrados = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pasosmostrados)
            {


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
            if (textBox4.Text == "")
            {
                new AdvertenciaXinvalido("Usted ingresó no ingresó un valor de k.").Show(this);
            }
            else
            {
                textBox3.Visible = true;
                label7.Visible = true;
                int k = (int)Convert.ToInt64(textBox4.Text);

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

                if (table.Rows.Count > 0)
                    table.Rows.RemoveAt(e.RowIndex);
                else
                    MessageBox.Show("Tabla sin puntos", "ERROR",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private int[][] ordenarMatriz(int[][] matrix)
        {
            int[] aux = new int[2];
            int length = matrix.Length;
            int a = 0;
            while (a != length)
            {
                if (a == 0)
                {
                    a++;
                }
                else if (matrix[a][0] < matrix[a - 1][0])
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
            for (a = 1; a < matrix.Length; a++)
            {
                if (distance == 0)
                {
                    distance = matrix[a][0] - matrix[a - 1][0];
                }
                else
                {
                    int newDistance = matrix[a][0] - matrix[a - 1][0];
                    if (newDistance != distance)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                new AdvertenciaXinvalido("Usted no ingresó un valor de k.").Show(this);
            }
            else
            {
                if (comboBox1.Text == "Progresivo")
                {
                    textBox3.Visible = true;
                    int k = (int)Convert.ToInt64(textBox4.Text);
                    double res = newtonGregory.calcularPolinomioEn(k, newtonGregory.obtenerListaDePolinomios());
                    textBox3.Text = Convert.ToString(res);
                    label7.Visible = true;
                }
                else
                {
                    textBox3.Visible = true;
                    int k = (int)Convert.ToInt64(textBox4.Text);
                    double res = newtonGregoryRegre.calcularPolinomioEn(k, newtonGregoryRegre.obtenerListaDePolinomios());
                    textBox3.Text = Convert.ToString(res);
                    label7.Visible = true;
                }



            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void table_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == table.Columns["Borrar"].Index)
            {

                if (table.Rows.Count > 0)
                    table.Rows.RemoveAt(e.RowIndex);
                else
                    MessageBox.Show("Tabla sin puntos", "ERROR",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
