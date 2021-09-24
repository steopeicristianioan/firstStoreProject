using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emag.control
{
    static class ControlFilters
    {
        private static string name = string.Empty;
        private static double price1 = 0;
        private static double price2 = 10000000000;

        public static string Name { get => name; set => name = value; }
        public static double Price1 { get => price1; set => price1 = value; }
        public static double Price2 { get => price2; set => price2 = value; }
    }
}
