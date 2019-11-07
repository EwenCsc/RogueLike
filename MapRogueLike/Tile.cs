using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MapRogueLike
{
    public class Tile
    {
        Room room;
        public Vector2 position;
        public static readonly Vector2 size = new Vector2(1);

        public Texture2D texture = null;

        Vector2 Position => new Vector2(position.X * size.X, position.Y * size.Y) + room.Position;

        public Tile(Vector2 _position, Room _room)
        {
            room = _room;
            texture = Tool.CreateRectangleTexture(size, Color.PaleVioletRed);
            position = _position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Position, Color.White);
            }
            else
            {
                Console.WriteLine("Pas de Texture ...");
            }
        }
    }
}