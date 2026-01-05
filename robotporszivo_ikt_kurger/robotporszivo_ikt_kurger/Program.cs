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
            char[,] lakas = new char[n, m];
            Feltolt(lakas);
            lakas = Clanker(lakas);
            Lakasmegjelenit(lakas);
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Takaritas(lakas);





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
        static void Takaritas(char[,] lakas)
        {
            int koszoshelyekkezd = 0;
            int butordb = 0;
            int jelenlegirobpoz_i = 0;
            int jelenlegirobpoz_j = 0;
            for (int i = 0; i < lakas.GetLength(0); i++)
            {

                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i, j] == 'r')
                    {
                        jelenlegirobpoz_i = i;
                        jelenlegirobpoz_j = j;

                    }
                    if (lakas[i, j] == 'k')
                    {
                        koszoshelyekkezd++;


                    }
                    if (lakas[i, j] == 'b')
                    {
                        butordb++;
                    }

                }


            }
            int maxsorindex = (lakas.GetLength(0)) - 1;
            int maxoszlopindex = (lakas.GetLength(1)) - 1;
            // Koszos területek elérhetetlenségének vizsgálása
            byte koszosbezarva = 0;
            byte elerhetetlenkosz = 0;
            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i, j] == 'k')
                    {
                        if (i == 0 && j == 0)
                        {
                            koszosbezarva += 2;
                            if (lakas[i + 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j + 1] == 'b')
                            {
                                koszosbezarva++;
                            }

                        }
                        else if (i == maxsorindex && j == maxoszlopindex)
                        {
                            koszosbezarva += 2;
                            if (lakas[i - 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j - 1] == 'b')
                            {
                                koszosbezarva++;
                            }

                        }
                        else if (i == 0 && j == maxoszlopindex)
                        {
                            koszosbezarva += 2;
                            if (lakas[i + 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j - 1] == 'b')
                            {
                                koszosbezarva++;
                            }

                        }
                        else if (i == maxsorindex && j == 0)
                        {
                            koszosbezarva += 2;
                            if (lakas[i - 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j + 1] == 'b')
                            {
                                koszosbezarva++;
                            }

                        }
                        else if (i == 0)
                        {
                            koszosbezarva++;
                            if (lakas[i + 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j + 1] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j - 1] == 'b')
                            {
                                koszosbezarva++;
                            }



                        }
                        else if (i == maxsorindex)
                        {
                            koszosbezarva++;
                            if (lakas[i - 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j + 1] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j - 1] == 'b')
                            {
                                koszosbezarva++;
                            }
                        }
                        else if (j == 0)
                        {
                            koszosbezarva++;
                            if (lakas[i + 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i - 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j + 1] == 'b')
                            {
                                koszosbezarva++;
                            }

                        }
                        else if (j == maxoszlopindex)
                        {
                            koszosbezarva++;
                            if (lakas[i + 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i - 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j - 1] == 'b')
                            {
                                koszosbezarva++;
                            }
                        }
                        else
                        {
                            if (lakas[i + 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i - 1, j] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j + 1] == 'b')
                            {
                                koszosbezarva++;
                            }
                            if (lakas[i, j - 1] == 'b')
                            {
                                koszosbezarva++;
                            }
                        }


                    }
                    if (koszosbezarva == 4)
                    {
                        elerhetetlenkosz++;
                    }
                    koszosbezarva = 0;
                }
            }


            int lepesekszama = 0;
            int maxlepes = ((lakas.GetLength(0) * lakas.GetLength(1)) - butordb) * koszoshelyekkezd;
            int jelkoszoshely;
            bool takaritasvege = false;
            Random rnd = new Random();
            //Robotmozgása
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

                int regirobpoz_i = jelenlegirobpoz_i;
                int regirobpoz_j = jelenlegirobpoz_j;

                int lepes = rnd.Next(1, 5); // 1: Fel, 2: Le, 3: Jobb, 4: Bal

                int robotbezarva = 0;
                switch (lepes)
                {
                    case 1:
                        if (jelenlegirobpoz_i - 1 >= 0 && lakas[jelenlegirobpoz_i - 1, jelenlegirobpoz_j] != 'b')
                        {
                            jelenlegirobpoz_i--;
                            lepesekszama++;


                        }
                        break;
                    case 2:
                        if (jelenlegirobpoz_i + 1 <= maxsorindex && lakas[jelenlegirobpoz_i + 1, jelenlegirobpoz_j] != 'b')
                        {
                            jelenlegirobpoz_i++;
                            lepesekszama++;

                        }
                        break;
                    case 3:
                        if (jelenlegirobpoz_j + 1 <= maxoszlopindex && lakas[jelenlegirobpoz_i, jelenlegirobpoz_j + 1] != 'b')
                        {
                            jelenlegirobpoz_j++;
                            lepesekszama++;


                        }
                        break;
                    case 4:
                        if (jelenlegirobpoz_j - 1 >= 0 && lakas[jelenlegirobpoz_i, jelenlegirobpoz_j - 1] != 'b')
                        {
                            jelenlegirobpoz_j--;
                            lepesekszama++;

                        }

                        break;
                }


                lakas[regirobpoz_i, regirobpoz_j] = '_';
                lakas[jelenlegirobpoz_i, jelenlegirobpoz_j] = 'r';


                if (lepesekszama == 0)
                {
                    robotbezarva++;

                }
                if (robotbezarva == 10)
                {
                    takaritasvege = true;
                }
                if (jelkoszoshely - elerhetetlenkosz == 0 || lepesekszama > maxlepes)
                {
                    takaritasvege = true;
                }
            } while (!takaritasvege);
            Lakasmegjelenit(lakas);


            Console.WriteLine($"Koszos helyek eredetileg: {koszoshelyekkezd} (Ebből {elerhetetlenkosz} biztosan elérhetetlen, szorosan bútorok közé van zárva)");
            Console.WriteLine($"Hátra maradt koszoshelyek száma: {jelkoszoshely}");
            Console.WriteLine($"Lépések száma: {lepesekszama}");


            List<string> koszoslista = new List<string>();
            string koszoshely;
            for (int i = 0; i < lakas.GetLength(0); i++)
            {
                for (int j = 0; j < lakas.GetLength(1); j++)
                {
                    if (lakas[i, j] == 'k')
                    {
                        koszoshely = Convert.ToString($"({i}:{j})");
                        koszoslista.Add(koszoshely);
                    }
                }
            }
            Console.WriteLine("A megmaradt koszos területek helyei:");
            if (jelkoszoshely > 0)
            {
                for (int i = 0; i < koszoslista.Count(); i++)
                {
                    Console.Write($"{koszoslista[i]}");
                }
            }
            else
            {
                Console.WriteLine("Nem maradtak koszos területek, az összes fel lett takarítva.");
            }

        }

    }

}














