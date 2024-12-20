﻿using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Objects;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Character
{
    public abstract class BaseCharacter : ISpriteProperties<TilemapPart>, ICollider
    {
        protected int _currentFrame;
        protected float _timeToUpdate;
        protected float _timeEleapsed;

        protected BaseCharacter()
        {
            this.SpriteContent = new Dictionary<TilemapPart, Rectangle>();
            this.Collider = new ColliderBox2D(16, 16);
            this.Collisions = new List<ICollider>();
        }

        public ColliderBox2D Collider { get; }
        public List<ICollider> Collisions { get; }

        public IDictionary<TilemapPart, Rectangle> SpriteContent { get; private set; }

        public Vector2 Position { get; protected set; }


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

        protected void SetupTilemapsCharacter(CharacterType characterType)
        {
            var shift = (int)characterType * 3;

            var shiftY1 = 0 + shift;
            var shiftY2 = 1 + shift;
            var shiftY3 = 2 + shift;

            this.SpriteContent.Add(TilemapPart.CharacterStandLeft, new Rectangle((16 * 23) + 23, (16 * shiftY1) + shiftY1, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkLeftFoodLeft, new Rectangle((16 * 23) + 23, (16 * shiftY2) + shiftY2, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkRightFoodLeft, new Rectangle((16 * 23) + 23, (16 * shiftY3) + shiftY3, 16, 16));

            this.SpriteContent.Add(TilemapPart.CharacterStandFront, new Rectangle((16 * 24) + 24, (16 * shiftY1) + shiftY1, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkLeftFoodFront, new Rectangle((16 * 24) + 24, (16 * shiftY2) + shiftY2, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkRightFoodFront, new Rectangle((16 * 24) + 24, (16 * shiftY3) + shiftY3, 16, 16));

            this.SpriteContent.Add(TilemapPart.CharacterStandBack, new Rectangle((16 * 25) + 25, (16 * shiftY1) + shiftY1, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkLeftFoodBack, new Rectangle((16 * 25) + 25, (16 * shiftY2) + shiftY2, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkRightFoodBack, new Rectangle((16 * 25) + 25, (16 * shiftY3) + shiftY3, 16, 16));

            this.SpriteContent.Add(TilemapPart.CharacterStandRight, new Rectangle((16 * 26) + 26, (16 * shiftY1) + shiftY1, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkLeftFoodRight, new Rectangle((16 * 26) + 26, (16 * shiftY2) + shiftY2, 16, 16));
            this.SpriteContent.Add(TilemapPart.CharacterWalkRightFoodRight, new Rectangle((16 * 26) + 26, (16 * shiftY3) + shiftY3, 16, 16));
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

        private IDictionary<TilemapPart, int> _charDirection = new Dictionary<TilemapPart, int>()
        {
            {TilemapPart.CharacterStandLeft, 4 },
            {TilemapPart.CharacterStandRight, 8 },
            {TilemapPart.CharacterStandBack, 12 },
        };

        protected TilemapPart GetWalkingFrame(TilemapPart tp)
        {
            if (!this.IsMoving)
            {
                return tp;
            }

            return this._walkingFrames[this._currentFrame + this._charDirection[tp]];
        }

        public void OnCollision(ICollider otherCollider)
        {
            this.Collisions.Add(otherCollider);
        }

        public void ClearCollisions()
        {
            this.Collisions.Clear();
        }
    }
}