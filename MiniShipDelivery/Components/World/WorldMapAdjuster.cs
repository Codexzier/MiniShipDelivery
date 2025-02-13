using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World;

public class WorldMapAdjuster
{
    private readonly WorldMap _map;

    private ApplicationBus Bus => ApplicationBus.Instance;

    public WorldMapAdjuster(WorldMap map)
    {
        this._map = map;
        
        SelectedMapMapLayer = map.WorldMapChunks[this.Bus.MapChunkIndex].WorldMapLayers[0].MapLayer;
        SelectedNumberPart = -1;
    }

    private MapTile CurrentMapTile { get; set; }
    public static MapLayer SelectedMapMapLayer { get; set; }

    public static int SelectedNumberPart { get; set; }
    public static bool SelectedDrawTop { get; set; }
        
    public void UpdateSetMapTile()
    {
        if(this.Bus.TextMessage.IsOn) return;
        
        this.UpdateCurrentSelectableMapTile();
        if(this.CurrentMapTile == null) return;
            
        var rePosition = this.CurrentMapTile.Position.TilePositionToVector() - this.Bus.Camera.GetPosition() ;
            
        rePosition += this.Bus.MapChunkPosition;
        if (this.Bus.Inputs.GetMouseButtonReleasedStateLeft(
                rePosition, 
                new SizeF(16, 16), "set tile"))
        {
            this.CurrentMapTile.AssetNumber = SelectedNumberPart;
            this.CurrentMapTile.DrawTop = SelectedDrawTop;
        }
        
        if (this.Bus.Inputs.GetMouseButtonReleasedStateRight(
                rePosition, 
                new SizeF(16, 16), "remove tile"))
        {
            this.CurrentMapTile.AssetNumber = 0;
        }
    }

    private void UpdateCurrentSelectableMapTile()
    {
        this.CurrentMapTile = null;
        if(HudManager.MouseIsOverMenu) return;
            
        var pos = this.Bus.Inputs.MousePosition;
        pos += this.Bus.Camera.GetPosition();
        pos -= ApplicationBus.Instance.MapChunkPosition;
            
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

        if (!this._map.TryTilemap(this.Bus.MapChunkIndex, SelectedMapMapLayer, x, y, out var result)) return;
            
        this.CurrentMapTile = result;
    }
    
    public void Draw(
        SpriteBatch spriteBatch)
    {
        if(GlobalGameParameters.HudView != HudOptionView.MapEditor) return;
            
        WorldManagerHelper.DrawGrid(spriteBatch, this.Bus.Camera.GetPosition());
            
        if(SelectedNumberPart < 0) return;

        this.DrawSelectedMapTile(spriteBatch);
        this.DrawHoverEffectOnGrid(spriteBatch);
    }

    private void DrawSelectedMapTile(
        SpriteBatch spriteBatch)
    {
        if(this.CurrentMapTile == null) return;

        if (!this._map.ValidTileNumber(this.Bus.MapChunkIndex, SelectedNumberPart, SelectedMapMapLayer))
        {
            return;
        }

        if (this.CurrentMapTile.AssetNumber == SelectedNumberPart)
        {
            return;
        }
        
        spriteBatch.DrawWithTransparency(
            this.CurrentMapTile.Position.TilePositionToVector() + this.Bus.MapChunkPosition,
            SelectedMapMapLayer,
            SelectedNumberPart);
    }

    private void DrawHoverEffectOnGrid(SpriteBatch spriteBatch)
    {
        if(this.CurrentMapTile == null) return;
            
        spriteBatch.DrawRectangle(
            this.CurrentMapTile.Position.TilePositionToVector()  + this.Bus.MapChunkPosition,
            new SizeF(16, 16),
            Color.White);
    }
}