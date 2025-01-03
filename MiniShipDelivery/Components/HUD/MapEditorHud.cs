using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    internal class MapEditorHud
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        /// <summary>
        /// Top Menu bar
        /// </summary>
        private readonly MapEditorMenuCommon _mapEditorMenuCommon;

        /// <summary>
        /// right Side menu
        /// </summary>
        private readonly MapEditorMenu _mapEditorMenu;

        public MapEditorHud(Game game, AssetManager spriteManager,
            InputManager input,
            OrthographicCamera camera,
            int screenWidth, int screenHeight)
        {
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;

            this._mapEditorMenuCommon = new MapEditorMenuCommon(
                spriteManager,
                input,
                camera,
                screenWidth,
                screenHeight);

            this._mapEditorMenu = new MapEditorMenu(
                game,
                spriteManager,
                input,
                camera,
                screenWidth,
                screenHeight);
        }

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

            var maxY = this._screenHeight / 16;
            var maxX = this._screenWidth / 16;
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