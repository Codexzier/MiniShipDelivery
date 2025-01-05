using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.HUD.Cursor;

public class UserInterfacesMouse : ISpriteProperties<MousePart>
{
    public UserInterfacesMouse(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Interface/mouseCursorIcons_packed");
        
        this.SpriteContent.Add(MousePart.Cursor, new Rectangle(7 * 16, 4 * 16, 16, 16));
        
    }

    public IDictionary<MousePart, Rectangle> SpriteContent { get; } = new Dictionary<MousePart, Rectangle>();
    public Texture2D Texture { get; }
}