using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike.Engine
{
    public abstract class DrawableAsset
    {
        protected Vector2 position = Vector2.Zero;
        public virtual Vector2 size => Vector2.Zero;
        public Rectangle Bounds => new Rectangle(position.ToPoint(), size.ToPoint());

        public DrawableAsset()
        {

        }

        abstract public Texture2D GetTexture();

        public virtual void SetPosition(Vector2 pos)
        {
            position = pos;
        }
    }
}