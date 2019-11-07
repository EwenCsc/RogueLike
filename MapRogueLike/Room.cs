using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    public class Room
    {
        Vector2 emplacement;
        static readonly Vector2 tilingDimension = new Vector2(16, 9);
        public static Vector2 Size => new Vector2(tilingDimension.X * Tile.size.X, tilingDimension.Y * Tile.size.Y);

        List<Tile> tiles = new List<Tile>();

        public Vector2 Emplacement => emplacement;
        //public Vector2 Position => new Vector2(emplacement.X + (emplacement.X * (Size.X  - 1)), emplacement.Y + (emplacement.Y * (Size.Y - 1)));
        public Vector2 Position => new Vector2(emplacement.X + (emplacement.X * Size.X), emplacement.Y + (emplacement.Y * Size.Y)); // Séparation entre les salles de 1px (jolie)

        public Room(Vector2 _empalcement)
        {
            emplacement = _empalcement;
            for (int i = 0; i < tilingDimension.X; i++)
            {
                for (int j = 0; j < tilingDimension.Y; j++)
                {
                    tiles.Add(new Tile(new Vector2(i, j), this));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}