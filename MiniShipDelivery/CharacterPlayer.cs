using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MiniShipDelivery
{
    public class CharacterPlayer : ITilemapProperties
    {
        private readonly SpriteManager _spriteManager;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Sprite { get; set; }

        public IDictionary<TilemapPart, Rectangle> Tilemaps { get; private set; }

        public CharacterPlayer(ContentManager content, SpriteManager spriteManager)
        {
            this.Sprite = content.Load<Texture2D>("Character/roguelikeChar_magenta");

            this.Tilemaps = new Dictionary<TilemapPart, Rectangle>
            {
                { TilemapPart.CharacterStandFront, new Rectangle((16 * 24) + 24, (16 * 0) + 0, 16, 16) },
                { TilemapPart.CharacterStandBack, new Rectangle((16 * 25) + 25, (16 * 0) + 0, 16, 16) },
                { TilemapPart.CharacterStandLeft, new Rectangle((16 * 23) + 23, (16 * 0) + 0, 16, 16) },
                { TilemapPart.CharacterStandRight, new Rectangle((16 * 26) + 26, (16 * 0) + 0, 16, 16) },

            };
            this._spriteManager = spriteManager;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var tp = TilemapPart.CharacterStandFront;

            switch (this.Direction)
            {
                case var d when d.X < 0:
                    tp = TilemapPart.CharacterStandLeft;
                    break;
                case var d when d.X > 0:
                    tp = TilemapPart.CharacterStandRight;
                    break;
                case var d when d.Y > 0:
                    tp = TilemapPart.CharacterStandFront;
                    break;
                case var d when d.Y < 0:
                    tp = TilemapPart.CharacterStandBack;
                    break;
                default:
                    break;
            }

            this._spriteManager.Draw(spriteBatch, this.Position, tp, this);
        }
    }
}