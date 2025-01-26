﻿using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public interface IMapEditableContent
{
    SpriteSetup GetSprite(int numberPart);
    bool IsLayer(MapLayer mapLayer);
    Texture2D Texture { get; }
    int NumberPartForIcon { get; }
    
    Type EnumType { get; }
    MapLayer Layer { get; }
    int SpriteCount { get; }
    bool HasSpecificNumberPart { get; }
    int[] GetNumberParts();
}