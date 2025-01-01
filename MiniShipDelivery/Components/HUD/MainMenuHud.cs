using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD;

internal class MainMenuHud : BaseMenu
{
    private readonly FunctionBar _functionBar;
        
    private readonly AssetManager _spriteManager;
    private readonly OrthographicCamera _camera;
    private readonly int _screenWidth;
    private readonly int _screenHeight;

    public MainMenuHud(
        AssetManager spriteManager,
        InputManager input,
        OrthographicCamera camera,
        int screenWidth,
        int screenHeight) 
        : base(
            spriteManager, 
            input, 
            camera, 
            screenWidth, screenHeight,
            new Vector2(screenWidth / 2 - 70, screenHeight / 2 - 50),
            new Size(140, 100))
    {
        this._spriteManager = spriteManager;
        
        
        // TODO: Make single Button class
        this._functionBar = new FunctionBar(
            input,
            camera,
            new Vector2(-70, 0),
            new Size(140, 100),
            this.GetPositionArea,
            this.IsMouseInRange);
        
        this._functionBar.FillOptions<HudOptionView>(1);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        this._functionBar.ButtonAreaHasExecutedEvent += this.ButtonAreaHasExecute;
    }

    private void ButtonAreaPressed(FunctionItem functionItem)
    {
        Debug.WriteLine($"Button pressed: {functionItem.AssetPart}");
        this.ButtonHasPressedEvent?.Invoke((HudOptionView)functionItem.AssetPart);
    }

    public delegate void ButtonHasPressedEventHandler(HudOptionView view);
    public event ButtonHasPressedEventHandler ButtonHasPressedEvent;
    
    private void ButtonAreaHasExecute(
        SpriteBatch spriteBatch, 
        bool inRange, 
        Vector2 position, 
        FunctionItem functionItem)
    {
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.DrawRectangle(
            position,
            new SizeF(18, 18),
            isInRangeColor);
        
        spriteBatch.DrawRectangle(
            position + new Vector2(1, 1),
            new SizeF(16, 16),
            Color.Aquamarine);
        
        spriteBatch.DrawString(
            this._spriteManager.Font,
            functionItem.AssetPart.ToString(),
            position,
            Color.White);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        this.DrawBaseFrame(
            spriteBatch,
            MenuFrameType.Type1);
        
        this._functionBar.Draw(spriteBatch);
    }

}