using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD.Editor;
using MiniShipDelivery.Components.HUD.Helpers;
using MiniShipDelivery.Components.HUD.MainMenu;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    public class HudManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        
        private readonly MainMenuHud _mainMenuHud;
        private readonly MapEditorHud _mapEditorHud;
        private readonly ConsoleManager _consoleManager;

        private HudOptionView _hudOptionView = HudOptionView.MainMenu;
        private readonly InputManager _input;
        private readonly OrthographicCamera _camera;
        private readonly CharacterManager _characterManager;

        public HudManager(Game game,
            AssetManager assetManager,
            InputManager input,
            OrthographicCamera camera,
            CharacterManager characterManager,
            int screenWidth,
            int screenHeight) : base(game)
        {
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            
            this._input = input;
            this._camera = camera;
            this._characterManager = characterManager;
            this._mainMenuHud = new MainMenuHud(assetManager, input, camera, screenWidth, screenHeight);
            this._mainMenuHud.ButtonHasPressedEvent += this.MenuButtonHasPressed;
            
            this._mapEditorHud = new MapEditorHud(game, assetManager, 
                input,
                camera,
                screenWidth, screenHeight);
            this._consoleManager = new ConsoleManager(
                assetManager,
                camera, 
                screenHeight);
        }

        private void MenuButtonHasPressed(HudOptionView view)
        {
            this._hudOptionView = view;
        }

        public override void Update(GameTime gameTime)
        {
            this._mapEditorHud.Update(gameTime);

            this._consoleManager.AddText($"{DateTime.Now:f}");
            this._consoleManager.AddText($"Mouse Pos.: {HudHelper.Vector2ToString(this._input.Inputs.MousePosition)}");
            this._consoleManager.AddText($"Char. Pos.: {HudHelper.Vector2ToString(this._characterManager.Player.Collider.Position)}");

            foreach (var charNpc in this._characterManager.CharacterNpCs)
            {
                this._consoleManager.AddText($"NPC: {charNpc.Collider.Position}");
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.Begin(
                transformMatrix: this._camera.GetViewMatrix(), 
                samplerState: SamplerState.PointClamp);
            
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

            this._consoleManager.DrawText(this._spriteBatch);
            
            this._spriteBatch.End();
        }
    }
}