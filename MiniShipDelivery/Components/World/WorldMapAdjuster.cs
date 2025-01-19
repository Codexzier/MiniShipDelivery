using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World;

public class WorldMapAdjuster(Game game, WorldMap map)
{
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();
    private readonly InputManager _input = game.GetComponent<InputManager>();

    private MapTile CurrentMapTile { get; set; }
    public static MapLayer SelectedMapMapLayer { get; set; } = MapLayer.Sidewalk;

    public static int SelectedNumberPart { get; set; }
        
    public void UpdateSetMapTile()
    {
        this.UpdateCurrentSelectableMapTile();
        if(this.CurrentMapTile == null) return;
            
        var rePosition = this.CurrentMapTile.Position.TilePositionToVector() - this._camera.Camera.Position ;
            
        if (this._input.GetMouseLeftButtonReleasedState(
                rePosition, 
                new SizeF(16, 16)))
        {
            this.CurrentMapTile.AssetNumber = SelectedNumberPart;
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

        if (!map.TryTilemap(SelectedMapMapLayer, x, y, out var result)) return;
            
        this.CurrentMapTile = result;
    }
    
    public void Draw(
        SpriteBatch spriteBatch)
    {
        if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            
        WorldManagerHelper.DrawGrid(spriteBatch, this._camera.Camera.Position);
            
        if(SelectedNumberPart == 0) return;

        this.DrawSelectedMapTile(spriteBatch);
        this.DrawHoverEffectOnGrid(spriteBatch);
    }

    private void DrawSelectedMapTile(
        SpriteBatch spriteBatch)
    {
        if(this.CurrentMapTile == null) return;

        if (!map.ValidTileNumber(SelectedNumberPart, SelectedMapMapLayer))
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