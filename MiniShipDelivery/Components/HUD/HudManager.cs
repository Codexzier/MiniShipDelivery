using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    public class HudManager : GameComponent
    {
        private readonly MapEditorHud _mapEditorHud;
        private readonly ConsoleManager _consoleManager;

        private readonly InputManager _input;
        private readonly CharacterPlayer _player;
        private readonly List<CharacterNpc> _characterNpCs;

        private HudOptionView hudOptionView = HudOptionView.MapEditor;

        public HudManager(Game game,
            AssetManager spriteManager,
            InputManager input,
            OrthographicCamera camera,
            CharacterPlayer player,
            List<CharacterNpc> characterNpCs,
            int screenWidth, int screenHeight) : base(game)
        {
            this._input = input;
            this._player = player;
            this._characterNpCs = characterNpCs;

            this._mapEditorHud = new MapEditorHud(spriteManager, 
                input,
                camera,
                screenWidth, screenHeight);
            
            this._consoleManager = new ConsoleManager(
                spriteManager,
                camera, 
                screenHeight);
        }

        public override void Update(GameTime gameTime)
        {
            this._mapEditorHud.Update(gameTime);

            this._consoleManager.AddText($"Mouse Pos.: {HudHelper.Vector2ToString(this._input.Inputs.MousePosition)}");
            this._consoleManager.AddText($"Char. Pos.: {HudHelper.Vector2ToString(this._player.Collider.Position)}");

            foreach (var charNpc in this._characterNpCs)
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