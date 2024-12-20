using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Character;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.World
{
    internal class MapManager
    {
        private AssetManager _spriteManager;
        private readonly CharacterPlayer _player;
        private readonly List<CharacterNpc> _characterNPCs;
        private int[][] _map = new int[][]
        {
            new int[] { 13, 14, 14, 14, 15, 0, 0, 0, 0, 22, 23, 0, 0, 26, 27 },
            new int[] { 16, 17, 17, 17, 18, 0, 0, 0, 0, 24, 25, 0, 0, 28, 29 },
            new int[] { 19, 20, 20, 20, 21 },
            new int[] { 0, },
            new int[] { 30, 31, 32 },
            new int[] { 0, },
            new int[] { 33 },
        };

        public MapManager(AssetManager spriteManager, CharacterPlayer player, List<CharacterNpc> characterNPCs)
        {
            this._spriteManager = spriteManager;
            this._player = player;
            this._characterNPCs = characterNPCs;
        }

        internal void Update(GameTime gameTime)
        {
            var relativePosition = this._player.Position;

            foreach (var character in this._characterNPCs)
            {
                character.Collider.Position = character.Position - relativePosition;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var relativePosition = this._player.Position;

            for (var y = 0; y < this._map.Length; y++)
            {
                for (var x = 0; x < this._map[y].Length; x++)
                {
                    this._spriteManager.Draw(spriteBatch,
                        new Vector2((x * 16) - relativePosition.X, (y * 16) - relativePosition.Y),
                        (TilemapPart)this._map[y][x], 
                        this._spriteManager.TilemapPack);
                }
            }
        }

    }
}