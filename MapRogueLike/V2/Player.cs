using System;
using MapRogueLike.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    public class Player
    {
        int life = 0;
        int maxLife = 100;

        int stamina = 0;
        int maxStamina = 100;

        IDrawableAsset drawable;

        public Player()
        {
            life = maxLife;
            stamina = maxStamina;
            drawable = ToolBox.Instance.Get<AssetManager>().DrawableAssets["Lava"];
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}