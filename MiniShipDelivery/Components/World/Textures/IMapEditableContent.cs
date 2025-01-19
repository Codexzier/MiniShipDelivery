using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public interface IMapEditableContent
{
    Rectangle GetSprite(MapLayer mapLayer, int numberPart);

    bool IsLayer(MapLayer mapLayer);
    MapLayer[] GetMapLayers();
    
    Texture2D Texture { get; }
    int NumberPartForIcon { get; }
    
    Type EnumType { get; }
}