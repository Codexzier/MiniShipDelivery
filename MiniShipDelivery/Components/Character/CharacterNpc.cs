using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Emote;
using System.Linq;

namespace MiniShipDelivery.Components.Character
{
    internal class CharacterNpc : BaseCharacter
    {
        private AssetManager spriteManager;
        private readonly EmoteManager _emote;

        public CharacterNpc(AssetManager spriteManager, EmoteManager emote, Vector2 startPosition) : base()
        {
            this.spriteManager = spriteManager;
            this._emote = emote;

            this.Position = startPosition;
            this.Collider.Position = startPosition;

            this.SetupTilemapsCharacter(CharacterType.Women);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.spriteManager.Draw(spriteBatch, this.Collider.Position, TilemapPart.CharacterStandFront, this);

            if(this.Collisions.Any(a => a.GetType() == typeof(CharacterPlayer)))
            {
                this._emote.Draw(spriteBatch, this.Collider.Position, EmotePart.EmoteLoveDouble);
            }
        }

        internal void Update(GameTime gameTime)
        {
        }
    }
}