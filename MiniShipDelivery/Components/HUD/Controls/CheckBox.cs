using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class CheckBox(
    Game game,
    string textLabel,
    Vector2 postion)
{
    private readonly SpriteFont _font = game.Content.Load<SpriteFont>("Fonts/BaseFont");
    private readonly Vector2 _positionRectangle = postion + new Vector2(75, -3);
    private readonly InputManager _input = game.GetComponent<InputManager>();
    private readonly SizeF _size = new(20, 20);

    public bool IsChecked { get; set; }

    public void Update()
    {
        var inRange =  HudHelper.IsMouseInRange(
            this._positionRectangle, 
            this._size);
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(
                this._positionRectangle,
                this._size))
        {
            this.IsChecked = !this.IsChecked;
            this.IsCheckedChangedEvent?.Invoke(this.IsChecked);
        }
    }
    
    public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
    {
        var pos = cameraPosition + postion;
        var posRectangle = cameraPosition + this._positionRectangle;

        spriteBatch.DrawString(this._font,
            textLabel,
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
        
        spriteBatch.DrawRectangle(
            posRectangle,
            new SizeF(20, 20),
            Color.Black);

        if (this.IsChecked)
        {
            spriteBatch.FillRectangle(
                posRectangle + new Vector2(2, 2),
                new SizeF(16, 16),
                Color.Black);
        }
    }
    
    #region event handler
    
    public delegate void IsCheckedChangedEventHandler(bool isChecked);
    public event IsCheckedChangedEventHandler IsCheckedChangedEvent;
    
    #endregion
}