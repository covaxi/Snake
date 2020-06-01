using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle
{
    class Program
    {
        const string GameUrl = "http://codebattle-pro-2020s1.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/su1nfuikcpedebzflkx0?code=5453968337040027982&gameName=snakebattle";

        static Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Logger.Info("");
            Logger.Info("========================================================");
            Logger.Info("Starting");

            var bot = new Bot(GameUrl);
            bot.Connect();
            Console.ReadKey();
        }
    }
}
