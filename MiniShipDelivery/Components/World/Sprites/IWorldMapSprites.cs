using System;
using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Sprites;

public interface IWorldMapSprites
{
    bool TryGetTextureAndCutout(
        MapLayer mapLayer, 
        int numberPart, 
        out Texture2D texture, 
        out Rectangle cutout,
        out bool drawTop);

    IEnumerable<EditableEnvironmentItem> GetEditableEnvironments();
}

public class EditableEnvironmentItem(
    MapLayer mapLayer,
    int numberPartForIcon,
    Texture2D texture,
    Rectangle cutout,
    Type enumType)
{
    public MapLayer Layer { get; } = mapLayer;

    public int NumberPartIcon { get; } = numberPartForIcon;
    public Rectangle Cutout { get; } = cutout;
    public Texture2D Texture { get; } = texture;
    
    public Type EnumType { get; } = enumType;
}