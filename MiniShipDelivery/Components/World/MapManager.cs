using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Tilemap;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.World
{
    internal class MapManager : ITilemapProperties
    {
        private AssetManager _spriteManager;

        private int[][] _map = new int[][]
        {
            new int[] { 13, 14, 14, 14, 15 },
            new int[] { 16, 17, 17, 17, 18 },
            new int[] { 19, 20, 20, 20, 21 },
        };

        public MapManager(AssetManager spriteManager)
        {
            this._spriteManager = spriteManager;

            this.Tilemaps = new Dictionary<TilemapPart, Rectangle>
            {
                { TilemapPart.RoomGray_TopLeft, new Rectangle(16 * 0 + 0, 16 * 3 + 3, 16, 16) },
                { TilemapPart.RoomGray_TopMiddle, new Rectangle(16 * 1 + 1, 16 * 3 + 3, 16, 16) },
                { TilemapPart.RoomGray_TopRight, new Rectangle(16 * 2 + 2, 16 * 3 + 3, 16, 16) },
                { TilemapPart.RoomGray_MiddleLeft, new Rectangle(16 * 0 + 0, 16 * 4 + 4, 16, 16) },
                { TilemapPart.RoomGray_MiddleMiddle, new Rectangle(16 * 1 + 1, 16 * 4 + 4, 16, 16) },
                { TilemapPart.RoomGray_MiddleRight, new Rectangle(16 * 2 + 2, 16 * 4 + 4, 16, 16) },
                { TilemapPart.RoomGray_DownLeft, new Rectangle(16 * 0 + 0, 16 * 5 + 5, 16, 16) },
                { TilemapPart.RoomGray_DownMiddle, new Rectangle(16 * 1 + 1, 16 * 5 + 5, 16, 16) },
                { TilemapPart.RoomGray_DownRight, new Rectangle(16 * 2 + 2, 16 * 5 + 5, 16, 16) },
            };
        }

        public IDictionary<TilemapPart, Rectangle> Tilemaps { get; private set; }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (var y = 0; y < this._map.Length; y++)
            {
                for (var x = 0; x < this._map[y].Length; x++)
                {
                    this._spriteManager.Draw(spriteBatch, new Vector2(x * 16, y * 16), (TilemapPart)this._map[y][x], this);
                }
            }
        }
    }
}