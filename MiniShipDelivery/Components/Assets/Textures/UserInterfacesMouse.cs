using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Assets.Textures;

public class UserInterfacesMouse : ISpriteProperties<MousePart>
{
    public UserInterfacesMouse(Texture2D texture)
    {
        this.Texture = texture;
            
        this.SpriteContent = new Dictionary<MousePart, Rectangle>();
    }

    public IDictionary<MousePart, Rectangle> SpriteContent { get; }
    public Texture2D Texture { get; }
}