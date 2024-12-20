using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Packs;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    public class AssetManager
    {
        private readonly Texture2D _spriteTilemap;
        private Texture2D _spriteInterface;
        private SpriteFont _font;
        private Texture2D _spriteEmotes;
        private IDictionary<string, Texture2D> _sprites = new Dictionary<string, Texture2D>();

        public AssetManager(ContentManager content)
        {
            this._spriteTilemap = content.Load<Texture2D>("RpgUrban/tilemap");
            this._spriteInterface = content.Load<Texture2D>("Interface/interfacePack_16x_packed");
            this._font = content.Load<SpriteFont>("Fonts/BaseFont");
            this._spriteEmotes = content.Load<Texture2D>("Emote/pixel_style1");

            this._sprites.Add("TilemapPart", this._spriteTilemap);
            this._sprites.Add("InterfacePart", this._spriteInterface);
            this._sprites.Add("EmotePart", this._spriteEmotes);
        }

        public SpriteFont Font => this._font;

        internal InterfacePack InterfacePack { get; set; } = new InterfacePack();
        internal TilemapPack TilemapPack { get; set; } = new TilemapPack();
        internal EmotePack EmotePack { get; set; } = new EmotePack();

        public void Draw<TAssetPart>(SpriteBatch spriteBatch, Vector2 position, TAssetPart assertPart, ISpriteProperties<TAssetPart> assetsProperties)
        {
            spriteBatch.Draw(this._sprites[typeof(TAssetPart).Name], position, assetsProperties.SpriteContent[assertPart], Color.AliceBlue);
        }
    }
}