using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Assets.Textures;
using System;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    public class AssetManager : GameComponent
    {
        
        private readonly IDictionary<string, IAssetTexture> _sprites = new Dictionary<string, IAssetTexture>();

        public AssetManager(Game game) : base(game)
        {
            this.Font = game.Content.Load<SpriteFont>("Fonts/BaseFont");

            //this.Characters = new TexturesCharacter(game);
            
            this.UiMenuMapFrames = new TexturesUiMenuMapFrames(game.Content.Load<Texture2D>("Interface/MenuMapOptions"));
            this.UiMenuMapOptions = new UiMenuMapOptions(game.Content.Load<Texture2D>("Interface/MenuMapOptions"));
            
            this.TexturesUiMenuMainButtons = new TexturesUiMenuMainButtons(game.Content.Load<Texture2D>("Interface/MainMenuButtons"));
            this.UserInterfacesMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game.Content.Load<Texture2D>("Interface/MenuEditorOptions"));

            this.UserInterfacesMouse = new UserInterfacesMouse(game.Content.Load<Texture2D>("Interface/tilemap_packed"));
            
            //this.Tilemaps = new TexturesTilemap(game.Content.Load<Texture2D>("RpgUrban/tilemap"));
            //this.Emotes = new TexturesEmote(game.Content.Load<Texture2D>("Emote/pixel_style1"));

            // register all textures
            //this._sprites.Add(nameof(TilemapPart), this.Tilemaps);
            this._sprites.Add(nameof(UiMenuFramePart), this.UiMenuMapFrames);
            this._sprites.Add(nameof(UiMenuMapOptionPart), this.UiMenuMapOptions);
            this._sprites.Add(nameof(InterfaceMenuEditorOptionPart), this.UserInterfacesMenuEditorOptions);
            this._sprites.Add(nameof(MousePart), this.UserInterfacesMouse);
            //this._sprites.Add(nameof(EmotePart), this.Emotes);
            //this._sprites.Add(nameof(CharacterPart), this.Characters);
            this._sprites.Add(nameof(UiMenuMainPart), this.TexturesUiMenuMainButtons);
        }

        public TexturesUiMenuMainButtons TexturesUiMenuMainButtons { get; }

        private TexturesUiMenuMapFrames UiMenuMapFrames { get; }
        private UiMenuMapOptions UiMenuMapOptions { get; }
        private UserInterfacesMouse UserInterfacesMouse { get; }
        public SpriteFont Font { get; }
        //private TexturesCharacter Characters { get; }
        private TexturesInterfaceMenuEditorOptions UserInterfacesMenuEditorOptions { get; }
        //private TexturesTilemap Tilemaps { get; }
        //public TexturesEmote Emotes { get; }

        private void Draw<TAssetPart>(
            SpriteBatch spriteBatch, 
            Vector2 position, 
            TAssetPart assertPart, 
            ISpriteProperties<TAssetPart> assetsProperties) where TAssetPart : Enum
        {
            spriteBatch.Draw(
                this._sprites[typeof(TAssetPart).Name].Texture, 
                position, 
                assetsProperties.SpriteContent[assertPart], 
                Color.AliceBlue);
        }

        // public void Draw<TAssetPart>(
        //     SpriteBatch spriteBatch, 
        //     Vector2 position, 
        //     TAssetPart assertPart) where TAssetPart : Enum
        // {
        //     ISpriteProperties<TAssetPart> assetsProperties = (ISpriteProperties<TAssetPart>)this.Tilemaps;
        //     
        //     this.Draw(spriteBatch, position, assertPart, assetsProperties);
        // }

        // public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart part)
        // {
        //     this.Draw(spriteBatch, position, part, this.Tilemaps);
        // }

        // public void Draw(SpriteBatch spriteBatch, Vector2 position, EmotePart part)
        // {
        //     this.Draw(spriteBatch, position, part, this.Emotes);
        // }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuFramePart part)
        {
            this.Draw(spriteBatch, position, part, this.UiMenuMapFrames);
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuMapOptionPart part)
        {
            this.Draw(spriteBatch, position, part, this.UiMenuMapOptions);
        }

        // public void Draw(SpriteBatch spriteBatch, Vector2 position, CharacterPart part, CharacterType characterType)
        // {
        //     this.Draw(spriteBatch, position, part, this.Characters);
        //
        //     var shift = (int)characterType * 3;
        //     var rect = new Rectangle(this.Characters.SpriteContent[part].X, 
        //         this.Characters.SpriteContent[part].Y + (16 * shift), 
        //         16, 
        //         16);
        //
        //     spriteBatch.Draw(this._sprites[nameof(CharacterPart)].Texture, position, rect, Color.AliceBlue);
        // }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, InterfaceMenuEditorOptionPart part)
        {
            this.Draw(spriteBatch, position, part, this.UserInterfacesMenuEditorOptions);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuMainPart part)
        {
            this.Draw(spriteBatch, position, part, this.TexturesUiMenuMainButtons);
        }
    }
}