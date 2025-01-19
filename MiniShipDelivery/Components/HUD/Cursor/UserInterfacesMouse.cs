using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.HUD.Cursor;

public class UserInterfacesMouse : ISpriteProperties<MousePart>
{
    public UserInterfacesMouse(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Mouse/MouseCursor");
        
        this.SpriteContent.Add(MousePart.Cursor, new Rectangle(0 * 16, 0 * 16, 16, 16));
        
    }

    public IDictionary<MousePart, Rectangle> SpriteContent { get; } = new Dictionary<MousePart, Rectangle>();
    public Texture2D Texture { get; }
    public Rectangle GetSprite(MapLayer mapLayer, MousePart numberPart) => this.SpriteContent[numberPart];
}