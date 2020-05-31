using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle.NewFolder1
{
    public class Board
    {
        public int Size { get; set; } = 0;
        public Element[,] Elements { get; set; } = new Element[0, 0];

        public static Random Rnd = new Random();

        public Board(string board = null)
        {
            if (board != null)
            {
                Process(board);
            }
        }

        public Board(int size)
        {
            Size = size;
            Elements = new Element[Size, Size];

        }

        public Board GetEmpty()
        {
            var newBoard = new Board(Size);
            
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    var old = Elements[i, j];
                    var elem = old;
                    if (Constants.Closed.All(e => e != old.Symbol))
                        elem = new Element(i,j, Constants.None);
                    else
                        elem = old;

                    newBoard.Elements[i, j] = elem;
                }
            }

            return newBoard;
        }

        public Direction GetMove()
        {
            var x = -1;
            var y = -1;

            var board = GetEmpty();
            for (int i = 0; i < Size && x == -1; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Elements[i, j].Symbol.AnyOf(Constants.MyHead))
                    {
                        x = i; y = j;
                        break;
                    }
                }
            }

            List<Direction> moves = new List<Direction>();
            foreach(Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (CanMove(x, y, dir))
                    moves.Add(dir);
            }

            if (moves.Count > 0)
                return moves[Rnd.Next(moves.Count)];
            return Direction.None;
        }

        public bool CanMove(int x, int y, Direction dir)
        {
            if (x < 0 || y < 0 || x >= Size || y >= Size)
                return false;
            switch(dir)
            {
                case Direction.Down:
                    return y < Size && Elements[x, y + 1].Symbol.AnyOf(Constants.Passable);
                case Direction.Up:
                    return y > 0 && Elements[x, y - 1].Symbol.AnyOf(Constants.Passable);
                case Direction.Left:
                    return x < Size && Elements[x - 1, y + 1].Symbol.AnyOf(Constants.Passable);
                case Direction.Right:
                    return x > 0 && Elements[x + 1, y].Symbol.AnyOf(Constants.Passable);
                case Direction.None:
                    return false; // Туда мы не пойдём
            }
            return false;
        }

        public void Process(string board)
        {
            board = board.Replace("board=", "");
            Size = (int)Math.Sqrt(board.Length);
            var x = 0;
            var y = 0;
            Elements = new Element[Size, Size];

            foreach(var c in board)
            {
                Elements[x++, y] = new Element(x, y, c);
                if (x >= Size)
                {
                    y++; 
                    x = 0;
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            for(int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                    result += Elements[x, y].Symbol;
                result += Environment.NewLine;
            }
            return result;
        }
    }
}
