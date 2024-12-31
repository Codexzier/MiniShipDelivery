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
        private readonly MapEditorHud _mapEditorHud = new(spriteManager, 
            input,
            camera,
            screenWidth, screenHeight);
        private readonly ConsoleManager _consoleManager = new(
            spriteManager,
            camera, 
            screenHeight);

        private HudOptionView hudOptionView = HudOptionView.MapEditor;

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
                case HudOptionView.MapEditor:
                    this._mapEditorHud.Draw(spriteBatch, gameTime);
                    break;
            }

            this._consoleManager.DrawText(spriteBatch);
        }
    }
}