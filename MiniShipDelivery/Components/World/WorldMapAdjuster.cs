using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Editor;
using MiniShipDelivery.Components.World.Textures;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World;

public class WorldMapAdjuster(Game game, WorldMap map)
{
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();
    private readonly InputManager _input = game.GetComponent<InputManager>();
        
    public MapTile CurrentMapTile { get; set; }
    public static LevelPart SelectedMapLayer { get; set; }

    public static int SelectedTilemapPart { get; set; }
        
    public void UpdateSetMapTile()
    {
        this.UpdateCurrentSelectableMapTile();
        if(this.CurrentMapTile == null) return;
            
        var rePosition = this.CurrentMapTile.Position.TilePositionToVector() - this._camera.Camera.Position ;
            
        if (this._input.GetMouseLeftButtonReleasedState(
                rePosition, 
                new SizeF(16, 16)))
        {
            this.CurrentMapTile.NumberPart = SelectedTilemapPart;
        }
    }
    
    public void UpdateCurrentSelectableMapTile()
    {
        this.CurrentMapTile = null;
        if(HudManager.MouseIsOverMenu) return;
            
        var pos = this._input.Inputs.MousePosition;
        pos += this._camera.Camera.Position;
            
        // Example
        // -------------------
        // |  1  |  2  |  3  |
        // |  4  |  5  |  6  |
        // |  7  |  8  |  9  |
        // -------------------
            
        // pos is bei x = 20, y = 10
        // so it must be field 2
        // 20 / 16 = 1 for x
        // 10 / 16 = 0 for y

        var x = (int)pos.X / 16;
        var y = (int)pos.Y / 16;

        if (!map.TryTilemap(MapEditorMenu.TilemapLevel, x, y, out var result)) return;
            
        this.CurrentMapTile = result;
    }
    
    public void Draw(
        SpriteBatch spriteBatch,
        WorldMapTextures worldMapTextures)
    {
        if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            
        WorldManagerHelper.DrawGrid(spriteBatch, this._camera.Camera.Position);
            
        if(SelectedTilemapPart == 0) return;

        this.DrawSelectedMapTile(
            spriteBatch,
            worldMapTextures);
        this.DrawHoverEffectOnGrid(spriteBatch);
    }

    private void DrawSelectedMapTile(
        SpriteBatch spriteBatch,
        WorldMapTextures worldMapTextures)
    {
        if(this.CurrentMapTile == null) return;

        if (!map.ValidTileNumber(SelectedTilemapPart, MapEditorMenu.TilemapLevel))
        {
            return;
        }

        if (this.CurrentMapTile.NumberPart == SelectedTilemapPart)
        {
            return;
        }
            
        spriteBatch.Draw(
            worldMapTextures.TexturesTilemap.Texture, 
            this.CurrentMapTile.Position.TilePositionToVector(), 
            worldMapTextures.TexturesTilemap.GetSprite(
                MapEditorMenu.TilemapLevel, 
                (TilemapPart)SelectedTilemapPart),
            new Color(Color.Gray, 0.8f));
    }

    private void DrawHoverEffectOnGrid(SpriteBatch spriteBatch)
    {
        if(this.CurrentMapTile == null) return;
            
        spriteBatch.DrawRectangle(
            this.CurrentMapTile.Position.TilePositionToVector(),
            new SizeF(16, 16),
            Color.White);
    }
}