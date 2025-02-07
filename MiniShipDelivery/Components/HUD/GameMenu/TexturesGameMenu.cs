using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.HUD.GameMenu;

public class TexturesGameMenu(Game game) : ISpriteContent<GameMenuPart>
{
    public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
    {
        throw new System.NotImplementedException();
    }

    public IDictionary<GameMenuPart, SpriteSetup> SpriteContent { get; } = new Dictionary<GameMenuPart, SpriteSetup>
    {
        { GameMenuPart.QuestLog,new SpriteSetup { Cutout =  new Rectangle(0, 0, 16, 16)} },
        { GameMenuPart.Map ,new SpriteSetup { Cutout =  new Rectangle(16, 0, 16, 16) }}
    };
    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/GameMenuOptions");
}