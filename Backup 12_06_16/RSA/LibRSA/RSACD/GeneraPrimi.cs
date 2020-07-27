using System;
using System.Collections.Generic;
using System.Linq;

namespace LibRSA
{
    public class GeneraPrimi
    {
        public List<BigInteger> P;

        //public List<BigInteger> P
        //{
        //    get { return _P = GeneraP(); }
        //}

        public GeneraPrimi()
        {
            P = GeneraP();
        }

        public List<BigInteger> GeneraP()
        {
            BigInteger _inizio = 1000000;
            BigInteger _fine = 1010000;
            BigInteger _limite = 0;
            List<BigInteger> _primi = new List<BigInteger>();
            BigInteger _primo = 0;

            for (BigInteger i = _inizio; i <= _fine; i++)
            {
                bool primo = true;
                if (!pari(i))
                {
                    _limite = (i.sqrt()) + 1;
                    for (int j = 3; (j <= _limite) && (primo); j++)
                        if (i % j == 0) primo = false;
                }
                else primo = false;
                if (primo)
                    _primi.Add(i);

            }
            return _primi;
        }
        public static bool pari(BigInteger numero)
        {
            if (numero % 2 == 0) return true;
            else return false;
        }
    }
}
