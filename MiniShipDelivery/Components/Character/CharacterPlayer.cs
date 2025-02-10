using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterPlayer : BaseCharacter
    {
        private readonly Vector2 _screenPosition;
        private readonly CharacterType _characterType;

        public CharacterPlayer(
            SpriteCharacter sprite,
            SpriteCharacterShadow spriteCharacterShadow,
            SpriteEmote spriteEmote,
            Vector2 screenPosition,
            CharacterType characterType) 
            : base(sprite, spriteCharacterShadow, spriteEmote)
        {
            this._screenPosition = screenPosition;
            this._characterType = characterType;
            this.Collider.Position = new Vector2(70, 70);
        }

        public bool IsCollide { get; set; }

        public Vector2 GetScreenPosition() => this.Collider.Position - this._screenPosition;

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            this.Emote = EmotePart.EmoteLove;
            
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           this.Direction = ApplicationBus.Instance.Inputs.MovementCharacter;

            if (this.Direction != Vector2.Zero)
            {
                this.IsMoving = true;
                
                if(!this.IsCollide) 
                {
                    this.Collider.Position += this.Direction * this.Speed * deltaTime;
                }
            }
            else
            {
                this.IsMoving = false;
            }

            this.UpdateFrame(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           var position = this.Collider.Position;
            
            if (this.IsMoving)
            {
                var tp = this.Direction switch
                {
                    { X: < 0 } => CharacterPart.StandLeft,
                    { X: > 0 } => CharacterPart.StandRight,
                    { Y: > 0 } => CharacterPart.StandFront,
                    { Y: < 0 } => CharacterPart.StandBack,
                    _ => CharacterPart.StandFront
                };

                tp = this.GetWalkingFrame(tp);

                this.Draw(spriteBatch,
                    position,
                    tp,
                    this._characterType);
            }
            else
            {
                this.Draw(spriteBatch,
                    position,
                    this.GetStandAnimation(),
                    this._characterType);
            }
            
            base.Draw(spriteBatch);
        }
    }
}