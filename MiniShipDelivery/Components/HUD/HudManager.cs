using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Tilemap;

namespace MiniShipDelivery.Components.HUD
{
    internal class HudManager
    {
        private readonly InterfacePack _interfacePack;
        private readonly MapEditorHud _mapEditorHud;

        private AssetManager _spriteManager;
        private readonly InputManager _input;
        private readonly CharacterPlayer _player;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        private HudOptionView hudOptionView = HudOptionView.MapEditor;

        
        public HudManager(AssetManager spriteManager, InputManager input, CharacterPlayer player, int screenWidth, int screenHeight)
        {
            this._interfacePack = new InterfacePack();
            this._mapEditorHud = new MapEditorHud(spriteManager, input, screenWidth, screenHeight, this._interfacePack);

            this._spriteManager = spriteManager;
            this._input = input;
            this._player = player;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
        }

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (this.hudOptionView)
            {
                case HudOptionView.MapEditor:
                    this._mapEditorHud.Draw(spriteBatch, gameTime);
                    break;
            }




            // write the mouse position in text format on the top left screen area
            spriteBatch.DrawString(this._spriteManager.Font,
                $"Mouse Position: {this._input.MovementMouse}",
                new Vector2(10, 6),
                Color.White,
                0f,
                new Vector2(0, 0),
                .5f,
                SpriteEffects.None, 1);

            // write collision information on the top right screen area
            spriteBatch.DrawString(this._spriteManager.Font,
                $"Collision: {this._player.Collisions.Count}",
                new Vector2(10, 14),
                Color.White, 
                0f, 
                new Vector2(0, 0), 
                .5f, 
                SpriteEffects.None, 1);
        }
    }
}