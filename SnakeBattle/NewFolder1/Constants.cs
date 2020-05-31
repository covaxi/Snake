using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBattle.NewFolder1
{
    public class Constants
    {
        public const int RageLength = 10;
        public const int StoneWeight = 3;

        public static readonly char[] Closed = new[]
        {
            Constants.Stone,
            Constants.Wall,
            Constants.StartFloor,
        };

        public static readonly char[] ElemsToDo = new[]
        {
            Constants.EnemyHeadSleep,
            Constants.EnemyTailEndDown,
            Constants.EnemyTailEndLeft,
            Constants.EnemyTailEndUp,
            Constants.EnemyTailEndRight,
            Constants.EnemyTailInactive,
            Constants.EnemyBodyHorizontal,
            Constants.EnemyBodyVertical,
            Constants.EnemyBodyLeftDown,
            Constants.EnemyBodyLeftUp,
            Constants.EnemyBodyRightDown,
            Constants.EnemyBodyRightUp,
            Constants.HeadSleep,
            Constants.TailEndDown,
            Constants.TailEndLeft,
            Constants.TailEndUp,
            Constants.TailEndRight,
            Constants.TailInactive,
            Constants.BodyHorizontal,
            Constants.BodyVertical,
            Constants.BodyLeftDown,
            Constants.BodyLeftUp,
            Constants.BodyRightDown,
            Constants.BodyRightUp,
            Constants.EnemyHeadDown,
            Constants.EnemyHeadLeft,
            Constants.EnemyHeadRight,
            Constants.EnemyHeadUp,
            Constants.EnemyHeadDead,
            Constants.EnemyHeadEvil,
        };

        public static readonly char[] ElementsToReach = new[]
        {
            Constants.Apple,
            Constants.FuryPill,
            Constants.Gold,
        };

        public static readonly char[] MyHead = new[]
        {
            Constants.HeadDead, 
            Constants.HeadDown, 
            Constants.HeadUp, 
            Constants.HeadLeft, 
            Constants.HeadRight, 
            Constants.HeadEvil, 
            Constants.HeadFly, 
            Constants.HeadSleep
        };

        public static readonly char[] Passable = new[]
        {
            Constants.None,
            Constants.Apple,
            Constants.FuryPill,
            Constants.Gold,
            Constants.TailEndDown,
            Constants.TailEndLeft,
            Constants.TailEndUp,
            Constants.TailEndRight,
            Constants.TailInactive,
            Constants.EnemyHeadDown,
            Constants.EnemyHeadLeft,
            Constants.EnemyHeadRight,
            Constants.EnemyHeadUp,
            Constants.EnemyHeadDead,
            Constants.BodyHorizontal,
            Constants.BodyVertical,
            Constants.BodyLeftDown,
            Constants.BodyLeftUp,
            Constants.BodyRightDown,
            Constants.BodyRightUp,
            Constants.EnemyHeadSleep,
            Constants.EnemyTailEndDown,
            Constants.EnemyTailEndLeft,
            Constants.EnemyTailEndUp,
            Constants.EnemyTailEndRight,
            Constants.EnemyTailInactive,
            Constants.EnemyBodyHorizontal,
            Constants.EnemyBodyVertical,
            Constants.EnemyBodyLeftDown,
            Constants.EnemyBodyLeftUp,
            Constants.EnemyBodyRightDown,
            Constants.EnemyBodyRightUp,
        };

        public static readonly char[] Enemy = new[]
        {
                        Constants.EnemyHeadSleep,
            Constants.EnemyTailEndDown,
            Constants.EnemyTailEndLeft,
            Constants.EnemyTailEndUp,
            Constants.EnemyTailEndRight,
            Constants.EnemyTailInactive,
            Constants.EnemyBodyHorizontal,
            Constants.EnemyBodyVertical,
            Constants.EnemyBodyLeftDown,
            Constants.EnemyBodyLeftUp,
            Constants.EnemyBodyRightDown,
            Constants.EnemyBodyRightUp,
                        Constants.EnemyHeadDown,
            Constants.EnemyHeadLeft,
            Constants.EnemyHeadRight,
            Constants.EnemyHeadUp,
            Constants.EnemyHeadDead,
        };

        public static readonly char[] Me = new[]
        {
            Constants.BodyHorizontal,
            Constants.BodyVertical,
            Constants.BodyLeftDown,
            Constants.BodyLeftUp,
            Constants.BodyRightDown,
            Constants.BodyRightUp,
        };

        public static readonly char[] MyTail = new[]
{
            Constants.TailEndDown,
            Constants.TailEndLeft,
            Constants.TailEndUp,
            Constants.TailEndRight,
        };


        #region Символы

        /// <summary>
        /// пустое место
        /// </summary>
        public const char None = ' ';

        /// <summary>
        /// стенка
        /// </summary>
        public const char Wall = '☼';

        /// <summary>
        /// место старта змей
        /// </summary>
        public const char StartFloor = '#';

        public const char Other = '?';

        /// <summary>
        /// яблоки надо кушать от них становишься длинее
        /// </summary>
        public const char Apple = '○';

        /// <summary>
        /// а это кушать не стоит - от этого укорачиваешься
        /// </summary>
        public const char Stone = '●';

        /// <summary>
        /// таблетка полета - дает суперсилы
        /// </summary>
        public const char FlyingPill = '©';

        /// <summary>
        /// таблетка ярости - дает суперсилы
        /// </summary>
        public const char FuryPill = '®';

        /// <summary>
        /// золото - просто очки
        /// </summary>
        public const char Gold = '$';

        // голова твоей змеи в разных состояниях и направлениях
        public const char HeadDown = '▼';
        public const char HeadLeft = '◄';
        public const char HeadRight = '►';
        public const char HeadUp = '▲';

        /// <summary>
        /// раунд проигран
        /// </summary>
        public const char HeadDead = '☻';

        /// <summary>
        /// ты скушал таблетку ярости
        /// </summary>
        public const char HeadEvil = '♥';

        /// <summary>
        /// ты скушал таблетку полета
        /// </summary>
        public const char HeadFly = '♠';

        /// <summary>
        /// твоя змейка ожидает начала раунда
        /// </summary>
        public const char HeadSleep = '&';

        // хвост твоей змейки
        public const char TailEndDown = '╙';
        public const char TailEndLeft = '╘';
        public const char TailEndUp = '╓';
        public const char TailEndRight = '╕';
        public const char TailInactive = '~';

        // туловище твоей змейки
        public const char BodyHorizontal = '═';
        public const char BodyVertical = '║';
        public const char BodyLeftDown = '╗';
        public const char BodyLeftUp = '╝';
        public const char BodyRightDown = '╔';
        public const char BodyRightUp = '╚';

        // змейки противников
        public const char EnemyHeadDown = '˅';
        public const char EnemyHeadLeft = '<';
        public const char EnemyHeadRight = '>';
        public const char EnemyHeadUp = '˄';

        /// <summary>
        /// этот раунд противник проиграл
        /// </summary>
        public const char EnemyHeadDead = '☺';

        /// <summary>
        /// противник скушал таблетку ярости
        /// </summary>
        public const char EnemyHeadEvil = '♣';

        /// <summary>
        /// противник скушал таблетку полета
        /// </summary>
        public const char EnemyHeadFly = '♦';

        /// <summary>
        /// змейка противника ожидает начала раунда
        /// </summary>
        public const char EnemyHeadSleep = 'ø';

        // хвосты змеек противников
        public const char EnemyTailEndDown = '¤';
        public const char EnemyTailEndLeft = '×';
        public const char EnemyTailEndUp = 'æ';
        public const char EnemyTailEndRight = 'ö';
        public const char EnemyTailInactive = '*';

        // туловище змеек противников
        public const char EnemyBodyHorizontal = '─';
        public const char EnemyBodyVertical = '│';
        public const char EnemyBodyLeftDown = '┐';
        public const char EnemyBodyLeftUp = '┘';
        public const char EnemyBodyRightDown = '┌';
        public const char EnemyBodyRightUp = '└';

        #endregion
    }
}
