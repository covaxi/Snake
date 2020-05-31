using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle.NewFolder1
{
    public static class Extensions
    {
        public static bool AnyOf(this char symbol, params char[] list)
        {
            return list.Contains(symbol);
        }
    }
}
