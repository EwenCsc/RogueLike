using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapRogueLike.Engine
{
    public class GameManager : /*Singleton<GameManager>*/ Manager
    {
        //private static GameManager instance = null;
        //public static GameManager Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new GameManager();
        //        }
        //        return instance;
        //    }
        //}

        Game1 Game = null;
        Map map;
        Camera cam;
        Player player;

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
            cam = new Camera(Game.GraphicsDevice.Viewport);
            player = new Player();
        }

        public override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.GetKeyDown(Keys.R))
            {
                map = new Map();
            }
            map.Update(gameTime);
            cam.Update(gameTime);
            player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;

        public Vector2 WindowSize => new Vector2(Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
    }
}
