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

        public Vector2 MapSize => mapSize;
        public Room[,] Rooms => rooms;

        public Map()
        {
            GraphicsDeviceManager gd = Tool.GraphicsDeviceManager;
            Vector2 center = new Vector2((gd.PreferredBackBufferWidth / 2) / (int)Room.Size.X, (gd.PreferredBackBufferHeight / 2) / (int)Room.Size.Y);

            // Init Rooms
            rooms = new Room[(int)mapSize.X, (int)mapSize.Y];
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(0); j++)
                {
                    rooms[i, j] = new Room(new Vector2(i, j), true);
                }
            }

            // Generating Map
            Random rnd;
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                List<Room> setedRooms = rooms.Cast<Room>().ToList().FindAll(x => !x.isEmpty);
                if (setedRooms.Count == 0)
                {
                    rooms[(int)center.X, (int)center.Y] = new Room(center);
                    Console.WriteLine("{0} -> ({1}, {2})", i, center.X, center.Y);
                }
                else
                {
                    rnd = new Random();
                    List<Room> notFullNeighbouredRooms = setedRooms.FindAll(x => !x.IsFullNeighboured(this));
                    Room selectedRoom = notFullNeighbouredRooms[rnd.Next(0, notFullNeighbouredRooms.Count)];

                    rnd = new Random();
                    List<Room> freeNeighbours = selectedRoom.GetFreeNeighbours(this);
                    Vector2 newEmplacement = freeNeighbours[rnd.Next(0, freeNeighbours.Count)].Emplacement;
                    rooms[(int)newEmplacement.X, (int)newEmplacement.Y] = new Room(newEmplacement);
                    Console.WriteLine("{0} -> ({1}, {2})", i, (int)newEmplacement.X, (int)newEmplacement.Y);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(0); j++)
                {
                    rooms[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
