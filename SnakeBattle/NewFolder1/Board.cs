using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle.NewFolder1
{
    public class Board : IEnumerable<Element>
    {
        public int Size { get; set; } = 0;
        private Element[,] Elements { get; set; } = new Element[0, 0];
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; private set; }

        public IEnumerable<Element> AllElements = new Element[0];

        public Element this[int x, int y]
        {
            get
            {
                return Elements[x, y];
            }
        }

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
            AllElements = new Element[0];
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

        public IEnumerable<Element> GetNearest()
        {
            return AllElements
                .Where(e => e.AnyOf(Constants.ElementsToReach))
                .OrderBy(e => Math.Abs(e.X - X) + Math.Abs(e.Y - Y));
        }

        public Direction GetMove()
        {
            var moves = new List<(Direction, Element)>();
            var nearest = GetNearest().Take(3).ToList();
            foreach (var elem in nearest)
            {
                if (X == elem.X)
                {
                    if (Y < elem.Y && CanMove(X, Y, Direction.Down))
                        moves.Add((Direction.Down, elem));
                    if (Y > elem.Y && CanMove(X, Y, Direction.Up))
                        moves.Add((Direction.Up, elem));
                }

                if (Y == elem.Y)
                {
                    if (X < elem.X && CanMove(X, Y, Direction.Right))
                        moves.Add((Direction.Right, elem));
                    if (X > elem.X && CanMove(X, Y, Direction.Left))
                        moves.Add((Direction.Right, elem));
                }

                if (X > elem.X && Y > elem.Y)
                {
                    if (CanMove(X, Y, Direction.Left))
                        moves.Add((Direction.Left, elem));
                    if (CanMove(X, Y, Direction.Up))
                        moves.Add((Direction.Up, elem));
                }

                if (X > elem.X && Y < elem.Y)
                {
                    if (CanMove(X, Y, Direction.Left))
                        moves.Add((Direction.Left, elem));
                    if (CanMove(X, Y, Direction.Down))
                        moves.Add((Direction.Down, elem));
                }

                if (X < elem.X && Y > elem.Y)
                {
                    if (CanMove(X, Y, Direction.Right))
                        moves.Add((Direction.Right, elem));
                    if (CanMove(X, Y, Direction.Up))
                        moves.Add((Direction.Up, elem));
                }

                if (X < elem.X && Y < elem.Y)
                {
                    if (CanMove(X, Y, Direction.Right))
                        moves.Add((Direction.Right, elem));
                    if (CanMove(X, Y, Direction.Down))
                        moves.Add((Direction.Down, elem));
                }

            }
            var res = (Direction.None, new Element(0,0));
            if (moves.Count > 0)
                res = moves[Rnd.Next(moves.Count)];
            Console.WriteLine($@"({X},{Y}) {res.Item1} => {res.Item2.X}{res.Item2.Y}
{string.Join(Environment.NewLine, nearest.Select(n => $"{n.Symbol} ({n.X}, {n.Y})"))}");
            return res.Item1;
        }

        public bool CanMove(int x, int y, Direction dir)
        {
            if (x < 0 || y < 0 || x >= Size || y >= Size)
                return false;
            switch(dir)
            {
                case Direction.Down:
                    return Direction != Direction.Up && y < Size - 1 && Elements[x, y + 1].Symbol.AnyOf(Constants.Passable);
                case Direction.Up:
                    return Direction != Direction.Down && y > 0 && Elements[x, y - 1].Symbol.AnyOf(Constants.Passable);
                case Direction.Left:
                    return Direction != Direction.Right && x < Size - 1&& Elements[x - 1, y].Symbol.AnyOf(Constants.Passable);
                case Direction.Right:
                    return Direction != Direction.Left && x > 0 && Elements[x + 1, y].Symbol.AnyOf(Constants.Passable);
                case Direction.None:
                    return false; // Туда мы не пойдём
            }
            return false;
        }

        public void Restart()
        {
            Console.WriteLine("RESTART RESTART RESTART RESTART RESTART RESTART RESTART RESTART RESTART RESTART RESTART RESTART ");
            // TODO
        }

        public void Process(string board)
        {
            board = board.Replace("board=", "");
            var newSize = (int)Math.Sqrt(board.Length);
            if (Size != newSize)
            {
                Size = newSize;
                Restart();
            }
            var x = 0;
            var y = 0;
            Elements = new Element[Size, Size];

            foreach(var c in board)
            {
                Elements[x, y] = new Element(x, y, c);
                if (c.AnyOf(Constants.MyHead))
                {
                    X = x;
                    Y = y;
                    Direction = Direction.None;

                    switch (c)
                    {
                        case Constants.HeadRight:
                        case Constants.HeadSleep:
                        case Constants.HeadDead:
                            {
                                Direction = Direction.Right;
                                break;
                            }
                        case Constants.HeadLeft:
                            {
                                Direction = Direction.Left;
                                break;
                            }
                        case Constants.HeadDown:
                            {
                                Direction = Direction.Down;
                                break;
                            }
                        case Constants.HeadUp:
                            {
                                Direction = Direction.Up;
                                break;
                            }
                        default:
                            Restart();
                            break;
                    }
                    
                }
                x++;
                if (x >= Size)
                {
                    y++;
                    x = 0;
                }
            }
            AllElements = Elements.Flatten();
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

        public IEnumerator<Element> GetEnumerator()
        {
            return Elements.Cast<Element>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.Cast<Element>().GetEnumerator();
        }
    }
}
