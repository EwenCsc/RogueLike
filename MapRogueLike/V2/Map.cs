using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike.V2
{
    /// <summary>
    /// Inspired by https://github.com/wbeaty/binding-of-isaac-style-procedural-generation.git
    /// </summary>
    class Map
    {
        Room[,] roomGrid;
        int gridSize = 16;
        int nbRooms = 25;
        List<Vector2i> takenRooms = new List<Vector2i>();
        
        public Map()
        {
            roomGrid = new Room[gridSize, gridSize];
            InitRooms();
            CreateRooms();
            SetConnectionWithNeighbours();
        }

        private void CreateRooms()
        {
            Room room = null;
            Vector2i gridPos = new Vector2i(roomGrid.GetLength(0) / 2, roomGrid.GetLength(1) / 2);

            //magic numbers
            float randomCompare = 0.2f;
            float randomCompareStart = 0.2f;
            float randomCompareEnd = 0.01f;

            for (int i = 0; i < nbRooms; i++)
            {
                if (i == 0)
                {
                    room = new Room(gridPos, Vector4.One);
                }
                else
                {
                    // Valeur entre 0 et 1. 
                    // 0 quand y'a 0 salles
                    // 1 quand toutes les salles sont remplies
                    float randomPerc = ((float)i) / (((float)nbRooms/* - 1*/));
                    randomCompare = MathHelper.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
                    // Get une salle vide voisine a une salle existant 
                    gridPos = GetEmptyRoomPosWithNeighbourgs();

                    // Regarde si la pièce a plus d'un voisin + proba de passer si y'a bcp de pièces
                    // Pour plus de détails / avoir des branches
                    if (NumberOfNeighbours(gridPos) > 1 && (float)new Random().NextDouble() > randomCompare)
                    {
                        int iterations = 0;
                        do
                        {
                            gridPos = GetEmptyRoomPosWithOneNeighbourg();
                            iterations++;
                        } while (NumberOfNeighbours(gridPos) > 1 && iterations < 100);
                    }
                    // Creation de la map
                    room = new Room(gridPos);
                }
                takenRooms.Add(room.GripPos);
                roomGrid[gridPos.X, gridPos.Y] = room;
            }
        }

        private void SetConnectionWithNeighbours()
        {
            roomGrid.Cast<Room>().ToList().FindAll(x => takenRooms.Contains(x.GripPos)).ForEach(y => y.SetOpenedRooms(Vector4.One));
        }

        private Vector2i GetEmptyRoomPosWithNeighbourgs()
        {
            Vector2i result = Vector2i.Zero;
            do
            {
                int index = new Random().Next(takenRooms.Count - 1);
                result = takenRooms[index];
                switch(new Random().Next(4))
                {
                    case 0: result -= Vector2i.UnitY; break;
                    case 1: result += Vector2i.UnitY; break;
                    case 2: result -= Vector2i.UnitX; break;
                    case 3: result += Vector2i.UnitX; break;
                }
            } while (takenRooms.Contains(result) || result.X >= roomGrid.GetLength(0) || result.X < 0 || result.Y >= roomGrid.GetLength(1) || result.Y < 0);
            return result;
        }

        private Vector2i GetEmptyRoomPosWithOneNeighbourg()
        {
            Vector2i result = Vector2i.Zero;
            int inc = 0;
            do
            {
                int index = -1;
                do
                {
                    //instead of getting a room to find an adject empty space, we start with one that only 
                    //as one neighbor. This will make it more likely that it returns a room that branches out
                    index = new Random().Next(takenRooms.Count - 1);
                    inc++;
                } while (NumberOfNeighbours(takenRooms[index]) > 1 && inc < 100);

                result = takenRooms[index];
                switch (new Random().Next(4))
                {
                    case 0: result -= Vector2i.UnitY; break;
                    case 1: result += Vector2i.UnitY; break;
                    case 2: result -= Vector2i.UnitX; break;
                    case 3: result += Vector2i.UnitX; break;
                    default: break;
                }
            } while (takenRooms.Contains(result) || result.X >= roomGrid.GetLength(0) || result.X < 0 || result.Y >= roomGrid.GetLength(1) || result.Y < 0);
            return result;
        }

        private int NumberOfNeighbours(Vector2i _pos)
        {
            //return
            //    ((takenRooms.Contains(_pos + Vector2i.UnitX)) ? 1 : 0) +
            //    ((takenRooms.Contains(_pos - Vector2i.UnitX)) ? 1 : 0) +
            //    ((takenRooms.Contains(_pos + Vector2i.UnitY)) ? 1 : 0) +
            //    ((takenRooms.Contains(_pos - Vector2i.UnitY)) ? 1 : 0);
            int result = 0;
            if (takenRooms.Contains(_pos + Vector2i.UnitX))
            {
                result++;
            }
            if (takenRooms.Contains(_pos - Vector2i.UnitX))
            {
                result++;
            }
            if (takenRooms.Contains(_pos + Vector2i.UnitY))
            {
                result++;
            }
            if (takenRooms.Contains(_pos - Vector2i.UnitY))
            {
                result++;
            }
            return result;
        }

        private void InitRooms()
        {
            for (int i = 0; i < roomGrid.GetLength(0); i++)
            {
                for (int j = 0; j < roomGrid.GetLength(1); j++)
                {
                    AddRoom(i, j, true);
                }
            }
        }

        private void AddRoom(int _i, int _j, bool _isEmpty = false)
        {
            if (_isEmpty)
            {
                roomGrid[_i, _j] = new Room(new Vector2i(_i, _j), Vector4.Zero);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < roomGrid.GetLength(0); i++)
            {
                for (int j = 0; j < roomGrid.GetLength(1); j++)
                {
                    roomGrid[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
