using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Lagrange
    {
        public string Calcular(int[][] puntos)
        {
            string respuesta = "";
            for (int i = 0; i < puntos.Length; i++)
            {
                int[] puntoActivo = puntos[i];
                List<int[]> listaPuntos = new List<int[]>();
                for (int j = 0; j < puntos.Length; j++)
                {
                    if (i != j)
                    {
                        listaPuntos.Add(puntos[j]);
                    }
                }
                int[][] demasPuntos = listaPuntos.ToArray();
                respuesta += CalcularLi(puntoActivo, demasPuntos) + " + ";
            }

            return respuesta.Substring(0, respuesta.Length - 3);
        }


        private string CalcularLi(int[] puntoActivo, int[][] puntos)
        {
            string numerador = "";
            int denominador = 1;
            foreach (int[] punto in puntos)
            {
                denominador *= (puntoActivo[0] - punto[0]);
                if (punto[0] > 0)
                {
                    numerador += "(x - " + punto[0] + ") ";
                }
                else if (punto[0] < 0)
                {
                    numerador += "(x + " + (-1) * punto[0] + ") ";
                }
                else
                {
                    numerador += "(x) ";
                }



            }
            return "(" + numerador + "/" + denominador + ")*" + puntoActivo[1];
        }

        public List<string> ListaLis(int[][] puntos)
        {
            List<string> respuesta = new List<string>();
            for (int i = 0; i < puntos.Length; i++)
            {
                int[] puntoActivo = puntos[i];
                List<int[]> listaPuntos = new List<int[]>();
                for (int j = 0; j < puntos.Length; j++)
                {
                    if (i != j)
                    {
                        listaPuntos.Add(puntos[j]);
                    }
                }
                int[][] demasPuntos = listaPuntos.ToArray();
                respuesta.Add(CalcularLi(puntoActivo, demasPuntos));
            }

            return respuesta;
        }


        public float CalcularEnPunto(int puntoACalular, int[][] puntos)
        {
            float resultado = new float();
            for (int i = 0; i < puntos.Length; i++)
            {
                int[] puntoActivo = puntos[i];
                List<int[]> listaPuntos = new List<int[]>();
                for (int j = 0; j < puntos.Length; j++)
                {
                    if (i != j)
                    {
                        listaPuntos.Add(puntos[j]);
                    }
                }
                int[][] demasPuntos = listaPuntos.ToArray();
                int numerador = 1;
                int denominador = 1;
                foreach (int[] punto in demasPuntos)
                {

                    denominador *= (puntoActivo[0] - punto[0]);
                    numerador *= (puntoACalular - punto[0]);
                }
                resultado += ((numerador / denominador) * puntoActivo[1]);
            }

            return resultado;



        }

        public void mostrarArray(int[][] puntos)
        {
            for (int n = 0; n < puntos.Length; n++)
            {
                System.Console.Write("Punto({0}): ", n);
                for (int k = 0; k < puntos[n].Length; k++)
                {
                    System.Console.Write("{0} ", puntos[n][k]);
                }
                System.Console.WriteLine();
            }
        }

        //Para Lagrange se brindará información de cada L i (x i ) utilizado para calcular el polinomio.
    }
}
