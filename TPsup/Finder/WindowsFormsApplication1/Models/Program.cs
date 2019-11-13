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
            var l1 = new Lagrange();
            Console.WriteLine($"Lagrange {l1.Calcular(puntos)}");
            List<string> Lis = l1.ListaLis(puntos);
            foreach(string Li in Lis)
            {
                Console.WriteLine($"Li: {Li}");
            }
            Console.WriteLine($"Lagrange en pto {l1.CalcularEnPunto(10,puntos)}");
        }
    }
}
