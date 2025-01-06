using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Controls;
using MiniShipDelivery.Components.HUD.Editor;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World
{
    public class MapManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly TexturesTilemap _texturesTilemap;
        
        private readonly CameraManager _camera;

        private readonly MapTile[][] _map;
        private readonly IEnumerable<int> _listOfValidateTileNumbers;
        
        private readonly InputManager _input;

        public MapManager(Game game) :base(game)
        {
            this._texturesTilemap = new TexturesTilemap(game);
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._camera = game.GetComponent<CameraManager>();
            this._input = game.GetComponent<InputManager>();

            // collected validate tiles
            this._listOfValidateTileNumbers = Enum.GetValues<TilemapPart>().Select(s => (int)s);
            
            // y, x
            this._map = new MapTile[10][];
            for (int indexY = 0; indexY < 10; indexY++)
            {
                this._map[indexY] = new MapTile[20];
                for (int indexX = 0; indexX < 20; indexX++)
                {
                    this._map[indexY][indexX] = new MapTile(
                        TilemapPart.GrassAndBrick_MiddleMiddle,
                        new Vector2(indexX * 16, indexY * 16));
                }
            }
        }
        
        public static bool ShowGrid { get; set; }
        public static TilemapPart SelectedTilemapPart { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // HUD depended content
            if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            if(SelectedTilemapPart == TilemapPart.None) return;
            
            this.UpdateSetMapTile();
        }

        private void UpdateSetMapTile()
        {
            var result = this.GetMapTile();
            if(result == null) return;
            
            // not selectable map area, well the menu is there.
            var rePosition = result.Position - this._camera.Camera.Position ;
            var rect = new RectangleF(rePosition.X, rePosition.Y, 16, 16);
            foreach (var rectangle in MapEditorMenu.MenuField)
            {
                if (rectangle.Intersects(rect))
                {
                    return;
                }
            }
            
            if (this._input.GetMouseLeftButtonReleasedState(rePosition, new SizeF(16, 16), UiMenuMainPart.None))
            {
                result.UpdateTilemapPart(SelectedTilemapPart);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            for (var y = 0; y < this._map.Length; y++)
            {
                for (var x = 0; x < this._map[y].Length; x++)
                {
                    var tileNumber = this._map[y][x].TilemapPart;
                    if (!this._listOfValidateTileNumbers.Contains((int)tileNumber))
                    {
                        tileNumber = TilemapPart.GrassAndBrick_AroundOutBorder;
                    }
                    
                    this._spriteBatch.Draw(
                        this._texturesTilemap.Texture, 
                        this._map[y][x].Position, 
                        this._texturesTilemap.SpriteContent[tileNumber],
                        Color.White);
                }
            }
            
            this.HudDependedDrawContent();
            
            this._spriteBatch.End();
        }

        private void HudDependedDrawContent()
        {
            if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            
            this.DrawGrid();
            
            if(SelectedTilemapPart == TilemapPart.None) return;

            this.DrawSelectedMapTile();
            this.DrawHoverEffectOnGrid();
        }
        
        
        
        private void DrawSelectedMapTile()
        {
            var result = this.GetMapTile();
            if(result == null) return;
            
            if (!this._listOfValidateTileNumbers.Contains((int)SelectedTilemapPart))
            {
                return;
            }
            
            this._spriteBatch.Draw(
                this._texturesTilemap.Texture, 
                result.Position, 
                this._texturesTilemap.SpriteContent[SelectedTilemapPart],
                Color.White);
        }

        private void DrawHoverEffectOnGrid()
        {
            var result = this.GetMapTile();
            if(result == null) return;
            
            this._spriteBatch.DrawRectangle(
                result.Position,
                new SizeF(16, 16),
                Color.White);

        }

        private MapTile GetMapTile()
        {
            var pos = this._input.Inputs.MousePosition;
            pos += this._camera.Camera.Position;
            
            // Example
            // -------------------
            // |  1  |  2  |  3  |
            // |  4  |  5  |  6  |
            // |  7  |  8  |  9  |
            // -------------------
            
            // pos is bei x=20, y=10
            // so it must be field 2
            // 20 / 16 = 1 for x
            // 10 / 16 = 0 for y

            var x = (int)pos.X / 16;
            var y = (int)pos.Y / 16;

            if(x < 0 || y < 0 || y >= this._map.Length || x >= this._map[y].Length) return null;
            
            return this._map[y][x];
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