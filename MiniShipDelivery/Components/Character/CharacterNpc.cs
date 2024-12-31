using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Emote;
using System.Linq;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.Character
{
    public class CharacterNpc : BaseCharacter
    {
        private AssetManager spriteManager;
        private readonly EmoteManager _emote;

        public CharacterType CharacterType { get; }

        public CharacterNpc(AssetManager spriteManager, EmoteManager emote, Vector2 position, CharacterType characterType)
        {
            this.spriteManager = spriteManager;
            this._emote = emote;
            this.CharacterType = characterType;
            this.Collider.Position = position;
        }

        internal void Update(GameTime gameTime)
        {
        }
        
        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.spriteManager.Draw(spriteBatch, this.Collider.Position, CharacterPart.StandFront, this.CharacterType);

            if(this.Collisions.Any(a => a.GetType() == typeof(CharacterPlayer)))
            {
                this._emote.Draw(spriteBatch, this.Collider.Position, EmotePart.EmoteLoveDouble);
            }
        }

        public override string ToString()
        {
            return $"{HudHelper.Vector2ToString(this.Collider.Position)}";
        }
    }
}