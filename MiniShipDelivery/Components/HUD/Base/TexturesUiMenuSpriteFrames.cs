using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.HUD.Base;

public class TexturesUiMenuSpriteFrames(Game game) : ISpriteContent<UiMenuFramePart>
{
    public IDictionary<UiMenuFramePart, SpriteSetup> SpriteContent { get; } = new Dictionary<UiMenuFramePart, SpriteSetup>
    {
        { UiMenuFramePart.None, new SpriteSetup { Cutout = new Rectangle(0, 0, 0, 0) }},
                
        // BaseFrame4x4 Type1
        { UiMenuFramePart.BaseFrame_Type1_TopLeft,new SpriteSetup { Cutout =  new Rectangle((7 * 16) + 0, 0, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_TopMiddle, new SpriteSetup { Cutout = new Rectangle((7 * 16) + 4, 0, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_TopRight,new SpriteSetup { Cutout =  new Rectangle((7 * 16) + 12, 0, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_MiddleLeft,new SpriteSetup { Cutout =  new Rectangle((7 * 16) + 0, 4, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_MiddleMiddle,new SpriteSetup { Cutout = new Rectangle((7 * 16) + 4, 4, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_MiddleRight, new SpriteSetup { Cutout = new Rectangle((7 * 16) + 12, 4, 4, 4)} },
        { UiMenuFramePart.BaseFrame_Type1_DownLeft,new SpriteSetup { Cutout =  new Rectangle((7 * 16) + 0, 12, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_DownMiddle, new SpriteSetup { Cutout = new Rectangle((7 * 16) + 4, 12, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type1_DownRight, new SpriteSetup { Cutout = new Rectangle((7 * 16) + 12, 12, 4, 4) }},
        // BaseFrame4x4 Type2
        { UiMenuFramePart.BaseFrame_Type2_TopLeft, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 0, 0, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_TopMiddle, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 4, 0, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_TopRight,new SpriteSetup { Cutout =  new Rectangle((8 * 16) + 12, 0, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_MiddleLeft, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 0, 4, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_MiddleMiddle, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 4, 4, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_MiddleRight, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 12, 4, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_DownLeft, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 0, 12, 4, 4)} },
        { UiMenuFramePart.BaseFrame_Type2_DownMiddle, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 4, 12, 4, 4) }},
        { UiMenuFramePart.BaseFrame_Type2_DownRight, new SpriteSetup { Cutout = new Rectangle((8 * 16) + 12, 12, 4, 4) }},
    };

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/MenuMapOptions");
    public Rectangle GetSprite(MapLayer mapLayer, UiMenuFramePart numberPart) => this.SpriteContent[numberPart].Cutout;
}