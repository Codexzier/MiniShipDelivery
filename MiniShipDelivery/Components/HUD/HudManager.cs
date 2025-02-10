using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Editor;
using MiniShipDelivery.Components.HUD.GameMenu;
using MiniShipDelivery.Components.HUD.GameMenuMap;
using MiniShipDelivery.Components.HUD.GameMenuQuest;
using MiniShipDelivery.Components.HUD.MainMenu;
using MiniShipDelivery.Components.HUD.OptionsMenu;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    public class HudManager : DrawableGameComponent
    {
        private ApplicationBus Bus => ApplicationBus.Instance;
        private readonly SpriteBatch _spriteBatch;
        
        private readonly MainMenuHud _mainMenuHud;
        private readonly GameMenuManager _gameMenuManager;
        private readonly GameMenuQuestManager _gameMenuQuestManager;
        private readonly GameMenuMapManager _gameMenuMapManager;

        private readonly MapEditorHud _mapEditorHud;
        
        private readonly OptionsMenuManager _optionsMenuManager;

        public HudManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            
            this._mainMenuHud = new MainMenuHud(
                game, 
                GlobaleGameParameters.ScreenWidthHalf, 
                GlobaleGameParameters.ScreenHeightHalf);
            this._mainMenuHud.ButtonHasPressedEvent += this.MenuButtonHasPressed;

            this._gameMenuManager = new GameMenuManager(game); 
            this._gameMenuQuestManager = new GameMenuQuestManager(game);
            this._gameMenuMapManager = new GameMenuMapManager(game);
            
            this._mapEditorHud = new MapEditorHud(game);
            
            this._optionsMenuManager = new OptionsMenuManager(game);
        }

        private void MenuButtonHasPressed(HudOptionView view)
        {
            GlobaleGameParameters.HudView = view;
        }

        public override void Update(GameTime gameTime)
        {
            this.UpdateCurrentMouseOverMenuState();

            switch (GlobaleGameParameters.HudView)
            {
                case HudOptionView.Game:
                    this._gameMenuManager.Update();
                    this._gameMenuMapManager.Update();
                    this._gameMenuQuestManager.Update();
                    break;
                case HudOptionView.MainMenu:
                    this._mainMenuHud.Update();
                    break;
                case HudOptionView.MapEditor:
                    this._mapEditorHud.Update();
                    break;
                case HudOptionView.Options:
                    this._optionsMenuManager.Update();
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix();
            
            switch (GlobaleGameParameters.HudView)
            {
                case HudOptionView.Game:
                    this._gameMenuManager.Draw(this._spriteBatch);
                    this._gameMenuMapManager.Draw(this._spriteBatch);
                    this._gameMenuQuestManager.Draw(this._spriteBatch);
                    break;
                case HudOptionView.MainMenu:
                    this._mainMenuHud.Draw(this._spriteBatch);
                    break;
                case HudOptionView.MapEditor:
                    this._mapEditorHud.Draw(this._spriteBatch);
                    break;
                case HudOptionView.Options:
                    this._optionsMenuManager.Draw(this._spriteBatch);
                    break;
            }

            if (GlobaleGameParameters.DebugMode)
            {
                // draw fields with rectangle
                this.DrawRectangles();
            }
            
            this._spriteBatch.End();
        }

        /// <summary>
        /// show rectangle of menu areas.
        /// </summary>
        private void DrawRectangles()
        {
            var pos = this.Bus.Camera.GetPosition();
            foreach (var rectangle in MapEditorMenu.MenuField)
            {
                this._spriteBatch.DrawRectangle(
                    new RectangleF(
                        rectangle.X + pos.X, 
                        rectangle.Y + pos.Y, 
                        rectangle.Width, 
                        rectangle.Height), 
                    Color.Chocolate);
            }
        }
        
        public static bool MouseIsOverMenu { get; set; }

        private void UpdateCurrentMouseOverMenuState()
        {
            if (MapEditorMenu.MenuField.Any(HudHelper.IsMouseInRange))
            {
                MouseIsOverMenu = true;
                return;
            }
            
            // Thats is dirty
            if (GlobaleGameParameters.HudView == HudOptionView.MapEditor &&
                GlobaleGameParameters.SystemDialogBox)
            {
                MouseIsOverMenu = true;
                return;
            }

            MouseIsOverMenu = false;
        }
    }
}