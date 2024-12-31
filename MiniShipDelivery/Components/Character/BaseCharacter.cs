using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Objects;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Character
{
    public abstract class BaseCharacter : ICollider
    {
        private int _currentFrame;
        private float _timeToUpdate;
        private float _timeElapsed;

        public ColliderBox2D Collider { get; } = new(16, 16);
        public List<ICollider> Collisions { get; } = new();

        public Vector2 Direction { get; internal set; }
        public int Speed { get; internal init; }

        protected bool IsMoving { get; set; }

        public float FramesPerSecond
        {
            set => this._timeToUpdate = 1f / value;
        }

        private readonly Dictionary<int, CharacterPart> _walkingFrames = new()
        {
            { 0, CharacterPart.WalkLeftFoodFront },
            { 1, CharacterPart.StandFront },
            { 2, CharacterPart.WalkRightFoodFront },
            { 3, CharacterPart.StandFront },

            { 4, CharacterPart.WalkLeftFoodLeft },
            { 5, CharacterPart.StandLeft },
            { 6, CharacterPart.WalkRightFoodLeft },
            { 7, CharacterPart.StandLeft },

            { 8, CharacterPart.WalkLeftFoodRight },
            { 9, CharacterPart.StandRight },
            { 10, CharacterPart.WalkRightFoodRight },
            { 11, CharacterPart.StandRight },

            { 12, CharacterPart.WalkLeftFoodBack },
            { 13, CharacterPart.StandBack },
            { 14, CharacterPart.WalkRightFoodBack },
            { 15, CharacterPart.StandBack },
        };

        protected void UpdateFrame(GameTime gameTime)
        {
            this._timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this._timeElapsed > this._timeToUpdate)
            {
                this._timeElapsed -= this._timeToUpdate;
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

        private readonly IDictionary<CharacterPart, int> _charDirection = new Dictionary<CharacterPart, int>()
        {
            {CharacterPart.StandFront, 0 },
            {CharacterPart.StandLeft, 4 },
            {CharacterPart.StandRight, 8 },
            {CharacterPart.StandBack, 12 },
        };

        protected CharacterPart GetWalkingFrame(CharacterPart tp)
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}