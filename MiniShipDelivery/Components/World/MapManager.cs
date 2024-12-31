using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Character;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.World
{
    public class MapManager : GameComponent
    {
        private AssetManager _spriteManager;
        private int[][] _map =
        [
            [13, 14, 14, 14, 15, 0, 0, 0, 0, 22, 23, 0, 0, 26, 27],
            [16, 17, 17, 17, 18, 0, 0, 0, 0, 24, 25, 0, 0, 28, 29],
            [19, 20, 20, 20, 21],
            [0,],
            [30, 31, 32],
            [0,],
            [33],
        ];

        public MapManager( Game game, AssetManager spriteManager) :base(game)
        {
            this._spriteManager = spriteManager;
        }

        internal void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var y = 0; y < this._map.Length; y++)
            {
                for (var x = 0; x < this._map[y].Length; x++)
                {
                    this._spriteManager.Draw(spriteBatch,
                        new Vector2(x * 16, y * 16),
                        (TilemapPart)this._map[y][x]);
                }
            }
        }

    }
}