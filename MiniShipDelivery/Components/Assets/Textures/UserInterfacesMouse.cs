using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Assets.Textures;

public class UserInterfacesMouse(Texture2D texture) : ISpriteProperties<MousePart>, IAssetTexture
{
    public IDictionary<MousePart, Rectangle> SpriteContent { get; } = new Dictionary<MousePart, Rectangle>();
    public Texture2D Texture { get; } = texture;
}