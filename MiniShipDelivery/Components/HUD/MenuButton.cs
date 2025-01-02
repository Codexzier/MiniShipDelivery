using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD;

public class MenuButton
{
    private readonly InputManager _input;
    private readonly OrthographicCamera _camera;
    private readonly Vector2 _position;
    private readonly Func<int, int, int, Vector2> _getPositionArea;
    private readonly Func<Vector2, SizeF, bool> _isMouseInRange;

    public MenuButton(
        InputManager input, 
        OrthographicCamera camera, 
        Vector2 position,
        Func<int, int, int, Vector2> getPositionArea, 
        Func<Vector2, SizeF, bool> isMouseInRange)
    {
        this._input = input;
        this._camera = camera;
        this._position = position;
        this._getPositionArea = getPositionArea;
        this._isMouseInRange = isMouseInRange;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this._camera.Position + 
                  this._position;
        
        var inRange = this._isMouseInRange(
            this._position + new Vector2(1,1), 
            new SizeF(64, 16));
        
        if (inRange)
        {
            if (this._input.GetMouseLeftButtonReleasedState())
            {
                this.ButtonAreaWasPressedEvent?.Invoke();
            }
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.DrawRectangle(
            pos,
            new SizeF(66, 18),
            isInRangeColor);
        
        spriteBatch.DrawRectangle(
            pos + new Vector2(1, 1),
            new SizeF(64, 16),
            Color.Aquamarine);
    }
    
    public delegate void ButtonAreaWasPressedEventHandler();
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;
}