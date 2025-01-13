using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Controls;
using MiniShipDelivery.Components.HUD.Editor;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World
{
    public class WorldManager(Game game) : DrawableGameComponent(game)
    {
        private readonly SpriteBatch _spriteBatch = new( game.GraphicsDevice );
        private readonly TexturesTilemap _texturesTilemap = new(game);
        
        private readonly CameraManager _camera = game.GetComponent<CameraManager>();

        public readonly WorldMap Map = new();
        
        
        private readonly InputManager _input = game.GetComponent<InputManager>();
        
        private MapTile _currentMapTile;

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
            this.UpdateCurrentSelectableMapTile();
            if(this._currentMapTile == null) return;
            
            var rePosition = this._currentMapTile.Position.TilePositionToVector() - this._camera.Camera.Position ;
            
            if (this._input.GetMouseLeftButtonReleasedState(
                    rePosition, 
                    new SizeF(16, 16), 
                    UiMenuMainPart.None))
            {
                this._currentMapTile.TilemapPart = (int)SelectedTilemapPart;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            this.Map.DrawAllLevels(this._spriteBatch, this._texturesTilemap);
            
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
            if(this._currentMapTile == null) return;

            if (!this.Map.ValidTileNumber((int)SelectedTilemapPart, MapEditorMenu.TilemapLevel))
            {
                return;
            }

            if (this._currentMapTile.TilemapPart == (int)SelectedTilemapPart)
            {
                return;
            }
            
            this._spriteBatch.Draw(
                this._texturesTilemap.Texture, 
                _currentMapTile.Position.TilePositionToVector(), 
                this._texturesTilemap.GetSprite(MapEditorMenu.TilemapLevel, SelectedTilemapPart),
                new Color(Color.Gray, 0.8f));
        }

        private void DrawHoverEffectOnGrid()
        {
            if(this._currentMapTile == null) return;
            
            this._spriteBatch.DrawRectangle(
                this._currentMapTile.Position.TilePositionToVector(),
                new SizeF(16, 16),
                Color.White);
        }

        private void UpdateCurrentSelectableMapTile()
        {
            this._currentMapTile = null;
            if(HudManager.MouseIsOverMenu) return;
            
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

            if (!this.Map.TryTilemap(MapEditorMenu.TilemapLevel, x, y, out var result)) return;
            
            this._currentMapTile = result;
        }

        private void DrawGrid()
        {
            if (!GlobaleGameParameters.ShowGrid) return;
            
            var pos = this._camera.Camera.Position;

            const int maxY = 5;
            const int maxX = 5;
            
            var posX = ((int)pos.X / 16) * 16 + (maxX * 16) + (maxX * 16 / 2) - 8;
            var posY = ((int)pos.Y / 16) * 16 + (maxY * 16 / 2) + 8;

            for (var iY = 0; iY < maxY; iY++)
            {
                for (var iX = 0; iX < maxX; iX++)
                {
                    this._spriteBatch.DrawRectangle(
                        new Vector2(iX * 16 + posX, iY * 16 + posY),
                        new SizeF(16.5f, 16.5f),
                        Color.Gray,
                        .5f);
                }
            }
        }
    }
}