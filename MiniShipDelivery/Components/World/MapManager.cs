using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Character;
using System.Collections.Generic;
using MiniShipDelivery.Components.Assets.Textures;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World
{
    public class MapManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly TexturesTilemap _texturesTilemap;
        
        private readonly OrthographicCamera _camera;

        private readonly int[][] _map =
        [
            [13, 14, 14, 14, 15, 0, 0, 0, 0, 22, 23, 0, 0, 26, 27],
            [16, 17, 17, 17, 18, 0, 0, 0, 0, 24, 25, 0, 0, 28, 29],
            [19, 20, 20, 20, 21],
            [0,],
            [30, 31, 32],
            [0,],
            [33],
        ];

        public MapManager(Game game, OrthographicCamera camera) :base(game)
        {
            this._texturesTilemap = new TexturesTilemap(game);
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._camera = camera;
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.Begin(
                transformMatrix: this._camera.GetViewMatrix(),
                samplerState: SamplerState.PointClamp);
            
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
    }
}