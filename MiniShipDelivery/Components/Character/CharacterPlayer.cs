using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using System.Linq;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterPlayer : BaseCharacter
    {
        private readonly AssetManager _assetManager;
        private readonly InputManager _input;
        private readonly Vector2 _screenPosition;
        private readonly CharacterType _characterType;

        public CharacterPlayer(
            AssetManager spriteManager, 
            InputManager input, 
            Vector2 screenPosition,
            CharacterType characterType)
        {
            this._assetManager = spriteManager;
            this._input = input;
            this._screenPosition = screenPosition;
            this._characterType = characterType;
            this.Collider.Position = screenPosition;
        }

        public Vector2 GetScreenPosition() => this.Collider.Position - this._screenPosition;

        public void Update(GameTime gameTime)
        {
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
            var tp = this.Direction switch
            {
                { X: < 0 } => CharacterPart.StandLeft,
                { X: > 0 } => CharacterPart.StandRight,
                { Y: > 0 } => CharacterPart.StandFront,
                { Y: < 0 } => CharacterPart.StandBack,
                _ => CharacterPart.StandFront
            };

            tp = this.GetWalkingFrame(tp);

            this._assetManager.Draw(
                spriteBatch, 
                this.Collider.Position, 
                tp, 
                this._characterType);

            if (this.Collisions.Any( a => a.GetType() == typeof(CharacterNpc)))
            {
                this._assetManager.Draw(
                    spriteBatch, 
                    this.Collider.Position - new Vector2(0, 16), 
                    EmotePart.EmoteLove);
            }
        }
    }
}