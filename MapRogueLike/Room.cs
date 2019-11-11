using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    public class Room
    {
        public static int cpt = 0;

        public bool isEmpty = false;
        Vector2 emplacement;
        static readonly Vector2 tilingDimension = new Vector2(16, 9);
        public static Vector2 Size => new Vector2(tilingDimension.X * Tile.size.X, tilingDimension.Y * Tile.size.Y);
        //public static Vector2 Size => new Vector2(tilingDimension.X + (tilingDimension.X * Tile.size.X - 1), tilingDimension.Y + (tilingDimension.Y * Tile.size.Y - 1));

        List<Tile> tiles = new List<Tile>();
        private Vector4 oppeningDirections;

        public Vector2 Emplacement => emplacement;
        //public Vector2 Position => new Vector2(emplacement.X + (emplacement.X * (Size.X  - 1)), emplacement.Y + (emplacement.Y * (Size.Y - 1)));
        //public Vector2 Position => new Vector2(emplacement.X + (emplacement.X * Size.X), emplacement.Y + (emplacement.Y * Size.Y)); // Séparation entre les salles de 1px (jolie)
        public Vector2 Position => new Vector2((emplacement.X * Size.X), (emplacement.Y * Size.Y));

        public Room(Vector2 _emplacement, bool _isEmpty = false)
        {
            isEmpty = _isEmpty;
            emplacement = _emplacement;
            if (!isEmpty)
            {
                for (int i = 0; i < tilingDimension.X; i++)
                {
                    for (int j = 0; j < tilingDimension.Y; j++)
                    {
                        tiles.Add(new Tile(new Vector2(i, j), this));
                    }
                }
            }
        }

        public Room(Vector2 _emplacement, Vector4 _oppeningDirections, Map _map, Vector4? _forbiddenDirection = null)
        {
            cpt++;

            Vector4 forbiddenDirection = (_forbiddenDirection == null) ? Vector4.Zero : (Vector4)_forbiddenDirection;
            isEmpty = false;
            emplacement = _emplacement;
            oppeningDirections = _oppeningDirections;

            List<int> doorTiles = new List<int>();
            if (!isEmpty)
            {
                GenerateTiles();
            }

            //if (cpt <= 25)
            {
                Random rnd = new Random();
                Vector4 nextOppeningDirections = Vector4.Zero;
                switch (rnd.Next(4))
                {
                    case 0: nextOppeningDirections.X = 1; break;
                    case 1: nextOppeningDirections.Y = 1; break;
                    case 2: nextOppeningDirections.Z = 1; break;
                    case 3: nextOppeningDirections.W = 1; break;
                }
                //nextOppeningDirections = new Vector4(rnd.Next(2), rnd.Next(2), rnd.Next(2), rnd.Next(2));

                if (oppeningDirections.X == 1 && forbiddenDirection.X == 0 && emplacement.Y - 1 >= 0)
                {
                    nextOppeningDirections.Y = 1;
                    Vector2 tmpEmp = emplacement - Vector2.UnitY;
                    Console.WriteLine(tmpEmp);
                    _map.Rooms[(int)tmpEmp.X, (int)tmpEmp.Y] = new Room(tmpEmp, nextOppeningDirections, _map, new Vector4(0, 1, 0, 0));
                }
                if (oppeningDirections.Y == 1 && forbiddenDirection.Y == 0 && emplacement.Y + 1 < _map.MapSize.Y)
                {
                    nextOppeningDirections.X = 1;
                    Vector2 tmpEmp = emplacement + Vector2.UnitY;
                    Console.WriteLine(tmpEmp);
                    _map.Rooms[(int)tmpEmp.X, (int)tmpEmp.Y] = new Room(tmpEmp, nextOppeningDirections, _map, new Vector4(1, 0, 0, 0));
                }
                if (oppeningDirections.Z == 1 && forbiddenDirection.Z == 0 && emplacement.X - 1 >= 0)
                {
                    nextOppeningDirections.W = 1;
                    Vector2 tmpEmp = emplacement - Vector2.UnitX;
                    Console.WriteLine(tmpEmp);
                    _map.Rooms[(int)tmpEmp.X, (int)tmpEmp.Y] = new Room(tmpEmp, nextOppeningDirections, _map, new Vector4(0, 0, 0, 1));
                }
                if (oppeningDirections.W == 1 && forbiddenDirection.W == 0 && emplacement.X + 1 < _map.MapSize.X)
                {
                    nextOppeningDirections.Z = 1;
                    Vector2 tmpEmp = emplacement + Vector2.UnitX;
                    Console.WriteLine(tmpEmp);
                    _map.Rooms[(int)tmpEmp.X, (int)tmpEmp.Y] = new Room(tmpEmp, nextOppeningDirections, _map, new Vector4(0, 0, 1, 0));
                }
            }
        }

        private void GenerateTiles()
        {
            for (int i = 0; i < tilingDimension.X; i++)
            {
                for (int j = 0; j < tilingDimension.Y; j++)
                {
                    Tile tile = new Tile(new Vector2(i, j), this);
                    if ((tilingDimension.X / 2) % 2 == 0)
                    {
                        if (i == (int)(tilingDimension.X / 2) || i == (int)(tilingDimension.X / 2) - 1)
                        {
                            if ((oppeningDirections.X == 1 && j == 0) || (oppeningDirections.Y == 1 && j == tilingDimension.Y - 1))
                                tile = new Tile(new Vector2(i, j), this, Color.Black);
                        }
                    }
                    else
                    {
                        if (i == (int)(tilingDimension.X / 2) - 1 || i == (int)(tilingDimension.X / 2) || i == (int)(tilingDimension.X / 2) + 1)
                        {
                            if ((oppeningDirections.X == 1 && j == 0) || (oppeningDirections.Y == 1 && j == tilingDimension.Y - 1))
                                tile = new Tile(new Vector2(i, j), this, Color.Black);
                        }
                    }
                    if ((tilingDimension.Y / 2) % 2 == 0)
                    {
                        if (j == (int)(tilingDimension.Y / 2) || j == (int)(tilingDimension.Y / 2) - 1)
                        {
                            if ((oppeningDirections.Z == 1 && i == 0) || (oppeningDirections.W == 1 && i == tilingDimension.X - 1))
                                tile = new Tile(new Vector2(i, j), this, Color.Black);
                        }
                    }
                    else
                    {
                        if (j == (int)(tilingDimension.Y / 2) - 1 || j == (int)(tilingDimension.Y / 2) || j == (int)(tilingDimension.Y / 2) + 1)
                        {
                            if ((oppeningDirections.Z == 1 && i == 0) || (oppeningDirections.W == 1 && i == tilingDimension.X - 1))
                                tile = new Tile(new Vector2(i, j), this, Color.Black);
                        }
                    }
                    tiles.Add(tile);
                }
            }
        }

        internal bool IsFullyNeighboured(Map map)
        {
            return (
                (emplacement.X + 1 < map.MapSize.X && !map.Rooms[(int)emplacement.X + 1, (int)emplacement.Y].isEmpty) && 
                (emplacement.X - 1 >= 0            && !map.Rooms[(int)emplacement.X - 1, (int)emplacement.Y].isEmpty) && 
                (emplacement.Y + 1 < map.MapSize.Y && !map.Rooms[(int)emplacement.X, (int)emplacement.Y + 1].isEmpty) && 
                (emplacement.Y - 1 >= 0            && !map.Rooms[(int)emplacement.X, (int)emplacement.Y - 1].isEmpty));
        }

        internal List<Room> GetFreeNeighbours(Map map)
        {
            List<Room> freeNeighbours = new List<Room>();
            //Console.WriteLine("Selected Room -> ({0}, {1})", emplacement.X, emplacement.Y);
            if (emplacement.X + 1 < map.MapSize.X && map.Rooms[(int)emplacement.X + 1, (int)emplacement.Y].isEmpty)
            {
                freeNeighbours.Add(map.Rooms[(int)emplacement.X + 1, (int)emplacement.Y]);
            }
            if (emplacement.X - 1 >= 0 && map.Rooms[(int)emplacement.X - 1, (int)emplacement.Y].isEmpty)
            {
                freeNeighbours.Add(map.Rooms[(int)emplacement.X - 1, (int)emplacement.Y]);
            }
            if (emplacement.Y + 1 < map.MapSize.Y && map.Rooms[(int)emplacement.X, (int)emplacement.Y + 1].isEmpty)
            {
                freeNeighbours.Add(map.Rooms[(int)emplacement.X, (int)emplacement.Y + 1]);
            }
            if (emplacement.Y - 1 >= 0 && map.Rooms[(int)emplacement.X, (int)emplacement.Y - 1].isEmpty)
            {
                freeNeighbours.Add(map.Rooms[(int)emplacement.X, (int)emplacement.Y - 1]);
            }
            //freeNeighbours.ForEach(x => Console.WriteLine("({0}, {1})", x.Emplacement.X, x.Emplacement.Y));
            return freeNeighbours;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isEmpty)
            {
                foreach (Tile tile in tiles)
                {
                    tile.Draw(spriteBatch);
                }
            }
        }
    }
}