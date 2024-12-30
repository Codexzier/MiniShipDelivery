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
            
            //this.UserInterfaces = new TexturesInterfacePack4x4(content.Load<Texture2D>("Interface/interfacePack_16x_packed"));
            this.UiMenuMapFrames = new TexturesUiMenuMapFrames(content.Load<Texture2D>("Interface/MenuMapOptions"));
            // this.UserInterfaces16x16 = new TexturesInterface16x16(content.Load<Texture2D>("Interface/interfacePack_16x_packed"));
            this.UiMenuMapOptions = new UiMenuMapOptions(content.Load<Texture2D>("Interface/MenuMapOptions"));
            
            this.UserInterfacesMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(content.Load<Texture2D>("Interface/MenuEditorOptions"));

            this.UserInterfacesMouse = new UserInterfacesMouse(content.Load<Texture2D>("Interface/tilemap_packed"));
            
            this.Tilemaps = new TexturesTilemap(content.Load<Texture2D>("RpgUrban/tilemap"));
            this.Emotes = new TexturesEmote(content.Load<Texture2D>("Emote/pixel_style1"));

            // register all textures
            this._sprites.Add(nameof(TilemapPart), this.Tilemaps.Texture);
            this._sprites.Add(nameof(UiMenuFramePart), this.UiMenuMapFrames.Texture);
            this._sprites.Add(nameof(UiMenuMapOptionPart), this.UiMenuMapOptions.Texture);
            //this._sprites.Add(nameof(InterfacePart4x4), this.UserInterfaces.Texture);
            //this._sprites.Add(nameof(InterfacePart16x16), this.UserInterfaces16x16.Texture);
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
        //private TexturesInterfacePack4x4 UserInterfaces { get; }
        //internal TexturesInterface16x16 UserInterfaces16x16 { get; }
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

        // public void Draw(SpriteBatch spriteBatch, Vector2 position, InterfacePart4x4 part)
        // {
        //     this.Draw(spriteBatch, position, part, this.UserInterfaces);
        // }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuFramePart part)
        {
            this.Draw(spriteBatch, position, part, this.UiMenuMapFrames);
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position, UiMenuMapOptionPart part)
        {
            this.Draw(spriteBatch, position, part, this.UiMenuMapOptions);
        }

        // public void Draw(SpriteBatch spriteBatch, Vector2 position, InterfacePart16x16 part)
        // {
        //     this.Draw(spriteBatch, position, part, this.UserInterfaces16x16);
        // }

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

    public class TexturesUiMenuMapFrames : ISpriteProperties<UiMenuFramePart>
    {
        public TexturesUiMenuMapFrames(Texture2D texture)
        {
            this.Texture = texture;
            this.SpriteContent = new Dictionary<UiMenuFramePart, Rectangle>
            {
                { UiMenuFramePart.None, new Rectangle(0, 0, 0, 0) },
                
                // BaseFrame4x4 Type1
                { UiMenuFramePart.BaseFrame_Type1_TopLeft, new Rectangle((7 * 16) + 0, 0, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_TopMiddle, new Rectangle((7 * 16) + 4, 0, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_TopRight, new Rectangle((7 * 16) + 12, 0, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_MiddleLeft, new Rectangle((7 * 16) + 0, 4, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_MiddleMiddle, new Rectangle((7 * 16) + 4, 4, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_MiddleRight, new Rectangle((7 * 16) + 12, 4, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_DownLeft, new Rectangle((7 * 16) + 0, 12, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_DownMiddle, new Rectangle((7 * 16) + 4, 12, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type1_DownRight, new Rectangle((7 * 16) + 12, 12, 4, 4) },
                // BaseFrame4x4 Type2
                { UiMenuFramePart.BaseFrame_Type2_TopLeft, new Rectangle((8 * 16) + 0, 0, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_TopMiddle, new Rectangle((8 * 16) + 4, 0, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_TopRight, new Rectangle((8 * 16) + 12, 0, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_MiddleLeft, new Rectangle((8 * 16) + 0, 4, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_MiddleMiddle, new Rectangle((8 * 16) + 4, 4, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_MiddleRight, new Rectangle((8 * 16) + 12, 4, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_DownLeft, new Rectangle((8 * 16) + 0, 12, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_DownMiddle, new Rectangle((8 * 16) + 4, 12, 4, 4) },
                { UiMenuFramePart.BaseFrame_Type2_DownRight, new Rectangle((8 * 16) + 12, 12, 4, 4) },
            };
        }

        public IDictionary<UiMenuFramePart, Rectangle> SpriteContent { get; }
        public Texture2D Texture { get; }
    }
}