using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Controls;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.OptionsMenu;

public class OptionsMenuOptions : BaseMenu
{
    private readonly MenuFrame _frame;
    private readonly Vector2 _menuFramePosition;

    private readonly MenuButton _buttonBack;

    private readonly CheckBox _checkBoxMusicOnOff;

    public OptionsMenuOptions(Game game) 
        : base(
            game, 
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 100, 
                GlobaleGameParameters.ScreenHeightHalf - 60), 
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
        
        this._checkBoxMusicOnOff = new CheckBox(
            game, 
            "Music on/off",
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 45, 
                GlobaleGameParameters.ScreenHeightHalf - 50));
        this._checkBoxMusicOnOff.IsCheckedChangedEvent += this.CheckBoxMusicOnOffIsCheckedChangedEvent;
        this._checkBoxMusicOnOff.IsChecked = true;
    }

    private void ButtonPressed(object assetPart)
    {
        GlobaleGameParameters.HudView = HudOptionView.MainMenu;
    }
    private void CheckBoxMusicOnOffIsCheckedChangedEvent(bool isChecked)
    {
        if (isChecked)
        {
            MediaPlayer.Play(MusicManager.SongFlowingRocks);
        }
        else
        {
            MediaPlayer.Stop();
        }
    }
    
    public void Update(GameTime gameTime)
    {
        this._checkBoxMusicOnOff.Update();
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
        
        this._checkBoxMusicOnOff.Draw(
            spriteBatch, 
            this.Camera.Camera.Position);
    }
}