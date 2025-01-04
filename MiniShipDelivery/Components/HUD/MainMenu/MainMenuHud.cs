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
    private readonly OrthographicCamera _camera;
    private readonly MenuFrame _menuFrame;
    
    private readonly MenuButton _menuButtonStartGame;
    private readonly MenuButton _menuButtonMapEditor;

    public MainMenuHud(
        AssetManager assetManager,
        InputManager input,
        OrthographicCamera camera,
        int screenWidth,
        int screenHeight) 
        : base(
            assetManager, 
            input, 
            camera, 
            screenWidth, screenHeight,
            new Vector2(screenWidth / 2 - 70, screenHeight / 2 - 50),
            new Size(140, 100))
    {
        
        this._camera = camera;
        
        this._menuFrame = new MenuFrame(assetManager);
        
        this._menuButtonStartGame = new MenuButton(
            assetManager,
            input, 
            camera,
            UiMenuMainPart.Start,
            new Vector2(screenWidth / 2 - 32, screenHeight / 2 - 12),
            this.IsMouseInRange);
        this._menuButtonStartGame.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        this._menuButtonMapEditor = new MenuButton(
            assetManager,
            input, 
            camera,
            UiMenuMainPart.MapEditor,
            new Vector2(screenWidth / 2 - 32, screenHeight / 2 + 6),
            this.IsMouseInRange);
        this._menuButtonMapEditor.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
    }

    private void ButtonAreaPressed(object assetpart)
    {
        Debug.WriteLine($"ButtonAreaPressed: {assetpart}");
        switch ((UiMenuMainPart)assetpart)
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
        var pos = this._camera.Position + new Vector2(this._screenWidth / 2 - 40, this._screenHeight / 2 - 20);
        
        this._menuFrame.DrawMenuFrame(spriteBatch,
            pos,
            new Size(80, 80),
            MenuFrameType.Type1);
        
        this._menuButtonStartGame.Draw(spriteBatch);
        this._menuButtonMapEditor.Draw(spriteBatch);
    }
}