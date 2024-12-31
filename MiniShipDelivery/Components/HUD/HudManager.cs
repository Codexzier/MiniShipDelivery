using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    public class HudManager(
        Game game,
        AssetManager spriteManager,
        InputManager input,
        OrthographicCamera camera,
        CharacterManager characterManager,
        int screenWidth,
        int screenHeight)
        : GameComponent(game)
    {
        private readonly MainMenuHud _mainMenuHud = new MainMenuHud(spriteManager);
        private readonly MapEditorHud _mapEditorHud = new(spriteManager, 
            input,
            camera,
            screenWidth, screenHeight);
        private readonly ConsoleManager _consoleManager = new(
            spriteManager,
            camera, 
            screenHeight);

        private HudOptionView hudOptionView = HudOptionView.MainMenu;

        public override void Update(GameTime gameTime)
        {
            this._mapEditorHud.Update(gameTime);

            this._consoleManager.AddText($"{DateTime.Now:f}");
            this._consoleManager.AddText($"Mouse Pos.: {HudHelper.Vector2ToString(input.Inputs.MousePosition)}");
            this._consoleManager.AddText($"Char. Pos.: {HudHelper.Vector2ToString(characterManager.Player.Collider.Position)}");

            foreach (var charNpc in characterManager.CharacterNpCs)
            {
                this._consoleManager.AddText($"NPC: {charNpc.Collider.Position}");
            }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (this.hudOptionView)
            {
                case HudOptionView.MainMenu:
                    this._mainMenuHud.Draw(spriteBatch, gameTime);
                    break;
                case HudOptionView.MapEditor:
                    this._mapEditorHud.Draw(spriteBatch, gameTime);
                    break;
            }

            this._consoleManager.DrawText(spriteBatch);
        }
    }

    internal class MainMenuHud(AssetManager spriteManager)
    {
        private readonly AssetManager _spriteManager = spriteManager;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawButton(spriteBatch, "Start Game", new Vector2(10, 10));
        }

        private void DrawButton(SpriteBatch spriteBatch, string buttonText, Vector2 position)
        {
            spriteBatch.DrawRectangle(
                position,
                new SizeF(120, 30),
                Color.White);
            
            spriteBatch.DrawString(
                this._spriteManager.Font,
                buttonText,
                position + new Vector2(10, 10),
                Color.White);
        }
    }
}