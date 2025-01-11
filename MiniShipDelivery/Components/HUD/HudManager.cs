using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Editor;
using MiniShipDelivery.Components.HUD.GameMenu;
using MiniShipDelivery.Components.HUD.MainMenu;

namespace MiniShipDelivery.Components.HUD
{
    public class HudManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        
        private readonly MainMenuHud _mainMenuHud;
        private readonly GameMenuManager _gameMenuManager;

        private readonly MapEditorHud _mapEditorHud;
        
        private readonly CameraManager _camera;

        public HudManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this._camera = game.GetComponent<CameraManager>();
            
            this._mainMenuHud = new MainMenuHud(game);
            this._mainMenuHud.ButtonHasPressedEvent += this.MenuButtonHasPressed;

            this._gameMenuManager = new GameMenuManager(game); 
            
            this._mapEditorHud = new MapEditorHud(game);
        }

        private void MenuButtonHasPressed(HudOptionView view)
        {
            GlobaleGameParameters.HudView = view;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            switch (GlobaleGameParameters.HudView)
            {
                case HudOptionView.Game:
                    this._gameMenuManager.Draw(this._spriteBatch);
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