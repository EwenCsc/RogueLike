using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRogueLike.Engine
{
    public static class Time
    {
        static float deltaTime = 0;
        public static float DeltaTime => deltaTime;

        static public void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
        }
    }
}
