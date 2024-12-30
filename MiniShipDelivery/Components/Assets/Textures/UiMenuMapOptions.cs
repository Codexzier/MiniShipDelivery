using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets;

public class UiMenuMapOptions : ISpriteProperties<UiMenuMapOptionPart>
{
    public UiMenuMapOptions(Texture2D texture)
    {
        this.Texture = texture;
        this.SpriteContent = new Dictionary<UiMenuMapOptionPart, Rectangle>
        {
            { UiMenuMapOptionPart.None, new Rectangle(0, 0, 0, 0) },
            { UiMenuMapOptionPart.ArrowLeft, new Rectangle(0, 0, 16, 16) },
            { UiMenuMapOptionPart.ArrowUp, new Rectangle(16, 0, 16, 16) },
            { UiMenuMapOptionPart.ArrowRight, new Rectangle(32, 0, 16, 16) },
            { UiMenuMapOptionPart.ArrowDown, new Rectangle(48, 0, 16, 16) },
                
            { UiMenuMapOptionPart.ExlamationWithe, new Rectangle(64, 0, 16, 16) },
            { UiMenuMapOptionPart.ExlamationYellow, new Rectangle(80, 0, 16, 16) },
            { UiMenuMapOptionPart.ExlamationRed, new Rectangle(96, 0, 16, 16) },
                
            { UiMenuMapOptionPart.SelectRed, new Rectangle(112, 0, 16, 16) },
            { UiMenuMapOptionPart.SelectGreen, new Rectangle(128, 0, 16, 16) },
        };
    }

    public IDictionary<UiMenuMapOptionPart, Rectangle> SpriteContent { get; }
    public Texture2D Texture { get; }
}