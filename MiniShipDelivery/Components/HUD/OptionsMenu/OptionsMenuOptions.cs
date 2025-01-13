using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Controls;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.OptionsMenu;

public class OptionsMenuOptions : BaseMenu
{
    private readonly MenuFrame _frame;
    private readonly Vector2 _menuFramePosition;

    private readonly MenuButton _buttonBack;

    #region check boxe
    
    private readonly SpriteFont _font;
    private bool _musicOnOff = true;
    private readonly Vector2 _musicOnOffPosition = new(120, 10);
    private readonly InputManager _input;

    #endregion


    public OptionsMenuOptions(Game game) 
        : base(
            game, 
            new Vector2(GlobaleGameParameters.ScreenWidthHalf - 100, GlobaleGameParameters.ScreenHeightHalf - 60), 
            new Size(200, 120))
    {
        this._frame = new MenuFrame(game);
        this._menuFramePosition = new Vector2(
            GlobaleGameParameters.ScreenWidthHalf - 100, 
            GlobaleGameParameters.ScreenHeightHalf - 60);
        
        this._buttonBack = new MenuButton(
            game,
            UiMenuMainPart.Back,
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 32, 
                GlobaleGameParameters.ScreenHeightHalf + 40));
        this._buttonBack.ButtonAreaWasPressedEvent += this.ButtonPressed;
        
        this._font = game.Content.Load<SpriteFont>("Fonts/BaseFont");
        this._input = game.GetComponent<InputManager>();
    }

    private void ButtonPressed(object assetPart)
    {
        GlobaleGameParameters.HudView = HudOptionView.MainMenu;
    }
    
    public void Update(GameTime gameTime)
    {
        var checkBoxSize = new SizeF(20, 20);
        var pos = this.Camera.Camera.Position + this._menuFramePosition;
        var inRange =  HudHelper.IsMouseInRange(
            pos + this._musicOnOffPosition, 
            checkBoxSize);

        if (inRange && this._input.GetMouseLeftButtonReleasedState(
                pos + this._musicOnOffPosition,
                checkBoxSize))
        {
            this._musicOnOff = !this._musicOnOff;

            if (this._musicOnOff)
            {
                MediaPlayer.Play(MusicManager.SongFlowingRocks);
            }
            else
            {
                MediaPlayer.Stop();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        var pos = this.Camera.Camera.Position + this._menuFramePosition;
        
        this._frame.DrawMenuFrame(
            spriteBatch, 
            pos, 
            this.Size,
            MenuFrameType.Type1);
        
        this._buttonBack.Draw(spriteBatch);
        
        spriteBatch.DrawString(this._font,
            "Music on/off",
            pos + this._musicOnOffPosition + new Vector2(-70, 3),
            Color.White,
            0f,
            new Vector2(0, 0),
            0.8f,
            SpriteEffects.None, 1);
        
        spriteBatch.FillRectangle(
            pos + this._musicOnOffPosition,
            new SizeF(20, 20),
            Color.Gray);

        if (this._musicOnOff)
        {
            spriteBatch.FillRectangle(
                pos + this._musicOnOffPosition + new Vector2(2, 2),
                new SizeF(16, 16),
                Color.Black);
        }
    }
}