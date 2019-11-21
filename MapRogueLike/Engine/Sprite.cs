using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike.Engine
{
    class Sprite : Asset, IDrawableAsset
    {
        Texture2D texture;

        public Sprite(Texture2D _texture)
        {
            texture = _texture;
        }

        public Texture2D GetTexture() => texture;
    }
}
