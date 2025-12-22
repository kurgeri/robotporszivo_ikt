using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace robotporszivo_ikt_kurger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n, m; // N sor, M oszlop
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
            Clanker(lakas);

        }

        static void Feltolt(char[,] t)
        {
            bool koszos = false, szabad = false;
            Random rnd = new Random();
            for (int i = 0; i < t.GetLength(0); i++)
            {
                for (int j = 0; j < t.GetLength(1); j++)
                {

                    int szam = rnd.Next(1, 11);

                    if (szam <= 5) // szabad
                    {
                        t[i, j] = '_';
                        szabad = true;
                    }
                    else if (szam <= 7) // bútor
                    {
                        t[i, j] = 'b';
                    }
                    else // koszos
                    {
                        t[i, j] = 'k';
                        koszos = true;
                    }



                }


            }

            if (!koszos || !szabad)
            {
                Feltolt(t);
            }
        }

        static void Clanker(char[,] t)
        {

            List<int> ureshelyek = new List<int>();




            Random rnd = new Random();

            for (int i = 0; i < t.GetLength(0); i++)
            {
                for (int j = 0; j < t.GetLength(1); j++)
                {


                    if (t[i, j] == '_')
                    {

                        ureshelyek.Add(i);
                        ureshelyek.Add(j);






                    }
                }


            }
            int robothelye = rnd.Next(0, ureshelyek.Count);

            for (int i = 0; i < t.GetLength(0); i++)
            {
                for (int j = 0; j < t.GetLength(1); j++)
                {
                    if (t[i, j] == '_')
                    {



                        if (robothelye % 2 == 0)
                        {
                            t[ureshelyek[robothelye], ureshelyek[robothelye + 1]] = 'r';
                           
                        }
                        else
                        {
                            t[ureshelyek[robothelye - 1], ureshelyek[robothelye]] = 'r';
                        }

                    }
                    if (t[i, j] == 'r')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write($"{t[i, j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            ureshelyek.ForEach(Console.Write);

       
        }









        //static void Robotmozg(char[,] t)
        //{
        //    Random rnd = new Random();
        //    for (int i = 0; i < t.GetLength(0); i++)
        //    {

        //        for (int j = 0; j < t.GetLength(1); j++)
        //        {
        //            int lepes = rnd.Next(1, 5);
        //            switch (lepes)
        //            {
        //                //case 1: // Fel
        //                //    t[i, j + 1]
        //                //        break;
        //                //case 2: // Le
        //                //    t[i, j - 1]
        //                //        break;
        //                //case 3: // Balra
        //                //    t[i-1,j]
        //                //        break;
        //                //case 4: // Jobbra
        //                //    t[i+1,j]
        //                //        break;


        //            }

        //        }

        //    }
        //}
    }
}
