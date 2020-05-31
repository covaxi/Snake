using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle.NewFolder1
{
    public static class Extensions
    {
        public static IEnumerable<T> Flatten<T>(this T[,] array)
        {
            return array.Cast<T>();
        }
        public static bool AnyOf(this char symbol, params char[] list)
        {
            return list.Contains(symbol);
        }

        public static bool AnyOf(this Element element, params char[] list)
        {
            return list.Contains(element.Symbol);
        }

        public static IEnumerable<Element> GetAll(this IEnumerable<Element> elements, char symbol)
        {
            return elements.Where(e => e.Symbol == symbol);
        }

        public static IEnumerable<Element> GetAll(this Board board, char symbol)
        {
            return board.Where(e => e.Symbol == symbol);
        }


    }
}
