using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapRogueLike
{
    class Map
    {

        List<Room> rooms = new List<Room>();
        int nbRooms = 15;

        public Map()
        {
            GraphicsDeviceManager gd = Tool.GraphicsDeviceManager;

            Vector2 center = new Vector2((gd.PreferredBackBufferWidth / 2) / (int)Room.Size.X, (gd.PreferredBackBufferHeight / 2) / (int)Room.Size.Y);
            Vector2 currentPos = Vector2.Zero;
            Random rnd;
            for(int i = 0; i < 50; i++)
            {
                rooms.Add(new Room(center + currentPos));
                Vector2 nextDir;
                do
                {
                    nextDir = Vector2.Zero;
                    rnd = new Random();
                    int nextRoom = rnd.Next(0, 4);
                    switch (nextRoom)
                    {
                        case 0:
                            nextDir.X++;
                            break;
                        case 1:
                            nextDir.X--;
                            break;
                        case 2:
                            nextDir.Y++;
                            break;
                        case 3:
                            nextDir.Y--;
                            break;
                        default:
                            Console.WriteLine("Weird Value for Next Room - {0}", nextRoom);
                            break;
                    }
                } while (rooms.Find(x => x.Emplacement == center + currentPos + nextDir) != null);
                currentPos += nextDir;
            }
            //rooms.Add(new Room(new Vector2(gd.PreferredBackBufferWidth / 2, gd.PreferredBackBufferHeight / 2)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rooms.ForEach(x => x.Draw(spriteBatch));
        }
    }
}
