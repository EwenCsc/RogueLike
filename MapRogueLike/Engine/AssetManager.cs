using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapRogueLike.Engine
{
    public class AssetManager
    {
        private ContentManager content;

        private static AssetManager instance = null;
        public static AssetManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AssetManager();
                return instance;
            }
        }

        private Dictionary<string, Texture2D> spriteSheets = new Dictionary<string, Texture2D>();
        private Dictionary<string, IDrawableAsset> drawableAssets = new Dictionary<string, IDrawableAsset>();

        public Dictionary<string, IDrawableAsset> DrawableAssets => drawableAssets;

        bool log = false;

        public ContentManager Content
        {
            set
            {
                content = value;
                LoadAssets();
            }
        }

        private void LoadAssets()
        {
            // SpriteSheet
            spriteSheets.Add("Rooms", content.Load<Texture2D>("Sprites/Rooms"));

            // Mini Map Rooms
            {
            AddDrawableAsset("0000", spriteSheets["Rooms"], new Rectangle(0, 0, 16, 9));
            AddDrawableAsset("1000", spriteSheets["Rooms"], new Rectangle(16, 0, 16, 9));
            AddDrawableAsset("0100", spriteSheets["Rooms"], new Rectangle(32, 0, 16, 9));
            AddDrawableAsset("0010", spriteSheets["Rooms"], new Rectangle(48, 0, 16, 9));
            AddDrawableAsset("0001", spriteSheets["Rooms"], new Rectangle(64, 0, 16, 9));
            AddDrawableAsset("1100", spriteSheets["Rooms"], new Rectangle(80, 0, 16, 9));
            AddDrawableAsset("1010", spriteSheets["Rooms"], new Rectangle(96, 0, 16, 9));
            AddDrawableAsset("1001", spriteSheets["Rooms"], new Rectangle(112, 0, 16, 9));
            AddDrawableAsset("0110", spriteSheets["Rooms"], new Rectangle(128, 0, 16, 9));
            AddDrawableAsset("0011", spriteSheets["Rooms"], new Rectangle(144, 0, 16, 9));
            AddDrawableAsset("0101", spriteSheets["Rooms"], new Rectangle(160, 0, 16, 9));
            AddDrawableAsset("0111", spriteSheets["Rooms"], new Rectangle(176, 0, 16, 9));
            AddDrawableAsset("1011", spriteSheets["Rooms"], new Rectangle(192, 0, 16, 9));
            AddDrawableAsset("1101", spriteSheets["Rooms"], new Rectangle(208, 0, 16, 9));
            AddDrawableAsset("1110", spriteSheets["Rooms"], new Rectangle(224, 0, 16, 9));
            AddDrawableAsset("1111", spriteSheets["Rooms"], new Rectangle(240, 0, 16, 9));
            }

            // Room Tiles
            AddDrawableAsset("Lava", content.Load<Texture2D>("Sprites/Lava"));
            AddDrawableAsset("Brick", content.Load<Texture2D>("Sprites/Brick"));
            AddDrawableAsset("Dirt", content.Load<Texture2D>("Sprites/Dirt"));
        }

        private void AddDrawableAsset(string key, Texture2D texture)
        {
            if (drawableAssets.ContainsKey(key) && log)
            {
                Console.WriteLine("Texture : {0} déja existante!", key);
                return;
            }
            drawableAssets.Add(key, new Sprite(texture));
            if (log)
                Console.WriteLine("Texture : {0} {1} chargée!", key, (drawableAssets.ContainsKey(key)) ? "" : "non");
        }

        private void AddDrawableAsset(string key, Texture2D spriteSheet, Rectangle source)
        {
            if (drawableAssets.ContainsKey(key) && log)
            {
                Console.WriteLine("Texture : {0} déja existante!", key);
                return;
            }
            Texture2D text = new Texture2D(GameManager.Instance.GraphicsDevice, source.Width, source.Height);
            Color[] colorSpriteSheet = new Color[spriteSheet.Width * spriteSheet.Height];
            Color[] colorSprite = new Color[text.Width * text.Height];

            spriteSheet.GetData<Color>(colorSpriteSheet);

            int index = 0;

            for (int y = source.Y; y < source.Y + source.Height; y++)
            {
                for (int x = source.X; x < source.X + source.Width; x++)
                {
                    colorSprite[index] = colorSpriteSheet[y * spriteSheet.Width + x];
                    index++;
                }
            }
            text.SetData(colorSprite);
            drawableAssets.Add(key, new Sprite(text));
            if (log)
                Console.WriteLine("Texture : {0} {1} chargée!", key, (drawableAssets.ContainsKey(key)) ? "" : "non");
        }
    }
}
