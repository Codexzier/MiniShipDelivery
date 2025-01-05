using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Assets.Textures;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Helpers;
using MiniShipDelivery.Components.World;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenu : BaseMenu
{
    private readonly TexturesTilemap _texturesTilemap;
    
    private const int MenuWidth = 60;

    private readonly FunctionBar _functionBarMapOption;
    private readonly FunctionBar _functionBarMapSprites;
    private readonly UiMenuMapOptions _uiMenuMapOptions;

    public MapEditorMenu(Game game, AssetManager assetManager,
        InputManager input,
        OrthographicCamera camera,
        int screenWidth,
        int screenHeight) : base(
        game,
        input,
        camera,
        screenWidth,
        screenHeight,
        new Vector2(screenWidth - MenuWidth, 24),
        new Size(MenuWidth, screenHeight - 24))
    {
        this._texturesTilemap = new TexturesTilemap(game);
        this._uiMenuMapOptions = new UiMenuMapOptions(game.Content.Load<Texture2D>("Interface/MenuMapOptions"));

        
        this._functionBarMapOption = new FunctionBar(
            input,
            camera,
            new Vector2(0, 0),
            new Size(MenuWidth, screenHeight - 24),
            this.GetPositionArea,
            this.IsMouseInRange);

        this._functionBarMapOption.FillOptions<MapEditorOption>(3);
        this._functionBarMapOption.ButtonAreaWasPressedEvent += this.MapMapOptionButtonAreaPressed;
        this._functionBarMapOption.ButtonAreaHasExecutedEvent += this.MapMapOptionButtonAreaHasExecutedEvent;


        this._functionBarMapSprites = new FunctionBar(
            input,
            camera,
            new Vector2(0, 20),
            new Size(MenuWidth, screenHeight - 24),
            this.GetPositionArea,
            this.IsMouseInRange);

        this._functionBarMapSprites.FillOptions<TilemapPart>(3);
        this._functionBarMapSprites.ButtonAreaWasPressedEvent += this.MapSpritesButtonAreaWasPressed;
        this._functionBarMapSprites.ButtonAreaHasExecutedEvent += this.MapSpritesButtonAreaHasExecuted;
    }

    public bool ShowGrid { get; private set; }

    public void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBarMapOption.Draw(spriteBatch);
        this._functionBarMapSprites.Draw(spriteBatch);
    }

    private void MapMapOptionButtonAreaPressed(FunctionItem functionItem)
    {
        switch (functionItem.AssetPart)
        {
            case MapEditorOption.OnOffGrid:
                this.ShowGrid = !this.ShowGrid;
                break;
            case MapEditorOption.ArrowLeft:
                break;
            case MapEditorOption.ArrowRight:
                break;
        }
    }

    private void MapMapOptionButtonAreaHasExecutedEvent(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        if (this.ShowGrid && (MapEditorOption)functionItem.AssetPart == MapEditorOption.OnOffGrid)
        {
            isInRangeColor = Color.Yellow;
        }

        spriteBatch.DrawRectangle(
            position,
            functionItem.Size,
            isInRangeColor);
        
        var shiftPosition = position + new Vector2(1, 1);

        var menuMapOption = UiMenuMapOptionPart.None;
        switch (functionItem.AssetPart)
        {
            case MapEditorOption.OnOffGrid:
                menuMapOption = UiMenuMapOptionPart.ExlamationWithe;
                
                // this._assetManager.Draw(
                //     spriteBatch,
                //     shiftPosition,
                //     UiMenuMapOptionPart.ExlamationWithe);
                break;
            case MapEditorOption.ArrowLeft:
                menuMapOption = UiMenuMapOptionPart.ArrowLeft;
                // this._assetManager.Draw(
                //     spriteBatch,
                //     shiftPosition,
                //     UiMenuMapOptionPart.ArrowLeft);
                break;
            case MapEditorOption.ArrowRight:
                menuMapOption = UiMenuMapOptionPart.ArrowRight;
                // this._assetManager.Draw(
                //     spriteBatch,
                //     shiftPosition,
                //     UiMenuMapOptionPart.ArrowRight);
                break;
        }
        
        spriteBatch.Draw(
            this._uiMenuMapOptions.Texture,
            shiftPosition,
            this._uiMenuMapOptions.SpriteContent[menuMapOption],
            isInRangeColor);
    }

    private void MapSpritesButtonAreaWasPressed(FunctionItem functionitem)
    {
        functionitem.Selected = true;
        this._functionBarMapSprites.ResetAllSelected(functionitem);
    }

    private void MapSpritesButtonAreaHasExecuted(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        if (functionItem.Selected)
        {
            isInRangeColor = Color.Yellow;
        }

        spriteBatch.DrawRectangle(
            position,
            functionItem.Size,
            isInRangeColor);

        spriteBatch.Draw(
            this._texturesTilemap.Texture, 
            position + new Vector2(1, 1), 
            this._texturesTilemap.SpriteContent[(TilemapPart)functionItem.AssetPart],
            Color.White);
        
        // Used only for debug
        //spriteBatch.WriteLine(this._spriteManager.Font,
        //    $"{item.TilemapPart}",
        //    pos + new Vector2(-40, 1));
    }
}