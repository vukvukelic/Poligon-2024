using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon_2024
{
    internal class Alati
    {

        public static poligon Unesi()
        {
            Console.Write("Unesite broj temena poligona: ");
            int brojTemena = int.Parse(Console.ReadLine());

            poligon noviPoligon = new poligon(brojTemena);

            for (int i = 0; i < brojTemena; i++)
            {
                Console.WriteLine($"Unesite koordinate temena {i + 1}:");

                Console.Write("x = ");
                double x = double.Parse(Console.ReadLine());

                Console.Write("y = ");
                double y = double.Parse(Console.ReadLine());

                noviPoligon.teme[i] = new tacka(x, y);
            }

            return noviPoligon;
        }

        public static void Snimi(poligon poligon, string imeDatoteke)
        {
                using (StreamWriter datoteka = new StreamWriter(imeDatoteke))
                {
                    datoteka.WriteLine(poligon.broj_temena);

                    for (int i = 0; i < poligon.broj_temena; i++)
                    {
                        datoteka.WriteLine($"{poligon.teme[i].x} {poligon.teme[i].y}");
                    }
                }

        }

        public static void Ucitaj(poligon poligon, string imeDatoteke)
        {
            try
            {
                using (StreamReader datoteka = new StreamReader(imeDatoteke))
                {
                    poligon.broj_temena = Convert.ToInt32(datoteka.ReadLine());
                    poligon.teme = new tacka[poligon.broj_temena];

                    for (int i = 0; i < poligon.broj_temena; i++)
                    {
                        string red = datoteka.ReadLine();
                        string[] podaci = red.Split();

                        double x, y;

                        if (podaci.Length >= 2 && double.TryParse(podaci[0], out x) && double.TryParse(podaci[1], out y))
                        {
                            poligon.teme[i] = new tacka(x, y);
                        }
                        else
                        {
                            Console.WriteLine($"Greska pri konverziji: {red}");
                        }
                    }
                }

                Console.WriteLine($"Poligon je uspešno učitan iz datoteke {imeDatoteke}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greska prilikom citanja fajla: {ex.Message}");
            }
        }

        public static bool Unutra(poligon poligon, tacka p)
        {
            int brojPreseka = 0;

            for (int i = 0; i < poligon.broj_temena; i++)
            {
                int sledeci = (i + 1) % poligon.broj_temena;

                if ((poligon.teme[i].y < p.y && poligon.teme[sledeci].y >= p.y) || (poligon.teme[sledeci].y < p.y && poligon.teme[i].y >= p.y))
                {

                    if (p.y == poligon.teme[i].y)
                    {
                        if ((poligon.teme[i].x <= p.x && p.x <= poligon.teme[sledeci].x) || (poligon.teme[sledeci].x <= p.x && p.x <= poligon.teme[i].x))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        double x_presek = (poligon.teme[i].x + (p.y - poligon.teme[i].y) / (poligon.teme[sledeci].y - poligon.teme[i].y) * (poligon.teme[sledeci].x - poligon.teme[i].x));

                        if (p.x < x_presek)
                        {
                            brojPreseka++;
                        }
                    }
                }
            }

            return brojPreseka % 2 == 1;
        }


    }
}
