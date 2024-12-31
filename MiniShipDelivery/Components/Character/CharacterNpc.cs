using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using System.Linq;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterNpc : BaseCharacter
    {
        private readonly AssetManager _spriteManager;
        private readonly CharacterType _characterType;

        public CharacterNpc(
            AssetManager spriteManager, 
            Vector2 position, 
            CharacterType characterType)
        {
            this._spriteManager = spriteManager;
            this._characterType = characterType;
            this.Collider.Position = position;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            this._spriteManager.Draw(spriteBatch, this.Collider.Position, CharacterPart.StandFront, this._characterType);

            if(this.Collisions.Any(a => a.GetType() == typeof(CharacterPlayer)))
            {
                this._spriteManager.Draw(
                    spriteBatch, 
                    this.Collider.Position - new Vector2(0, 16), 
                    EmotePart.EmoteLoveDouble);
            }
        }

        public override string ToString()
        {
            return $"{HudHelper.Vector2ToString(this.Collider.Position)}";
        }
    }
}