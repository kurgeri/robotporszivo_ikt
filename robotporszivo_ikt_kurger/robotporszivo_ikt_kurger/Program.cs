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
                Console.Write("Adja meg hány oszlopból álljon a tömb (20-30, sorok és oszlopok NEM = ):");
                m = Convert.ToInt32(Console.ReadLine());
                if (n < 20 || n > 30 || m < 20 || m > 30 || n == m)
                {
                    Console.WriteLine("Nem megfelelő értéktartományban adott összeget!");
                }

            } while (n < 20 || n > 30 || m < 20 || m > 30 || n == m);
            char[,] lakas = new char[n, m];
            Feltolt(lakas);
            Clanker(lakas);
            Robotmozg(lakas);


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
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    if (t[i, j] == 'b')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    if (t[i, j] == 'k')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write($"{t[i, j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }




        static void Robotmozg(char[,] t)
        {
            int rhely_i, rhely_j ;
            for (int i = 0; i < t.GetLength(0); i++)
            {

                for (int j = 0; j < t.GetLength(1); j++)
                {
                    if (t[i, j] == 'r')
                    {
                        rhely_i = i;
                        rhely_j = j;

                    }
                }
            }

            Random rnd = new Random();
            for (int i = 0; i < t.GetLength(0); i++)
            {

                for (int j = 0; j < t.GetLength(1); j++)
                {
                    int lepes = rnd.Next(1, 5); // 1 Fel, 2 Le, 3 Jobb, 4 Bal


                   



                    if (lepes == 1 && t[rhely_i - 1, rhely_j] != 'b' && rhely_i - 1 >= 0)
                    {


                        for (int k = 0; k < t.GetLength(0); k++)
                        {
                            for (int l = 0; l < t.GetLength(1); l++)
                            {
                                t[rhely_i - 1, rhely_j] = 'r';
                                Console.Write($"{t[rhely_i, rhely_j]} ");
                            }
                            Console.WriteLine();
                        }




                    }
                    else if (lepes == 2 && t[rhely_i + 1, rhely_j] != 'b' && rhely_i + 1 <= t.GetLength(0))
                    {
                        for (int k = 0; k < t.GetLength(0); k++)
                        {
                            for (int l = 0; l < t.GetLength(1); l++)
                            {
                                t[rhely_i + 1, rhely_j] = 'r';
                                Console.Write($"{t[rhely_i, rhely_j]} ");
                            }
                            Console.WriteLine();
                        }




                    }
                    else if (lepes == 3 && t[rhely_i, rhely_j + 1] != 'b' && rhely_j + 1 <= t.GetLength(1))
                    {
                        for (int k = 0; k < t.GetLength(0); k++)
                        {
                            for (int l = 0; l < t.GetLength(1); l++)
                            {
                                t[rhely_i , rhely_j + 1 ] = 'r';
                                Console.Write($"{t[rhely_i, rhely_j]} ");
                            }
                            Console.WriteLine();
                        }




                    }
                    else if (lepes == 4 && t[rhely_i, rhely_j - 1] != 'b' && rhely_j - 1 >= 0)
                    {
                        for (int k = 0; k < t.GetLength(0); k++)
                        {
                            for (int l = 0; l < t.GetLength(1); l++)
                            {
                                t[rhely_i, rhely_j - 1] = 'r';
                                Console.Write($"{t[rhely_i, rhely_j]} ");
                            }
                            Console.WriteLine();
                        }




                    }






                }


            }
            for (int i = 0; i < t.GetLength(0); i++)
            {
                for (int j = 0; j < t.GetLength(1); j++)
                {
                    if (t[i, j] == 'r')
                    {

                        Console.WriteLine($"{i}{j}");
                    }

                }

            }

        }

    }
}












