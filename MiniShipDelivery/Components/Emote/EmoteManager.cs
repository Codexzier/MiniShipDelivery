using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.Emote
{
    public class EmoteManager
    {
        private AssetManager _spriteManager;

        public EmoteManager(AssetManager assetManager)
        {
            this._spriteManager = assetManager;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, EmotePart tilemapPart)
        {
            // draw the emote
            spriteBatch.Draw(this._spriteManager.Emotes,
                position + new Vector2(0, -16),
                this._spriteManager.EmotePack.SpriteContent[tilemapPart],
                Color.White);
        }
    }
}