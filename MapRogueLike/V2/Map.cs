﻿using System;
using System.Collections.Generic;
using System.Linq;
using MapRogueLike.Engine;
using MapRogueLike.V2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    /// <summary>
    /// Inspired by https://github.com/wbeaty/binding-of-isaac-style-procedural-generation.git
    /// </summary>
    class Map
    {
        public bool generatingStepByStep = false;
        public bool isGenerated = false;
        Room[,] roomGrid;
        int gridSize = 16;
        int nbRooms = 25;

        float timeBetweenCreatingNewRoom = 0.09f;
        float timer;
        List<Vector2i> takenRooms = new List<Vector2i>();

        public Map()
        {
            timer = 0;
            roomGrid = new Room[gridSize, gridSize];
            InitRooms();
            if (!generatingStepByStep)
            {
                CreateRooms();
                SetConnectionWithNeighbours();
                isGenerated = true;
                RoomManager.Instance.SetGrid(roomGrid);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (generatingStepByStep)
            {
                GenerateStepByStep();
            }
        }

        private void GenerateStepByStep()
        {
            timer -= Time.DeltaTime;
            if (!isGenerated && timer <= 0)
            {
                timer = timeBetweenCreatingNewRoom + timer;
                CreateOneRoom();
                if (takenRooms.Count == nbRooms)
                {
                    isGenerated = true;
                    SetConnectionWithNeighbours();
                    RoomManager.Instance.SetGrid(roomGrid);
                }
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

        private void CreateOneRoom()
        {
            Room room = null;
            Vector2i gridPos = new Vector2i(roomGrid.GetLength(0) / 2, roomGrid.GetLength(1) / 2);

            //magic numbers
            float randomCompare = 0.2f;
            float randomCompareStart = 0.2f;
            float randomCompareEnd = 0.01f;

            if (takenRooms.Count == 0)
            {
                room = new Room(gridPos);
            }
            else
            {
                // Valeur entre 0 et 1. 
                // 0 quand y'a 0 salles
                // 1 quand toutes les salles sont remplies
                float randomPerc = ((float)takenRooms.Count) / (((float)nbRooms/* - 1*/));
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
            room.SetOpenedRooms(Vector4.One);
            takenRooms.Add(room.GridPos);
            roomGrid[gridPos.X, gridPos.Y] = room;
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
                    room = new Room(gridPos);
                    //room = new Room(new Vector2i(0));
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
                takenRooms.Add(room.GridPos);
                roomGrid[gridPos.X, gridPos.Y] = room;
            }
        }

        private void SetConnectionWithNeighbours()
        {
            for (int i = 0; i < roomGrid.GetLength(0); i++)
            {
                for (int j = 0; j < roomGrid.GetLength(1); j++)
                {
                    if (takenRooms.Contains(roomGrid[i, j].GridPos))
                    {
                        Room room = roomGrid[i, j];
                        Vector4 open = new Vector4();
                        open.X = takenRooms.Contains(room.GridPos - Vector2i.UnitY) ? 1 : 0;
                        open.Y = takenRooms.Contains(room.GridPos + Vector2i.UnitY) ? 1 : 0;
                        open.Z = takenRooms.Contains(room.GridPos - Vector2i.UnitX) ? 1 : 0;
                        open.W = takenRooms.Contains(room.GridPos + Vector2i.UnitX) ? 1 : 0;
                        room.SetOpenedRooms(open);
                    }
                }
            }
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
                    roomGrid[i, j] = new Room(new Vector2i(i, j));
                    roomGrid[i, j].SetOpenedRooms(Vector4.Zero);
                }
            }
        }
    }
}