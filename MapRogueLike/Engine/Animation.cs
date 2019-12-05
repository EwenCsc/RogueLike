using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapRogueLike.Engine
{
    public class Animation : DrawableAsset
    {
        List<Sprite> sprites = null;
        int currentSprite;
        public override Vector2 size => (sprites != null) ? new Vector2(GetTexture().Width, GetTexture().Height) : base.size;

        float timer;
        const float timeBetweenSprites = 0.30f;

        public Animation(List<Sprite> _sprites)
        {
            timer = timeBetweenSprites;
            sprites = _sprites;
            currentSprite = 0;
        }

        public void Update()
        {
            timer -= Time.DeltaTime;
            if (timer <= 0)
            {
                timer += timeBetweenSprites;
                currentSprite++;
                currentSprite = currentSprite % sprites.Count;
            }
        }

        public override Texture2D GetTexture()
        {
            return sprites[currentSprite].GetTexture();
        }
    }
}