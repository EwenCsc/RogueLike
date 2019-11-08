using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    public class Room
    {
        public bool isEmpty = false;
        Vector2 emplacement;
        static readonly Vector2 tilingDimension = new Vector2(16, 9);
        public static Vector2 Size => new Vector2(tilingDimension.X * Tile.size.X, tilingDimension.Y * Tile.size.Y);

        List<Tile> tiles = new List<Tile>();
        
        public Vector2 Emplacement => emplacement;
        //public Vector2 Position => new Vector2(emplacement.X + (emplacement.X * (Size.X  - 1)), emplacement.Y + (emplacement.Y * (Size.Y - 1)));
        public Vector2 Position => new Vector2(emplacement.X + (emplacement.X * Size.X), emplacement.Y + (emplacement.Y * Size.Y)); // Séparation entre les salles de 1px (jolie)

        public Room(Vector2 _empalcement, bool _isEmpty = false)
        {
            isEmpty = _isEmpty;
            emplacement = _empalcement;
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

        internal bool IsFullNeighboured(Map map)
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