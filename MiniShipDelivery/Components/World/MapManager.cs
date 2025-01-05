using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Character;
using System.Collections.Generic;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World
{
    public class MapManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly TexturesTilemap _texturesTilemap;
        
        private readonly CameraManager _camera;

        private readonly int[][] _map =
        [
            [13, 14, 14, 14, 15, 0, 0, 0, 0, 22, 23, 0, 0, 26, 27],
            [16, 17, 17, 17, 18, 0, 0, 0, 0, 24, 25, 0, 0, 28, 29],
            [19, 20, 20, 20, 21, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 13, 14, 14, 14, 15],
            [30, 31, 32, 0, 0, 0, 0, 0,16, 17, 17, 17, 18, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 19, 20, 20, 20, 21, 0, 0],
            [0, 0, 33, 0, 0, 0, 0],
            [33],
        ];

        public MapManager(Game game) :base(game)
        {
            this._texturesTilemap = new TexturesTilemap(game);
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._camera = game.GetComponent<CameraManager>();
        }
        
        public static bool ShowGrid { get; set; }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            this.DrawGrid();
            
            for (var y = 0; y < this._map.Length; y++)
            {
                for (var x = 0; x < this._map[y].Length; x++)
                {
                    this._spriteBatch.Draw(
                        this._texturesTilemap.Texture, 
                        new Vector2(x * 16, y * 16), 
                        this._texturesTilemap.SpriteContent[(TilemapPart)this._map[y][x]],
                        Color.White);
                }
            }
            
            this._spriteBatch.End();
        }
        
        private void DrawGrid()
        {
            if (!ShowGrid) return;

            const int maxY = GlobaleGameParameters.ScreenHeight / 16;
            const int maxX = GlobaleGameParameters.ScreenWidth / 16;
            for (var iY = 0; iY < maxY; iY++)
            {
                for (var iX = 0; iX < maxX; iX++)
                {
                    this._spriteBatch.DrawRectangle(
                        new Vector2(iX * 16, iY * 16),
                        new SizeF(16.5f, 16.5f),
                        Color.Gray,
                        .5f);
                }
            }
        }
    }
}