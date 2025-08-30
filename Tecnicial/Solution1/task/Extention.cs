using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task
{
    public static class Extention
    {
            public static double calc(this double a, double b, Func<double, double, double> result)
    {
        return result(a, b);
    }

    }
}
