﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Tilemap
{
    public class AssetManager
    {
        private Texture2D _spriteSheet;
        private SpriteFont _font;
        private Texture2D _emotes;

        public AssetManager(ContentManager content)
        {
            this._spriteSheet = content.Load<Texture2D>("RpgUrban/tilemap");
            this._font = content.Load<SpriteFont>("Fonts/BaseFont");
           this._emotes = content.Load<Texture2D>("Emote/pixel_style1");
        }

        public SpriteFont Font => this._font;

        public Texture2D Emotes => this._emotes;

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart tilemapPart, ITilemapProperties tilemapProperties)
        {
            spriteBatch.Draw(this._spriteSheet, position, tilemapProperties.Tilemaps[tilemapPart], Color.AliceBlue);
        }
    }
}