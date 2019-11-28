using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike.Engine
{
    public abstract class DrawableAsset
    {
        public Vector2 Position { get; private set; }
        public virtual Vector2 size => Vector2.Zero;
        public Rectangle Bounds => new Rectangle(Position.ToPoint(), size.ToPoint());

        abstract public Texture2D GetTexture();

        public virtual void SetPosition(Vector2 pos)
        {
            Position = pos;
        }
    }
}