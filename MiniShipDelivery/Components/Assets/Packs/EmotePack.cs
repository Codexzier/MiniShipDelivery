﻿using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets.Packs
{
    internal class EmotePack : ISpriteProperties<EmotePart>
    {
        public Texture2D Texture { get; }
        public IDictionary<EmotePart, Rectangle> SpriteContent { get; }
        public EmotePack(Texture2D texture)
        {
            this.Texture = texture;
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