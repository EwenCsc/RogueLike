using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapRogueLike.Engine
{
    public class AssetManager : Manager
    {
        private ContentManager content;

        private Dictionary<string, Texture2D> spriteSheets = new Dictionary<string, Texture2D>();
        private Dictionary<string, IDrawableAsset> drawableAssets = new Dictionary<string, IDrawableAsset>();

        public Dictionary<string, IDrawableAsset> DrawableAssets => drawableAssets;
        private Dictionary<string, Sprite> sprites
        {
            get
            {
                Dictionary<string, Sprite> result = new Dictionary<string, Sprite>();
                foreach (KeyValuePair<string, IDrawableAsset> assets in drawableAssets)
                {
                    if (assets.Value is Sprite)
                    {
                        result.Add(assets.Key, (Sprite)assets.Value);
                    }
                }
                return result;
            }
        }

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
            #region SpriteSheet
            spriteSheets.Add("Rooms", content.Load<Texture2D>("Sprites/Rooms"));
            spriteSheets.Add("AllLinks", content.Load<Texture2D>("Sprites/LinkSprites"));
            #endregion

            #region MiniMapRooms
            AddSprite("0000", spriteSheets["Rooms"], new Rectangle(0, 0, 16, 9));
            AddSprite("1000", spriteSheets["Rooms"], new Rectangle(16, 0, 16, 9));
            AddSprite("0100", spriteSheets["Rooms"], new Rectangle(32, 0, 16, 9));
            AddSprite("0010", spriteSheets["Rooms"], new Rectangle(48, 0, 16, 9));
            AddSprite("0001", spriteSheets["Rooms"], new Rectangle(64, 0, 16, 9));
            AddSprite("1100", spriteSheets["Rooms"], new Rectangle(80, 0, 16, 9));
            AddSprite("1010", spriteSheets["Rooms"], new Rectangle(96, 0, 16, 9));
            AddSprite("1001", spriteSheets["Rooms"], new Rectangle(112, 0, 16, 9));
            AddSprite("0110", spriteSheets["Rooms"], new Rectangle(128, 0, 16, 9));
            AddSprite("0011", spriteSheets["Rooms"], new Rectangle(144, 0, 16, 9));
            AddSprite("0101", spriteSheets["Rooms"], new Rectangle(160, 0, 16, 9));
            AddSprite("0111", spriteSheets["Rooms"], new Rectangle(176, 0, 16, 9));
            AddSprite("1011", spriteSheets["Rooms"], new Rectangle(192, 0, 16, 9));
            AddSprite("1101", spriteSheets["Rooms"], new Rectangle(208, 0, 16, 9));
            AddSprite("1110", spriteSheets["Rooms"], new Rectangle(224, 0, 16, 9));
            AddSprite("1111", spriteSheets["Rooms"], new Rectangle(240, 0, 16, 9));
            #endregion

            #region RoomTiles
            AddSprite("Lava", content.Load<Texture2D>("Sprites/Lava"));
            AddSprite("Brick", content.Load<Texture2D>("Sprites/Brick"));
            AddSprite("Dirt", content.Load<Texture2D>("Sprites/Dirt"));
            #endregion

            #region Sprites
            #region Down
            AddSprite("LinkDown_00_Barbare", spriteSheets["AllLinks"], new Rectangle(159, 1, 16, 16));
            AddSprite("LinkDown_01_Barbare", spriteSheets["AllLinks"], new Rectangle(177, 1, 16, 16));
            AddSprite("LinkDown_00_Mage", spriteSheets["AllLinks"], new Rectangle(159, 19, 16, 16));
            AddSprite("LinkDown_01_Mage", spriteSheets["AllLinks"], new Rectangle(177, 19, 16, 16));
            AddSprite("LinkDown_00_Assasin", spriteSheets["AllLinks"], new Rectangle(159, 37, 16, 16));
            AddSprite("LinkDown_01_Assasin", spriteSheets["AllLinks"], new Rectangle(177, 37, 16, 16));
            #endregion
            #region Up
            AddSprite("LinkUp_00_Barbare", spriteSheets["AllLinks"], new Rectangle(195, 1, 16, 16));
            AddSprite("LinkUp_01_Barbare", spriteSheets["AllLinks"], new Rectangle(195, 1, 16, 16), true);
            AddSprite("LinkUp_00_Mage", spriteSheets["AllLinks"], new Rectangle(195, 19, 16, 16));
            AddSprite("LinkUp_01_Mage", spriteSheets["AllLinks"], new Rectangle(195, 19, 16, 16), true);
            AddSprite("LinkUp_00_Assasin", spriteSheets["AllLinks"], new Rectangle(195, 37, 16, 16));
            AddSprite("LinkUp_01_Assasin", spriteSheets["AllLinks"], new Rectangle(195, 37, 16, 16), true);
            #endregion
            #region Right
            AddSprite("LinkRight_00_Barbare", spriteSheets["AllLinks"], new Rectangle(213, 1, 16, 16));
            AddSprite("LinkRight_01_Barbare", spriteSheets["AllLinks"], new Rectangle(231, 1, 16, 16));
            AddSprite("LinkRight_00_Mage", spriteSheets["AllLinks"], new Rectangle(213, 19, 16, 16));
            AddSprite("LinkRight_01_Mage", spriteSheets["AllLinks"], new Rectangle(231, 19, 16, 16));
            AddSprite("LinkRight_00_Assasin", spriteSheets["AllLinks"], new Rectangle(213, 37, 16, 16));
            AddSprite("LinkRight_01_Assasin", spriteSheets["AllLinks"], new Rectangle(231, 37, 16, 16));
            #endregion
            #region Left
            AddSprite("LinkLeft_00_Barbare", spriteSheets["AllLinks"], new Rectangle(213, 1, 16, 16), true);
            AddSprite("LinkLeft_01_Barbare", spriteSheets["AllLinks"], new Rectangle(231, 1, 16, 16), true);
            AddSprite("LinkLeft_00_Mage", spriteSheets["AllLinks"], new Rectangle(213, 19, 16, 16), true);
            AddSprite("LinkLeft_01_Mage", spriteSheets["AllLinks"], new Rectangle(231, 19, 16, 16), true);
            AddSprite("LinkLeft_00_Assasin", spriteSheets["AllLinks"], new Rectangle(213, 37, 16, 16), true);
            AddSprite("LinkLeft_01_Assasin", spriteSheets["AllLinks"], new Rectangle(231, 37, 16, 16), true);
            #endregion
            #endregion

            #region Animations
            #region Down
            List<Sprite> downBarbare = new List<Sprite>();
            downBarbare.Add(sprites["LinkDown_00_Barbare"]);
            downBarbare.Add(sprites["LinkDown_01_Barbare"]);
            AddAnim("Down_Barbare", new Animation(downBarbare));
            List<Sprite> downMage = new List<Sprite>();
            downMage.Add(sprites["LinkDown_00_Mage"]);
            downMage.Add(sprites["LinkDown_01_Mage"]);
            AddAnim("Down_Mage", new Animation(downMage));
            List<Sprite> downAssasin = new List<Sprite>();
            downAssasin.Add(sprites["LinkDown_00_Assasin"]);
            downAssasin.Add(sprites["LinkDown_01_Assasin"]);
            AddAnim("Down_Assasin", new Animation(downAssasin));
            #endregion
            #region Up
            List<Sprite> upB = new List<Sprite>();
            upB.Add(sprites["LinkUp_00_Barbare"]);
            upB.Add(sprites["LinkUp_01_Barbare"]);
            AddAnim("Up_Barbare", new Animation(upB));
            List<Sprite> upM = new List<Sprite>();
            upM.Add(sprites["LinkUp_00_Mage"]);
            upM.Add(sprites["LinkUp_01_Mage"]);
            AddAnim("Up_Mage", new Animation(upM));
            List<Sprite> upA = new List<Sprite>();
            upA.Add(sprites["LinkUp_00_Assasin"]);
            upA.Add(sprites["LinkUp_01_Assasin"]);
            AddAnim("Up_Assasin", new Animation(upA));
            #endregion
            #region Right
            List<Sprite> rightD = new List<Sprite>();
            rightD.Add(sprites["LinkRight_00_Barbare"]);
            rightD.Add(sprites["LinkRight_01_Barbare"]);
            AddAnim("Right_Barbare", new Animation(rightD));
            List<Sprite> rightM = new List<Sprite>();
            rightM.Add(sprites["LinkRight_00_Mage"]);
            rightM.Add(sprites["LinkRight_01_Mage"]);
            AddAnim("Right_Mage", new Animation(rightM));
            List<Sprite> rightA = new List<Sprite>();
            rightA.Add(sprites["LinkRight_00_Assasin"]);
            rightA.Add(sprites["LinkRight_01_Assasin"]);
            AddAnim("Right_Assasin", new Animation(rightA));
            #endregion
            #region Left
            List<Sprite> leftD = new List<Sprite>();
            leftD.Add(sprites["LinkLeft_00_Barbare"]);
            leftD.Add(sprites["LinkLeft_01_Barbare"]);
            AddAnim("Left_Barbare", new Animation(leftD));
            List<Sprite> leftM = new List<Sprite>();
            leftM.Add(sprites["LinkLeft_00_Mage"]);
            leftM.Add(sprites["LinkLeft_01_Mage"]);
            AddAnim("Left_Mage", new Animation(leftM));
            List<Sprite> leftA = new List<Sprite>();
            leftA.Add(sprites["LinkLeft_00_Assasin"]);
            leftA.Add(sprites["LinkLeft_01_Assasin"]);
            AddAnim("Left_Assasin", new Animation(leftA));
            #endregion
            #endregion
        }

        private void AddSprite(string key, Texture2D sprite)
        {
            if (drawableAssets.ContainsKey(key) && log)
            {
                LogExists(key);
                return;
            }
            drawableAssets.Add(key, new Sprite(sprite));
            LogCharged(key);
        }

        private void AddSprite(string key, Texture2D sprite, Rectangle source, bool reverse = false)
        {
            if (drawableAssets.ContainsKey(key) && log)
            {
                LogExists(key);
                return;
            }
            Texture2D text = new Texture2D(ToolBox.Instance.Get<GameManager>().GraphicsDevice, source.Width, source.Height);
            Color[] colorSpriteSheet = new Color[sprite.Width * sprite.Height];
            Color[] colorSprite = new Color[text.Width * text.Height];

            sprite.GetData<Color>(colorSpriteSheet);

            int index = 0;

            for (int y = source.Y; y < source.Y + source.Height; y++)
            {
                if (reverse)
                {
                    for (int x = source.X + source.Width - 1; x >= source.X ; x--)
                    {
                        colorSprite[index] = colorSpriteSheet[y * sprite.Width + x];
                        index++;
                    }
                }
                else
                {
                    for (int x = source.X; x < source.X + source.Width; x++)
                    {
                        colorSprite[index] = colorSpriteSheet[y * sprite.Width + x];
                        index++;
                    }
                }
            }
            text.SetData(colorSprite);
            drawableAssets.Add(key, new Sprite(text));
            LogCharged(key);
        }

        private void AddAnim(string key, Animation anim)
        {
            if (drawableAssets.ContainsKey(key) && log)
            {
                LogExists(key);
                return;
            }
            drawableAssets.Add(key, anim);
            LogCharged(key);
        }

        private void LogCharged(string key)
        {
            if (log)
            {
                Console.ForegroundColor = drawableAssets.ContainsKey(key) ? System.ConsoleColor.Green : System.ConsoleColor.Red;
                Console.WriteLine("Texture / Anim : {0} {1}\tchargée!", key, (drawableAssets.ContainsKey(key)) ? "" : "non");
                Console.ForegroundColor = System.ConsoleColor.White;
            }
        }

        private static void LogExists(string key)
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.WriteLine("Texture / Anim : {0} déja existante!", key);
            Console.ForegroundColor = System.ConsoleColor.White;
        }
    }
}
