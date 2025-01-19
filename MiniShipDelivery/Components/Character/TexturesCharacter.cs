using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Character
{
    public class TexturesCharacter : ISpriteContent<CharacterPart>
    {
        public Texture2D Texture { get; }
        public Rectangle GetSprite(MapLayer mapLayer, CharacterPart numberPart) => this.SpriteContent[numberPart];

        public IDictionary<CharacterPart, Rectangle> SpriteContent { get; }
        
        public Texture2D TextureStandMen { get; }
        public IDictionary<CharacterStandPart, Rectangle> SpriteContentStandMen { get; }
        public Texture2D TextureStandWomen { get; }
        public Dictionary<CharacterStandPart,Rectangle> SpriteContentStandWomen { get;  }

        public TexturesCharacter(Game game)
        {
            this.Texture = game.Content.Load<Texture2D>("Character/UrbanCharacters");

            const int shiftY1 = 0;
            const int shiftY2 = 1;
            const int shiftY3 = 2;

            this.SpriteContent = new Dictionary<CharacterPart, Rectangle>
            {
                { CharacterPart.StandLeft, new Rectangle(16 * 0, 16 * shiftY1, 16, 16) },
                { CharacterPart.WalkLeftFoodLeft, new Rectangle(16 * 0, 16 * shiftY2, 16, 16) },
                { CharacterPart.WalkRightFoodLeft, new Rectangle(16 * 0, 16 * shiftY3, 16, 16) },
                { CharacterPart.StandFront, new Rectangle(16 * 1, 16 * shiftY1, 16, 16) },
                { CharacterPart.WalkLeftFoodFront, new Rectangle(16 * 1, 16 * shiftY2, 16, 16) },
                { CharacterPart.WalkRightFoodFront, new Rectangle(16 * 1, 16 * shiftY3, 16, 16) },
                { CharacterPart.StandBack, new Rectangle(16 * 2, 16 * shiftY1, 16, 16) },
                { CharacterPart.WalkLeftFoodBack, new Rectangle(16 * 2, 16 * shiftY2, 16, 16) },
                { CharacterPart.WalkRightFoodBack, new Rectangle(16 * 2, 16 * shiftY3, 16, 16) },
                { CharacterPart.StandRight, new Rectangle(16 * 3, 16 * shiftY1, 16, 16) },
                { CharacterPart.WalkLeftFoodRight, new Rectangle(16 * 3, 16 * shiftY2, 16, 16) },
                { CharacterPart.WalkRightFoodRight, new Rectangle(16 * 3, 16 * shiftY3, 16, 16) }
            };
            
            this.TextureStandMen = game.Content.Load<Texture2D>("Character/UrbanCharactersMen");
            this.SpriteContentStandMen = new Dictionary<CharacterStandPart, Rectangle>
            {
                { CharacterStandPart.Stand01, new Rectangle(16 * 0, 16 * shiftY1, 16, 16) },
                { CharacterStandPart.Stand02, new Rectangle(16 * 1, 16 * shiftY1, 16, 16) },
                { CharacterStandPart.Stand03, new Rectangle(16 * 2, 16 * shiftY1, 16, 16) },
                { CharacterStandPart.Stand04, new Rectangle(16 * 3, 16 * shiftY1, 16, 16) }
            };
            
            this.TextureStandWomen = game.Content.Load<Texture2D>("Character/UrbanCharactersWomen");
            this.SpriteContentStandWomen = new Dictionary<CharacterStandPart, Rectangle>
            {
                { CharacterStandPart.Stand01, new Rectangle(16 * 0, 16 * shiftY1, 16, 16) },
                { CharacterStandPart.Stand02, new Rectangle(16 * 1, 16 * shiftY1, 16, 16) },
                { CharacterStandPart.Stand03, new Rectangle(16 * 2, 16 * shiftY1, 16, 16) },
                { CharacterStandPart.Stand04, new Rectangle(16 * 3, 16 * shiftY1, 16, 16) }
            };
        }

    }
}