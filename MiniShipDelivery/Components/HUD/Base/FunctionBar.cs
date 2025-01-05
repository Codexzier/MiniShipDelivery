using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public class FunctionBar(
    Game game,
    Vector2 startPosition,
    Size size,
    Func<int, int, int, Vector2> funcGetPositionArea,
    Func<Vector2, SizeF, bool> isMouseInRange)
{
    private readonly InputManager _input = game.GetComponent<InputManager>();
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();
    private readonly List<FunctionItem> _functionItems = new();

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
        var pos = this._camera.Camera.Position + 
                  item.Position + 
                  startPosition;
            
        var positionSelectable = item.Position +
                                 startPosition;
            
        var inRange = isMouseInRange(positionSelectable, item.Size);
        if (inRange)
        {
            if (this._input.GetMouseLeftButtonReleasedState(positionSelectable, item.Size, UiMenuMainPart.None))
            {
                this.ButtonAreaWasPressedEvent?.Invoke(item);
            }
        }

        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        spriteBatch.DrawRectangle(
            pos,
            new SizeF(18, 18),
            isInRangeColor);
        
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
                funcGetPositionArea(this._functionItems.Count, size.Width, columns),
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