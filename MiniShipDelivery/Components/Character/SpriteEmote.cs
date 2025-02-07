using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Character
{
    public class SpriteEmote(Game game) : ISpriteContent<EmotePart>
    {
        public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Emote/pixel_style1");
        public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
        {
            throw new System.NotImplementedException();
        }

        public IDictionary<EmotePart, SpriteSetup> SpriteContent { get; } = new Dictionary<EmotePart, SpriteSetup>
        {
            { EmotePart.EmoteHappy, new SpriteSetup { Cutout = new Rectangle(0, 0, 16, 16)} },
            { EmotePart.EmoteSad, new SpriteSetup { Cutout = new Rectangle(16, 0, 16, 16) }},
            { EmotePart.EmoteAngry, new SpriteSetup { Cutout = new Rectangle(32, 0, 16, 16) }},
            { EmotePart.EmoteSurprised, new SpriteSetup { Cutout = new Rectangle(48, 0, 16, 16) }},
            { EmotePart.EmoteSleepy, new SpriteSetup { Cutout = new Rectangle(64, 0, 16, 16) }},
            { EmotePart.EmoteSick, new SpriteSetup { Cutout = new Rectangle(80, 0, 16, 16) }},
            { EmotePart.EmoteConfused, new SpriteSetup { Cutout = new Rectangle(96, 0, 16, 16)} },
            { EmotePart.EmoteLove, new SpriteSetup { Cutout = new Rectangle(80, 0, 16, 16)} },
            { EmotePart.EmoteLoveDouble, new SpriteSetup { Cutout = new Rectangle(96, 0, 16, 16) }},
            { EmotePart.EmoteQuestion, new SpriteSetup { Cutout = new Rectangle(128, 0, 16, 16)} },
            { EmotePart.EmoteExclamation, new SpriteSetup { Cutout = new Rectangle(144, 0, 16, 16)} },
            { EmotePart.EmoteMusic, new SpriteSetup { Cutout = new Rectangle(160, 0, 16, 16) }},
            { EmotePart.EmoteZzz, new SpriteSetup { Cutout = new Rectangle(176, 0, 16, 16) }},
            { EmotePart.EmoteDotDotDot, new SpriteSetup { Cutout = new Rectangle(192, 0, 16, 16)} }
        };
    }
}