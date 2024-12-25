using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD
{
    public static class DebugWriteHelper
    {
        public static void WriteLine(this SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position)
        {
            spriteBatch.DrawString(
                font, 
                text, 
                position, 
                Color.White,
                20f,
                new Vector2(0, 0),
                0.3f,
                SpriteEffects.None,
                1);
        }
    }
}