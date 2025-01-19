using System;
using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public interface IWorldMapTextures
{
    bool TryGetTextureAndCutout(
        MapLayer mapLayer, 
        int numberPart, 
        out Texture2D texture, 
        out Rectangle cutout);

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