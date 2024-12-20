using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

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
            this._spriteManager.Draw(spriteBatch, position + new Vector2(0, -16), tilemapPart, this._spriteManager.EmotePack);
        }
    }
}