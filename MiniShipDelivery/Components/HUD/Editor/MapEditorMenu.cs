using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using MiniShipDelivery.Components.HUD.Base;

namespace MiniShipDelivery.Components.HUD.Editor
{
    public class MapEditorMenu : BaseMenu
    {
    private const int MenuWidth = 60;

    private readonly Vector2 _sideMenuMapOptionPosition = new(0, 0);
    private readonly Vector2 _sideMenuMapTilePositionStart = new(0, 20);

    private readonly List<ISelectableAreItem<TilemapPart>> _selectableMapItems = new();
    private readonly List<MapEditorItem> _mapOptionItems = new();

    public MapEditorMenu(
        AssetManager assetManager,
        InputManager input,
        OrthographicCamera camera,
        int screenWidth,
        int screenHeight) : base(assetManager,
        input,
        camera,
        screenWidth,
        screenHeight,
        new Vector2(screenWidth - MenuWidth, 20),
        new Size(MenuWidth, screenHeight - 20))
    {
        // tile map option
        this.AddMapoption(MapEditorOption.Deselect);
        this.AddMapoption(MapEditorOption.OnOffGrid);
        this.AddMapoption(MapEditorOption.Remove);

        // tile map
        foreach (var tilemapPart in Enum.GetValues<TilemapPart>())
        {
            if (tilemapPart == TilemapPart.None) continue;
            this.AddSelectableMapItem(tilemapPart);
        }
    }

    public bool ShowGrid { get; private set; }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        // map option
        foreach (var item in this._mapOptionItems)
        {
            this.DrawMapOption(spriteBatch, item);
        }

        // map sprites
        foreach (var item in this._selectableMapItems)
        {
            this.DrawSelectableMapPart(spriteBatch, item);
        }
    }

    private void AddMapoption(MapEditorOption option)
    {
        this._mapOptionItems.Add(new MapEditorItem(
            this.GetPositionArea(this._mapOptionItems.Count, MenuWidth, 3),
            new SizeF(16, 16),
            option));
    }

    private void AddSelectableMapItem(TilemapPart item)
    {
        this._selectableMapItems.Add(
            new SelectableAreaItem(
                this.GetPositionArea(this._selectableMapItems.Count, MenuWidth, 3), 
                new SizeF(18, 18), 
                item)
            );
    }

    private void DrawMapOption(SpriteBatch spriteBatch, MapEditorItem item)
    {
        var pos = this._camera.Position + item.Position + this._sideMenuMapOptionPosition;
        
        var inRange = this.IsMouseInRange(item.Position, item.Size);
        
        if (inRange)
        {
            if (this._input.GetMouseLeftButtonReleasedState())
            {
                switch (item.MapEditorOption)
                {
                    case MapEditorOption.Deselect:
                        this.ResetAllSelected<TilemapPart>();
                        break;
                    case MapEditorOption.OnOffGrid:
                        this.ShowGrid = !this.ShowGrid;
                        break;
                    case MapEditorOption.Remove:
                        break;
                }
            }
        }

        var isInRangeColor = this.BoolToColor(inRange);
        
        if (this.ShowGrid && item.MapEditorOption == MapEditorOption.OnOffGrid)
        {
            isInRangeColor = Color.Yellow;
        }

        spriteBatch.DrawRectangle(
            pos,
            item.Size,
            isInRangeColor);

        switch (item.MapEditorOption)
        {
            case MapEditorOption.Deselect:
                this._assetManager.Draw(
                    spriteBatch,
                    pos,
                    InterfacePart16x16.Arrow_Type1);
                break;
            case MapEditorOption.OnOffGrid:
                this._assetManager.Draw(
                    spriteBatch,
                    pos,
                    InterfacePart16x16.Arrow_Type2);
                break;
            case MapEditorOption.Remove:
                this._assetManager.Draw(
                    spriteBatch,
                    pos,
                    InterfacePart16x16.Arrow_Type3);
                break;
        }
    }

    private void DrawSelectableMapPart<TAssetPart>(
        SpriteBatch spriteBatch,
        ISelectableAreItem<TAssetPart> item) where TAssetPart : Enum
    {
        var pos = this._camera.Position +
                  item.Position +
                  this._sideMenuMapTilePositionStart;
        var posSelectable = item.Position +
                            this._sideMenuMapTilePositionStart;

        var inRange = this.IsMouseInRange(posSelectable, item.Size);

        if (inRange)
        {
            if (this._input.GetMouseLeftButtonReleasedState())
            {
                item.Selected = true;

                this.ResetAllSelected(item);
            }
        }

        var isInRangeColor = this.BoolToColor(inRange);

        if (item.Selected)
        {
            isInRangeColor = Color.Yellow;
        }

        spriteBatch.DrawRectangle(
            pos,
            item.Size,
            isInRangeColor);

        this._assetManager.Draw(spriteBatch,
            pos + new Vector2(1, 1),
            item.AssetPart);

        // Used only for debug
        //spriteBatch.WriteLine(this._spriteManager.Font,
        //    $"{item.TilemapPart}",
        //    pos + new Vector2(-40, 1));
    }

    /// <summary>
    /// Reset all selected items.
    /// </summary>
    private void ResetAllSelected<TAssetPart>(
        ISelectableAreItem<TAssetPart> item = null) where TAssetPart : Enum
    {
        foreach (var mapItem in this._selectableMapItems.Where(smi => !smi.Equals(item)))
        {
            mapItem.Selected = false;
        }
    }
    }
}
