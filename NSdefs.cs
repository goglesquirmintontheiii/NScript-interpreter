using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScolors
{
    class RGB
    {
        int r;
        int b;
        int g;
        public RGB(int rv, int gv, int bv)
        {
            r = rv;
            g = gv;
            b = bv;
        }
    }
}
namespace NS
{
    class nil
    {
        public string Value = "nilcode";
    }
    class Function
    {
        public string name;
        public string code;
        public Function(string namev, string codev)
        {
            name = namev;
            code = codev;
        }
    }
    class Numval
    {
        public string name;
        public double value;
        public Numval(string namev, double inv)
        {
            name = namev;
            value = inv;
        }
    }
    class Stringval
    {
        public string name;
        public string value;
        public Stringval(string namev, string inv)
        {
            value = inv;
            name = namev;
        }
    }
}

namespace luacompilerV1
{
    
}
