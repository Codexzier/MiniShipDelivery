﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public static HudOptionView HudView = HudOptionView.MainMenu;
        private readonly CameraManager _camera;

        public HudManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this._camera = game.GetComponent<CameraManager>();
            
            this._mainMenuHud = new MainMenuHud(game);
            
            this._mainMenuHud.ButtonHasPressedEvent += this.MenuButtonHasPressed;
            
            this._mapEditorHud = new MapEditorHud(game);
        }

        private void MenuButtonHasPressed(HudOptionView view)
        {
            HudView = view;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            switch (HudView)
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