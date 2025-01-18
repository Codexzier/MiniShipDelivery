using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Controls;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public class FunctionBar(
    Game game,
    Vector2 position,
    Vector2 startPosition,
    Size size,
    Action<SpriteBatch, Vector2, FunctionItem> drawButton,
    Func<FunctionItem, Color, Color> changeColorForActive)
{
    private readonly InputManager _input = game.GetComponent<InputManager>();
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();

    /// <summary>
    /// I use the dictionary for paging.
    /// </summary>
    private readonly Dictionary<int, List<FunctionItem>> _functionItems = new()
    {
        { 0, new List<FunctionItem>() }
    };

    private int _maxPerPage = 18;
    private int _indexForPaging = 0;

    public void FillOptions<TAssertPart>(int columns) where TAssertPart : Enum
    {
        var rows = (int)(size.Height - startPosition.Y) / 18;
        this._maxPerPage = rows * columns;
        
        foreach (var valPart in Enum.GetValues(typeof(TAssertPart)))
        {
            if((int)valPart == 0) continue;
            this.AddFunctionItem(valPart, columns);
        }
    }

    public void ManuelOptions<TAssertPart>(List<TAssertPart> parts, int columns) where TAssertPart : Enum
    {
        var rows = (int)(size.Height - startPosition.Y) / 18;
        this._maxPerPage = rows * columns;

        foreach (var valPart in parts)
        {
            this.AddFunctionItem(valPart, columns);
        }
    }

    public void PageUp()
    {
        if (this._indexForPaging >= this._functionItems.Count - 1)
        {
            this._indexForPaging = this._functionItems.Count - 1;
            return;
        }
        
        this._indexForPaging++;
    }

    public void PageDown()
    {
        if (this._indexForPaging <= 0)
        {
            this._indexForPaging = 0;
            return;
        }
        
        this._indexForPaging--;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var item in this._functionItems[this._indexForPaging])
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
            
        var inRange = HudHelper.IsMouseInRange(positionSelectable, item.Size);
        if (inRange)
        {
            if (this._input.GetMouseLeftButtonReleasedState(
                    positionSelectable, 
                    item.Size))
            {
                this.ButtonAreaWasPressedEvent?.Invoke(item);
            }
        }

        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        isInRangeColor = changeColorForActive(item, isInRangeColor);
        
        spriteBatch.DrawRectangle(
            pos,
            new SizeF(18, 18),
            isInRangeColor);
        
        drawButton?.Invoke(spriteBatch, pos, item);
    }
    
    private void AddFunctionItem(object part, int columns)
    {
        var index = this._functionItems.Count - 1;
        if (this._functionItems[index].Count >= this._maxPerPage)
        {
            this._functionItems.Add(index + 1, new List<FunctionItem>());
            index = this._functionItems.Count - 1;
        }
        
        this._functionItems[index].Add(
            new FunctionItem(
                HudHelper.GetPositionArea(position.Y, this._functionItems[index].Count, size.Width, columns),
                new SizeF(18, 18),
                part));
    }
    
    /// <summary>
    /// Default for max 20 columns
    /// </summary>
    /// <param name="part"></param>
    public void AddOption(object part)
    {
        this.AddFunctionItem(part, 20);
    }

    public delegate void ButtonAreaWasPressedEventHandler(FunctionItem functionItem);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;


    public void ResetAllSelected(FunctionItem functionItem)
    {
        foreach (var keyValuePair in this._functionItems)
        {
            foreach (var item in keyValuePair.Value.Where(w => !w.Equals(functionItem)))
            {
                item.Selected = false;
            }
        }
    }

    public void SetOption(UiMenuMapOptionPart uiMenuMapOptionPart)
    {
        
    }
}