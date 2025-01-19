using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public class WorldMapTextures(Game game) : IWorldMapTextures
{
    private readonly IMapEditableContent[] _editorContents =
    [
        new TexturesStreet(game),
        new TexturesTilemap(game),
        new TexturesBuildingWalls(game)
    ];

    public bool TryGetTextureAndCutout(MapLayer mapLayer, int numberPart, out Texture2D texture, out Rectangle cutout)
    {
        texture = null;
        cutout = Rectangle.Empty;

        foreach (var editorContent in this._editorContents)
        {
            if(!editorContent.IsLayer(mapLayer)) continue;
            
            texture = editorContent.Texture;
            cutout = editorContent.GetSprite(mapLayer, numberPart);
            break;
        }

        if (texture == null || cutout == Rectangle.Empty)
        {
            throw new ArgumentOutOfRangeException(
                nameof(mapLayer), 
                numberPart, 
                "Missing Texture Layer");
        }

        return texture != null && cutout != Rectangle.Empty;
    }

    public IEnumerable<EditableEnvironmentItem> GetEditableEnvironments()
    {
        if (this._editorContents == null || this._editorContents.Length == 0)
        {
            throw new MissingMemberException("Missing Texture Layer");
        }

        var list = new List<EditableEnvironmentItem>();
        foreach (var editorContent in this._editorContents)
        {
            var layers = editorContent.GetMapLayers();
            foreach (var mapLayer in editorContent.GetMapLayers())
            {
                list.Add(new EditableEnvironmentItem(
                    mapLayer,
                    editorContent.NumberPartForIcon,
                    editorContent.Texture,
                    editorContent.GetSprite(mapLayer, editorContent.NumberPartForIcon),
                    editorContent.EnumType));
            }
        }

        return list;
    }
}