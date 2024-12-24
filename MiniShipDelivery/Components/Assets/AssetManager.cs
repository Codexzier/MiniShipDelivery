using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Assets.Textures;
using MiniShipDelivery.Components.Character;
using System;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    public class AssetManager
    {
        private readonly IDictionary<string, Texture2D> _sprites = new Dictionary<string, Texture2D>();

        public AssetManager(ContentManager content)
        {
            this.Font = content.Load<SpriteFont>("Fonts/BaseFont");

            this.Characters = new TexturesCharacter(content.Load<Texture2D>("Character/UrbanCharacters"));
            this.UserInterfaces = new TexturesInterfacePack4x4(content.Load<Texture2D>("Interface/interfacePack_16x_packed"));
            this.Tilemaps = new TexturesTilemap(content.Load<Texture2D>("RpgUrban/tilemap"));
            this.Emotes = new TexturesEmote(content.Load<Texture2D>("Emote/pixel_style1"));

            this._sprites.Add(nameof(TilemapPart), this.Tilemaps.Texture);
            this._sprites.Add(nameof(InterfacePart4x4), this.UserInterfaces.Texture);
            this._sprites.Add(nameof(EmotePart), this.Emotes.Texture);
            this._sprites.Add(nameof(CharacterPart), this.Characters.Texture);
        }


        public SpriteFont Font { get; }

        public TexturesCharacter Characters { get; }
        private TexturesInterfacePack4x4 UserInterfaces { get; }
        private TexturesTilemap Tilemaps { get; }
        private TexturesEmote Emotes { get; } 

        public void Draw<TAssetPart>(SpriteBatch spriteBatch, Vector2 position, TAssetPart assertPart, ISpriteProperties<TAssetPart> assetsProperties) where TAssetPart : Enum
        {
            spriteBatch.Draw(this._sprites[typeof(TAssetPart).Name], position, assetsProperties.SpriteContent[assertPart], Color.AliceBlue);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart part)
        {
            this.Draw(spriteBatch, position, part, this.Tilemaps);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, EmotePart part)
        {
            this.Draw(spriteBatch, position, part, this.Emotes);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, InterfacePart4x4 part)
        {
            this.Draw(spriteBatch, position, part, this.UserInterfaces);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, CharacterPart part, CharacterType characterType)
        {
            this.Draw(spriteBatch, position, part, this.Characters);

            var shift = (int)characterType * 3;
            var rect = new Rectangle(this.Characters.SpriteContent[part].X, 
                this.Characters.SpriteContent[part].Y + (16 * shift), 
                16, 
                16);

            spriteBatch.Draw(this._sprites[typeof(CharacterPart).Name], position, rect, Color.AliceBlue);
        }
    }
}