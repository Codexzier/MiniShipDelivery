﻿using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Objects;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Character
{
    public abstract class BaseCharacter(
        SpriteCharacter spriteCharacter,
        SpriteCharacterShadow spriteCharacterShadow,
        SpriteEmote spriteEmote) : ICollider
    {
        private int _currentFrame;
        private float _timeToUpdate;
        private float _timeElapsed;
        
        private int _currentFrameStand;
        private float _timeToUpdateStand;
        private float _timeElapsedStand;

        private readonly Vector2 _positionShiftDraw = new(2, 8);
        
        public ColliderBox2D Collider { get; } = new(12, 8, new Vector2(0, 0), 0, 0);
        public List<ICollider> Collisions { get; } = new();
        public bool IsColliding { get; private set; }
        
        public EmotePart Emote { get; protected set; }

        public Vector2 Direction { get; internal set; }
        public int Speed { get; internal init; }

        protected bool IsMoving { get; set; }

        public float FramesPerSecond
        {
            set
            {
                this._timeToUpdate = 1f / value;
                this._timeToUpdateStand = 2.5f / value;
            }
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

            this._timeElapsedStand += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this._timeElapsedStand > this._timeToUpdateStand)
            {
                this._timeElapsedStand -= this._timeToUpdateStand;
                if (this._currentFrameStand < 3)
                {
                    this._currentFrameStand++;
                }
                else
                {
                    this._currentFrameStand = 0;
                }
            }
        }

        private readonly IDictionary<CharacterPart, int> _charDirection = new Dictionary<CharacterPart, int>
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

        protected CharacterStandPart GetStandAnimation()
        {
            return (CharacterStandPart)this._currentFrameStand;
        }

        public void OnCollision(ICollider otherCollider)
        {
            this.Collisions.Add(otherCollider);
        }

        public void ClearCollisions()
        {
            this.Collisions.Clear();
        }

        public virtual void Update(GameTime gameTime)
        {
            this.IsColliding = this
                .Collisions
                .Any(a => a.GetType() == typeof(CharacterPlayer) || 
                          a.GetType() == typeof(CharacterNpc));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this.DrawShadow(spriteBatch);
        }
        
        protected void Draw(
            SpriteBatch spriteBatch, 
            Vector2 position, 
            CharacterPart tp, 
            CharacterType characterType)
        {
            var shift = (int)characterType * 3;
            var rect = new Rectangle(
                spriteCharacter.SpriteContent[tp].Cutout.X, 
                     spriteCharacter.SpriteContent[tp].Cutout.Y + (16 * shift), 
                     16, 
                     16);
            
            spriteBatch.Draw(
                spriteCharacter.Texture,
                position - this._positionShiftDraw,
                rect,
                Color.White);
        }
        
        protected void Draw(
            SpriteBatch spriteBatch, 
            Vector2 position, 
            CharacterStandPart tp, 
            CharacterType characterType)
        {
            if (characterType != CharacterType.Men && characterType != CharacterType.Women)
            {
                this.Draw(
                    spriteBatch, 
                    position - this._positionShiftDraw, 
                    CharacterPart.StandFront, 
                    characterType);
                return;
            }

            switch (characterType)
            {
                case CharacterType.Men:
                    spriteBatch.Draw(
                        spriteCharacter.TextureStandMen,
                        position - this._positionShiftDraw,
                        spriteCharacter.SpriteContentStandMen[tp].Cutout,
                        Color.White);
                    break;
                case CharacterType.Women:
                    spriteBatch.Draw(
                        spriteCharacter.TextureStandWomen,
                        position - this._positionShiftDraw,
                        spriteCharacter.SpriteContentStandWomen[tp].Cutout,
                        Color.White);
                    break;
            }
            
        }


        private void DrawShadow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteCharacterShadow.Texture,
                this.Collider.Position - this._positionShiftDraw + new Vector2(0, 7),
                spriteCharacterShadow.SpriteContent[0].Cutout,
                Color.White);
        }
        
        public virtual void DrawEmote(
            SpriteBatch spriteBatch,
            Vector2 position)
        {
            spriteBatch.Draw(
                spriteEmote.Texture, 
                position - this._positionShiftDraw, 
                spriteEmote.SpriteContent[this.Emote].Cutout, 
                Color.White);
        }
    }
}