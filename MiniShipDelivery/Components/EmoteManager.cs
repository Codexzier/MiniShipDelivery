using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Tilemap;
using System.Collections.Generic;

namespace MiniShipDelivery.Components
{
    public class EmoteManager : ITilemapProperties
    {
        private AssetManager _spriteManager;

        public EmoteManager(AssetManager spriteManager)
        {
            this._spriteManager = spriteManager;

            this.Tilemaps = new Dictionary<TilemapPart, Rectangle>
            {
                { TilemapPart.EmoteHappy, new Rectangle(0, 0, 16, 16) },
                { TilemapPart.EmoteSad, new Rectangle(16, 0, 16, 16) },
                { TilemapPart.EmoteAngry, new Rectangle(32, 0, 16, 16) },
                { TilemapPart.EmoteSurprised, new Rectangle(48, 0, 16, 16) },
                { TilemapPart.EmoteSleepy, new Rectangle(64, 0, 16, 16) },
                { TilemapPart.EmoteSick, new Rectangle(80, 0, 16, 16) },
                { TilemapPart.EmoteConfused, new Rectangle(96, 0, 16, 16) },
                { TilemapPart.EmoteLove, new Rectangle(80, 0, 16, 16) },
                { TilemapPart.EmoteLoveDouble, new Rectangle(96, 0, 16, 16) },
                { TilemapPart.EmoteQuestion, new Rectangle(128, 0, 16, 16) },
                { TilemapPart.EmoteExclamation, new Rectangle(144, 0, 16, 16) },
                { TilemapPart.EmoteMusic, new Rectangle(160, 0, 16, 16) },
                { TilemapPart.EmoteZzz, new Rectangle(176, 0, 16, 16) },
                { TilemapPart.EmoteDotDotDot, new Rectangle(192, 0, 16, 16) }
            };
        }

        public IDictionary<TilemapPart, Rectangle> Tilemaps { get; }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart tilemapPart)
        {
            // draw the emote
            spriteBatch.Draw(this._spriteManager.Emotes, 
                position + new Vector2(0, -16), 
                this.Tilemaps[tilemapPart], 
                Color.White);
        }
    }
}