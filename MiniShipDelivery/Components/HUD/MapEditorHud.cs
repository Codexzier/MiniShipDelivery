using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.HUD
{
    internal class MapEditorHud
    {
        private AssetManager _spriteManager;
        private InputManager _input;
        private int _screenWidth;
        private int _screenHeight;
        private MenuFrame _menuTop;

        public MapEditorHud(AssetManager spriteManager, InputManager input, int screenWidth, int screenHeight)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;

            this._menuTop = new MenuFrame(spriteManager);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.TopMenu(spriteBatch);
            this.SideMenu(spriteBatch);

            // draw Tilemap on Sidemenu
        }

        private void TopMenu(SpriteBatch spriteBatch)
        {
            this._menuTop.DrawMenuFrame(spriteBatch,
                0, 0,
                this._screenWidth, 20);
        }

        private void SideMenu(SpriteBatch spriteBatch)
        {
            this._menuTop.DrawMenuFrame(spriteBatch,
                this._screenWidth - 20, 20,
                20, this._screenHeight - 20);

        }
    }
}