using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using System.Linq;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterNpc : BaseCharacter
    {
        private readonly CharacterType _characterType;

        public CharacterNpc(TexturesCharacter texturesCharacter,
            TexturesEmote texturesEmote,
            Vector2 position,
            CharacterType characterType) : base(texturesCharacter, texturesEmote)
        {
            this._characterType = characterType;
            this.Collider.Position = position;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            this.Emote = EmotePart.EmoteLoveDouble;
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