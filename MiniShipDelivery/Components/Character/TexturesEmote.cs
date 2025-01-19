using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Character
{
    public class TexturesEmote(Game game) : ISpriteProperties<EmotePart>
    {
        public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Emote/pixel_style1");
        public Rectangle GetSprite(MapLayer mapLayer, EmotePart numberPart) => this.SpriteContent[numberPart];

        public IDictionary<EmotePart, Rectangle> SpriteContent { get; } = new Dictionary<EmotePart, Rectangle>
        {
            { EmotePart.EmoteHappy, new Rectangle(0, 0, 16, 16) },
            { EmotePart.EmoteSad, new Rectangle(16, 0, 16, 16) },
            { EmotePart.EmoteAngry, new Rectangle(32, 0, 16, 16) },
            { EmotePart.EmoteSurprised, new Rectangle(48, 0, 16, 16) },
            { EmotePart.EmoteSleepy, new Rectangle(64, 0, 16, 16) },
            { EmotePart.EmoteSick, new Rectangle(80, 0, 16, 16) },
            { EmotePart.EmoteConfused, new Rectangle(96, 0, 16, 16) },
            { EmotePart.EmoteLove, new Rectangle(80, 0, 16, 16) },
            { EmotePart.EmoteLoveDouble, new Rectangle(96, 0, 16, 16) },
            { EmotePart.EmoteQuestion, new Rectangle(128, 0, 16, 16) },
            { EmotePart.EmoteExclamation, new Rectangle(144, 0, 16, 16) },
            { EmotePart.EmoteMusic, new Rectangle(160, 0, 16, 16) },
            { EmotePart.EmoteZzz, new Rectangle(176, 0, 16, 16) },
            { EmotePart.EmoteDotDotDot, new Rectangle(192, 0, 16, 16) }
        };
    }
}