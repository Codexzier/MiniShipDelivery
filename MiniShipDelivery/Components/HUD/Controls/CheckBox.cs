using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class CheckBox
{
    private readonly SpriteFont _font;
    private readonly string _textLabel;
    private readonly Vector2 _position;
    private readonly Vector2 _positionReactangle;
    private readonly InputManager _input;
    private readonly SizeF _size = new(20, 20);

    public CheckBox(
        Game game, 
        string textLabel, 
        Vector2 postion)
    {
        this._font = game.Content.Load<SpriteFont>("Fonts/BaseFont");
        this._input = game.GetComponent<InputManager>();
        this._textLabel = textLabel;
        this._position = postion;
        this._positionReactangle = postion + new Vector2(70, -3);
    }

    public bool IsChecked { get; set; }

    public void Update()
    {
        var pos = this._positionReactangle;
        var inRange =  HudHelper.IsMouseInRange(
            pos, 
            this._size);
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(
                pos,
                this._size))
        {
            this.IsChecked = !this.IsChecked;
            this.IsCheckedChangedEvent?.Invoke(this.IsChecked);
        }
    }
    
    public delegate void IsCheckedChangedEventHandler(bool isChecked);
    public event IsCheckedChangedEventHandler IsCheckedChangedEvent;
    
    public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
    {
        var pos = cameraPosition + this._position;
        var posRectangle = cameraPosition + this._positionReactangle;

        spriteBatch.DrawString(this._font,
            this._textLabel,
            pos,
            Color.White,
            0f,
            new Vector2(0, 0),
            0.8f,
            SpriteEffects.None, 1);
        
        spriteBatch.FillRectangle(
            posRectangle,
            new SizeF(20, 20),
            Color.Gray);

        if (this.IsChecked)
        {
            spriteBatch.FillRectangle(
                posRectangle + new Vector2(2, 2),
                new SizeF(16, 16),
                Color.Black);
        }
    }
}