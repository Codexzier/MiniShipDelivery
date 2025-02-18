using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.GameDebug;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterNpc : BaseCharacter
    {
        private readonly CharacterType _characterType;

        public CharacterNpc(
            SpriteCharacter spriteCharacter,
            SpriteCharacterShadow spriteCharacterShadow,
            SpriteEmote spriteEmote,
            Vector2 position,
            CharacterType characterType) 
            : base(spriteCharacter, spriteCharacterShadow, spriteEmote)
        {
            this._characterType = characterType;
            this.Collider.SetPosition(position);
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            this.Emote = EmotePart.EmoteLoveDouble;
            
            this.IsMoving = false;
            this.UpdateFrame(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsMoving)
            {
                this.Draw(
                    spriteBatch, 
                    this.Collider.Position, 
                    CharacterPart.StandFront, 
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

        public override string ToString()
        {
            return $"{HudHelper.Vector2ToString(this.Collider.Position)}";
        }
    }
}