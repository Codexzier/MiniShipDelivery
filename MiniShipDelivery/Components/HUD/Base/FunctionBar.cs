using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public class FunctionBar<TOptionFunctionItem, TAssetPart> where TAssetPart : Enum
{
    private readonly Vector2 _sideMenuMapTilePositionStart;
    private readonly Size _size;
    private readonly Func<int, int, int, Vector2> _funcGetPositionArea;
    private readonly List<FunctionItem> _functionItems = new();

    public FunctionBar(
        Vector2 startPosition, 
        Size size, 
        Func<int, int, int, Vector2> funcGetPositionArea)
    {
        this._sideMenuMapTilePositionStart = startPosition;
        this._size = size;
        this._funcGetPositionArea = funcGetPositionArea;

        // foreach (var valPart in Enum.GetValues<TAssetPart>())
        // {
        //     if((int)valPart == 0) continue;
        //     
        //     this.AddFunctionItem(valPart);
        // }
    }
    
    private void AddFunctionItem<TAssetPart>(TAssetPart part) where TAssetPart : Enum
    {
        this._functionItems.Add(
            new FunctionItem(
                this._funcGetPositionArea(this._functionItems.Count, this._size.Width, 6),
                new SizeF(18, 18),
                part));
    }
}