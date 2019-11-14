using System;
using System.Collections.Generic;

namespace TPMatSup
{
    class Program
    {
        static void Main(string[] args)
        {
           int [][] puntos = new int[][] 
            {
                new int[2] {1, 3},
                new int[2] {2, 7},
                new int[2] {3, 13},
                
            };
            /*
            var l1 = new Lagrange();
            Console.WriteLine($"Lagrange {l1.Calcular(puntos)}");
            List<string> Lis = l1.ListaLis(puntos);
            foreach(string Li in Lis)
            {
                Console.WriteLine($"Li: {Li}");
            }
            Console.WriteLine($"Lagrange en pto {l1.CalcularEnPunto(-9,puntos)}");*/
            NewtonGregory();
        }

        private static void NewtonGregory()
        {
            /*Para funcionar la clase Newton Gregory necesita:
             * -los valores de X como lista de Double [x0, x1, x2, x3]
             * -Los valores de y como matriz con el siguiente formato
             * /*   y[0, 0] = y0;
                    y[1, 0] = y1;
                    y[2, 0] = y2;
                    y[3, 0] = y3;

            // La matriz y[,] es para la tabla de diferencias finitas
            // con y[,0] para los valores de Y=f(x) respectivos a cada X
            */
            Console.WriteLine("Newton Gregory");


            //Valores de X
            double[] x = { 1, 2, 3, 4 };
            // Cantidad de numeros de prueba
            int n = x.Length;


            
            double[,] y = new double[n, n];
            /*y[0, 0] = 4;
            y[1, 0] = 15;
            y[2, 0] = 40;
            y[3, 0] = 85;*/
            y[0, 0] = 1;
            y[1, 0] = 1;
            y[2, 0] = 2;
            y[3, 0] = 5;


            var ng1 = new NewtonGregory();
            ng1.calcularValoresProgresivos(x, y);
            Console.WriteLine("Obtengo los coeficientes de la tabla y calculo la suma de polinomios");
            List<Polinomio> listaPolinomios = ng1.obtenerListaDePolinomios(x, ng1.obtenerCoeficientes(n, y));
            ng1.calcularPolinomioEn(10, listaPolinomios);

        }
    }
}
