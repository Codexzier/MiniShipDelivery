using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    internal class HudManager
    {
        private readonly MapEditorHud _mapEditorHud;
        private readonly ConsoleManager _consoleManager;

        private AssetManager _spriteManager;
        private readonly InputManager _input;
        private readonly CharacterPlayer _player;
        private readonly List<CharacterNpc> _characterNpCs;
        private readonly OrthographicCamera _camera;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        private HudOptionView hudOptionView = HudOptionView.MapEditor;

        
        public HudManager(AssetManager spriteManager,
            InputManager input,
            CharacterPlayer player,
            List<CharacterNpc> characterNpCs,
            OrthographicCamera camera,
            int screenWidth, int screenHeight)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._player = player;
            this._characterNpCs = characterNpCs;
            this._camera = camera;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            
            this._mapEditorHud = new MapEditorHud(spriteManager, 
                input,
                camera,
                screenWidth, screenHeight);
            
            this._consoleManager = new ConsoleManager(
                spriteManager,
                input,
                camera,
                screenWidth, 
                screenHeight);
        }

        internal void Update(GameTime gameTime)
        {
            this._mapEditorHud.Update(gameTime);

            this._consoleManager.AddText($"Mouse Pos.: {HudHelper.Vector2ToString(this._input.MousePosition)}");
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