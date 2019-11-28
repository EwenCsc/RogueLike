using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike.Engine
{
    public class Sprite : DrawableAsset
    {
        Texture2D texture = null;
        public override Vector2 size => (texture != null) ? new Vector2(texture.Width, texture.Height) : base.size;

        public Sprite(Texture2D _texture)
        {
            texture = _texture;
        }

        public override Texture2D GetTexture() => texture;
    }
}
