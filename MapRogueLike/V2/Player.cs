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

        Vector2 position;
        IDrawableAsset drawable;

        Room currentRoom;


        public Player()
        {
            life = maxLife;
            stamina = maxStamina;
            drawable = ToolBox.Instance.Get<AssetManager>().DrawableAssets["Down_Barbare"];
            position = ToolBox.Instance.Get<GameManager>().WindowSize / 2;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(drawable.GetTexture(), position, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public void SetCurrentRoom(Room room)
        {
            currentRoom = room;
        }
    }
}