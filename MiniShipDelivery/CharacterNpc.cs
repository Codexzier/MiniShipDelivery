using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MiniShipDelivery
{
    internal class CharacterNpc : ITilemapProperties
    {
        private SpriteManager spriteManager;

        public CharacterNpc(SpriteManager spriteManager)
        {
            this.spriteManager = spriteManager;

            this.Tilemaps = new Dictionary<TilemapPart, Rectangle>();

            // women
            int shiftY = 3;
            this.Tilemaps.Add(TilemapPart.CharacterWomenStandFront, new Rectangle((16 * 24) + 24, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenStandBack, new Rectangle((16 * 25) + 25, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenStandLeft, new Rectangle((16 * 23) + 23, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenStandRight, new Rectangle((16 * 26) + 26, (16 * shiftY) + shiftY, 16, 16));
            shiftY = 4;
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkLeftFoodFront, new Rectangle((16 * 24) + 24, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkLeftFoodBack, new Rectangle((16 * 25) + 25, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkLeftFoodLeft, new Rectangle((16 * 23) + 23, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkLeftFoodRight, new Rectangle((16 * 26) + 26, (16 * shiftY) + shiftY, 16, 16));
            shiftY = 5;
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkRightFoodFront, new Rectangle((16 * 24) + 24, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkRightFoodBack, new Rectangle((16 * 25) + 25, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkRightFoodLeft, new Rectangle((16 * 23) + 23, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWomenWalkRightFoodRight, new Rectangle((16 * 26) + 26, (16 * shiftY) + shiftY, 16, 16));

        }

        public Vector2 Position { get; internal set; }
        public Vector2 Direction { get; internal set; }
        public int Speed { get; internal set; }
        public int FramesPerSecond { get; internal set; }

        public IDictionary<TilemapPart, Rectangle> Tilemaps { get; private set; }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.spriteManager.Draw(spriteBatch, this.Position, TilemapPart.CharacterWomenStandFront, this);
        }

        internal void Update(GameTime gameTime)
        {
        }
    }
}