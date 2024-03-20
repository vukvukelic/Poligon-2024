using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon_2024
{
    internal class poligon
    {
        public int broj_temena;
        public tacka[] teme;
        public poligon()
        {
            teme = new tacka[broj_temena];
        }
        public poligon(int n)
        {
            broj_temena = n;
            teme = new tacka[broj_temena];
        }

        public poligon(tacka[] temena)
        {
            broj_temena = temena.Length;
            teme = temena;

            /*for (int i = 0; i < broj_temena; i++)
            {
                teme[i] = temena[i];
            }*/
        }
        public Boolean konveksan()
        {
            int plusevi = 0;
            for (int i = 0; i < teme.Length - 1; i++)
            {
                vektor prvi = new vektor(teme[i], teme[(i + 1) % broj_temena]);
                vektor drugi = new vektor(teme[(i + 1) % broj_temena], teme[(i + 2) % broj_temena]);
                if (vektor.VP(prvi, drugi) > 0) plusevi++;
            }
            if ((plusevi == 0) || plusevi == broj_temena) return true;
            else return false;
        }

        public bool Prost()
        {
            for (int i = 0; i < broj_temena; i++)
            {
                vektor trenutni = new vektor(teme[i], teme[(i + 1) % broj_temena]);

                for (int j = i + 2; j < broj_temena; j++)
                {
                    vektor drugi = new vektor(teme[j], teme[(j + 1) % broj_temena]);
                    if (Presek(trenutni, drugi))
                        return false;
                }
            }

            return true;
        }

        private bool Presek(vektor vektor1, vektor vektor2)
        {
            double vp1 = vektor.VP(vektor1, new vektor(vektor1.pocetak, vektor2.pocetak));
            double vp2 = vektor.VP(vektor1, new vektor(vektor1.pocetak, vektor2.kraj));

            double vp3 = vektor.VP(vektor2, new vektor(vektor2.pocetak, vektor1.pocetak));
            double vp4 = vektor.VP(vektor2, new vektor(vektor2.pocetak, vektor1.kraj));

            return (vp1 * vp2 < 0) && (vp3 * vp4 < 0);
        }


        public double Obim()
        {
            double obim = 0;
            for (int i = 0; i < broj_temena; i++)
            {
                int sledeciIndeks = (i + 1) % broj_temena;
                obim += DužinaStranice(teme[i], teme[sledeciIndeks]);
            }
            return obim;
        }

        private double DužinaStranice(tacka tacka1, tacka tacka2)
        {
            return Math.Sqrt(Math.Pow(tacka2.x - tacka1.x, 2) + Math.Pow(tacka2.y - tacka1.y, 2));
        }

        public double Povrsina()
        {
            double povrsina = 0;
            tacka centar = CentarPoligona();

            for (int i = 0; i < broj_temena; i++)
            {
                int sledeciIndeks = (i + 1) % broj_temena;
                tacka trenutna = teme[i];
                tacka sledeca = teme[sledeciIndeks];

                povrsina += PovrsinaTrougla(trenutna, sledeca, centar);
            }

            return Math.Abs(povrsina);
        }

        private tacka CentarPoligona()
        {
            double xSum = 0, ySum = 0;
            foreach (tacka t in teme)
            {
                xSum += t.x;
                ySum += t.y;
            }
            double xCentar = xSum / broj_temena;
            double yCentar = ySum / broj_temena;
            return new tacka(xCentar, yCentar);
        }

        private double PovrsinaTrougla(tacka a, tacka b, tacka c)
        {
            double s = (DužinaStranice(a, b) + DužinaStranice(b, c) + DužinaStranice(c, a)) / 2;
            return Math.Sqrt(s * (s - DužinaStranice(a, b)) * (s - DužinaStranice(b, c)) * (s - DužinaStranice(c, a)));
        }

        public void Ispisi()
        {
            Console.WriteLine($"Broj temena: {broj_temena}");

            for (int i = 0; i < broj_temena; i++)
            {
                Console.WriteLine($"Teme {i + 1}: ({teme[i].x}, {teme[i].y})");
            }
        }

        public poligon GrahamovSkeniranje()
        {
            // Ako imamo manje od 3 temena, ne možemo formirati konveksni omotač
            if (broj_temena < 3) return null;

            // Pronalazimo tačku sa najmanjom y-koordinatom (i najlevlju ako ima više takvih)
            tacka pocetnaTacka = teme.OrderBy(t => t.y).ThenBy(t => t.x).First();

            // Sortiramo temena prema polaru od pocetne tacke
            List<tacka> sortiranaTemena = teme.ToList();
            sortiranaTemena.Remove(pocetnaTacka);
            sortiranaTemena.Sort((t1, t2) =>
            {
                double ugao1 = Math.Atan2(t1.y - pocetnaTacka.y, t1.x - pocetnaTacka.x);
                double ugao2 = Math.Atan2(t2.y - pocetnaTacka.y, t2.x - pocetnaTacka.x);
                if (ugao1 < ugao2) return -1;
                if (ugao1 > ugao2) return 1;
                return 0;
            });
            sortiranaTemena.Insert(0, pocetnaTacka); // Dodajemo pocetnu tacku na pocetak sortirane liste

            // Inicijalizujemo stek za konveksni omotač
            Stack<tacka> konveksniOmotac = new Stack<tacka>();
            konveksniOmotac.Push(sortiranaTemena[0]);
            konveksniOmotac.Push(sortiranaTemena[1]);

            // Dodajemo temena u konveksni omotač
            for (int i = 2; i < sortiranaTemena.Count; i++)
            {
                tacka vrh = konveksniOmotac.Pop();
                while (Orientation(konveksniOmotac.Peek(), vrh, sortiranaTemena[i]) != 2)
                {
                    vrh = konveksniOmotac.Pop();
                }
                konveksniOmotac.Push(vrh);
                konveksniOmotac.Push(sortiranaTemena[i]);
            }

            // Konvertujemo stek u niz tačaka
            tacka[] tackeOmotača = konveksniOmotac.ToArray();

            // Kreiramo novi poligon sa temenima konveksnog omotača
            return new poligon(tackeOmotača);
        }

        // Pomoćna funkcija za određivanje orijentacije trojki tačaka
        private static int Orientation(tacka p, tacka q, tacka r)
        {
            double val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);
            if (Math.Abs(val) < 0.000001) return 0; // Kolinearne
            return (val > 0) ? 1 : 2; // U smeru kazaljke na satu ili u suprotnom smeru
        }

    }
}
