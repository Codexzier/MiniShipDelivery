using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public abstract class BaseSpriteContent
{
    protected IDictionary<int, SpriteSetup> SpriteContent { get; init; }
    public Texture2D Texture { get; protected init; }
    public int SpriteCount => this.SpriteContent.Count;
}