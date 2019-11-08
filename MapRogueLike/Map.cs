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
        Vector2 mapSize = new Vector2(25);
        int nbRooms = 25;
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
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    rooms[i, j] = new Room(new Vector2(i, j), true);
                }
            }

            // Generating Map
            PrimAlgoGenerating();
            //GeneratingRoomsRandomly();
        }

        private void PrimAlgoGenerating()
        {
            List<Vector2> emplacements = new List<Vector2>();
            Vector2 center = new Vector2(rooms.GetLength(0) / 2, rooms.GetLength(1) / 2);
            Random rnd;

            for (int i = 0; i < nbRooms / 5; i++)
            {
                if (i == 0)
                {
                    emplacements.Add(center);
                }
                else
                {
                    Vector2 emplacement;
                    do
                    {
                        rnd = new Random();
                        emplacement = new Vector2(rnd.Next(rooms.GetLength(0)), rnd.Next(rooms.GetLength(1)));
                    } while (emplacements.FindAll(x => x == emplacement).Count > 0);
                    emplacements.Add(emplacement);
                }
            }

            for (int i = 0; i < nbRooms - (nbRooms / 5); i++)
            {

            }

            emplacements.ForEach(x => Console.WriteLine("{0}", x));
            emplacements.ForEach(x => rooms[(int)x.X, (int)x.Y] = new Room(x));
        }

        private void GeneratingRoomsRandomly()
        {
            //Vector2 center = new Vector2((Tool.GraphicsDeviceManager.PreferredBackBufferWidth / 2) / (int)Room.Size.X, (Tool.GraphicsDeviceManager.PreferredBackBufferHeight / 2) / (int)Room.Size.Y);
            Vector2 center = new Vector2(rooms.GetLength(0) / 2, rooms.GetLength(1) / 2);
            Random rnd;
            for (int i = 0; i < nbRooms; i++)
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
                    int notFullNeighbouredRoomsOriginalLength = notFullNeighbouredRooms.Count;
                    for (int j = 0; j < notFullNeighbouredRoomsOriginalLength; j++)
                    {
                        for (int k = 0; k < 4 - notFullNeighbouredRooms[j].GetFreeNeighbours(this).Count; k++)
                        {
                            notFullNeighbouredRooms.Add(notFullNeighbouredRooms[j]);
                        }
                    }
                    Room selectedRoom = notFullNeighbouredRooms[rnd.Next(notFullNeighbouredRooms.Count)];

                    rnd = new Random();
                    List<Room> freeNeighbours = selectedRoom.GetFreeNeighbours(this);
                    Vector2 newEmplacement = freeNeighbours[rnd.Next(freeNeighbours.Count)].Emplacement;
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
