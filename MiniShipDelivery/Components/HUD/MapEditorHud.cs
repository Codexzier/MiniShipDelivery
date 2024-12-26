using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    internal class MapEditorHud
    {
        private int _screenWidth;
        private int _screenHeight;

        // =================================
        // Top Menu
        private MenuEditorOptions _menuEditorOptions;

        // =================================
        // side menu
        private MapEditorMenu _mapEditorMenu;

        public MapEditorHud(AssetManager spriteManager,
            InputManager input,
            OrthographicCamera camera,
            int screenWidth, int screenHeight)
        {
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;

            // ==============================================
            // Top Menu
            this._menuEditorOptions = new MenuEditorOptions(
                spriteManager,
                camera,
                screenWidth,
                screenHeight,
                new Vector2(0, 0),
                new Size(screenWidth, 20));

            // ==============================================
            // side Menu
            this._mapEditorMenu = new MapEditorMenu(
                spriteManager,
                input,
                camera,
                screenWidth,
                screenHeight);
        }

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawGrid(spriteBatch);

            this._menuEditorOptions.Draw(spriteBatch, gameTime);
            this._mapEditorMenu.Draw(spriteBatch, gameTime);
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
                        .5f,
                        0f);
                }
            }
        }
    }
}