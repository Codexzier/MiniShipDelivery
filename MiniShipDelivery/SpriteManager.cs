using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery
{
    public class SpriteManager
    {
        private Texture2D _spriteSheet;

        public SpriteManager(ContentManager content)
        {
            this._spriteSheet = content.Load<Texture2D>("RpgUrban/tilemap");
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TilemapPart tilemapPart, ITilemapProperties tilemapProperties)
        {
            spriteBatch.Draw(this._spriteSheet, position, tilemapProperties.Tilemaps[tilemapPart], Color.AliceBlue);
        }
    }
}