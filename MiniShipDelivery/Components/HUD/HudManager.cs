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

        public IDictionary<InterfacePart, Rectangle> Tilemaps { get; private set; }

        private int[][] _frame = new int[][]
        {
            new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
            new int[] { 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5 },
            new int[] { 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8 },
        };

        public HudManager(AssetManager spriteManager, InputManager input, CharacterPlayer player)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._player = player;

            this.Tilemaps = new Dictionary<InterfacePart, Rectangle>();
            this.Tilemaps.Add(InterfacePart.BaseFrame_TopLeft, new Rectangle(10 * 16 + 0, 0, 4, 4));
            this.Tilemaps.Add(InterfacePart.BaseFrame_TopMiddle, new Rectangle(10 * 16 + 4, 0, 4, 4));
            this.Tilemaps.Add(InterfacePart.BaseFrame_TopRight, new Rectangle(10 * 16 + 12, 0, 4, 4));

            this.Tilemaps.Add(InterfacePart.BaseFrame_MiddleLeft, new Rectangle(10 * 16 + 0, 4, 4, 4));
            this.Tilemaps.Add(InterfacePart.BaseFrame_MiddleMiddle, new Rectangle(10 * 16 + 4, 4, 4, 4));
            this.Tilemaps.Add(InterfacePart.BaseFrame_MiddleRight, new Rectangle(10 * 16 + 12, 4, 4, 4));
            
            this.Tilemaps.Add(InterfacePart.BaseFrame_DownLeft, new Rectangle(10 * 16 + 0, 12, 4, 4));
            this.Tilemaps.Add(InterfacePart.BaseFrame_DownMiddle, new Rectangle(10 * 16 + 4, 12, 4, 4));
            this.Tilemaps.Add(InterfacePart.BaseFrame_DownRight, new Rectangle(10 * 16 + 12, 12, 4, 4));
        }

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // draw the frame
            for (var y = 0; y < this._frame.Length; y++)
            {
                for (var x = 0; x < this._frame[y].Length; x++)
                {
                    var t = (InterfacePart)this._frame[y][x];
                    this._spriteManager.Draw(spriteBatch, new Vector2(x * this.Tilemaps[t].Width, y * this.Tilemaps[t].Height), this.Tilemaps[t]);
                }
            }

            // write the mouse position in text format on the top left screen area
            spriteBatch.DrawString(this._spriteManager.Font,
                $"Mouse Position: {this._input.MovementMouse}",
                new Vector2(10, 10),
                Color.White);

            // write collision information on the top right screen area
            spriteBatch.DrawString(this._spriteManager.Font,
                $"Collision: {this._player.Collisions.Count}",
                new Vector2(10, 30),
                Color.White);

            
        }

    }
}