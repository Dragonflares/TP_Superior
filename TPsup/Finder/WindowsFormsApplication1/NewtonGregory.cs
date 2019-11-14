using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class NewtonGregory
    {

        //Tengo que tener los coeficientes como valores publicos

        static double calculoCoeficienteA(double u, int n)
        {
            double temp = u;
            for (int i = 1; i < n; i++)
                temp = temp * (u - i);
            return temp;
        }

        // calculating factorial of given number n 
        static int factorial(int n)
        {
            int f = 1;
            for (int i = 2; i <= n; i++)
                f *= i;
            return f;
        }


        public void calcularValoresProgresivos(double[] x, double[,] y)
        {

            Console.WriteLine("Muestro tabla de diferencias finitas");
            // Calculo de la tabla de diferencias finitas
            for (int i = 1; i < x.Length; i++)
            {
                for (int j = 0; j < x.Length - i; j++)

                    y[j, i] = (y[j + 1, i - 1] - y[j, i - 1]);


            }

            this.tablaDeDiferencias(x, y);
        }

        public void tablaDeDiferencias(double[] x, double[,] y)
        {

            int cantidadDeValores = x.Length;
            // Muestro la tabla de diferencias
            for (int i = 0; i < cantidadDeValores; i++)
            {
                Console.Write(x[i] + "\t");
                for (int j = 0; j < cantidadDeValores - i; j++)
                    Console.Write(y[i, j] + "\t");
                Console.WriteLine();
            }
        }

        public void obtenerPolinomio(int cantidadDeValores, double[,] y)
        {

            //Obtengo los valores de la diagonal que son los coeficientes obtenidos
            //Ordenados de A0 , A1, ...,An
            double[] coeficientes = new double[cantidadDeValores];
            coeficientes = obtenerCoeficientes(cantidadDeValores, y);

            //Voy a tener que armar "cantidadDeValores" terminos para armar el polinomio intermedio
            //y luego resolver la multiplicación
        }

        public List<Polinomio> obtenerListaDePolinomios(double[] x, double[] coeficientes)
        {
            List<Polinomio> polinomios = new List<Polinomio>();

            //Agrego el coeficiente que no multiplica a un (X-Xn)

            //Esto deberia poder ser un double
            polinomios.Add(new Polinomio(new double[1] { coeficientes[0] }));


            for (int i = 1; i < coeficientes.Length; i++)
            {
                polinomios.Add(obtenerPolMultiplicadoHasta(i, x) * coeficientes[i]);

            }
            for (int j = 0; j < coeficientes.Length; j++)
            {
                //Arreglar ese +
                Console.Write(polinomios[j] + " + ");
            }

            return polinomios;
            /*Polinomio polinomioFinal = null;


            for (int j = 0; j < polinomios.Count-1;j++)
            {
                if (polinomioFinal == null)
                {
                    polinomioFinal = polinomios[j];
                }
                else
                {

                    polinomioFinal += polinomios[j];
                    
                }
            }
            Console.Write("FInall: " + polinomioFinal);*/
        }
        public Polinomio obtenerPolMultiplicadoHasta(int posicion, double[] x)
        {
            Polinomio polinomioTemp;
            Polinomio terminoPolinomioFinal = null;
            for (int i = 0; i < posicion; i++)
            {
                //Simula ser un polinomio de la forma (x-a)
                double[] aux = { x[i] * (-1), 1 };
                polinomioTemp = new Polinomio(aux);
                if (terminoPolinomioFinal == null)
                {
                    terminoPolinomioFinal = polinomioTemp;
                }
                else
                {
                    terminoPolinomioFinal = terminoPolinomioFinal * polinomioTemp;
                }

            }
            //Console.Write(terminoPolinomioFinal);
            return terminoPolinomioFinal;
        }
        public double[] obtenerCoeficientes(int cantidadDeValores, double[,] y)
        {
            double h = 1;
            double[] coeficientes = new double[cantidadDeValores];

            for (int i = 0; i < cantidadDeValores; i++)
            {
                coeficientes[i] = Math.Round(y[0, i] / (factorial(i) * Math.Pow(h, i)), 3);
            }
            return coeficientes;
        }




        //Recibo el array de valores de X
        //la matriz de valores en Y
        //El valor a interpolar
        /*public void calcularEnPunto(double[] x, double[,] y, double valorAInterpolar)
        {
            // obtengo el valor del An y sumo
            double sum = y[0, 0];
            double h = x[1]-x[0];
            double a = (valorAInterpolar - x[0]) / (x[1] - x[0]);
            for (int i = 1; i < x.Length; i++)
            {
                //Hay que calcular la distancia entre los puntos de x
                sum = sum + (calculoCoeficienteA(a, i) * y[0, i]) /
                                        (factorial(i-1)*(Math.Pow(h,i-1)));
            }

            Console.WriteLine("\n Interpolacion en " + valorAInterpolar + " es " + Math.Round(sum, 6));
        }*/

        public void calcularPolinomioEn(double punto, List<Polinomio> listaPolinomios)
        {

            double acumulador = 0;

            for (int i = 0; i < listaPolinomios.Count; i++)
            {
                acumulador += evaluarUnPolinomio(listaPolinomios[i], punto);
            }
            Console.WriteLine(acumulador);

        }
        public double evaluarUnPolinomio(Polinomio poli, double punto)
        {
            int grado = 0;
            double[] coeficientes = poli.coeficiente;
            double suma = 0;

            for (int i = 0; i <= poli.grado; i++)
            {
                suma += coeficientes[i] * Math.Pow(punto, grado);
                grado++;
            }
            return suma;
        }



    }
}
