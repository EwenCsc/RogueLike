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

        Vector2 velocity = Vector2.Zero; 

        Room currentRoom;
        private float moveSpeed = .80f;

        Camera cam;

        public Player()
        {
            life = maxLife;
            stamina = maxStamina;
            drawable = ToolBox.Instance.Get<AssetManager>().DrawableAssets["Down_Barbare"];
            position = ToolBox.Instance.Get<GameManager>().WindowSize / 2;
            cam = new Camera(ToolBox.Instance.Get<GameManager>().GraphicsDevice.Viewport);
        }

        public void Update(GameTime gameTime)
        {
            Movements();
            UpdatePosition();
            ToolBox.Instance.Get<GameManager>().GraphicsDevice.Viewport.X = position.X;
            cam.UpdateCamera(ToolBox.Instance.Get<GameManager>().GraphicsDevice.Viewport);
        }

        private void UpdatePosition()
        {
            position += velocity;
            cam.Position = position;
        }

        private void Movements()
        {
            Vector2 v = Vector2.Zero;
            if (Input.GetKey(KeyBinds.MovevementUp))
            {
                v += new Vector2(0, -moveSpeed);
            }
            if (Input.GetKey(KeyBinds.MovevementDown))
            {
                v += new Vector2(0, moveSpeed);
            }
            if (Input.GetKey(KeyBinds.MovevementLeft))
            {
                v += new Vector2(-moveSpeed, 0);
            }
            if (Input.GetKey(KeyBinds.MovevementRight))
            {
                v += new Vector2(moveSpeed, 0);
            }

            if (v != Vector2.Zero)
            {
                v.Normalize();
            }
            velocity = v * moveSpeed;
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