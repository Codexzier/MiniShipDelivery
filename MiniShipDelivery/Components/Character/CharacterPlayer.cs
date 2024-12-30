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

        public CharacterType CharacterType { get; }

        public CharacterPlayer(
            AssetManager spriteManager, 
            InputManager input, 
            Vector2 screenPosition,
            CharacterType characterType)
        {
            this._assetManager = spriteManager;
            this._input = input;
            this.CharacterType = characterType;
            this.Collider.Position = screenPosition;
        }


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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var tp = CharacterPart.StandFront;

            switch (this.Direction)
            {
                case var d when d.X < 0:
                    tp = CharacterPart.StandLeft;
                    break;
                case var d when d.X > 0:
                    tp = CharacterPart.StandRight;
                    break;
                case var d when d.Y > 0:
                    tp = CharacterPart.StandFront;
                    break;
                case var d when d.Y < 0:
                    tp = CharacterPart.StandBack;
                    break;
            }

            tp = this.GetWalkingFrame(tp);

            this._assetManager.Draw(
                spriteBatch, 
                this.Collider.Position, 
                tp, 
                this.CharacterType);

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