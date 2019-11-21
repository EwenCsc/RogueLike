using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MapRogueLike.Deprecated
{
    public class Map
    {
        Vector2 mapSize = new Vector2(25);
        Room[,] rooms;

        public Vector2 MapSize => mapSize;
        public Room[,] Rooms => rooms;

        public Map()
        {
            // Init Rooms
            rooms = new Room[(int)mapSize.X, (int)mapSize.Y];
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    rooms[i, j] = new Room(new Vector2(i, j), true);
                }
            }

            // Generating Rooms
            GenerateRoomsWithDoors();
        }

        /// <summary>
        /// Generate One room then room create others rooms
        /// </summary>
        private void GenerateRoomsWithDoors()
        {
            Random rnd = new Random();
            Vector2 center = new Vector2(rooms.GetLength(0) / 2, rooms.GetLength(1) / 2);
            AddRoom(center, Vector4.One, Vector4.Zero);

            // Debug
            {
                for (int i = 0; i < rooms.GetLength(0); i++)
                {
                    for (int j = 0; j < rooms.GetLength(1); j++)
                    {
                        if (!rooms[i, j].isEmpty)
                        {
                            Console.WriteLine("Room[{0}, {1}] -> {2}", rooms[i, j].Emplacement.X, rooms[i, j].Emplacement.Y, rooms[i, j].Position);
                        }
                    }
                }
                Console.WriteLine("Rooms Generated - {0}", rooms.Cast<Room>().ToList().FindAll(x => !x.isEmpty).Count);
            }
        }

        /// <summary>
        /// Add Room and Add Rooms where door is open
        /// </summary>
        /// <param name="_emplacement">Coordonate of the array</param>
        /// <param name="_openingDirections">Opened Doors</param>
        /// <param name="_forbiddenDirection"></param>
        void AddRoom(Vector2 _emplacement, Vector4 _openingDirections, Vector4 _forbiddenDirections)
        {
            rooms[(int)_emplacement.X, (int)_emplacement.Y] = new Room(_emplacement, _openingDirections);


            if (_openingDirections.X == 1 && _forbiddenDirections.X == 0 && _emplacement.Y - 1 >= 0)
            {
                Vector4 nextOpeningDirections = SetOpeningDirection(_emplacement, _openingDirections, _forbiddenDirections);
                nextOpeningDirections.Y = 1;
                Vector2 tmpEmp = _emplacement;
                tmpEmp += -Vector2.UnitY;
                Vector4 nextForbiddenDir = new Vector4(0, 1, 0, 0);
                AddRoom(tmpEmp, nextOpeningDirections, nextForbiddenDir);
            }
            if (_openingDirections.Y == 1 && _forbiddenDirections.Y == 0 && _emplacement.Y + 1 < MapSize.Y)
            {
                Vector4 nextOpeningDirections = SetOpeningDirection(_emplacement, _openingDirections, _forbiddenDirections);
                nextOpeningDirections.X = 1;
                Vector2 tmpEmp = _emplacement;
                tmpEmp += Vector2.UnitY;
                Vector4 nextForbiddenDir = new Vector4(1, 0, 0, 0);
                AddRoom(tmpEmp, nextOpeningDirections, nextForbiddenDir);
            }
            if (_openingDirections.Z == 1 && _forbiddenDirections.Z == 0 && _emplacement.X - 1 >= 0)
            {
                Vector4 nextOpeningDirections = SetOpeningDirection(_emplacement, _openingDirections, _forbiddenDirections);
                nextOpeningDirections.W = 1;
                Vector2 tmpEmp = _emplacement;
                tmpEmp += - Vector2.UnitX;
                Vector4 nextForbiddenDir = new Vector4(0, 0, 0, 1);
                AddRoom(tmpEmp, nextOpeningDirections, nextForbiddenDir);
            }
            if (_openingDirections.W == 1 && _forbiddenDirections.W == 0 && _emplacement.X + 1 < MapSize.X)
            {
                Vector4 nextOpeningDirections = SetOpeningDirection(_emplacement, _openingDirections, _forbiddenDirections);
                nextOpeningDirections.Z = 1;
                Vector2 tmpEmp = _emplacement;
                tmpEmp += Vector2.UnitX;
                Vector4 nextForbiddenDir = new Vector4(0, 0, 1, 0);
                AddRoom(tmpEmp, nextOpeningDirections, nextForbiddenDir);
            }
        }

        private Vector4 SetOpeningDirection(Vector2 _emplacement, Vector4 _openingDirections, Vector4 _forbiddenDirections)
        {
            Random rnd = new Random();
            Vector4 nextOpeningDirections = Vector4.Zero;
            switch (rnd.Next(4))
            {
                case 0: nextOpeningDirections.X = 1; break;
                case 1: nextOpeningDirections.Y = 1; break;
                case 2: nextOpeningDirections.Z = 1; break;
                case 3: nextOpeningDirections.W = 1; break;
            }
            if (nextOpeningDirections.X == 1 && _forbiddenDirections.X == 0 && _emplacement.Y - 1 >= 0 && !rooms[(int)_emplacement.X, (int)_emplacement.Y - 1].isEmpty)
            {
                nextOpeningDirections.X = 0;
            }
            if (nextOpeningDirections.Y == 1 && _forbiddenDirections.Y == 0 && _emplacement.Y + 1 < mapSize.Y && !rooms[(int)_emplacement.X, (int)_emplacement.Y + 1].isEmpty)
            {
                nextOpeningDirections.Y = 0;
            }
            if (nextOpeningDirections.Z == 1 && _forbiddenDirections.Z == 0 && _emplacement.X - 1 >= 0 && !rooms[(int)_emplacement.X, (int)_emplacement.X - 1].isEmpty)
            {
                nextOpeningDirections.Z = 0;
            }
            if (nextOpeningDirections.W == 1 && _forbiddenDirections.W == 0 && _emplacement.X + 1 < mapSize.X && !rooms[(int)_emplacement.X, (int)_emplacement.X + 1].isEmpty)
            {
                nextOpeningDirections.W = 0;
            }

            return nextOpeningDirections;
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
