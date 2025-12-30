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

            } while (n < 5 || n > 30 || m < 5 || m > 30 || n == m);
            char[,] lakass = new char[n, m];
            Feltolt(lakass);
            lakass = Clanker(lakass);
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
            int koszoshelyekkezd = 0;
            int jelkoszoshely;
            for (int i = 0; i < lakas.GetLength(0); i++)
            {

                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i, j] == 'r')
                    {
                        robotpoz[0] = i;
                        robotpoz[1] = j;

                    }
                    if (lakas[i, j] == 'k')
                    {
                        koszoshelyekkezd++;


                    }

                }


            }


            int jelenlegirobpoz_i = robotpoz[0];
            int jelenlegirobpoz_j = robotpoz[1];

            int lepesekszama = 0;

            Random rnd = new Random();


            do
            {
                jelkoszoshely = 0;
                for (int i = 0; i < lakas.GetLength(0); i++)
                {
                    for (int j = 0; j < lakas.GetLength(1); j++)
                    {
                        if (lakas[i, j] == 'k')
                        {
                            jelkoszoshely++;
                        }
                    }
                }


                Console.Clear();

                Lakasmegjelenit(lakas);
                Console.WriteLine($"Robot jelenlegi helye: {jelenlegirobpoz_i}:{jelenlegirobpoz_j}");
                Console.WriteLine($"Koszos helyek eredetileg: {koszoshelyekkezd}");
                Console.WriteLine($"Hátra maradt koszoshelyek száma: {jelkoszoshely}");
                Console.WriteLine($"Lépések száma: {lepesekszama}");


                int regirobpoz_i = jelenlegirobpoz_i;
                int regirobpoz_j = jelenlegirobpoz_j;




                int lepes = rnd.Next(1, 5); // 1: Fel, 2: Le, 3: Jobb, 4: Bal
                string merrelep = "";
                Console.WriteLine($"Maga a lépés: {lepes} - {merrelep} ");
                switch (lepes)
                {
                    case 1:
                        if (jelenlegirobpoz_i - 1 >= 0 && lakas[jelenlegirobpoz_i - 1, jelenlegirobpoz_j] != 'b')
                        {
                            jelenlegirobpoz_i--;
                            lepesekszama++;
                            merrelep = "Fel";

                        }
                        else
                        {
                            lepes = rnd.Next(2, 5);

                        }

                        break;
                    case 2:
                        if (jelenlegirobpoz_i + 1 <= lakas.GetLength(0) - 1 && lakas[jelenlegirobpoz_i + 1, jelenlegirobpoz_j] != 'b')
                        {
                            jelenlegirobpoz_i++;
                            lepesekszama++;
                            merrelep = "Le";
                        }
                        else
                        {

                            lepes = rnd.Next(1, 5);
                            if (lepes == 2)
                            {
                                lepes--;
                            }
                        }


                        break;
                    case 3:
                        if (jelenlegirobpoz_j + 1 <= lakas.GetLength(1) - 1 && lakas[jelenlegirobpoz_i, jelenlegirobpoz_j + 1] != 'b')
                        {
                            jelenlegirobpoz_j++;
                            lepesekszama++;
                            merrelep = "Jobb";

                        }
                        else
                        {
                            lepes = rnd.Next(1, 5);
                            if (lepes == 3)
                            {
                                lepes--;
                            }
                        }


                        break;
                    case 4:
                        if (jelenlegirobpoz_j - 1 >= 0 && lakas[jelenlegirobpoz_i, jelenlegirobpoz_j - 1] != 'b')
                        {
                            jelenlegirobpoz_j--;
                            lepesekszama++;
                            merrelep = "Bal";

                        }
                        else
                        {
                            lepes = rnd.Next(1, 4);
                        }
                        break;






                }

                lakas[regirobpoz_i, regirobpoz_j] = '_';
                lakas[jelenlegirobpoz_i, jelenlegirobpoz_j] = 'r';
                Thread.Sleep(50);








                if (jelkoszoshely == 0 || lepesekszama > 200)
                {
                    takaritasvege = true;
                }
            } while (!takaritasvege);
            List<string> koszoslista = new List<string>();
            string koszoshely;
            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i,j] == 'k')
                    {
                       koszoshely = Convert.ToString($"({i}:{j})");
                        koszoslista.Add(koszoshely);
                    }
                }
            }
            Console.WriteLine("A megmaradt koszos területek helyei:");
            for (int i = 0; i < koszoslista.Count(); i++ )
            {
                Console.Write($"{koszoslista[i]}");
            }




        }

    }

}













