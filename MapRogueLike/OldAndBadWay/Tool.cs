using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{
    public static class Tool
    {
        private static GraphicsDeviceManager graphicsDeviceManager = null;
        public static GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                if (graphicsDeviceManager == null)
                {
                    try
                    {
                        graphicsDeviceManager = Game1.graphics;
                    }
                    catch
                    {
                        Console.WriteLine("No Graphic Device ... (Class Tool)");
                        return null;
                    }
                }
                return graphicsDeviceManager;
            }
        }

        public static Texture2D CreateRectangleTexture(Vector2 _size, Color _color)
        {
            Texture2D text = new Texture2D(GraphicsDeviceManager.GraphicsDevice, (int)_size.X, (int)_size.Y);
            Color[] data = new Color[(int)_size.X * (int)_size.Y];
            for (int i = 0; i < data.Length; ++i) data[i] = _color;
            text.SetData(data);
            return text;
        }
    }
}