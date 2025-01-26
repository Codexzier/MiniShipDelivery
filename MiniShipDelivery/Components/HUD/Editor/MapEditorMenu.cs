using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MonoGame.Extended;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MiniShipDelivery.Components.World;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Sound;
using MiniShipDelivery.Components.World.Sprites;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenu : BaseMenu
{
    private const int MenuWidth = 60;

    private readonly FunctionBar _functionBarMapLayer;
    private readonly FunctionBar _functionBarMapOption;
    private readonly FunctionBar _functionBarMapTilemapBasement;
    private readonly SpriteUiMenuSpriteOptions _spriteUiMenuSpriteOptions;
    private readonly IEnumerable<EditableEnvironmentItem> _editableEnvironments;
    private readonly SoundManager _sound;

    public static readonly List<RectangleF> MenuField = new();
    
    public MapEditorMenu(Game game) : base(
        game,
        new Vector2(GlobaleGameParameters.ScreenWidth - MenuWidth, 24),
        new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24))
    {
        this._spriteUiMenuSpriteOptions = new SpriteUiMenuSpriteOptions(game);

        this._editableEnvironments = WorldMapHelper.MapSprites.GetEditableEnvironments();
        
        this._functionBarMapLayer = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 2),
            new Size(MenuWidth, 20),
            this.DrawButtonMapLayer,
            this.ChangeColorForActiveLayer);
        this._functionBarMapLayer.ManuelOptions(
        [
            UiMenuMapOptionPart.ArrowLeft,
            UiMenuMapOptionPart.EnvironmentSelect,
            UiMenuMapOptionPart.ArrowRight
        ], 3);
        this._functionBarMapLayer.ButtonAreaWasPressedEvent += this.MapMapLayerButtonAreaPressed;
        
        this._functionBarMapOption = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 22),
            new Size(MenuWidth, 40),
            this.DrawButtonMapOption,
            this.ChangeColorForActive);
        this._functionBarMapOption.ManuelOptions(
        [
            UiMenuMapOptionPart.ArrowLeft,
            UiMenuMapOptionPart.EnvironmentSelect,
            UiMenuMapOptionPart.ArrowRight
        ], 3);
        this._functionBarMapOption.SetOption(UiMenuMapOptionPart.Sidewalk);
        this._functionBarMapOption.ButtonAreaWasPressedEvent += this.MapMapOptionButtonAreaPressed;
        
        this._functionBarMapTilemapBasement = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 42),
            new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 30),
            this.DrawButtonMapSprite,
            this.ChangeColorForActiveMapSprite);
        //this._functionBarMapTilemapBasement.FillOptions<TilemapPart>(3);
        this._functionBarMapTilemapBasement.RefillOptions(this._editableEnvironments.First(), 3);
        this._functionBarMapTilemapBasement.ButtonAreaWasPressedEvent += this.MapTilemapBasementButtonAreaWasPressed;

        const int left = GlobaleGameParameters.ScreenWidth - MenuWidth;
        MenuField.Add(new RectangleF(left, 0, MenuWidth, 24));
        MenuField.Add(new RectangleF(left, 24, MenuWidth, GlobaleGameParameters.ScreenHeight - 24));

        this._sound = game.GetComponent<SoundManager>();
    }

    #region map layer

    private int _editableEnvironmentsIndex;

    private void DrawButtonMapLayer( 
        SpriteBatch spriteBatch,
        Vector2 position,
        FunctionItem functionItem)
    {
        var shiftPosition = position + new Vector2(1, 1);
        var menuMapOption = (UiMenuMapOptionPart)functionItem.NumberPart;

        if (menuMapOption == UiMenuMapOptionPart.EnvironmentSelect)
        {
            var ee = this._editableEnvironments.ElementAt(this._editableEnvironmentsIndex);
            
            spriteBatch.Draw(
                ee.Texture,
                shiftPosition,
                ee.Cutout,
                Color.AliceBlue);
        }
        else
        {
            spriteBatch.Draw(
                this._spriteUiMenuSpriteOptions.Texture,
                shiftPosition,
                this._spriteUiMenuSpriteOptions.SpriteContent[menuMapOption].Cutout,
                Color.AliceBlue);
        }
    }

    private Color ChangeColorForActiveLayer(FunctionItem functionItem, Color color)
    {
        if ((UiMenuMapOptionPart)functionItem.NumberPart == UiMenuMapOptionPart.EnvironmentSelect)
        {
            color = Color.Transparent;
        }
        
        return color;
    }
    
    private void MapMapLayerButtonAreaPressed(FunctionItem functionItem, Action<FunctionItem> itemSetup)
    {
        switch ((UiMenuMapOptionPart)functionItem.NumberPart)
        {
            case UiMenuMapOptionPart.ArrowLeft:
                this._editableEnvironmentsIndex--;
                break;
            case UiMenuMapOptionPart.ArrowRight:
                this._editableEnvironmentsIndex++;
                break;
        }
        
        if (this._editableEnvironmentsIndex >= this._editableEnvironments.Count())
        {
            this._editableEnvironmentsIndex = 0;
        }
        else if(this._editableEnvironmentsIndex < 0)
        {
            this._editableEnvironmentsIndex = this._editableEnvironments.Count() - 1;
        }
        
        var ee = this._editableEnvironments.ElementAt(this._editableEnvironmentsIndex);
        //this._functionBarMapTilemapBasement.RefillOptions(ee.EnumType, 3);
        this._functionBarMapTilemapBasement.RefillOptions(ee, 3);
        WorldMapAdjuster.SelectedMapMapLayer = ee.Layer;
        WorldMapAdjuster.SelectedNumberPart = -1;
        
        this._sound.PlaySwitch();
    }

    #endregion

    #region map option
    
    private void MapMapOptionButtonAreaPressed(FunctionItem functionItem, Action<FunctionItem> itemSetup)
    {
        switch ((UiMenuMapOptionPart)functionItem.NumberPart)
        {
            case UiMenuMapOptionPart.ArrowLeft:
                this._functionBarMapTilemapBasement.PageDown();
                break;
            case UiMenuMapOptionPart.ArrowRight:
                this._functionBarMapTilemapBasement.PageUp();
                break;
        }
        
        this._sound.PlaySwitch();
    }
    
    private Color ChangeColorForActive(FunctionItem functionItem, Color color)
    {
        if ((UiMenuMapOptionPart)functionItem.NumberPart == UiMenuMapOptionPart.EnvironmentSelect)
        {
            color = Color.Transparent;
        }
        
        return color;
    }
    
    private void DrawButtonMapOption(
        SpriteBatch spriteBatch,
        Vector2 position,
        FunctionItem functionItem)
    {
        var shiftPosition = position + new Vector2(1, 1);
        var menuMapOption = (UiMenuMapOptionPart)functionItem.NumberPart;
        if (menuMapOption == UiMenuMapOptionPart.EnvironmentSelect)
        {
            return;
        }

        spriteBatch.Draw(
            this._spriteUiMenuSpriteOptions.Texture,
            shiftPosition,
            this._spriteUiMenuSpriteOptions.SpriteContent[menuMapOption].Cutout,
            Color.AliceBlue);
    }

    #endregion
    
    #region map sprite buttons

    private void DrawButtonMapSprite(SpriteBatch spriteBatch, Vector2 position, FunctionItem functionItem)
    {
        var ee = this._editableEnvironments.ElementAt(this._editableEnvironmentsIndex);
        
        spriteBatch.Draw(
            position + new Vector2(1, 1),
            ee.Layer,
            functionItem.NumberPart);
    }
    
    private Color ChangeColorForActiveMapSprite(FunctionItem functionItem, Color color)
    {
        if (functionItem.Selected)
        {
            color = Color.Yellow;
            
            ConsoleManager.AddText($"Select map tile: {functionItem.NumberPart}");
        }
        
        return color;
    }

    private void MapTilemapBasementButtonAreaWasPressed(FunctionItem item, Action<FunctionItem> itemSetup)
    {
        item.Selected = true;

        WorldMapAdjuster.SelectedNumberPart = item.NumberPart;
        WorldMapAdjuster.SelectedDrawTop = true;
        
        this._sound.PlaySelected();

        itemSetup(item);
    }

    #endregion
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBarMapLayer.Draw(spriteBatch);
        this._functionBarMapOption.Draw(spriteBatch);
        this._functionBarMapTilemapBasement.Draw(spriteBatch);
    }
}