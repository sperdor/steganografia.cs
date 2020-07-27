using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRSA.RSACD
{
    public class SessioneCollection:List<Sessione>
    {
        public bool Add(Sessione sessione)
        {
            bool ok = false;
            int pos = this.IndexOf(sessione);
            if (pos == -1)
            {
                ok = true;
                base.Add(sessione);
            }

            return ok;
        }
    }
}
