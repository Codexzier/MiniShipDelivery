using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using System.Linq;
using MiniShipDelivery.Components.Assets.Textures;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Helpers;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterNpc : BaseCharacter
    {
        private readonly CharacterType _characterType;

        public CharacterNpc(
            TexturesCharacter spriteManager, 
            Vector2 position, 
            CharacterType characterType) : base(spriteManager)
        {
            this._characterType = characterType;
            this.Collider.Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // if(this.Collisions.Any(a => a.GetType() == typeof(CharacterPlayer)))
            // {
            //     this._spriteManager.Draw(
            //         spriteBatch, 
            //         this.Collider.Position - new Vector2(0, 16), 
            //         EmotePart.EmoteLoveDouble);
            // }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Draw(
                spriteBatch, 
                this.Collider.Position, 
                CharacterPart.StandFront, 
                this._characterType);
        }

        public override string ToString()
        {
            return $"{HudHelper.Vector2ToString(this.Collider.Position)}";
        }
    }
}