using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MapRogueLike.Engine
{
    internal class Animation : DrawableAsset
    {
        List<Sprite> sprites = null;
        Sprite currentSprite;
        public override Vector2 size => (sprites != null) ? new Vector2(currentSprite.GetTexture().Width, currentSprite.GetTexture().Height) : base.size;

        public Animation(List<Sprite> _sprites)
        {
            sprites = _sprites;
            currentSprite = sprites.First();
        }

        public override Texture2D GetTexture()
        {
            return currentSprite.GetTexture();
        }
    }
}