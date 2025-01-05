using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Assets.Textures;

namespace MiniShipDelivery.Components.HUD.Cursor;

public class UserInterfacesMouse(Game game) : ISpriteProperties<MousePart>
{
    public IDictionary<MousePart, Rectangle> SpriteContent { get; } = new Dictionary<MousePart, Rectangle>();
    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/mouseCursorIcons_packed");
}