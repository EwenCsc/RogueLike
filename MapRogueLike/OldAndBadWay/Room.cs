using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    public class Room
    {
        static readonly Vector2 tilingDimension = new Vector2(16, 9);
        
        IDrawableAsset drawableAsset = null;
        public bool isEmpty = false;
        Vector2 emplacement;
        List<Tile> tiles = new List<Tile>();
        private Vector4 oppeningDirections;


        public static Vector2 Size => new Vector2(tilingDimension.X * Tile.size.X, tilingDimension.Y * Tile.size.Y);
        public Vector2 Emplacement => emplacement;
        public Vector2 Position => new Vector2((emplacement.X * Size.X), (emplacement.Y * Size.Y));
        public string OppeningDirectionsString
        {
            get
            {
                return oppeningDirections.X.ToString() + oppeningDirections.Y.ToString() + oppeningDirections.Z.ToString() + oppeningDirections.W.ToString();
            }
        }

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

        public Room(Vector2 _emplacement, Vector4 _oppeningDirections/*, Vector4? _forbiddenDirection = null*/)
        {
            isEmpty = false;
            emplacement = _emplacement;
            oppeningDirections = _oppeningDirections;

            List<int> doorTiles = new List<int>();
            if (AssetManager.Instance.DrawableAssets[OppeningDirectionsString] != null)
            {
                drawableAsset = AssetManager.Instance.DrawableAssets[OppeningDirectionsString];
            }
            if (!isEmpty)
            {
                GenerateTiles();
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
                        if (i == (int)(tilingDimension.X / 2) || i == (int)(tilingDimension.X / 2) - 1 || i == (int)(tilingDimension.X / 2) + 1 || i == (int)(tilingDimension.X / 2) - 2)
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
                        if (j == (int)(tilingDimension.Y / 2) || j == (int)(tilingDimension.Y / 2) - 1 || j == (int)(tilingDimension.Y / 2) + 1 || j == (int)(tilingDimension.Y / 2) - 2)
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
            if (drawableAsset != null)
            {
                spriteBatch.Draw(drawableAsset.GetTexture(), Position, null, Color.White, 0, Vector2.Zero, Tile.size.X, SpriteEffects.None, 0);
            }
            else if (!isEmpty)
            {
                foreach (Tile tile in tiles)
                {
                    tile.Draw(spriteBatch);
                }
            }
        }
    }
}