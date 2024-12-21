using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    internal class MapEditorHud
    {
        private AssetManager _spriteManager;
        private InputManager _input;
        private readonly OrthographicCamera _camera;
        private int _screenWidth;
        private int _screenHeight;
        private MenuFrame _menuTop;

        private Vector2 _menuTopPosition = new Vector2(0, 0);

        public MapEditorHud(AssetManager spriteManager, 
            InputManager input, 
            OrthographicCamera camera, 
            int screenWidth, int screenHeight)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._camera = camera;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;

            this._menuTop = new MenuFrame(spriteManager);
        }

        internal void Update(GameTime gameTime)
        {
            this._menuTopPosition = this._camera.Position;
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
                this._menuTopPosition,
                this._screenWidth, 20,
                MenuFrameType.Type3);
        }

        private void SideMenu(SpriteBatch spriteBatch)
        {
            int sideMenuWidth = 60;

            this._menuTop.DrawMenuFrame(spriteBatch, 
                this._menuTopPosition + new Vector2(this._screenWidth - sideMenuWidth, 20),
                sideMenuWidth, this._screenHeight - 20,
                MenuFrameType.Type2);

            // map sprites
            // |---------|----------|
            // |    <<   |   >>     |
            // |---------|----------|
            // | MapPar1 | MapPart2 |
            // |---------|----------|
            // | MapPar1 | MapPart2 |
            // |---------|----------|
            
            this._spriteManager.Draw(spriteBatch, 
                this._menuTopPosition  + new Vector2(this._screenWidth - sideMenuWidth + 3, 20 + 3),
                TilemapPart.GrassAndBrick_TopLeft,
                this._spriteManager.TilemapPack);
        }
    }
}