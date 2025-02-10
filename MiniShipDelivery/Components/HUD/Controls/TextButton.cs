using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Input;
using MiniShipDelivery.Components.Sound;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class TextButton(
    Game game,
    Vector2 position,
    string text)
{
    private ApplicationBus Bus => ApplicationBus.Instance;
    private readonly SoundManager _sound = game.GetComponent<SoundManager>();

    private readonly SizeF _buttonSize = new(64, 16);
    private readonly SpriteFont _font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
    private readonly Texture2D _texture = game.Content.Load<Texture2D>("Interface/EmptyButton");

    public Vector2 ShiftPosition { get; set; } = new(0, 0);

    public void Update()
    {
        var inRange =  HudHelper.IsMouseInRange(position, this._buttonSize);
        this._sound.PlayHover(inRange, text);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this.Bus.Camera.GetPosition() + position;

        var inRange =  HudHelper.IsMouseInRange(position, this._buttonSize);
        var hasPressed = this.Bus.Inputs.GetMouseButtonReleasedStateLeft(position, this._buttonSize);

        if (hasPressed)
        {
            Debug.WriteLine($"Pressed: {position}, Button: {text}");
        }
        
        if (inRange && hasPressed)
        {
            this._sound.PlayPressed();
            this.WasPressedEvent?.Invoke(text);
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.Draw(
            this._texture, 
            pos,
            Color.AliceBlue);
        
        spriteBatch.DrawString(
            this._font,
            text,
            pos + this.ShiftPosition,
            Color.White);
        
        spriteBatch.DrawRectangle(
            pos, 
            this._buttonSize,
            isInRangeColor);
    }
    
    #region event handler
    
    public delegate void ButtonAreaWasPressedEventHandler(string buttonText);
    public event ButtonAreaWasPressedEventHandler WasPressedEvent;
    
    #endregion
}