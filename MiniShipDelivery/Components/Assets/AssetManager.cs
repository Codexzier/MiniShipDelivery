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
        private readonly IDictionary<string, Texture2D> _sprites = new Dictionary<string, Texture2D>();

        public AssetManager(ContentManager content)
        {
            this.Font = content.Load<SpriteFont>("Fonts/BaseFont");

            this.CharacterPack = new CharacterPack(content.Load<Texture2D>("Character/UrbanCharacters"));
            this.InterfacePack = new InterfacePack4x4(content.Load<Texture2D>("Interface/interfacePack_16x_packed"));
            this.TilemapPack = new TilemapPack(content.Load<Texture2D>("RpgUrban/tilemap"));
            this.EmotePack = new EmotePack(content.Load<Texture2D>("Emote/pixel_style1"));

            this._sprites.Add(nameof(TilemapPart), this.TilemapPack.Texture);
            this._sprites.Add(nameof(InterfacePart4x4), this.InterfacePack.Texture);
            this._sprites.Add(nameof(EmotePart), this.EmotePack.Texture);
        }

        public CharacterPack CharacterPack { get; }

        public SpriteFont Font { get; }

        private InterfacePack4x4 InterfacePack { get; }
        private TilemapPack TilemapPack { get; }
        private EmotePack EmotePack { get; } 

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

    public class CharacterPack : ISpriteProperties<CharacterPart>
    {
        public Texture2D Texture { get; }

        public CharacterPack(Texture2D texture)
        {
            this.Texture = texture;
        }

        public IDictionary<CharacterPart, Rectangle> SpriteContent { get; }
    }
}