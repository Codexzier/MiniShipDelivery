using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class MenuButton
{
    private readonly AssetManager _assetManager;
    private readonly InputManager _input;
    private readonly OrthographicCamera _camera;
    private readonly UiMenuMainPart _menuMainPart;
    private readonly Vector2 _position;
    private readonly Func<Vector2, SizeF, bool> _isMouseInRange;

    public MenuButton(
        AssetManager assetManager, 
        InputManager input,
        OrthographicCamera camera,
        UiMenuMainPart menuMainPart,
        Vector2 position,
        Func<Vector2, SizeF, bool> isMouseInRange)
    {
        this._assetManager = assetManager;
        this._input = input;
        this._camera = camera;
        this._menuMainPart = menuMainPart;
        this._position = position;
        this._isMouseInRange = isMouseInRange;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this._camera.Position + 
                  this._position;

        var buttonSize = new SizeF(64, 16);
        var inRange = this._isMouseInRange(
            this._position, 
            buttonSize);
        
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(this._position, buttonSize, this._menuMainPart))
        {
            this.ButtonAreaWasPressedEvent?.Invoke(this._menuMainPart);
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        this._assetManager.Draw(
            spriteBatch,
            pos,
            this._menuMainPart);
        
        spriteBatch.DrawRectangle(
            pos,
            new SizeF(64, 16),
            isInRangeColor);
    }
    
    public delegate void ButtonAreaWasPressedEventHandler(object assetPart);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;
}