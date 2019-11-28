using MapRogueLike.Engine;
using MapRogueLike.V2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapRogueLike
{
    public class Room
    {
        public static readonly Vector2 roomDimension    = new Vector2(16, 9);
        public static readonly Vector2 tileSize         = new Vector2(32);
        public static Vector2 realSize => roomDimension * tileSize;

        Vector2i gridPos;
        Vector4 openedDoors = new Vector4(-1, -1, -1,-1);
        IDrawableAsset miniMapSprite = null;
        Dictionary<Vector2, IDrawableAsset> roomTiles = new Dictionary<Vector2, IDrawableAsset>();

        public Vector4 OpenedDoors => openedDoors;
        public Vector2i GridPos => gridPos;
        public Vector2 Position => gridPos.Vector2 * realSize;
        public Rectangle Bounds => new Rectangle(Position.ToPoint(), realSize.ToPoint());
        
        public Room (Vector2i _gridPos)
        {
            gridPos = _gridPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawRealMap(spriteBatch);
            //DrawMiniMap(spriteBatch);
        }

        public void DrawMiniMap(SpriteBatch spriteBatch)
        {
            // MiniMap
            if (miniMapSprite != null)
            {
                Texture2D texture = miniMapSprite.GetTexture();
                Vector2 pos = new Vector2(texture.Bounds.Width * gridPos.X, texture.Bounds.Height * gridPos.Y);
                Vector2 offset = new Vector2(ToolBox.Instance.Get<GameManager>().WindowSize.X - (16/*Map width*/ * 16/*sprite Width*/), 0);
                //offset += ToolBox.Instance.Get<GameManager>().camera.Position;
                spriteBatch.Draw(texture, pos + offset, Color.White);
            }
        }

        private void DrawRealMap(SpriteBatch spriteBatch)
        {
            // Real Map
            foreach (KeyValuePair<Vector2, IDrawableAsset> tile in roomTiles)
            {
                IDrawableAsset drawable = tile.Value;
                Vector2 pos = tile.Key;

                Texture2D text = drawable.GetTexture();
                float scale = 0.2f;
                //spriteBatch.Draw(text, pos * scale, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                spriteBatch.Draw(text, pos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
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
            if (roomTiles.Count != 0)
            {
                roomTiles.Clear();
            }
            Texture2D text = miniMapSprite.GetTexture();
            Color[] data = new Color[text.Width * text.Height];
            text.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                Vector2 pos =
                      new Vector2(i % text.Width, i / text.Width) * tileSize
                    + new Vector2(tileSize.X * text.Width * gridPos.X, tileSize.Y * text.Height * gridPos.Y);

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

        public Room GetNeighbourg(uint neighbour)
        {
            if ((neighbour == 0 && openedDoors.X == 0) 
                || (neighbour == 1 && openedDoors.Y == 0)
                || (neighbour == 2 && openedDoors.Z == 0)
                || (neighbour == 3 && openedDoors.W == 0))
            {
                Console.WriteLine("No Neighbourg at this pos");
                return null;
            }

            switch(neighbour)
            {
                case 0: return RoomManager.Instance.GetRoom(gridPos - Vector2i.UnitY);
                case 1: return RoomManager.Instance.GetRoom(gridPos + Vector2i.UnitY);
                case 2: return RoomManager.Instance.GetRoom(gridPos - Vector2i.UnitX);
                case 3: return RoomManager.Instance.GetRoom(gridPos + Vector2i.UnitX);
                default: return null;
            }
        }

    }
}
