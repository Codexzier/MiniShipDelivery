using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures;

public interface IMapEditableContent
{
    SpriteSetup GetSprite(MapLayer mapLayer, int numberPart);

    bool IsLayer(MapLayer mapLayer);
    MapLayer[] GetMapLayers();
    
    Texture2D Texture { get; }
    int NumberPartForIcon { get; }
    
    Type EnumType { get; }
}