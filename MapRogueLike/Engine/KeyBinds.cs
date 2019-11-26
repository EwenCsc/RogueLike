using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MapRogueLike.Engine
{
    public class KeyBinds
    {
        private static List<Keys> cameraMoveUp = new List<Keys> { Keys.Z };
        private static List<Keys> cameraMoveDown = new List<Keys> { Keys.S };
        private static List<Keys> cameraMoveLeft = new List<Keys> { Keys.Q };
        private static List<Keys> cameraMoveRight = new List<Keys> { Keys.D };
        private static List<Keys> movevementUp = new List<Keys> { Keys.Z, Keys.Up };
        private static List<Keys> movevementDown = new List<Keys> { Keys.S, Keys.Down };
        private static List<Keys> movevementLeft = new List<Keys> { Keys.Q, Keys.Left };
        private static List<Keys> movevementRight = new List<Keys> { Keys.D, Keys.Right };

        public static List<Keys> CameraMoveUp => cameraMoveUp;
        public static List<Keys> CameraMoveDown => cameraMoveDown;
        public static List<Keys> CameraMoveLeft => cameraMoveLeft;
        public static List<Keys> CameraMoveRight => cameraMoveRight;
        public static List<Keys> MovevementUp => movevementUp;
        public static List<Keys> MovevementDown => movevementDown;
        public static List<Keys> MovevementLeft => movevementLeft;
        public static List<Keys> MovevementRight => movevementRight;
    }
}