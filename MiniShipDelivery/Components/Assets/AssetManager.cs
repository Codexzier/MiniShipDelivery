using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets
{
    public class AssetManager
    {
        private Texture2D _spriteTilemap;
        private Texture2D _spriteInterface;
        private SpriteFont _font;
        private Texture2D _spriteEmotes;

        public AssetManager(ContentManager content)
        {
            this._spriteTilemap = content.Load<Texture2D>("RpgUrban/tilemap");
            this._spriteInterface = content.Load<Texture2D>("Interface/interfacePack_16x_packed");
            this._font = content.Load<SpriteFont>("Fonts/BaseFont");
           this._spriteEmotes = content.Load<Texture2D>("Emote/pixel_style1");
        }

        public SpriteFont Font => this._font;

        public Texture2D Emotes => this._spriteEmotes;

        public void Draw<TAssetPart>(SpriteBatch spriteBatch, Vector2 position, TAssetPart assertPart, ISpriteProperties<TAssetPart> assetsProperties)
        {
            spriteBatch.Draw(this._spriteTilemap, position, assetsProperties.SpriteContent[assertPart], Color.AliceBlue);
        }
    }
}