using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private readonly WorldMap _worldMap = new();
        
        
        private readonly InputManager _input;

        public MapManager(Game game) :base(game)
        {
            this._texturesTilemap = new TexturesTilemap(game);
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._camera = game.GetComponent<CameraManager>();
            this._input = game.GetComponent<InputManager>();
            
            NewMapEvent += this.NewMapReset;
            LoadMapFromFileEvent += this.LoadMapFromFile;
            SaveMapToFileEvent += this.SaveMapToFile;
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
            
            this._worldMap.DrawAllLevels(this._spriteBatch, this._texturesTilemap);
            
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

            if (!this._worldMap.ValidTileNumber((int)SelectedTilemapPart, MapEditorMenu.TilemapLevel))
            {
                return;
            }
            
            this._spriteBatch.Draw(
                this._texturesTilemap.Texture, 
                result.Position, 
                this._texturesTilemap.GetSprite(MapEditorMenu.TilemapLevel, SelectedTilemapPart),
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

            if(this._worldMap.TryTilemap(MapEditorMenu.TilemapLevel, x, y, out var result))
            {
                return result;
            }
            
            return null;
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
        
        private delegate void NewMapDelegateEventHandler();
        private static event NewMapDelegateEventHandler NewMapEvent;
        private delegate void SaveMapToFileDelegateEventHandler();
        private static event SaveMapToFileDelegateEventHandler SaveMapToFileEvent;
        private delegate void LoadMapFromFileDelegateEventHandler();
        private static event LoadMapFromFileDelegateEventHandler LoadMapFromFileEvent;

        
        public static void NewMap()
        {
            NewMapEvent?.Invoke();
        }
        public static void SaveMap()
        {
            SaveMapToFileEvent?.Invoke();
        }
        public static void LoadMap()
        {
            LoadMapFromFileEvent?.Invoke();
        }
        
        private readonly string _mapFile = $"{Environment.CurrentDirectory}/map.txt";
        
        private void NewMapReset()
        {
            foreach (var worldMapLevel in this._worldMap.WorldMapLevels.Values)
            {
                for (var y = 0; y < worldMapLevel.Map.Length; y++)
                {
                    for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                    {
                        worldMapLevel.Map[y][x].UpdateTilemapPart(TilemapPart.None);
                    }
                }
            }
        }
        
        private void SaveMapToFile()
        {
            using var file = new StreamWriter(this._mapFile);
            var str = new StringBuilder();

            foreach (var worldMapLevel in this._worldMap.WorldMapLevels.Values)
            {
                foreach (var mapTile in worldMapLevel.Map)
                {
                    foreach (var tile in mapTile)
                    {
                        str.Append($"{worldMapLevel.LevelPart}:{tile.TilemapPart}:{tile.Position.X}:{tile.Position.Y}");
                        str.Append("; ");
                    }
                    str.AppendLine();
                }
            }
                
            file.Write(str.ToString());
        }

        private void LoadMapFromFile()
        {
            var lines = File.ReadAllLines(this._mapFile);

            //var dd = new Dictionary<>();
            
            // rows
            foreach (var line in lines)
            {
                // row --> Sidewalk:5; Sidewalk:5;...
                var parts = line.Split(";");
                foreach (var part in parts)
                {
                    // cell --> Sidewalk:5
                    var data = part.Split(":").Select(s => s.Trim()).ToArray();
                    
                    if(data.Length == 0 || string.IsNullOrEmpty(data[0])) continue;
                    
                    var levelPart = (LevelPart)Enum.Parse(typeof(LevelPart), data[0]);
                    var tilemapPart = (TilemapPart)int.Parse(data[1]);
                    
                    
                }
            }
            
            // for (var y = 0; y < this._worldMap.WorldMapLevels[levelPart].Map.Length; y++)
            // {
            //     for (var x = 0; x < this._worldMap.WorldMapLevels[levelPart].Map[y].Length; x++)
            //     {
            //         this._worldMap.WorldMapLevels[levelPart].Map[y][x].UpdateTilemapPart(tilemapPart);
            //     }
            // }
        }

    }
}