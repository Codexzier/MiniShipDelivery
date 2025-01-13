using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class MenuButton
{
    private readonly InputManager _input;
    private readonly CameraManager _camera;
    private readonly TexturesUiMenuMainButtons _texturesButtons;
    
    private readonly ButtonSound _buttonSound;
    
    private readonly UiMenuMainPart _menuMainPart;
    private readonly Vector2 _position;

    public MenuButton(Game game,
        UiMenuMainPart menuMainPart,
        Vector2 position)
    {
        this._menuMainPart = menuMainPart;
        this._position = position;
        this._input = game.GetComponent<InputManager>();
        this._camera = game.GetComponent<CameraManager>();
        this._texturesButtons = new TexturesUiMenuMainButtons(game);
        this._buttonSound = new ButtonSound(game);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this._camera.Camera.Position + this._position;

        var buttonSize = new SizeF(64, 16);
        var inRange =  HudHelper.IsMouseInRange(this._position, buttonSize);

        this._buttonSound.PlayHover(inRange);
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(this._position, buttonSize))
        {
            this._buttonSound.PlayPressed();
            this.ButtonAreaWasPressedEvent?.Invoke(this._menuMainPart);
        }
        
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.Draw(
            this._texturesButtons.Texture, 
            pos,
            this._texturesButtons.SpriteContent[this._menuMainPart],
            Color.AliceBlue);
        
        spriteBatch.DrawRectangle(
            pos,
            new SizeF(64, 16),
            isInRangeColor);
    }
    
    public delegate void ButtonAreaWasPressedEventHandler(object assetPart);
    public event ButtonAreaWasPressedEventHandler ButtonAreaWasPressedEvent;
}