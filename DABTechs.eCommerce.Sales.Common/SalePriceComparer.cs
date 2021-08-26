using System;
using System.Collections.Generic;
using System.Linq;

namespace DABTechs.eCommerce.Sales.Common
{
    /// <summary>
    /// The Sale Price Comparer.
    /// </summary>
    /// <seealso cref="IComparer{string}" />
    public class SalePriceComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            bool xInt, yInt;

            xInt = int.TryParse(x, out int s1);
            yInt = int.TryParse(y, out int s2);

            if (xInt && yInt)
            {
                return s1.CompareTo(s2);
            }

            if (xInt && !yInt)
            {
                if (SplitInt(y, out s2, out _))
                {
                    return s1.CompareTo(s2);
                }
                else
                {
                    return -1;
                }
            }

            if (!xInt && yInt)
            {
                if (SplitInt(x, out s1, out _))
                {
                    return s2.CompareTo(s1);
                }
                else
                {
                    return 1;
                }
            }

            return x.CompareTo(y);
        }

        private bool SplitInt(string sin, out int x, out string sout)
        {
            x = 0;
            sout = null;

            int i = -1;
            bool isNumeric = false;
            IEnumerable<string> numbers = Enumerable.Range(0, 10).Select(it => it.ToString());
            CharEnumerator ie = sin.GetEnumerator();

            while (ie.MoveNext() && numbers.Contains(ie.Current.ToString()))
            {
                isNumeric |= true;
                ++i;
            }

            if (isNumeric)
            {
                sout = sin.Substring(i + 1);
                sin = sin.Substring(0, i + 1);
                int.TryParse(sin, out x);
            }

            return false;
        }
    }
}