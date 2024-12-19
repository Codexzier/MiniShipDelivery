using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.World
{
    internal class MapManager : ISpriteProperties<TilemapPart>
    {
        private AssetManager _spriteManager;
        private readonly CharacterPlayer _player;
        private readonly List<CharacterNpc> _characterNPCs;
        private int[][] _map = new int[][]
        {
            new int[] { 13, 14, 14, 14, 15 },
            new int[] { 16, 17, 17, 17, 18 },
            new int[] { 19, 20, 20, 20, 21 },
        };

        public MapManager(AssetManager spriteManager, CharacterPlayer player, List<CharacterNpc> characterNPCs)
        {
            this._spriteManager = spriteManager;
            this._player = player;
            this._characterNPCs = characterNPCs;

            this.SpriteContent = new Dictionary<TilemapPart, Rectangle>
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

        public IDictionary<TilemapPart, Rectangle> SpriteContent { get; private set; }


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
                        this);
                }
            }
        }

    }
}