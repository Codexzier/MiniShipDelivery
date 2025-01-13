using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class MenuButton(
    Game game,
    UiMenuMainPart menuMainPart,
    Vector2 position)
{
    private readonly InputManager _input = game.GetComponent<InputManager>();
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();
    private readonly TexturesUiMenuMainButtons _texturesButtons = new(game);

    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this._camera.Camera.Position + position;

        var buttonSize = new SizeF(64, 16);
        var inRange =  HudHelper.IsMouseInRange(position, buttonSize);
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(position, buttonSize, menuMainPart))
        {
            this.ButtonAreaWasPressedEvent?.Invoke(menuMainPart);
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.Draw(
            this._texturesButtons.Texture, 
            pos,
            this._texturesButtons.SpriteContent[menuMainPart],
            Color.AliceBlue);
        
        spriteBatch.DrawRectangle(
            pos,
            new SizeF(64, 16),
            isInRangeColor);
    }
    
    public delegate void ButtonAreaWasPressedEventHandler(object assetPart);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;
}