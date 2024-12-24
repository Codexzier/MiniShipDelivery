using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets.Textures
{
    public class TexturesCharacter : ISpriteProperties<CharacterPart>
    {
        public Texture2D Texture { get; }
        public IDictionary<CharacterPart, Rectangle> SpriteContent { get; }

        public TexturesCharacter(Texture2D texture)
        {
            this.Texture = texture;

            var shift = 0; // (int)characterType * 3;

            var shiftY1 = 0 + shift;
            var shiftY2 = 1 + shift;
            var shiftY3 = 2 + shift;

            this.SpriteContent = new Dictionary<CharacterPart, Rectangle>();
            this.SpriteContent.Add(CharacterPart.StandLeft, new Rectangle((16 * 0), (16 * shiftY1), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkLeftFoodLeft, new Rectangle((16 * 0), (16 * shiftY2), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkRightFoodLeft, new Rectangle((16 * 0), (16 * shiftY3), 16, 16));

            this.SpriteContent.Add(CharacterPart.StandFront, new Rectangle((16 * 1), (16 * shiftY1), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkLeftFoodFront, new Rectangle((16 * 1), (16 * shiftY2), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkRightFoodFront, new Rectangle((16 * 1), (16 * shiftY3), 16, 16));

            this.SpriteContent.Add(CharacterPart.StandBack, new Rectangle((16 * 2), (16 * shiftY1), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkLeftFoodBack, new Rectangle((16 * 2), (16 * shiftY2), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkRightFoodBack, new Rectangle((16 * 2), (16 * shiftY3), 16, 16));

            this.SpriteContent.Add(CharacterPart.StandRight, new Rectangle((16 * 3), (16 * shiftY1), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkLeftFoodRight, new Rectangle((16 * 3), (16 * shiftY2), 16, 16));
            this.SpriteContent.Add(CharacterPart.WalkRightFoodRight, new Rectangle((16 * 3), (16 * shiftY3), 16, 16));
        }

    }
}