using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Packs;
using MiniShipDelivery.Components.Assets.Parts;
using System.Collections.Generic;
using MiniShipDelivery.Components.Character;

namespace MiniShipDelivery.Components.Assets
{
    public class AssetManager
    {
        private readonly SpriteFont _font;
        private readonly IDictionary<string, Texture2D> _sprites = new Dictionary<string, Texture2D>();

        public AssetManager(ContentManager content)
        {
            this._font = content.Load<SpriteFont>("Fonts/BaseFont");

            this.InterfacePack = new InterfacePack4x4(content.Load<Texture2D>("Interface/interfacePack_16x_packed"));
            this.TilemapPack = new TilemapPack(content.Load<Texture2D>("RpgUrban/tilemap"));
            this.EmotePack = new EmotePack(content.Load<Texture2D>("Emote/pixel_style1"));

            this._sprites.Add(nameof(TilemapPart), this.TilemapPack.Texture);
            this._sprites.Add(nameof(InterfacePart4x4), this.InterfacePack.Texture);
            this._sprites.Add(nameof(EmotePart), this.EmotePack.Texture);
        }
  
        public SpriteFont Font => this._font;

        internal InterfacePack4x4 InterfacePack { get; }
        internal TilemapPack TilemapPack { get; } 
        internal EmotePack EmotePack { get; } 

        public void Draw<TAssetPart>(SpriteBatch spriteBatch, Vector2 position, TAssetPart assertPart, ISpriteProperties<TAssetPart> assetsProperties) where TAssetPart : Enum
        {
            spriteBatch.Draw(this._sprites[typeof(TAssetPart).Name], position, assetsProperties.SpriteContent[assertPart], Color.AliceBlue);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart part)
        {
            this.Draw(spriteBatch, position, part, this.TilemapPack);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, EmotePart part)
        {
            this.Draw(spriteBatch, position, part, this.EmotePack);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, InterfacePart4x4 part)
        {
            this.Draw(spriteBatch, position, part, this.InterfacePack);
        }
    }
}