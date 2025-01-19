using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor.Options;

namespace MiniShipDelivery.Components.HUD.Editor.Textures;

public class TexturesUiMenuSpriteOptions(Game game) : ISpriteContent<UiMenuMapOptionPart>
{
    public IDictionary<UiMenuMapOptionPart, Rectangle> SpriteContent { get; } = new Dictionary<UiMenuMapOptionPart, Rectangle>
    {
        { UiMenuMapOptionPart.None, new Rectangle(0, 0, 0, 0) },
        { UiMenuMapOptionPart.ArrowLeft, new Rectangle(0, 0, 16, 16) },
        { UiMenuMapOptionPart.ArrowUp, new Rectangle(16, 0, 16, 16) },
        { UiMenuMapOptionPart.ArrowRight, new Rectangle(32, 0, 16, 16) },
        { UiMenuMapOptionPart.ArrowDown, new Rectangle(48, 0, 16, 16) },
                
        { UiMenuMapOptionPart.ExclamationWithe, new Rectangle(64, 0, 16, 16) },
        { UiMenuMapOptionPart.ExclamationYellow, new Rectangle(80, 0, 16, 16) },
        { UiMenuMapOptionPart.ExclamationRed, new Rectangle(96, 0, 16, 16) },
        
        { UiMenuMapOptionPart.BoxBrown, new Rectangle(112, 0, 16, 16) },
        { UiMenuMapOptionPart.BoxGray , new Rectangle(128, 0, 16, 16)},
        
        { UiMenuMapOptionPart.SelectRed, new Rectangle(144, 0, 16, 16) },
        { UiMenuMapOptionPart.SelectGreen, new Rectangle(160, 0, 16, 16) },
        
        { UiMenuMapOptionPart.TilemapGrass, new Rectangle(176, 0, 16, 16) },
        { UiMenuMapOptionPart.TilemapSidewalk, new Rectangle(192, 0, 16, 16) },
        { UiMenuMapOptionPart.TilemapGrayRoof, new Rectangle(208, 0, 16, 16) },
        { UiMenuMapOptionPart.TilemapBrownRoof, new Rectangle(224, 0, 16, 16) },

        { UiMenuMapOptionPart.Street, new Rectangle(240, 0, 16, 16) },
        { UiMenuMapOptionPart.Sidewalk, new Rectangle(256, 0, 16, 16) },
        { UiMenuMapOptionPart.Building, new Rectangle(272, 0, 16, 16) },
        { UiMenuMapOptionPart.Water, new Rectangle(288, 0, 16, 16) },
    };

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/MenuMapOptions");
    public Rectangle GetSprite(MapLayer mapLayer, UiMenuMapOptionPart numberPart)
    {
        return this.SpriteContent[numberPart];
    }
}