using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public class SpriteBaseBuildingWalls(Game game) : ISpriteContent<BuildingWallPart>
{
    public IDictionary<BuildingWallPart, SpriteSetup> SpriteContent { get; } = new Dictionary<BuildingWallPart, SpriteSetup>
    {
        { BuildingWallPart.SmallTop, new SpriteSetup { Cutout = new Rectangle(0, 0, 16, 16), IsTopLayer = true} },
        { BuildingWallPart.SmallMiddle1, new SpriteSetup { Cutout = new Rectangle(0, 16, 16, 16), IsTopLayer = true} },
        { BuildingWallPart.SmallMiddle2, new SpriteSetup { Cutout = new Rectangle(0, 32, 16, 16), IsTopLayer = true }},
        { BuildingWallPart.SmallDown, new SpriteSetup { Cutout = new Rectangle(0, 48, 16, 16), IsBarrier = true }},
            
        { BuildingWallPart.WithDecorTopLeft, new SpriteSetup { Cutout = new Rectangle(16, 0, 16, 16), IsTopLayer = true} },
        { BuildingWallPart.WithDecorTopMiddle, new SpriteSetup { Cutout = new Rectangle(32, 0, 16, 16), IsTopLayer = true} },
        { BuildingWallPart.WithDecorTopRight, new SpriteSetup { Cutout = new Rectangle(48, 0, 16, 16), IsTopLayer = true }},
            
        { BuildingWallPart.WithDecorMiddle1Left, new SpriteSetup { Cutout = new Rectangle(16, 16, 16, 16), IsTopLayer = true }},
        { BuildingWallPart.WithDecorMiddle1Middle, new SpriteSetup { Cutout = new Rectangle(32, 16, 16, 16), IsTopLayer = true }},
        { BuildingWallPart.WithDecorMiddle1Right, new SpriteSetup { Cutout = new Rectangle(48, 16, 16, 16), IsTopLayer = true }},
            
        { BuildingWallPart.WithDecorMiddle2Left, new SpriteSetup { Cutout = new Rectangle(16, 32, 16, 16), IsTopLayer = true }},
        { BuildingWallPart.WithDecorMiddle2Middle, new SpriteSetup { Cutout = new Rectangle(32, 32, 16, 16), IsTopLayer = true }},
        { BuildingWallPart.WithDecorMiddle2Right, new SpriteSetup { Cutout = new Rectangle(48, 32, 16, 16), IsTopLayer = true }},
            
        { BuildingWallPart.WithDecorDownLeft, new SpriteSetup { Cutout = new Rectangle(16, 48, 16, 16), IsBarrier = true }},
        { BuildingWallPart.WithDecorDownMiddle, new SpriteSetup { Cutout = new Rectangle(32, 48, 16, 16), IsBarrier = true }},
        { BuildingWallPart.WithDecorDownRight, new SpriteSetup { Cutout = new Rectangle(48, 48, 16, 16), IsBarrier = true }},
    };

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("RpgUrban/BuildingWalls");
    public int NumberPartForIcon => (int) BuildingWallPart.SmallTop;

    public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
    {
        BuildingWallPart buildingWallPart = (BuildingWallPart)numberPart;
        if (!this.SpriteContent.ContainsKey(buildingWallPart))
        {
            buildingWallPart = BuildingWallPart.SmallTop;
        }
        
        var mapTile = this.SpriteContent[buildingWallPart];
        if (mapLayer == MapLayer.BuildingRed)
        {
            return mapTile;
        }

        var rec = mapTile.Cutout;
        switch (mapLayer)
        {
            case MapLayer.BuildingBrown:
                rec = new Rectangle(mapTile.Cutout.X, mapTile.Cutout.Y + 16 * 4, 16, 16);

                break;
        }
        
        return new SpriteSetup{ Cutout = rec, IsTopLayer = mapTile.IsTopLayer};
    }
}