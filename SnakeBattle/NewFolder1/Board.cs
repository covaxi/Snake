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
    public class Board
    {
        public int Size { get; set; } = 0;
        private Element[,] Elements { get; set; } = new Element[0, 0];
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; private set; }
        public int Rage { get; set; } = 0;
        public int Length { get; set; } = 2;
        public int Score { get; set; } = 0;
        public int Stones { get; private set; }
        public List<Direction> Moves = new List<Direction>();
        public string QQ = "";

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

        public IEnumerable<Element> GetNearest(int maxDistance = 0, params char[] elements)
        {
            if (!elements.Any())
                elements = Constants.ElementsToReach;
            return AllElements
                .Where(e => e.AnyOf(elements))
                .Select(e => new { Element = e, Distance = Math.Abs(e.X - X) + Math.Abs(e.Y - Y) })
                .Where(e => maxDistance == 0 || e.Distance < maxDistance)
                .OrderBy(e => e.Distance)
                .Select(e => e.Element);
        }

        public Direction GetMove()
        {
            var boards = new List<(Board Board, bool Processed)>();
            boards.Add((this, false));
            var allBoards = new Board[Size, Size];
            allBoards[X, Y] = this;

            int moves = 0;
            int oldCount = 0;
            while(moves < 150)
            {
                var newBoards = new List<(Board Board, bool Processed)>();
                int i = 0;
                while(i < boards.Count)
                {
                    var board = boards[i];
                    if (board.Processed)
                    {
                        continue;
                    }
                    var tempBoards = new[]
                    {
                        board.Board.GetMoved(Direction.Left),
                        board.Board.GetMoved(Direction.Right),
                        board.Board.GetMoved(Direction.Up),
                        board.Board.GetMoved(Direction.Down),
                    };

                    foreach (var tempBoard in tempBoards)
                    {
                        if (tempBoard.Score < 0)
                            continue;
                        var x = tempBoard.X;
                        var y = tempBoard.Y;
                        var oldBoard = allBoards[x, y];
                        if (oldBoard == null || oldBoard.Score < tempBoard.Score && tempBoard.CanMoveAny())
                        {
                            newBoards.Add((allBoards[x, y] = tempBoard, false));
                        }
                    }
                    board.Processed = true;
                    i++;
                }
                if (newBoards.Count == 0)
                    break;
                boards = newBoards;
                moves++;
            }

            var found = boards.OrderByDescending(b => b.Board.Score).First().Board.Moves;
            Console.WriteLine($"({X},{Y}) : {string.Join("+", found.Select(f => f.ToString()))}");
            if (found.Count > 0)
                return found.First();
            return Direction.None;
            

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

        public Board Clone(Direction dir)
        {
            var board = new Board()
            {
                X = X,
                Y = Y,
                Direction = dir,
                Elements = new Element[Size, Size],
                Rage = Rage - 1,
                Length = Length,
                Stones = Stones,
                Moves = new List<Direction>(Moves),
                Size = Size,
            };

            for (int x = 0; x < Size; x++)
            {
                for(int y = 0; y < Size; y++)
                {
                    board.Elements[x,y] = new Element(x, y, Elements[x, y].Symbol, Elements[x, y].Moves);
                }
            }

            switch(dir)
            {
                case Direction.Left:
                    board.X--;
                    break;
                case Direction.Right:
                    board.X++;
                    break;
                case Direction.Up:
                    board.Y--;
                    break;
                case Direction.Down:
                    board.Y++;
                    break;
            }

            return board;
        }

        public int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2-x1) + Math.Abs(y2-y1);
        }

        public Board GetMoved(Direction dir)
        {
            Board board = new Board();

            if (dir == Direction.Left && (Direction == Direction.Right || X < 1) ||
                dir == Direction.Right && (Direction == Direction.Left || X > Size - 2) ||
                dir == Direction.Down && (Direction == Direction.Up || Y > Size - 2) ||
                dir == Direction.Up && (Direction == Direction.Down || Y < 1))
            {
                board.Score = -1000;
                return board;
            }

            board = Clone(dir);
            var newPos = (board.X, board.Y);
            bool apple = false;
            board.Moves.Add(dir);
            var qq = $"{board.Elements[newPos.X, newPos.Y].Symbol} ({newPos.X},{newPos.Y})";
            board.QQ += qq;

            for (int x = 0; x < Size; x++)
            {
                for(int y = 0; y < Size; y++)
                {
                    switch(board.Elements[newPos.X, newPos.Y].Symbol)
                    {
                        case Constants.Wall:
                            board.Score -= 1000;
                            break;
                        case Constants.None:
                            break;
                        case Constants.Apple:
                            board.Length ++;
                            board.Score ++;
                            apple = true;
                            break;
                        case Constants.FuryPill:
                            board.Rage += Constants.RageLength;
                            break;
                        case Constants.Gold:
                            board.Score += 10;
                            break;
                        case Constants.Stone:
                            board.Stones++;
                            board.Score += Constants.StoneWeight;
                            if (board.Rage > 0 || board.Length > 4)
                            {
                                board.Length -= 3;
                            }
                            else
                            {
                                board.Score -= 1000;
                            }
                            break;
                        case Constants.EnemyTailEndDown:
                        case Constants.EnemyTailEndLeft:
                        case Constants.EnemyTailEndUp:
                        case Constants.EnemyTailEndRight:
                        case Constants.EnemyTailInactive:
                        case Constants.EnemyBodyHorizontal:
                        case Constants.EnemyBodyVertical:
                        case Constants.EnemyBodyLeftDown:
                        case Constants.EnemyBodyLeftUp:
                        case Constants.EnemyBodyRightDown:
                        case Constants.EnemyBodyRightUp:
                        case Constants.EnemyHeadDown:
                        case Constants.EnemyHeadUp:
                        case Constants.EnemyHeadLeft:
                        case Constants.EnemyHeadRight:
                        case Constants.EnemyHeadEvil:
                            board.Score += 20;
                            break;
                        
                        case Constants.TailEndDown:
                        case Constants.TailEndLeft:
                        case Constants.TailEndUp:
                        case Constants.TailEndRight:
                        case Constants.TailInactive:
                        case Constants.EnemyHeadDead:
                        case Constants.BodyHorizontal:
                        case Constants.BodyVertical:
                        case Constants.BodyLeftDown:
                        case Constants.BodyLeftUp:
                        case Constants.BodyRightDown:
                        case Constants.BodyRightUp:
                            break;
                    }
                }
            }
            bool tailFound = false;
            for (int x = 0; !tailFound && x < Size; x++)
            {
                for (int y = 0; !tailFound && y < Size; y++)
                {
                    if (!apple && Elements[x, y].AnyOf(Constants.MyTail))
                    {
                        tailFound = true;
                        board.Elements[x, y].Symbol = Constants.None;
                        switch (Elements[x, y].Symbol)
                        {
                            case Constants.TailEndDown:
                                if (y < 1)
                                    break;
                                switch (Elements[x, y - 1].Symbol)
                                {
                                    case Constants.BodyRightDown:
                                        board.Elements[x, y - 1].Symbol = Constants.TailEndRight;
                                        break;
                                    case Constants.BodyLeftDown:
                                        board.Elements[x, y - 1].Symbol = Constants.TailEndLeft;
                                        break;
                                    default:
                                        board.Elements[x, y - 1].Symbol = Constants.TailEndDown;
                                        break;
                                }

                                break;
                            case Constants.TailEndUp:
                                if (y > Size - 2)
                                    break;
                                switch (Elements[x, y + 1].Symbol)
                                {
                                    case Constants.BodyRightUp:
                                        board.Elements[x, y + 1].Symbol = Constants.TailEndRight;
                                        break;
                                    case Constants.BodyLeftUp:
                                        board.Elements[x, y + 1].Symbol = Constants.TailEndLeft;
                                        break;
                                    default:
                                        board.Elements[x, y + 1].Symbol = Constants.TailEndUp;
                                        break;
                                }
                                break;
                            case Constants.TailEndLeft:
                                if (x < 1)
                                    break;
                                switch (Elements[x - 1, y].Symbol)
                                {
                                    case Constants.BodyRightUp:
                                        board.Elements[x - 1, y].Symbol = Constants.TailEndUp;
                                        break;
                                    case Constants.BodyRightDown:
                                        board.Elements[x - 1, y].Symbol = Constants.TailEndDown;
                                        break;
                                    default:
                                        board.Elements[x - 1, y].Symbol = Constants.TailEndLeft;
                                        break;
                                }
                                break;
                            case Constants.TailEndRight:
                                if (x > Size - 2)
                                    break;
                                switch (Elements[x + 1, y].Symbol)
                                {
                                    case Constants.BodyRightUp:
                                        board.Elements[x + 1, y].Symbol = Constants.TailEndUp;
                                        break;
                                    case Constants.BodyLeftUp:
                                        board.Elements[x + 1, y].Symbol = Constants.TailEndDown;
                                        break;
                                    default:
                                        board.Elements[x + 1, y].Symbol = Constants.TailEndRight;

                                        break;
                                }
                                return board;
                        }
                    }
                }
            }
            return board;
        }

        public bool CanMoveAny()
        {
            return CanMove(Direction.Left) ||
                CanMove(Direction.Right) ||
                CanMove(Direction.Up) ||
                CanMove(Direction.Down);
        }

        public bool CanMove(Direction dir)
        {
            var x = X;
            var y = Y;
            var el = Elements[x, y].Symbol;
            switch (dir)
            {
                case Direction.Left:
                    if (x > 0)
                    {
                        el = Elements[x - 1, y].Symbol;
                        return el != Constants.BodyHorizontal && el != Constants.BodyRightDown && el != Constants.BodyRightUp;
                    }
                    else
                        return false;
                case Direction.Right:
                    if (x < Size - 1)
                    {
                        return el != Constants.BodyHorizontal && el != Constants.BodyLeftDown && el != Constants.BodyLeftUp;
                    }
                    else
                        return false;
                case Direction.Up:
                    if (y > 0)
                    {
                        return el != Constants.BodyVertical && el != Constants.BodyRightDown && el != Constants.BodyLeftDown;
                    }
                    else
                        return false;
                case Direction.Down:
                    if (y < Size - 1)
                    {
                        return el != Constants.BodyVertical && el != Constants.BodyRightUp && el != Constants.BodyLeftUp;
                    }
                    else
                        return false;
                default:
                    return false;
            }
        }
    }
}
