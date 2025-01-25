using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Sidewalk", 2, true)]
public class SpritesMapLayerSidewalk(SpriteBaseTilemap spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart)
    {
        return spriteBase.GetSprite(MapLayer.Sidewalk, numberPart);
    }

    public bool IsLayer(MapLayer mapLayer) => MapLayer.Sidewalk == mapLayer;

    public Texture2D Texture => spriteBase.Texture;
    public int NumberPartForIcon => (int)TilemapPart.AroundOutBorder;
    public Type EnumType { get; } = typeof(TilemapPart);
    public MapLayer Layer => MapLayer.Sidewalk;
}