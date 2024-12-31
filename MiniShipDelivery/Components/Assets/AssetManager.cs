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
    public class AssetManager : GameComponent
    {
        
        private readonly IDictionary<string, Texture2D> _sprites = new Dictionary<string, Texture2D>();

        public AssetManager(Game game) : base(game)
        {
            
            this.Font = game.Content.Load<SpriteFont>("Fonts/BaseFont");

            this.Characters = new TexturesCharacter(game.Content.Load<Texture2D>("Character/UrbanCharacters"));
            
            this.UiMenuMapFrames = new TexturesUiMenuMapFrames(game.Content.Load<Texture2D>("Interface/MenuMapOptions"));
            this.UiMenuMapOptions = new UiMenuMapOptions(game.Content.Load<Texture2D>("Interface/MenuMapOptions"));
            
            this.UserInterfacesMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game.Content.Load<Texture2D>("Interface/MenuEditorOptions"));

            this.UserInterfacesMouse = new UserInterfacesMouse(game.Content.Load<Texture2D>("Interface/tilemap_packed"));
            
            this.Tilemaps = new TexturesTilemap(game.Content.Load<Texture2D>("RpgUrban/tilemap"));
            this.Emotes = new TexturesEmote(game.Content.Load<Texture2D>("Emote/pixel_style1"));

            // register all textures
            this._sprites.Add(nameof(TilemapPart), this.Tilemaps.Texture);
            this._sprites.Add(nameof(UiMenuFramePart), this.UiMenuMapFrames.Texture);
            this._sprites.Add(nameof(UiMenuMapOptionPart), this.UiMenuMapOptions.Texture);
            this._sprites.Add(nameof(InterfaceMenuEditorOptionPart), this.UserInterfacesMenuEditorOptions.Texture);
            this._sprites.Add(nameof(MousePart), this.UserInterfacesMouse.Texture);
            this._sprites.Add(nameof(EmotePart), this.Emotes.Texture);
            this._sprites.Add(nameof(CharacterPart), this.Characters.Texture);
        }

        public TexturesUiMenuMapFrames UiMenuMapFrames { get; }
        public UiMenuMapOptions UiMenuMapOptions { get; }
        public UserInterfacesMouse UserInterfacesMouse { get; }
        public SpriteFont Font { get; }
        public TexturesCharacter Characters { get; }
        internal TexturesInterfaceMenuEditorOptions UserInterfacesMenuEditorOptions { get; }
        private TexturesTilemap Tilemaps { get; }
        private TexturesEmote Emotes { get; } 
        
        public void Draw<TAssetPart>(
            SpriteBatch spriteBatch, 
            Vector2 position, 
            TAssetPart assertPart, 
            ISpriteProperties<TAssetPart> assetsProperties) where TAssetPart : Enum
        {
            spriteBatch.Draw(
                this._sprites[typeof(TAssetPart).Name], 
                position, 
                assetsProperties.SpriteContent[assertPart], 
                Color.AliceBlue);
        }

        public void Draw<TAssetPart>(
            SpriteBatch spriteBatch, 
            Vector2 position, 
            TAssetPart assertPart) where TAssetPart : Enum
        {
            ISpriteProperties<TAssetPart> assetsProperties = (ISpriteProperties<TAssetPart>)this.Tilemaps;
            
            this.Draw(spriteBatch, position, assertPart, assetsProperties);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart part)
        {
            this.Draw(spriteBatch, position, part, this.Tilemaps);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, EmotePart part)
        {
            this.Draw(spriteBatch, position, part, this.Emotes);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuFramePart part)
        {
            this.Draw(spriteBatch, position, part, this.UiMenuMapFrames);
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuMapOptionPart part)
        {
            this.Draw(spriteBatch, position, part, this.UiMenuMapOptions);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, CharacterPart part, CharacterType characterType)
        {
            this.Draw(spriteBatch, position, part, this.Characters);

            var shift = (int)characterType * 3;
            var rect = new Rectangle(this.Characters.SpriteContent[part].X, 
                this.Characters.SpriteContent[part].Y + (16 * shift), 
                16, 
                16);

            spriteBatch.Draw(this._sprites[nameof(CharacterPart)], position, rect, Color.AliceBlue);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, InterfaceMenuEditorOptionPart part)
        {
            this.Draw(spriteBatch, position, part, this.UserInterfacesMenuEditorOptions);
        }
    }
}