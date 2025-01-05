using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Assets.Textures;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenuCommon : BaseMenu
{
    private readonly FunctionBar _functionBar;
    private readonly TexturesInterfaceMenuEditorOptions _userInterfacesMenuEditorOptions;

    public MapEditorMenuCommon(
        Game game,
        int screenWidth,
        int screenHeight)
        : base(
            game, 
            screenWidth, 
            screenHeight,
            new Vector2(0, 0), new Size(screenWidth, 24))
    {
        this._userInterfacesMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game);
        
        this._functionBar = new FunctionBar(
            game,
            new Vector2(0, 0),
            new Size(screenWidth, 24),
            this.GetPositionArea,
            this.IsMouseInRange);

        this._functionBar.FillOptions<InterfaceMenuEditorOptionPart>(6);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        this._functionBar.ButtonAreaHasExecutedEvent += this.ButtonAreaHasExecute;
    }

    private void ButtonAreaPressed(FunctionItem functionItem)
    {
        Debug.WriteLine($"ButtonAreaPressed: {functionItem.AssetPart}");
    }
    
    private void ButtonAreaHasExecute(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        // TODO: Wrong area to draw the button
        var tt = (InterfaceMenuEditorOptionPart)functionItem.AssetPart;
        spriteBatch.Draw(
            this._userInterfacesMenuEditorOptions.Texture,
            position + new Vector2(1, 1),
            this._userInterfacesMenuEditorOptions.SpriteContent[tt],
            Color.AliceBlue);

        // this._assetManager.Draw(spriteBatch,
        //     position + new Vector2(1, 1),
        //     (InterfaceMenuEditorOptionPart)functionItem.AssetPart);
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBar.Draw(spriteBatch);
    }
}