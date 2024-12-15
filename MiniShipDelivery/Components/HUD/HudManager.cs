using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Tilemap;

namespace MiniShipDelivery.Components.HUD
{
    internal class HudManager
    {
        private AssetManager _spriteManager;
        private readonly InputManager _input;

        public HudManager(AssetManager spriteManager, InputManager input)
        {
            this._spriteManager = spriteManager;
            this._input = input;
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
        }

    }
}