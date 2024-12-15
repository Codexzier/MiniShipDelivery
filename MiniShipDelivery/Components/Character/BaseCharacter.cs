using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Tilemap;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Character
{
    public abstract class BaseCharacter : ITilemapProperties
    {

        protected int _currentFrame;
        protected float _timeToUpdate;
        protected float _timeEleapsed;

        protected BaseCharacter()
        {
            this.Tilemaps = new Dictionary<TilemapPart, Rectangle>();

        }

        public IDictionary<TilemapPart, Rectangle> Tilemaps { get; private set; }

        public Vector2 Position { get; internal set; }
        public Vector2 Direction { get; internal set; }
        public int Speed { get; internal set; }

        public bool IsMoving { get; set; }

        public float FramesPerSecond
        {
            set
            {
                this._timeToUpdate = 1f / value;
            }
        }

        protected Dictionary<int, TilemapPart> _walkingFrames = new Dictionary<int, TilemapPart>
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

        protected void UpdateFrame(GameTime gameTime)
        {
            this._timeEleapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
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

        protected TilemapPart GetWalkingFrame(TilemapPart tp)
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