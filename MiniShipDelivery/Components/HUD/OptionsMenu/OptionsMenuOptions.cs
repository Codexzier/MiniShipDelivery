using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Controls;
using MiniShipDelivery.Components.Persistence;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.OptionsMenu;

public class OptionsMenuOptions : BaseMenu
{
    private readonly MenuFrame _frame;
    private readonly Vector2 _menuFramePosition;

    private readonly MenuButton _buttonBack;

    private readonly CheckBox _checkBoxMusicOnOff;
    private readonly CheckBox _checkBoxDebugMode;

    public OptionsMenuOptions(Game game) 
        : base(
            game, 
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 100, 
                GlobaleGameParameters.ScreenHeightHalf - 60), 
            new SizeF(200, 120))
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
                GlobaleGameParameters.ScreenHeightHalf + 40),
            "Back");
        this._buttonBack.ButtonAreaWasPressedEvent += this.ButtonPressedBack;
        
        this._checkBoxMusicOnOff = new CheckBox(
            game, 
            "Music on/off",
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 45, 
                GlobaleGameParameters.ScreenHeightHalf - 50));
        this._checkBoxMusicOnOff.IsCheckedChangedEvent += this.CheckBoxMusicOnOffIsCheckedChangedEvent;
        this._checkBoxMusicOnOff.IsChecked = GameSettingManager.GameSetting.MusicOn;
        
        this._checkBoxDebugMode = new CheckBox(
            game, 
            "Debug mode",
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 45, 
                GlobaleGameParameters.ScreenHeightHalf - 29));
        this._checkBoxDebugMode.IsCheckedChangedEvent += this.CheckBoxDebugModeIsCheckedChangedEvent;
        this._checkBoxDebugMode.IsChecked = GameSettingManager.GameSetting.DebugMode;
    }

   

    #region reveived events

    private void ButtonPressedBack(object assetPart)
    {
        GameSettingManager.SaveGameSetting();
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
        
        GameSettingManager.GameSetting.MusicOn = isChecked;
    }
    
    private void CheckBoxDebugModeIsCheckedChangedEvent(bool isChecked)
    {
        GlobaleGameParameters.DebugMode = isChecked;
        GameSettingManager.GameSetting.DebugMode = isChecked;
    }
    
    #endregion
    
    
    public override void Update()
    {
        this._checkBoxMusicOnOff.Update();
        this._checkBoxDebugMode.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        var pos = this.Bus.Camera.GetPosition() + this._menuFramePosition;
        
        this._frame.DrawMenuFrame(
            spriteBatch, 
            pos, 
            this.Size,
            MenuFrameType.Type1);
        
        this._buttonBack.Draw(spriteBatch);
        
        this._checkBoxMusicOnOff.Draw(
            spriteBatch, 
            this.Bus.Camera.GetPosition());
        
        this._checkBoxDebugMode.Draw(
            spriteBatch,
            this.Bus.Camera.GetPosition());
    }
}