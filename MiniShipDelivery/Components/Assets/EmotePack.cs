using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    internal class EmotePack : ISpriteProperties<EmotePart>
    {
        public IDictionary<EmotePart, Rectangle> SpriteContent { get; private set; }
        public EmotePack()
        {
            this.SpriteContent = new Dictionary<EmotePart, Rectangle>
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
}