using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Tilemap
{
    public class AssetManager
    {
        private Texture2D _spriteSheet;
        private SpriteFont _font;

        public AssetManager(ContentManager content)
        {
            this._spriteSheet = content.Load<Texture2D>("RpgUrban/tilemap");
            this._font = content.Load<SpriteFont>("Fonts/BaseFont");
           
        }

        public SpriteFont Font => this._font;

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart tilemapPart, ITilemapProperties tilemapProperties)
        {
            spriteBatch.Draw(this._spriteSheet, position, tilemapProperties.Tilemaps[tilemapPart], Color.AliceBlue);
        }
    }
}