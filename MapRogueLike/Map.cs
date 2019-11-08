using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MapRogueLike
{
    class Map
    {

        //List<Room> rooms = new List<Room>();
        Vector2 mapSize = new Vector2(50);
        int nbRooms = 15;
        Room[,] rooms;

        public Map()
        {
            GraphicsDeviceManager gd = Tool.GraphicsDeviceManager;
            Vector2 center = new Vector2((gd.PreferredBackBufferWidth / 2) / (int)Room.Size.X, (gd.PreferredBackBufferHeight / 2) / (int)Room.Size.Y);

            rooms = new Room[(int)mapSize.X, (int)mapSize.Y];

            Random rnd;
            for(int i = 0; i < nbRooms; i++)
            {
            }
            //rooms.Add(new Room(new Vector2(gd.PreferredBackBufferWidth / 2, gd.PreferredBackBufferHeight / 2)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rooms.ToList().ForEach(x => x.Draw(spriteBatch));
        }
    }
}
