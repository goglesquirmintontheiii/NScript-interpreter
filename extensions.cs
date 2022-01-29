using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luacompilerV1
{
    public static class extensions
    {
        public static string Replace(this string str, int index, string character)
        {
            string nstr = str.Remove(index,1);
            nstr = nstr.Insert(index,character);
            return nstr;
        }
    }
}
