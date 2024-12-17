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

        public CharacterPlayer(AssetManager spriteManager, InputManager input, EmoteManager emote, Vector2 screenPosition) : base()
        {
            this._assetManager = spriteManager;
            this._input = input;
            this._emote = emote;
            
            this.Collider.Position = screenPosition;
            this.Position = new Vector2(0, 0);

            this.SetupTilemapsCharacter(CharacterType.Men);
        }


        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           this.Direction = this._input.MovementCharacter;

            if (this.Direction != Vector2.Zero)
            {
                this.IsMoving = true;
                this.Position += this.Direction * this.Speed * deltaTime;
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