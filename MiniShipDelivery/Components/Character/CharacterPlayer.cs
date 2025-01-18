using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterPlayer : BaseCharacter
    {
        private readonly InputManager _input;
        private readonly Vector2 _screenPosition;
        private readonly CharacterType _characterType;

        public CharacterPlayer(
            TexturesCharacter textures,
            TexturesCharacterShadow texturesCharacterShadow,
            TexturesEmote texturesEmote,
            InputManager input,
            Vector2 screenPosition,
            CharacterType characterType) 
            : base(textures, texturesCharacterShadow, texturesEmote)
        {
            this._input = input;
            this._screenPosition = screenPosition;
            this._characterType = characterType;
            this.Collider.Position = new Vector2(70, 70);
        }

        public Vector2 GetScreenPosition() => this.Collider.Position - this._screenPosition;

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            this.Emote = EmotePart.EmoteLove;
            
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           this.Direction = this._input.Inputs.MovementCharacter;

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

        public override void Draw(SpriteBatch spriteBatch)
        {
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
                    this.Collider.Position,
                    tp,
                    this._characterType);
            }
            else
            {
                this.Draw(spriteBatch,
                    this.Collider.Position,
                    this.GetStandAnimation(),
                    this._characterType);
            }
            
            base.Draw(spriteBatch);
        }
    }
}