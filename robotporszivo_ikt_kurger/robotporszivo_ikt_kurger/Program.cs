using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
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
                Console.Write("Adja meg hány oszlopból álljon a tömb (20-30, sorok és oszlopok NEM = ):");
                m = Convert.ToInt32(Console.ReadLine());
                if (n < 20 || n > 30 || m < 20 || m > 30 || n == m)
                {
                    Console.WriteLine("Nem megfelelő értéktartományban adott összeget!");
                }

            } while (n < 20 || n > 30 || m < 20 || m > 30 || n == m);
            char[,] lakass = new char[n, m];
            Feltolt(lakass);
            lakass = Clanker(lakass);
            Lakasmegjelenit(lakass);
            Robotmozg(lakass);




        }

        static void Feltolt(char[,] lakas)
        {
            bool koszos = false, szabad = false;
            Random rnd = new Random();
            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {

                    int szam = rnd.Next(1, 11);

                    if (szam <= 5) // szabad
                    {
                        lakas[i, j] = '_';
                        szabad = true;
                    }
                    else if (szam <= 7) // bútor
                    {
                        lakas[i, j] = 'b';
                    }
                    else // koszos
                    {
                        lakas[i, j] = 'k';
                        koszos = true;
                    }



                }


            }

            if (!koszos || !szabad)
            {
                Feltolt(lakas);
            }
        }

        static char[,] Clanker(char[,] lakas)
        {

            List<int> ureshelyek = new List<int>();




            Random rnd = new Random();

            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {


                    if (lakas[i, j] == '_')
                    {

                        ureshelyek.Add(i);
                        ureshelyek.Add(j);
                    }
                }


            }

            int robothelye = rnd.Next(0, ureshelyek.Count);

            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i, j] == '_')
                    {



                        if (robothelye % 2 == 0)
                        {
                            lakas[ureshelyek[robothelye], ureshelyek[robothelye + 1]] = 'r';

                        }
                        else
                        {
                            lakas[ureshelyek[robothelye - 1], ureshelyek[robothelye]] = 'r';
                        }

                    }


                }
            }
            return lakas;
        }
        static void Lakasmegjelenit(char[,] lakas)
        {
            for (int i = 0; i < lakas.GetLength(0); i++)
            {

                for (int j = 0; j < lakas.GetLength(1); j++)
                {

                    if (lakas[i, j] == 'r')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    if (lakas[i, j] == 'b')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    if (lakas[i, j] == 'k')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write($"{lakas[i, j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }




        static void Robotmozg(char[,] lakas)
        {
            bool takaritasvege = false;
            int[] robotpoz = new int[2];
            int koszoshelyek = 0;
            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i, j] == 'r')
                    {
                        robotpoz[0] = i;
                        robotpoz[1] = j;
                        Console.Write($"{robotpoz[0]} {robotpoz[1]}");
                    }
                    if (lakas[i, j] == 'k')
                    {
                        koszoshelyek++;

                    }
                }

            }

            int jelenlegirobpoz_i = robotpoz[0];
            int jelenlegirobpoz_j = robotpoz[1];

            Random rnd = new Random();
           

            do
            {
                Console.Clear();

                Lakasmegjelenit(lakas);
                Console.WriteLine($"{jelenlegirobpoz_i}{jelenlegirobpoz_j}");
                int regirobpoz_i = jelenlegirobpoz_i;
                int regirobpoz_j = jelenlegirobpoz_j;




                int lepes = rnd.Next(1, 5); // 1: Fel, 2: Le, 3: Jobb, 4: Bal
                switch (lepes)
                {
                    case 1:
                        if (lakas[jelenlegirobpoz_i - 1, jelenlegirobpoz_j] != 'b' && jelenlegirobpoz_i - 1 >= 0)
                        {
                           jelenlegirobpoz_i--;
                        }
                        else
                        {
                            lepes = rnd.Next(1, 5);
                        }
                        break;
                    case 2:
                        if (lakas[jelenlegirobpoz_i + 1, jelenlegirobpoz_j] != 'b' && jelenlegirobpoz_i + 1 >= lakas.GetLength(0))
                        {
                           jelenlegirobpoz_i++;
                        }
                        else
                        {
                            lepes = rnd.Next(1, 5);
                        }
                        break;
                    case 3:
                        if (lakas[jelenlegirobpoz_i, jelenlegirobpoz_j + 1] != 'b' && jelenlegirobpoz_j + 1 >= lakas.GetLength(1))
                        {
                            jelenlegirobpoz_j++;
                        }
                        else
                        {
                            lepes = rnd.Next(1, 5);
                        }
                        break;
                    case 4:
                        if (lakas[jelenlegirobpoz_i, jelenlegirobpoz_j - 1] != 'b' && jelenlegirobpoz_j - 1 >= 0)
                        {
                            jelenlegirobpoz_j--;
                        }
                        else
                        {
                            lepes = rnd.Next(1, 5);
                        }
                        break;






                }
                lakas[regirobpoz_i, regirobpoz_j] = '_';
                lakas[jelenlegirobpoz_i, jelenlegirobpoz_j] = 'r';
                Thread.Sleep(500);
            
                





            } while (!takaritasvege);
        


        }

    }

}













