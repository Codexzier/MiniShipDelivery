using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures;

public class TexturesBuildingWalls(Game game) : ISpriteContent<BuildingWallPart>, IMapEditableContent
{
    public IDictionary<BuildingWallPart, Rectangle> SpriteContent { get; } = new Dictionary<BuildingWallPart, Rectangle>
    {
        { BuildingWallPart.SmallElementTop, new Rectangle(0, 0, 16, 16) },
        { BuildingWallPart.SmallElementMiddle1, new Rectangle(0, 16, 16, 16) },
        { BuildingWallPart.SmallElementMiddle2, new Rectangle(0, 32, 16, 16) },
        { BuildingWallPart.SmallElementDown, new Rectangle(0, 48, 16, 16) },
            
        { BuildingWallPart.ElementWithDecorTopLeft, new Rectangle(16, 0, 16, 16) },
        { BuildingWallPart.ElementWithDecorTopMiddle, new Rectangle(32, 0, 16, 16) },
        { BuildingWallPart.ElementWithDecorTopRight, new Rectangle(48, 0, 16, 16) },
            
        { BuildingWallPart.ElementWithDecorMiddle1Left, new Rectangle(16, 16, 16, 16) },
        { BuildingWallPart.ElementWithDecorMiddle1Middle, new Rectangle(32, 16, 16, 16) },
        { BuildingWallPart.ElementWithDecorMiddle1Right, new Rectangle(48, 16, 16, 16) },
            
        { BuildingWallPart.ElementWithDecorMiddle2Left, new Rectangle(16, 32, 16, 16) },
        { BuildingWallPart.ElementWithDecorMiddle2Middle, new Rectangle(32, 32, 16, 16) },
        { BuildingWallPart.ElementWithDecorMiddle2Right, new Rectangle(48, 32, 16, 16) },
            
        { BuildingWallPart.ElementWithDecorDownLeft, new Rectangle(16, 48, 16, 16) },
        { BuildingWallPart.ElementWithDecorDownMiddle, new Rectangle(32, 48, 16, 16) },
        { BuildingWallPart.ElementWithDecorDownRight, new Rectangle(48, 48, 16, 16) },
    };

    public bool IsLayer(MapLayer mapLayer)
    {
        return this.GetMapLayers().Contains(mapLayer);
    }

    public MapLayer[] GetMapLayers()
    {
        return [MapLayer.BuildingRed, MapLayer.BuildingBrown];
    }

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("RpgUrban/BuildingWalls");
    public int NumberPartForIcon { get; } = (int) BuildingWallPart.SmallElementTop;
    public Type EnumType { get; } = typeof(BuildingWallPart);

    public Rectangle GetSprite(MapLayer mapLayer, int numberPart)
    {
        BuildingWallPart buildingWallPart = (BuildingWallPart)numberPart;
        if (!this.SpriteContent.ContainsKey(buildingWallPart))
        {
            buildingWallPart = BuildingWallPart.SmallElementTop;
        }
        
        var mapTile = this.SpriteContent[buildingWallPart];
        if (mapLayer == MapLayer.BuildingRed)
        {
            return mapTile;
        }

        switch (mapLayer)
        {
            case MapLayer.BuildingBrown:
                mapTile.Y += 16 * 4;
                break;
        }
        
        return mapTile;
    }
}