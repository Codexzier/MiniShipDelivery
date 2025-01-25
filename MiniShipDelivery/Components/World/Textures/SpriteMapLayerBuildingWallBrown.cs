using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures;

public class SpriteMapLayerBuildingWallBrown(SpriteBaseBuildingWalls spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart)
    {
        return spriteBase.GetSprite(MapLayer.BuildingBrown, numberPart);
    }

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;

    public Texture2D Texture { get; } = spriteBase.Texture;
    public int NumberPartForIcon { get; } = spriteBase.NumberPartForIcon;
    public Type EnumType { get; } = typeof(BuildingWallPart);
    public MapLayer Layer => MapLayer.BuildingBrown;
}