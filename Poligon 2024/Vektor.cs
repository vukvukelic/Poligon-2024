using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon_2024
{
    internal class vektor
    {
        public tacka pocetak, kraj;
        public vektor()
        {

        }
        public vektor(tacka pocetak, tacka kraj)
        {
            this.pocetak = pocetak;
            this.kraj = kraj;
        }
        public static tacka vektor_c(vektor A)
        {
            tacka nova = new tacka();
            nova.x = A.kraj.x - A.pocetak.x;
            nova.y = A.kraj.y - A.pocetak.y;
            return nova;
        }
        public static double skalarni(vektor A, vektor B)
        {
            tacka A_c = vektor_c(A);
            tacka B_c = vektor_c(B);
            double skalarni = A_c.x * B_c.x + A_c.y * B_c.y;
            return skalarni;
        }
        public static double VP(vektor A, vektor B)
        {
            tacka A_c = vektor_c(A);
            tacka B_c = vektor_c(B);
            return A_c.x * B_c.y - A_c.y * B_c.x;
        }
        public static double ugao(vektor A, vektor B)
        {
            tacka Ac = vektor_c(A);
            tacka Bc = vektor_c(B);
            double ugaoA = Math.Atan2(Ac.y, Ac.x) * 180 / Math.PI;
            double ugaoB = Math.Atan2(Bc.y, Bc.x) * 180 / Math.PI;
            Console.WriteLine("ugao a={0}", ugaoA);
            Console.WriteLine("ugao b={0}", ugaoB);
            if (ugaoB - ugaoA < 0)
            {
                return ugaoB - ugaoA + 360;
            }
            return ugaoB - ugaoA;
        }
    }
}
