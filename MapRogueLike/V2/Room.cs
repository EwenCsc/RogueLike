using MapRogueLike.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapRogueLike
{
    class Room
    {
        Vector2i gridPos;
        Vector4 openedDoors = new Vector4(-1, -1, -1,-1);
        IDrawableAsset miniMapSprite = null;
        Dictionary<Vector2, IDrawableAsset> roomTiles = new Dictionary<Vector2, IDrawableAsset>();

        public Vector4 OpenedDoors => openedDoors;
        public Vector2i GripPos => gridPos;
        
        public Room (Vector2i _gridPos)
        {
            gridPos = _gridPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // MiniMap
            if (miniMapSprite != null)
            {
                Texture2D texture = miniMapSprite.GetTexture();
                Vector2 pos = new Vector2(texture.Bounds.Width * gridPos.X, texture.Bounds.Height * gridPos.Y);
                Vector2 offset = new Vector2(ToolBox.Instance.Get<GameManager>().WindowSize.X - (16/*Map width*/ * 16/*sprite Width*/), 0);
                spriteBatch.Draw(texture, pos + offset, Color.White);
            }
            // Real Map
            foreach (KeyValuePair<Vector2, IDrawableAsset> tile in roomTiles)
            {
                IDrawableAsset drawable = tile.Value;
                Vector2 pos = tile.Key;

                Texture2D text = drawable.GetTexture();
                float scale = 0.2f;
                spriteBatch.Draw(text, pos * scale, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
        }
        
        public void SetOpenedRooms(Vector4 _openedDoors)
        {
            openedDoors = _openedDoors;
            SetSprite();
        }

        private void SetSprite()
        {
            miniMapSprite = ToolBox.Instance.Get<AssetManager>().DrawableAssets[openedDoors.X + "" + openedDoors.Y + "" + openedDoors.Z + "" + openedDoors.W];
            SetRoomTiles();
        }

        private void SetRoomTiles()
        {
            Texture2D text = miniMapSprite.GetTexture();
            Color[] data = new Color[text.Width * text.Height];
            text.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                Vector2 pos =
                    new Vector2(i % text.Width, i / text.Width) * new Vector2(32)
                    + new Vector2(32 * text.Width * gridPos.X, 32 * text.Height * gridPos.Y);
                if (gridPos == Vector2i.UnitX)
                {
                    Console.WriteLine(new Vector2(32 * text.Width * gridPos.X, 32 * text.Height * gridPos.Y));
                    Console.WriteLine(pos);
                }

                Color c = data[i];
                if (c == new Color(255, 0, 0))
                {
                    roomTiles.Add(pos, new Sprite(ToolBox.Instance.Get<AssetManager>().DrawableAssets["Lava"].GetTexture()));
                }
                else if (c == new Color(126, 91, 62))
                {
                    roomTiles.Add(pos, new Sprite(ToolBox.Instance.Get<AssetManager>().DrawableAssets["Dirt"].GetTexture()));
                }
                else if (c == new Color(0, 0, 0))
                {
                    roomTiles.Add(pos, new Sprite(ToolBox.Instance.Get<AssetManager>().DrawableAssets["Brick"].GetTexture()));
                }
            }
        }
    }
}
