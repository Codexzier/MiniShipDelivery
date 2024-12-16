using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Tilemap;
using System.Linq;

namespace MiniShipDelivery.Components.Character
{
    internal class CharacterNpc : BaseCharacter
    {
        private AssetManager spriteManager;
        private readonly EmoteManager _emote;

        public CharacterNpc(AssetManager spriteManager, EmoteManager emote) : base()
        {
            this.spriteManager = spriteManager;
            this._emote = emote;

            // women
            var shiftY = 3;
            this.Tilemaps.Add(TilemapPart.CharacterStandFront, new Rectangle(16 * 24 + 24, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandBack, new Rectangle(16 * 25 + 25, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandLeft, new Rectangle(16 * 23 + 23, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandRight, new Rectangle(16 * 26 + 26, 16 * shiftY + shiftY, 16, 16));
            shiftY = 4;
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodFront, new Rectangle(16 * 24 + 24, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodBack, new Rectangle(16 * 25 + 25, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodLeft, new Rectangle(16 * 23 + 23, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodRight, new Rectangle(16 * 26 + 26, 16 * shiftY + shiftY, 16, 16));
            shiftY = 5;
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodFront, new Rectangle(16 * 24 + 24, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodBack, new Rectangle(16 * 25 + 25, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodLeft, new Rectangle(16 * 23 + 23, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodRight, new Rectangle(16 * 26 + 26, 16 * shiftY + shiftY, 16, 16));

        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.spriteManager.Draw(spriteBatch, this.Collider.Position, TilemapPart.CharacterStandFront, this);

            if(this.Collisions.Any(a => a.GetType() == typeof(CharacterPlayer)))
            {
                this._emote.Draw(spriteBatch, this.Collider.Position, TilemapPart.EmoteLoveDouble);
            }
        }

        internal void Update(GameTime gameTime)
        {
        }
    }
}