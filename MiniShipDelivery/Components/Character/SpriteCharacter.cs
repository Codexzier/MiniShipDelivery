using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Character
{
    public class SpriteCharacter : ISpriteContent<CharacterPart>
    {
        public Texture2D Texture { get; }
        public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
        {
            return this.SpriteContent[(CharacterPart)numberPart];
        }

        public IDictionary<CharacterPart, SpriteSetup> SpriteContent { get; }
        
        public Texture2D TextureStandMen { get; }
        public IDictionary<CharacterStandPart, SpriteSetup> SpriteContentStandMen { get; }
        public Texture2D TextureStandWomen { get; }
        public Dictionary<CharacterStandPart,SpriteSetup> SpriteContentStandWomen { get;  }

        public SpriteCharacter(Game game)
        {
            this.Texture = game.Content.Load<Texture2D>("Character/UrbanCharacters");

            const int shiftY1 = 0;
            const int shiftY2 = 1;
            const int shiftY3 = 2;

            this.SpriteContent = new Dictionary<CharacterPart, SpriteSetup>
            {
                { CharacterPart.StandLeft, new SpriteSetup { Cutout = new Rectangle(16 * 0, 16 * shiftY1, 16, 16) }},
                { CharacterPart.WalkLeftFoodLeft, new SpriteSetup { Cutout = new Rectangle(16 * 0, 16 * shiftY2, 16, 16) }},
                { CharacterPart.WalkRightFoodLeft, new SpriteSetup { Cutout = new Rectangle(16 * 0, 16 * shiftY3, 16, 16)} },
                { CharacterPart.StandFront, new SpriteSetup { Cutout = new Rectangle(16 * 1, 16 * shiftY1, 16, 16) }},
                { CharacterPart.WalkLeftFoodFront, new SpriteSetup { Cutout = new Rectangle(16 * 1, 16 * shiftY2, 16, 16) }},
                { CharacterPart.WalkRightFoodFront, new SpriteSetup { Cutout = new Rectangle(16 * 1, 16 * shiftY3, 16, 16) }},
                { CharacterPart.StandBack, new SpriteSetup { Cutout = new Rectangle(16 * 2, 16 * shiftY1, 16, 16) }},
                { CharacterPart.WalkLeftFoodBack, new SpriteSetup { Cutout = new Rectangle(16 * 2, 16 * shiftY2, 16, 16) }},
                { CharacterPart.WalkRightFoodBack, new SpriteSetup { Cutout = new Rectangle(16 * 2, 16 * shiftY3, 16, 16) }},
                { CharacterPart.StandRight, new SpriteSetup { Cutout = new Rectangle(16 * 3, 16 * shiftY1, 16, 16) }},
                { CharacterPart.WalkLeftFoodRight, new SpriteSetup { Cutout = new Rectangle(16 * 3, 16 * shiftY2, 16, 16) }},
                { CharacterPart.WalkRightFoodRight, new SpriteSetup { Cutout = new Rectangle(16 * 3, 16 * shiftY3, 16, 16) }}
            };
            
            this.TextureStandMen = game.Content.Load<Texture2D>("Character/UrbanCharactersMen");
            this.SpriteContentStandMen = new Dictionary<CharacterStandPart, SpriteSetup>
            {
                { CharacterStandPart.Stand01, new SpriteSetup { Cutout = new Rectangle(16 * 0, 16 * shiftY1, 16, 16)} },
                { CharacterStandPart.Stand02, new SpriteSetup { Cutout = new Rectangle(16 * 1, 16 * shiftY1, 16, 16)} },
                { CharacterStandPart.Stand03, new SpriteSetup { Cutout = new Rectangle(16 * 2, 16 * shiftY1, 16, 16)} },
                { CharacterStandPart.Stand04, new SpriteSetup { Cutout = new Rectangle(16 * 3, 16 * shiftY1, 16, 16)} }
            };
            
            this.TextureStandWomen = game.Content.Load<Texture2D>("Character/UrbanCharactersWomen");
            this.SpriteContentStandWomen = new Dictionary<CharacterStandPart, SpriteSetup>
            {
                { CharacterStandPart.Stand01, new SpriteSetup { Cutout = new Rectangle(16 * 0, 16 * shiftY1, 16, 16)} },
                { CharacterStandPart.Stand02, new SpriteSetup { Cutout = new Rectangle(16 * 1, 16 * shiftY1, 16, 16)} },
                { CharacterStandPart.Stand03, new SpriteSetup { Cutout = new Rectangle(16 * 2, 16 * shiftY1, 16, 16)} },
                { CharacterStandPart.Stand04, new SpriteSetup { Cutout = new Rectangle(16 * 3, 16 * shiftY1, 16, 16)} }
            };
        }

    }
}