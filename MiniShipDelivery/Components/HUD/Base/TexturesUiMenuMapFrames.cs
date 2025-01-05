using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Textures;

namespace MiniShipDelivery.Components.HUD.Base;

public class TexturesUiMenuMapFrames(Game game) : ISpriteProperties<UiMenuFramePart>
{
    public IDictionary<UiMenuFramePart, Rectangle> SpriteContent { get; } = new Dictionary<UiMenuFramePart, Rectangle>
    {
        { UiMenuFramePart.None, new Rectangle(0, 0, 0, 0) },
                
        // BaseFrame4x4 Type1
        { UiMenuFramePart.BaseFrame_Type1_TopLeft, new Rectangle((7 * 16) + 0, 0, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_TopMiddle, new Rectangle((7 * 16) + 4, 0, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_TopRight, new Rectangle((7 * 16) + 12, 0, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_MiddleLeft, new Rectangle((7 * 16) + 0, 4, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_MiddleMiddle, new Rectangle((7 * 16) + 4, 4, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_MiddleRight, new Rectangle((7 * 16) + 12, 4, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_DownLeft, new Rectangle((7 * 16) + 0, 12, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_DownMiddle, new Rectangle((7 * 16) + 4, 12, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type1_DownRight, new Rectangle((7 * 16) + 12, 12, 4, 4) },
        // BaseFrame4x4 Type2
        { UiMenuFramePart.BaseFrame_Type2_TopLeft, new Rectangle((8 * 16) + 0, 0, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_TopMiddle, new Rectangle((8 * 16) + 4, 0, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_TopRight, new Rectangle((8 * 16) + 12, 0, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_MiddleLeft, new Rectangle((8 * 16) + 0, 4, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_MiddleMiddle, new Rectangle((8 * 16) + 4, 4, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_MiddleRight, new Rectangle((8 * 16) + 12, 4, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_DownLeft, new Rectangle((8 * 16) + 0, 12, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_DownMiddle, new Rectangle((8 * 16) + 4, 12, 4, 4) },
        { UiMenuFramePart.BaseFrame_Type2_DownRight, new Rectangle((8 * 16) + 12, 12, 4, 4) },
    };

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/MenuMapOptions");
}