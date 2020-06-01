using NLog;
using SnakeBattle.NewFolder1;
using System;
using WebSocketSharp;
using Logger = NLog.Logger;

namespace SnakeBattle
{
    internal class Bot
    {
        const string RESPONSE_PREFIX = "board=";
        Logger Logger = LogManager.GetCurrentClassLogger();
        WebSocket socket;
        Random rnd = new Random();
        Board board = new Board();

        public Bot(string url)
        {
            var server = url.Replace("http", "ws").Replace("board/player/", "ws?user=").Replace("?code=", "&code=");
            socket = new WebSocket(server);
            socket.OnMessage += Socket_OnMessage;
            socket.OnClose += Socket_OnClose;
            socket.OnError += Socket_OnError;
            socket.OnOpen += Socket_OnOpen;
        }

        void Log(string s)
        {
            Logger.Info(s);
            // Console.WriteLine(s);
        }

        string prevBoard = "";
        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            prevBoard = e.Data;
            board.Process(e.Data.Replace("board=", ""));
            if (board.X == 1)
            {
                Console.Clear();
                socket.Send("ACT");
                return;
            }
            // Console.Clear();

            Log($"{Environment.NewLine}{board}");
            var move = board.GetMove();
            string moveStr = "";
            if ((int)move == 0)
                moveStr = "ACT";
            else
                moveStr = move.ToString();

            Console.WriteLine($"  -- {moveStr} --");

            socket.Send(moveStr);
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            Log("### Connection established");
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            Log("### Error");
            Log($"e");
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            Log("### Disconnected");
        }

        public void Connect()
        {
            socket.Connect();
        }
    }
}