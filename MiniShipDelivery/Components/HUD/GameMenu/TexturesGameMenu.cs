using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.HUD.GameMenu;

public class TexturesGameMenu(Game game) : ISpriteProperties<GameMenuPart>
{
    public IDictionary<GameMenuPart, Rectangle> SpriteContent { get; } = new Dictionary<GameMenuPart, Rectangle>
    {
        { GameMenuPart.QuestLog, new Rectangle(0, 0, 16, 16) },
        { GameMenuPart.Map , new Rectangle(16, 0, 16, 16) }
    };
    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/GameMenuOptions");
    public Rectangle GetSprite(MapLayer mapLayer, GameMenuPart numberPart)
    {
        return this.SpriteContent[numberPart];
    }
}