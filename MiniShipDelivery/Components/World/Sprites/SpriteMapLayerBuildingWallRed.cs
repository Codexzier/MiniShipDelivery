using System;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public class SpriteMapLayerBuildingWallRed(SpriteBaseBuildingWalls spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart)
    {
        return spriteBase.GetSprite(MapLayer.BuildingRed, numberPart);
    }

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;

    public Texture2D Texture { get; } = spriteBase.Texture;
    public int NumberPartForIcon { get; } = spriteBase.NumberPartForIcon;
    public Type EnumType { get; } = typeof(BuildingWallPart);
    public MapLayer Layer => MapLayer.BuildingRed;
    public int SpriteCount => spriteBase.SpriteContent.Count;
    public bool HasSpecificNumberPart => true;
    
    public int[] GetNumberParts() => spriteBase.SpriteContent.Select(s => (int)s.Key).ToArray();
}