using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor.Options;

namespace MiniShipDelivery.Components.HUD.Editor.Textures;

public class SpriteUiMenuSpriteOptions(Game game) : ISpriteContent<UiMenuMapOptionPart>
{
    public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
    {
        return this.SpriteContent[(UiMenuMapOptionPart)numberPart];
    }

    public IDictionary<UiMenuMapOptionPart, SpriteSetup> SpriteContent { get; } = new Dictionary<UiMenuMapOptionPart, SpriteSetup>
    {
        { UiMenuMapOptionPart.None, new SpriteSetup { Cutout = new Rectangle(0, 0, 0, 0) }},
        { UiMenuMapOptionPart.ArrowLeft, new SpriteSetup { Cutout = new Rectangle(0, 0, 16, 16) }},
        { UiMenuMapOptionPart.ArrowUp, new SpriteSetup { Cutout = new Rectangle(16, 0, 16, 16) }},
        { UiMenuMapOptionPart.ArrowRight, new SpriteSetup { Cutout = new Rectangle(32, 0, 16, 16) }},
        { UiMenuMapOptionPart.ArrowDown, new SpriteSetup { Cutout = new Rectangle(48, 0, 16, 16) }},
                
        { UiMenuMapOptionPart.ExclamationWithe, new SpriteSetup { Cutout = new Rectangle(64, 0, 16, 16)} },
        { UiMenuMapOptionPart.ExclamationYellow, new SpriteSetup { Cutout = new Rectangle(80, 0, 16, 16) }},
        { UiMenuMapOptionPart.ExclamationRed, new SpriteSetup { Cutout = new Rectangle(96, 0, 16, 16) }},
        
        { UiMenuMapOptionPart.BoxBrown, new SpriteSetup { Cutout = new Rectangle(112, 0, 16, 16) }},
        { UiMenuMapOptionPart.BoxGray ,new SpriteSetup { Cutout =  new Rectangle(128, 0, 16, 16)}},
        
        { UiMenuMapOptionPart.SelectRed, new SpriteSetup { Cutout = new Rectangle(144, 0, 16, 16) }},
        { UiMenuMapOptionPart.SelectGreen, new SpriteSetup { Cutout = new Rectangle(160, 0, 16, 16)} },
        
        { UiMenuMapOptionPart.TilemapGrass, new SpriteSetup { Cutout = new Rectangle(176, 0, 16, 16) }},
        { UiMenuMapOptionPart.TilemapSidewalk, new SpriteSetup { Cutout = new Rectangle(192, 0, 16, 16)} },
        { UiMenuMapOptionPart.TilemapGrayRoof, new SpriteSetup { Cutout = new Rectangle(208, 0, 16, 16) }},
        { UiMenuMapOptionPart.TilemapBrownRoof, new SpriteSetup { Cutout = new Rectangle(224, 0, 16, 16) }},

        { UiMenuMapOptionPart.Street,new SpriteSetup { Cutout =  new Rectangle(240, 0, 16, 16) }},
        { UiMenuMapOptionPart.Sidewalk,new SpriteSetup { Cutout =  new Rectangle(256, 0, 16, 16)} },
        { UiMenuMapOptionPart.Building, new SpriteSetup { Cutout = new Rectangle(272, 0, 16, 16) }},
        { UiMenuMapOptionPart.Water, new SpriteSetup { Cutout = new Rectangle(288, 0, 16, 16) }},
    };

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/MenuMapOptions");
}