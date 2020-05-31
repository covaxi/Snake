using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SnakeBattle.Classes
{
    public abstract class BotBase
    {
        private const string RESPONSE_PREFIX = "board=";
        private readonly WebSocket _socket;

        protected BotBase(string url)
        {
            var server = url.Replace("http", "ws").Replace("board/player/", "ws?user=").Replace("?code=", "&code=");
            this._socket = new WebSocket(server);
            _socket.OnMessage += Socket_OnMessage;
            _socket.OnClose += Socket_OnClose;
            _socket.OnError += Socket_OnError;
            _socket.OnOpen += Socket_OnOpen;
        }

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            Console.WriteLine("Connection established");
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("### error ###");
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            Console.WriteLine("### disconnected ###");
        }
    }
}
