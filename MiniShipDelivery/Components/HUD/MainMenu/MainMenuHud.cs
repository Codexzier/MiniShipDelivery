using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Controls;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.MainMenu;

internal class MainMenuHud : BaseMenu
{
    private readonly MenuFrame _menuFrame;
    private readonly Vector2 _menuFramePosition;
    
    private readonly MenuButton _menuButtonStartGame;
    private readonly MenuButton _menuButtonMapEditor;
    private readonly MenuButton _menuButtonOptions;
    private readonly MenuButton _menuButtonExit;
    

    public MainMenuHud(Game game, int screenHalfWidth, int screenHalfHeight) 
        : base(
            game, 
            new Vector2(screenHalfWidth - 70, screenHalfHeight - 50),
            new SizeF(140, 100))
    {
        this._menuFrame = new MenuFrame(game);
        this._menuFramePosition = new Vector2(
            screenHalfWidth - 40, 
            screenHalfHeight - 20);
        
        var middleStartX = screenHalfWidth - 32;
        var middleStartY = screenHalfHeight - 15;
        
        this._menuButtonStartGame = new MenuButton(
            game,
            UiMenuMainPart.Start,
            new Vector2(middleStartX, middleStartY),
            "Start");
        this._menuButtonStartGame.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        this._menuButtonMapEditor = new MenuButton(
            game,
            UiMenuMainPart.MapEditor,
            new Vector2(middleStartX, middleStartY + 18),
            "Map Editor");
        this._menuButtonMapEditor.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        this._menuButtonOptions = new MenuButton(
            game,
            UiMenuMainPart.Options,
            new Vector2(middleStartX, middleStartY + 36),
            "Options");
        this._menuButtonOptions.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        this._menuButtonExit = new MenuButton(
            game,
            UiMenuMainPart.Exit,
            new Vector2(middleStartX, middleStartY + 54),
            "Exit");
        this._menuButtonExit.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
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
            case UiMenuMainPart.Options:
                this.ButtonHasPressedEvent?.Invoke(HudOptionView.Options);
                break;
            case UiMenuMainPart.Exit:
                this.Game.Exit();
                break;
        }
    }
    
    public delegate void ButtonHasPressedEventHandle(HudOptionView view);
    public event ButtonHasPressedEventHandle ButtonHasPressedEvent;

    public override void Update()
    {
        base.Update();
        
        this._menuButtonStartGame.Update();
        this._menuButtonMapEditor.Update();
        this._menuButtonOptions.Update();
        this._menuButtonExit.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = this.Bus.Camera.GetPosition() + this._menuFramePosition;
        
        this._menuFrame.DrawMenuFrame(spriteBatch,
            pos,
            new SizeF(80, 80),
            MenuFrameType.Type1);
        
        this._menuButtonStartGame.Draw(spriteBatch);
        this._menuButtonMapEditor.Draw(spriteBatch);
        this._menuButtonOptions.Draw(spriteBatch);
        this._menuButtonExit.Draw(spriteBatch);
    }
}