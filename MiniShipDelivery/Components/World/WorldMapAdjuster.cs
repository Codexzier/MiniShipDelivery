using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World;

public class WorldMapAdjuster
{
    private readonly CameraManager _camera;
    private readonly InputManager _input;
    private readonly WorldMap _map;

    public WorldMapAdjuster(Game game, WorldMap map)
    {
        this._map = map;
        this._camera = game.GetComponent<CameraManager>();
        this._input = game.GetComponent<InputManager>();
        
        SelectedMapMapLayer = map.WorldMapChunk.WorldMapLayers[0].MapLayer;
        SelectedNumberPart = -1;
    }

    private MapTile CurrentMapTile { get; set; }
    public static MapLayer SelectedMapMapLayer { get; set; }

    public static int SelectedNumberPart { get; set; }
    public static bool SelectedDrawTop { get; set; }
        
    public void UpdateSetMapTile()
    {
        if(GlobaleGameParameters.SystemDialogBox) return;
        
        this.UpdateCurrentSelectableMapTile();
        if(this.CurrentMapTile == null) return;
            
        var rePosition = this.CurrentMapTile.Position.TilePositionToVector() - this._camera.Camera.Position ;
            
        if (this._input.GetMouseLeftButtonReleasedState(
                rePosition, 
                new SizeF(16, 16)))
        {
            this.CurrentMapTile.AssetNumber = SelectedNumberPart;
            this.CurrentMapTile.DrawTop = SelectedDrawTop;
        }
        
        if (this._input.GetMouseRightButtonReleasedState(
                rePosition, 
                new SizeF(16, 16)))
        {
            this.CurrentMapTile.AssetNumber = 0;
        }
    }

    private void UpdateCurrentSelectableMapTile()
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

        if (!this._map.TryTilemap(SelectedMapMapLayer, x, y, out var result)) return;
            
        this.CurrentMapTile = result;
    }
    
    public void Draw(
        SpriteBatch spriteBatch)
    {
        if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            
        WorldManagerHelper.DrawGrid(spriteBatch, this._camera.Camera.Position);
            
        if(SelectedNumberPart < 0) return;

        this.DrawSelectedMapTile(spriteBatch);
        this.DrawHoverEffectOnGrid(spriteBatch);
    }

    private void DrawSelectedMapTile(
        SpriteBatch spriteBatch)
    {
        if(this.CurrentMapTile == null) return;

        if (!this._map.ValidTileNumber(SelectedNumberPart, SelectedMapMapLayer))
        {
            return;
        }

        if (this.CurrentMapTile.AssetNumber == SelectedNumberPart)
        {
            return;
        }
         
        spriteBatch.DrawWithTransparency(
            this.CurrentMapTile.Position.TilePositionToVector(),
            SelectedMapMapLayer,
            SelectedNumberPart);
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