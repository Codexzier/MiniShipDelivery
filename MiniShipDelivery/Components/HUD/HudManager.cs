using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Tilemap;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.HUD
{
    internal class HudManager
    {
        private AssetManager _spriteManager;
        private readonly InputManager _input;
        private readonly CharacterPlayer _player;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public IDictionary<InterfacePart, Rectangle> Tilemaps { get; private set; }

        private int[][] _frame = new int[][]
        {
            new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
            new int[] { 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5 },
            new int[] { 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8 },
        };

        public HudManager(AssetManager spriteManager, InputManager input, CharacterPlayer player, int screenWidth, int screenHeight)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._player = player;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this.Tilemaps = new Dictionary<InterfacePart, Rectangle>
            {
                { InterfacePart.BaseFrame_TopLeft, new Rectangle(10 * 16 + 0, 0, 4, 4) },
                { InterfacePart.BaseFrame_TopMiddle, new Rectangle(10 * 16 + 4, 0, 4, 4) },
                { InterfacePart.BaseFrame_TopRight, new Rectangle(10 * 16 + 12, 0, 4, 4) },
                { InterfacePart.BaseFrame_MiddleLeft, new Rectangle(10 * 16 + 0, 4, 4, 4) },
                { InterfacePart.BaseFrame_MiddleMiddle, new Rectangle(10 * 16 + 4, 4, 4, 4) },
                { InterfacePart.BaseFrame_MiddleRight, new Rectangle(10 * 16 + 12, 4, 4, 4) },
                { InterfacePart.BaseFrame_DownLeft, new Rectangle(10 * 16 + 0, 12, 4, 4) },
                { InterfacePart.BaseFrame_DownMiddle, new Rectangle(10 * 16 + 4, 12, 4, 4) },
                { InterfacePart.BaseFrame_DownRight, new Rectangle(10 * 16 + 12, 12, 4, 4) }
            };
        }

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // draw the frame that fit on screen
            var countMiddleForWidth = this._screenWidth / 4;
            // start single frame
            this.CreateScreenWidthFrame(spriteBatch, 
                countMiddleForWidth, 
                0,
                InterfacePart.BaseFrame_TopLeft, 
                InterfacePart.BaseFrame_TopMiddle, 
                InterfacePart.BaseFrame_TopRight);
            for (var y = 0; y < 3; y++)
            {
                this.CreateScreenWidthFrame(spriteBatch, 
                    countMiddleForWidth,
                    4 + (y * 4),
                    InterfacePart.BaseFrame_MiddleLeft, 
                    InterfacePart.BaseFrame_MiddleMiddle, 
                    InterfacePart.BaseFrame_MiddleRight);
            }
            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                16,
                InterfacePart.BaseFrame_DownLeft,
                InterfacePart.BaseFrame_DownMiddle,
                InterfacePart.BaseFrame_DownRight);





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

        private void CreateScreenWidthFrame(SpriteBatch spriteBatch, int countMiddleForWidth, int shiftHeight, InterfacePart left, InterfacePart middle, InterfacePart right)
        {
            this._spriteManager.Draw(spriteBatch, new Vector2(0, shiftHeight), this.Tilemaps[left]);
            for (var x = 1; x < countMiddleForWidth - 1; x++)
            {
                this._spriteManager.Draw(spriteBatch, new Vector2(x * 4, shiftHeight), this.Tilemaps[middle]);
            }
            this._spriteManager.Draw(spriteBatch, new Vector2((countMiddleForWidth - 1) * 4, shiftHeight), this.Tilemaps[right]);
        }
    }
}