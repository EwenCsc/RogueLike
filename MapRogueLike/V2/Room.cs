using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRogueLike.V2
{
    class Room
    {
        Vector2i gridPos;
        Vector4 openedDoors = new Vector4(-1, -1, -1,-1);
        IDrawableAsset miniMapSprite = null;
        List<IDrawableAsset> roomTiles = new List<IDrawableAsset>();

        public Vector4 OpenedDoors => openedDoors;
        public Vector2i GripPos => gridPos;

        /// <summary>
        /// Set [i, j] Room
        /// </summary>
        /// <param name="_openedDoors">0 -> Up, 1 -> Down, 2 -> Left, 3 -> Right</param>
        public Room(Vector2i _gridPos, Vector4 _openedDoors)
        {
            gridPos = _gridPos;
            SetOpenedRooms(_openedDoors);
        }

        public Room (Vector2i _gridPos)
        {
            gridPos = _gridPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (miniMapSprite != null)
            {
                Texture2D texture = miniMapSprite.GetTexture();
                Vector2 pos = new Vector2(texture.Bounds.Width * gridPos.X, texture.Bounds.Height * gridPos.Y);
                Vector2 offset = new Vector2(Tool.GraphicsDeviceManager.PreferredBackBufferWidth - (16/*Map width*/ * 16/*sprite Width*/), 0);
                spriteBatch.Draw(texture, pos + offset, Color.White);
            }
            for (int i = 0; i < roomTiles.Count; i++)
            {
                IDrawableAsset drawable = roomTiles[i];
                Texture2D text = drawable.GetTexture();
                Vector2 pos = Vector2.Zero;
                spriteBatch.Draw(text, pos, Color.White);
            }
        }
        
        public void SetOpenedRooms(Vector4 _openedDoors)
        {
            openedDoors = _openedDoors;
            SetSprite();
        }

        private void SetSprite()
        {
            miniMapSprite = AssetManager.Instance.DrawableAssets[openedDoors.X + "" + openedDoors.Y + "" + openedDoors.Z + "" + openedDoors.W];
        }
    }
}
