using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor
{
    internal class MapEditorHud(
        Game game,
        int screenWidth,
        int screenHeight)
    {
        /// <summary>
        /// Top Menu bar
        /// </summary>
        private readonly MapEditorMenuCommon _mapEditorMenuCommon = new(
            game,
            screenWidth,
            screenHeight);

        /// <summary>
        /// right Side menu
        /// </summary>
        private readonly MapEditorMenu _mapEditorMenu = new(
            game,
            screenWidth,
            screenHeight);

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            this.DrawGrid(spriteBatch);

            this._mapEditorMenuCommon.Draw(spriteBatch);
            this._mapEditorMenu.Draw(spriteBatch);
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            if (!this._mapEditorMenu.ShowGrid) return;

            var maxY = screenHeight / 16;
            var maxX = screenWidth / 16;
            for (int iY = 0; iY < maxY; iY++)
            {
                for (int iX = 0; iX < maxX; iX++)
                {
                    spriteBatch.DrawRectangle(
                        new Vector2(iX * 16, iY * 16),
                        new SizeF(16.5f, 16.5f),
                        Color.Gray,
                        .5f);
                }
            }
        }
    }
}