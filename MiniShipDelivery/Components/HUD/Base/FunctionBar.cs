using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public class FunctionBar
{
    private readonly InputManager _input;
    private readonly OrthographicCamera _camera;
    private readonly Vector2 _sideMenuMapTilePositionStart;
    private readonly Size _size;
    private readonly Func<int, int, int, Vector2> _funcGetPositionArea;
    private readonly Func<Vector2, SizeF, bool> _isMouseInRange;
    private readonly List<FunctionItem> _functionItems = new();

    public FunctionBar(
        InputManager input, 
        OrthographicCamera camera, 
        Vector2 startPosition,
        Size size,
        Func<int, int, int, Vector2> funcGetPositionArea,
        Func<Vector2, SizeF, bool> isMouseInRange)
    {
        this._input = input;
        this._camera = camera;
        this._sideMenuMapTilePositionStart = startPosition;
        this._size = size;
        this._funcGetPositionArea = funcGetPositionArea;
        this._isMouseInRange = isMouseInRange;
    }

    public void FillOptions<TAssertPart>(int columns) where TAssertPart : Enum
    {
        foreach (var valPart in Enum.GetValues(typeof(TAssertPart)))
        {
            if((int)valPart == 0) continue;
            this.AddFunctionItem(valPart, columns);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var item in this._functionItems)
        {
            this.DrawSelectableArea(spriteBatch, item);
        }
    }
    
    private void DrawSelectableArea(SpriteBatch spriteBatch, FunctionItem item)
    {
        var pos = this._camera.Position + 
                  item.Position + 
                  this._sideMenuMapTilePositionStart;
            
        var positionSelectable = item.Position +
                                 this._sideMenuMapTilePositionStart;
            
        var inRange = this._isMouseInRange(positionSelectable, item.Size);
        if (inRange)
        {
            if (this._input.GetMouseLeftButtonReleasedState(positionSelectable, item.Size, UiMenuMainPart.None))
            {
                this.ButtonAreaWasPressedEvent?.Invoke(item);
            }
        }

        this.ButtonAreaHasExecutedEvent?.Invoke(
            spriteBatch,
            inRange,
            pos,
            item);
    }
    
    private void AddFunctionItem(object part, int columns)
    {
        this._functionItems.Add(
            new FunctionItem(
                this._funcGetPositionArea(this._functionItems.Count, this._size.Width, columns),
                new SizeF(18, 18),
                part));
    }

    public delegate void ButtonAreaWasPressedEventHandler(FunctionItem functionItem);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;

    public delegate void ButtonAreaHasExecutedEventHandler(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem);
    public event ButtonAreaHasExecutedEventHandler ButtonAreaHasExecutedEvent;

    public void ResetAllSelected(FunctionItem functionitem)
    {
        foreach (var item in this._functionItems.Where(w => !w.Equals(functionitem)))
        {
            item.Selected = false;
        }
    }
}