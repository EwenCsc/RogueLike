using System;
using MapRogueLike.V2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapRogueLike.Engine
{
    public class GameManager : Manager
    {
        Game1 Game = null;
        Map map;
        Player player;
        Camera camera;

        public GameManager()
        {
        }

        public void Initialize(Game1 game)
        {
            Game = game;
            ToolBox.Instance.Get<AssetManager>().Content = game.Content;
            Game.graphics.PreferredBackBufferWidth *= 2;
            Game.graphics.PreferredBackBufferHeight *= 2;
            Game.graphics.ApplyChanges();
            map = new Map();
            player = new Player();
            camera = new Camera(GraphicsDevice.Viewport);

            player.Position = RoomManager.Instance.GetCenterRoom().Position;
        }

        public override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.GetKeyDown(Keys.R) && map.isGenerated)
            {
                map = new Map();
            }
            map.Update(gameTime);
            player.Update(gameTime);
            camera.SetPosition(player.Position);
            camera.UpdateCamera(GraphicsDevice.Viewport);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
        
        public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;

        public Vector2 WindowSize => new Vector2(Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
    }
}
