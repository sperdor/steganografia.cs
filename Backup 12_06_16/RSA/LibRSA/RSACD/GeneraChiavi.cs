using System;
using System.Collections.Generic;
using System.IO;


namespace LibRSA
{
    public class GeneraChiavi : GeneraPrimi
    {
        BigInteger _p;
        BigInteger _q;
        BigInteger _N;
        BigInteger _b;
        BigInteger _e;
        BigInteger _d;

        public BigInteger N
        {
            get { return _N; }
        }
        public BigInteger E
        {
            get { return _e; }
        }
        public BigInteger D
        {
            get { return _d; }
        }

        public GeneraChiavi()
            : base()
        {
            _p = AssegnaP();
            _q = AssegnaP();
            _N = CalcolaN(_p, _q);
            _b = CalcolaB(_p, _q);
            _e = CalcolaE(_b);
            _d = CalcolaD(_e, _b);
        }

        private BigInteger AssegnaP()
        {
            Random rdm = new Random();
            BigInteger _primo = 0;
            _primo = base.P[rdm.Next(0, base.P.Count - 1)];
            int index = base.P.IndexOf(_primo);
            base.P.RemoveAt(index);

            return _primo;
        }

        private BigInteger CalcolaN(BigInteger _p, BigInteger _q)
        {
            BigInteger _N = 0;
            _N = _p * _q;

            return _N;
        }

        private BigInteger CalcolaB(BigInteger _p, BigInteger _q)
        {
            BigInteger _b = 0;
            _b = (_p - 1) * (_q - 1);

            return _b;
        }

        private BigInteger CalcolaE(BigInteger _b)
        {
            BigInteger _E = 0;
            BigInteger _iAppo = 0;
            List<BigInteger> _mcd = new List<BigInteger>();

            for (BigInteger _e = 2; _e < _b; _e++)
            {
                if (_e == 2)
                {
                    _mcd.Add(_b);
                    _mcd.Add(_e);
                }
                else
                {
                    _mcd[0] = _b;
                    _mcd[1] = _e;
                }

                while (_mcd[1] > 0)
                {
                    if (_mcd[0] > _mcd[1])
                        _mcd.Reverse();

                    _mcd[1] %= _mcd[0];
                }

                if (_mcd[0] == 1)
                {
                    _E = _e;
                    break;
                }
            }

            return _E;
        }

        private BigInteger CalcolaD(BigInteger _e, BigInteger _b)
        {
            BigInteger _d = 0;

            _d = _e.modInverse(_b);

            return _d;
        }
    }
}
