using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MapRogueLike.Engine
{
    internal class Animation : IDrawableAsset
    {
        List<Sprite> sprites = new List<Sprite>();
        Sprite currentSprite;

        public Animation(List<Sprite> _sprites)
        {
            sprites = _sprites;
            currentSprite = sprites.First();
        }

        public Texture2D GetTexture()
        {
            return currentSprite.GetTexture();
        }
    }
}