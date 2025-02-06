using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Sprites;

public class WorldMapSprites : IWorldMapSprites
{
    private readonly IMapEditableContent[] _editorContents;

    public WorldMapSprites(Game game)
    {
        var spriteBaseTilemap = new SpriteBaseTilemap(game);
        var spriteBaseBuildingWalls = new SpriteBaseBuildingWalls(game);

        this._editorContents =
        [
            new SpriteMapStreetV2(game),

            new SpriteMapLayerSidewalk(spriteBaseTilemap),
            new SpriteMapLayerGrass(spriteBaseTilemap),
            new SpriteMapLayerBuildingWallRed(spriteBaseBuildingWalls),
            new SpriteMapLayerBuildingWallBrown(spriteBaseBuildingWalls),
            new SpriteMapLayerGrayRoof(spriteBaseTilemap),
            new SpriteMapLayerBrownRoof(spriteBaseTilemap),
            new SpriteMapLayerWindow(game),
            new SpriteMapLayerDoor(game),
            new SpriteMapLayerStreetObjects(game),
            new SpriteMapLayerStreetLampsAndTrafficLights(game),
            new SpriteMapLayerTrees(game),

            new SpriteMapLayerCollider(game)
        ];
    }

    public bool TryGetTextureAndCutout(
        MapLayer mapLayer,
        int numberPart,
        out Texture2D texture,
        out Rectangle cutout,
        out bool drawTop)
    {
        texture = null;
        cutout = Rectangle.Empty;
        drawTop = false;

        foreach (var editorContent in this._editorContents)
        {
            if (!editorContent.IsLayer(mapLayer)) continue;

            texture = editorContent.Texture;

            var mapTile = editorContent.GetSprite(numberPart);
            cutout = mapTile.Cutout;
            drawTop = mapTile.IsTopLayer;
            break;
        }

        if (texture == null || cutout == Rectangle.Empty)
        {
            throw new ArgumentOutOfRangeException(
                nameof(mapLayer),
                numberPart,
                "Missing Sprite Layer");
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
            list.Add(new EditableEnvironmentItem(
                editorContent.Layer,
                editorContent.Texture,
                editorContent.GetSprite(editorContent.NumberPartForIcon).Cutout,
                editorContent.GetNumberParts()));
        }

        return list;
    }

    public int[] GetListOfValidateTileNumbers(MapLayer mapLayer)
    {
        return this._editorContents.First(w => w.IsLayer(mapLayer)).GetNumberParts();
    }

    public MapLayer[] GetLayers() => this._editorContents.Select(w => w.Layer).ToArray();
}