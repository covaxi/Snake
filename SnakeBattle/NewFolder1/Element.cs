using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle.NewFolder1
{
    public class Element
    {
        public Element(int x, int y, char symbol = Constants.None, int moves = 0)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Moves = moves;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public char Symbol { get; set; }

        public int Moves { get; set; }
    }

    
}

