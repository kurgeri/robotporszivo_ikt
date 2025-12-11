using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotporszivo_ikt_kurger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 0, m = 0; // N sor, M oszlop
            do
            {
                Console.Write("Adja meg hány sorból álljon a tömb (20-30, sorok és oszlopok NEM = ):");
                n = Convert.ToInt32(Console.ReadLine());
                Console.Write("Adja meg hány álljon a tömb (20-30, sorok és oszlopok NEM = ):");
                m = Convert.ToInt32(Console.ReadLine());
                if (n < 20 || n > 30 || m < 20 || m > 30 || n == m)
                {
                    Console.WriteLine("Nem megfelelő értéktartományban adott összeget!");
                }

            } while (n < 20 || n > 30 || m < 20 || m > 30 || n == m);
            char[,] lakas = new char[n, m];
            Feltolt(lakas);
           
        }

        static void Feltolt(char[,] t)
        {

            Random rnd = new Random();
            for (int i = 0; i < t.GetLength(0); i++)
        {
           for (int j = 0 ; j < t.GetLength(1); j++)
           {
                   
                    int szam = rnd.Next(1, 11);
                   
                    if (szam <= 5) // szabad
                    {
                        t[i, j] = '_';
                    }
                    else if (szam <= 7) // bútor
                    {
                        t[i, j] = 'b';
                    }
                    else // koszos
                    {
                        t[i, j] = 'k';
                    }

                    Console.Write($"{t[i, j]}  ");  
           }
           Console.WriteLine();
        }
        
        
        }
    }
}
