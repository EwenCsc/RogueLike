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
        IDrawableAsset sprite = null;

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
            //if (sprite != null)
            {
                Texture2D texture = sprite.GetTexture();
                Vector2 pos = new Vector2(texture.Bounds.Width * gridPos.X, texture.Bounds.Height * gridPos.Y);
                spriteBatch.Draw(texture, pos, Color.White);
            }
        }
        
        public void SetOpenedRooms(Vector4 _openedDoors)
        {
            openedDoors = _openedDoors;
            SetSprite();
        }

        private void SetSprite()
        {
            sprite = AssetManager.Instance.DrawableAssets[openedDoors.X + "" + openedDoors.Y + "" + openedDoors.Z + "" + openedDoors.W];
        }
    }
}
