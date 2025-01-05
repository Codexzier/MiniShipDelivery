using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Controls;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.MainMenu;

internal class MainMenuHud : BaseMenu
{
    private readonly MenuFrame _menuFrame;
    
    private readonly MenuButton _menuButtonStartGame;
    private readonly MenuButton _menuButtonMapEditor;
    
    private readonly Vector2 _menuFramePosition;

    public MainMenuHud(Game game,
        int screenWidth,
        int screenHeight) 
        : base(
            game, 
            screenWidth, screenHeight,
            new Vector2(screenWidth / 2 - 70, screenHeight / 2 - 50),
            new Size(140, 100))
    {
        this._menuFrame = new MenuFrame(game);
        this._menuFramePosition = new Vector2(this.ScreenWidth / 2 - 40, this.ScreenHeight / 2 - 20);
        
        var middleStartX = screenWidth / 2 - 32;
        var middleStartY = screenHeight / 2 - 12;
        
        this._menuButtonStartGame = new MenuButton(
            game,
            UiMenuMainPart.Start,
            new Vector2(middleStartX, middleStartY),
            this.IsMouseInRange);
        this._menuButtonStartGame.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        this._menuButtonMapEditor = new MenuButton(
            game,
            UiMenuMainPart.MapEditor,
            new Vector2(middleStartX, middleStartY + 18),
            this.IsMouseInRange);
        this._menuButtonMapEditor.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
    }

    private void ButtonAreaPressed(object assetPart)
    {
        Debug.WriteLine($"ButtonAreaPressed: {assetPart}");
        switch ((UiMenuMainPart)assetPart)
        {
            case UiMenuMainPart.Start:
                this.ButtonHasPressedEvent?.Invoke(HudOptionView.Game);
                break;
            case UiMenuMainPart.MapEditor:
                this.ButtonHasPressedEvent?.Invoke(HudOptionView.MapEditor);
                break;
        }
    }
    
    public delegate void ButtonHasPressedEventHandle(HudOptionView view);
    public event ButtonHasPressedEventHandle ButtonHasPressedEvent;

    public void Draw(SpriteBatch spriteBatch)
    {
        var pos = this.Camera.Camera.Position + this._menuFramePosition;
        
        this._menuFrame.DrawMenuFrame(spriteBatch,
            pos,
            new Size(80, 80),
            MenuFrameType.Type1);
        
        this._menuButtonStartGame.Draw(spriteBatch);
        this._menuButtonMapEditor.Draw(spriteBatch);
    }
}