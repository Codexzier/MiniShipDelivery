using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.Character;

public class TexturesCharacterShadow(Game game) : ISpriteContent<CharacterShadowPart>
{
    public IDictionary<CharacterShadowPart, Rectangle> SpriteContent { get; } = new Dictionary<CharacterShadowPart, Rectangle>
    {
        { CharacterShadowPart.Shadow01, new Rectangle(0, 0, 16, 16) },
        { CharacterShadowPart.Shadow02, new Rectangle(16, 0, 16, 16) },
        { CharacterShadowPart.Shadow03, new Rectangle(32, 0, 16, 16) },
        { CharacterShadowPart.Shadow04, new Rectangle(48, 0, 16, 16) },
    };

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>(@"Character\CharacterShadow");
    public Rectangle GetSprite(MapLayer mapLayer, CharacterShadowPart numberPart) => this.SpriteContent[numberPart];
}