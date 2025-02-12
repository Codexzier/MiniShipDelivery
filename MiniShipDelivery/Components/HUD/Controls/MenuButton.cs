using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Sound;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class MenuButton(
    Game game,
    UiMenuMainPart menuMainPart,
    Vector2 position,
    string text)
{
    private ApplicationBus Bus => ApplicationBus.Instance;
    
    private readonly SpriteUiMenuMainButtons _spriteButtons = new(game);
    private readonly SoundManager _sound = game.GetComponent<SoundManager>();

    private readonly SizeF _buttonSize = new(64, 16);
    
    public void Update()
    {
        var inRange =  HudHelper.IsMouseInRange(position, this._buttonSize);
        this._sound.PlayHover(inRange, text);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this.Bus.Camera.GetPosition() + position;

        var inRange =  HudHelper.IsMouseInRange(position, this._buttonSize);
        
        if (inRange && this.Bus.Inputs.GetMouseButtonReleasedStateLeft(position, this._buttonSize, ""))
        {
            this._sound.PlayPressed();
            this.ButtonAreaWasPressedEvent?.Invoke(menuMainPart);
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.Draw(
            this._spriteButtons.Texture, 
            pos,
            this._spriteButtons.SpriteContent[menuMainPart].Cutout,
            Color.AliceBlue);
        
        spriteBatch.DrawRectangle(
            pos, this._buttonSize,
            isInRangeColor);
    }
    
    #region event handler
    
    public delegate void ButtonAreaWasPressedEventHandler(object assetPart);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;
    
    #endregion
}