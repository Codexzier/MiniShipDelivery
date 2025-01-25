using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures;

public class SpriteMapLayerBrownRoof(SpriteBaseTilemap spriteBase) : IMapEditableContent
{
    private readonly SpriteBaseTilemap _spriteBase = spriteBase;

    public SpriteSetup GetSprite(int numberPart)
    {
        return this._spriteBase.GetSprite(MapLayer.BrownRoof, numberPart);
    }

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public Texture2D Texture => this._spriteBase.Texture;
    public int NumberPartForIcon => (int)TilemapPart.AroundOutBorder;
    public Type EnumType { get; } = typeof(TilemapPart);

    public MapLayer Layer => MapLayer.BrownRoof;
}