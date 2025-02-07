using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Input;
using MiniShipDelivery.Components.Sound;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class TextButton
{
    private readonly InputManager _input;
    private readonly CameraManager _camera ;
    private readonly SoundManager _sound;
    private readonly Vector2 _position;
    private readonly string _text;
    
    private readonly SizeF _buttonSize = new(64, 16);
    private readonly SpriteFont _font;
    private readonly Texture2D _texture;

    public Vector2 ShiftPosition { get; set; } = new(0, 0);

    public TextButton(
        Game game, 
        Vector2 position,
        string text)
    {
        this._input = game.GetComponent<InputManager>();
        this._camera = game.GetComponent<CameraManager>();
        this._sound = game.GetComponent<SoundManager>();
        this._font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
        this._texture = game.Content.Load<Texture2D>("Interface/EmptyButton");

        this._position = position;
        this._text = text;
    }
    
    public void Update()
    {
        var inRange =  HudHelper.IsMouseInRange(this._position, this._buttonSize);
        this._sound.PlayHover(inRange, this._text);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this._camera.Camera.Position + this._position;

        var inRange =  HudHelper.IsMouseInRange(this._position, this._buttonSize);
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(this._position, this._buttonSize))
        {
            this._sound.PlayPressed();
            this.ButtonAreaWasPressedEvent?.Invoke(this._text);
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.Draw(
            this._texture, 
            pos,
            Color.AliceBlue);
        
        spriteBatch.DrawString(
            this._font,
            this._text,
            pos + this.ShiftPosition,
            Color.White);
        
        spriteBatch.DrawRectangle(
            pos, this._buttonSize,
            isInRangeColor);
    }
    
    #region event handler
    
    public delegate void ButtonAreaWasPressedEventHandler(string buttonText);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;
    
    #endregion
}