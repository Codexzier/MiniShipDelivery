using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Tilemap;
using System.Linq;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterPlayer : BaseCharacter
    {
        private readonly AssetManager _assetManager;
        private readonly InputManager _input;
        private readonly EmoteManager _emote;

        public CharacterPlayer(AssetManager spriteManager, InputManager input, EmoteManager emote) : base()
        {
            var shiftY = 0;
            this.Tilemaps.Add(TilemapPart.CharacterStandFront, new Rectangle(16 * 24 + 24, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandBack, new Rectangle(16 * 25 + 25, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandLeft, new Rectangle(16 * 23 + 23, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandRight, new Rectangle(16 * 26 + 26, 16 * shiftY + shiftY, 16, 16));
            shiftY = 1;
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodFront, new Rectangle(16 * 24 + 24, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodBack, new Rectangle(16 * 25 + 25, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodLeft, new Rectangle(16 * 23 + 23, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodRight, new Rectangle(16 * 26 + 26, 16 * shiftY + shiftY, 16, 16));
            shiftY = 2;
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodFront, new Rectangle(16 * 24 + 24, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodBack, new Rectangle(16 * 25 + 25, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodLeft, new Rectangle(16 * 23 + 23, 16 * shiftY + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodRight, new Rectangle(16 * 26 + 26, 16 * shiftY + shiftY, 16, 16));

            this._assetManager = spriteManager;
            this._input = input;
            this._emote = emote;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           this.Direction = this._input.MovementCharacter;

            if (this.Direction != Vector2.Zero)
            {
                this.IsMoving = true;
                this.Collider.Position += this.Direction * this.Speed * deltaTime;
            }
            else
            {
                this.IsMoving = false;
            }



            this.UpdateFrame(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
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

            tp = this.GetWalkingFrame(tp);

            this._assetManager.Draw(spriteBatch, this.Collider.Position, tp, this);

            if (this.Collisions.Any( a => a.GetType() == typeof(CharacterNpc)))
            {
                this._emote.Draw(spriteBatch, this.Collider.Position, TilemapPart.EmoteLove);
            }
        }

        
    }
}