using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MapRogueLike.V2
{
    class Input
    {
        static KeyboardState state = Keyboard.GetState();
        static List<Keys> lastKeys = new List<Keys>();
        static List<Keys> currentKeys = new List<Keys>();

        public static void Update()
        {
            lastKeys = currentKeys;
            state = Keyboard.GetState();
            currentKeys = state.GetPressedKeys().ToList();
        }

        public static bool GetKeyDown(Keys k)
        {
            bool result = false;
            if (state.IsKeyDown(k))
            {
                result = currentKeys.Contains(k) && !lastKeys.Contains(k);
            }
            return result;
        }

        public static bool GetKeyUp(Keys k)
        {
            return false;
        }

        public static bool GetKey(Keys k)
        {
            return state.IsKeyDown(k);
        }
    }
}
