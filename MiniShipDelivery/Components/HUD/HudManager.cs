using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Tilemap;

namespace MiniShipDelivery.Components.HUD
{
    internal class HudManager
    {
        private AssetManager _spriteManager;
        private readonly InputManager _input;
        private readonly CharacterPlayer _player;

        public HudManager(AssetManager spriteManager, InputManager input, Character.CharacterPlayer player)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._player = player;
        }

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // write the mouse position in text format on the top left screen area
            spriteBatch.DrawString(this._spriteManager.Font,
                $"Mouse Position: {this._input.MovementMouse}",
                new Vector2(10, 10),
                Color.White);

            // write collision information on the top right screen area
            spriteBatch.DrawString(this._spriteManager.Font,
                $"Collision: {this._player.Collisions.Count}",
                new Vector2(10, 30),
                Color.White);
        }

    }
}