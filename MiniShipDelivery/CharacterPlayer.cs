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

        public bool IsMoving { get; set; }

        private int _currentFrame;

        public IDictionary<TilemapPart, Rectangle> Tilemaps { get; private set; }
        public float FramesPerSecond
        {
            set
            {
                this._timeToUpdate = 1f / value;
            }
        }

        public CharacterPlayer(SpriteManager spriteManager)
        {
            this.Tilemaps = new Dictionary<TilemapPart, Rectangle>();
            int shiftY = 0;
            this.Tilemaps.Add(TilemapPart.CharacterStandFront, new Rectangle((16 * 24) + 24, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandBack, new Rectangle((16 * 25) + 25, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterStandLeft, new Rectangle((16 * 23) + 23, (16 * shiftY) + shiftY, 16, 16) );
            this.Tilemaps.Add(TilemapPart.CharacterStandRight, new Rectangle((16 * 26) + 26, (16 * shiftY) + shiftY, 16, 16));
            shiftY = 1;
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodFront, new Rectangle((16 * 24) + 24, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodBack, new Rectangle((16 * 25) + 25, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodLeft, new Rectangle((16 * 23) + 23, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkLeftFoodRight, new Rectangle((16 * 26) + 26, (16 * shiftY) + shiftY, 16, 16));
            shiftY = 2;
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodFront, new Rectangle((16 * 24) + 24, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodBack, new Rectangle((16 * 25) + 25, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodLeft, new Rectangle((16 * 23) + 23, (16 * shiftY) + shiftY, 16, 16));
            this.Tilemaps.Add(TilemapPart.CharacterWalkRightFoodRight, new Rectangle((16 * 26) + 26, (16 * shiftY) + shiftY, 16, 16));

            this._spriteManager = spriteManager;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.Direction != Vector2.Zero)
            {
                this.IsMoving = true;
                this.Position += this.Direction * this.Speed * deltaTime;
            }
            else
            {
                this.IsMoving = false;
            }

            this._timeEleapsed += deltaTime;

            if (this._timeEleapsed > this._timeToUpdate)
            {
                this._timeEleapsed -= this._timeToUpdate;

                if (this._currentFrame < 3)
                {
                    this._currentFrame++;
                }
                else
                {
                    this._currentFrame = 0;
                }
            }
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

            this._spriteManager.Draw(spriteBatch, this.Position, tp, this);
        }

        private Dictionary<int, TilemapPart> _walkingFrames = new Dictionary<int, TilemapPart>
        {
            { 0, TilemapPart.CharacterWalkLeftFoodFront },
            { 1, TilemapPart.CharacterStandFront },
            { 2, TilemapPart.CharacterWalkRightFoodFront },
            { 3, TilemapPart.CharacterStandFront },

            { 4, TilemapPart.CharacterWalkLeftFoodLeft },
            { 5, TilemapPart.CharacterStandLeft },
            { 6, TilemapPart.CharacterWalkRightFoodLeft },
            { 7, TilemapPart.CharacterStandLeft },

            { 8, TilemapPart.CharacterWalkLeftFoodRight },
            { 9, TilemapPart.CharacterStandRight },
            { 10, TilemapPart.CharacterWalkRightFoodRight },
            { 11, TilemapPart.CharacterStandRight },

            { 12, TilemapPart.CharacterWalkLeftFoodBack },
            { 13, TilemapPart.CharacterStandBack },
            { 14, TilemapPart.CharacterWalkRightFoodBack },
            { 15, TilemapPart.CharacterStandBack },
        };
        private float _timeEleapsed;
        private float _timeToUpdate;

        private TilemapPart GetWalkingFrame(TilemapPart tp)
        {
            if (!this.IsMoving)
            {
                return tp;
            }

            var shiftFrameIndex = 0;
            switch (tp)
            {
                case TilemapPart.CharacterStandLeft:
                    shiftFrameIndex = 4;
                    break;
                case TilemapPart.CharacterStandRight:
                    shiftFrameIndex = 8;
                    break;
                case TilemapPart.CharacterStandBack:
                    shiftFrameIndex = 12;
                    break;
            }

            return this._walkingFrames[this._currentFrame + shiftFrameIndex];
        }
    }
}