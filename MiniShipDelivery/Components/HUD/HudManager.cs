using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Editor;
using MiniShipDelivery.Components.HUD.MainMenu;

namespace MiniShipDelivery.Components.HUD
{
    public class HudManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        
        private readonly MainMenuHud _mainMenuHud;
        private readonly MapEditorHud _mapEditorHud;

        private HudOptionView _hudOptionView = HudOptionView.MainMenu;
        private readonly CameraManager _camera;

        public HudManager(Game game,
            AssetManager assetManager,
            int screenWidth,
            int screenHeight) : base(game)
        {
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);

            var input = game.GetComponent<InputManager>();
            this._camera = game.GetComponent<CameraManager>();
            
            this._mainMenuHud = new MainMenuHud(game, assetManager, input, this._camera.Camera, screenWidth, screenHeight);
            this._mainMenuHud.ButtonHasPressedEvent += this.MenuButtonHasPressed;
            
            this._mapEditorHud = new MapEditorHud(
                game, 
                assetManager, 
                screenWidth, screenHeight);
        }

        private void MenuButtonHasPressed(HudOptionView view)
        {
            this._hudOptionView = view;
        }

        public override void Update(GameTime gameTime)
        {
            this._mapEditorHud.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            switch (this._hudOptionView)
            {
                case HudOptionView.Game:
                    break;
                case HudOptionView.MainMenu:
                    this._mainMenuHud.Draw(this._spriteBatch);
                    break;
                case HudOptionView.MapEditor:
                    this._mapEditorHud.Draw(this._spriteBatch);
                    break;
            }
            
            this._spriteBatch.End();
        }
    }
}