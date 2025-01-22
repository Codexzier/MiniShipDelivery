using System;
using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures;

public class TexturesStreet(Game game) : ISpriteContent<StreetPart>, IMapEditableContent
{
    public IDictionary<StreetPart, SpriteSetup> SpriteContent { get; } = new Dictionary<StreetPart, SpriteSetup>
    {
        // { StreetPart.None, new Rectangle(0, 0, 0, 0) },
                
        { StreetPart.ZebraCrossingVerticalTop, new SpriteSetup { Cutout = new Rectangle(0, 0, 16, 16) }},
        { StreetPart.ZebraCrossingVerticalMiddle, new SpriteSetup { Cutout = new Rectangle(0, 16, 16, 16)} },
        { StreetPart.ZebraCrossingVerticalDown, new SpriteSetup { Cutout = new Rectangle(0, 32, 16, 16) }},
                
        { StreetPart.StreetVerticalTop, new SpriteSetup { Cutout = new Rectangle(16, 0, 16, 16) }},
        { StreetPart.StreetVerticalMiddle, new SpriteSetup { Cutout = new Rectangle(16, 16, 16, 16) }},
        { StreetPart.StreetVerticalDown, new SpriteSetup { Cutout = new Rectangle(16, 32, 16, 16) }},
                
        { StreetPart.StreetCross, new SpriteSetup { Cutout = new Rectangle(32, 0, 16, 16) }},
        { StreetPart.Street01, new SpriteSetup { Cutout = new Rectangle(48, 0, 16, 16) }},
        { StreetPart.StreetParking, new SpriteSetup { Cutout = new Rectangle(64, 0, 16, 16) }},
        { StreetPart.Street02, new SpriteSetup { Cutout = new Rectangle(80, 0, 16, 16) }},
        { StreetPart.StreetBicylceParking, new SpriteSetup { Cutout = new Rectangle(96, 0, 16, 16) }},
                
        { StreetPart.ZebraCrossingHorizontalLeft, new SpriteSetup { Cutout = new Rectangle(32, 16, 16, 16) }},
        { StreetPart.ZebraCrossingHorizontalMiddle, new SpriteSetup { Cutout = new Rectangle(48, 16, 16, 16) }},
        { StreetPart.ZebraCrossingHorizontalRight, new SpriteSetup { Cutout = new Rectangle(64, 16, 16, 16) }},
                
        { StreetPart.StreetHorizontalLeft, new SpriteSetup { Cutout = new Rectangle(32, 32, 16, 16) }},
        { StreetPart.StreetHorizontalMiddle, new SpriteSetup { Cutout = new Rectangle(48, 32, 16, 16) }},
        { StreetPart.StreetHorizontalRight, new SpriteSetup { Cutout = new Rectangle(64, 32, 16, 16) }},
                
        { StreetPart.ParkingLineTopLeft, new SpriteSetup { Cutout = new Rectangle(80, 16, 16, 16) }},
        { StreetPart.ParkingLineTopRight, new SpriteSetup { Cutout = new Rectangle(96, 16, 16, 16) }},
        { StreetPart.ParkingLineDownLeft, new SpriteSetup { Cutout = new Rectangle(80, 32, 16, 16) }},
        { StreetPart.ParkingLineDownRight, new SpriteSetup { Cutout = new Rectangle(96, 32, 16, 16) }},
                
        { StreetPart.ParkingLineInnenTopLeft, new SpriteSetup { Cutout = new Rectangle(112, 16, 16, 16) }},
        { StreetPart.ParkingLineInnenTopRight, new SpriteSetup { Cutout = new Rectangle(128, 16, 16, 16) }},
        { StreetPart.ParkingLineInnenDownLeft, new SpriteSetup { Cutout = new Rectangle(112, 32, 16, 16) }},
        { StreetPart.ParkingLineInnenDownRight, new SpriteSetup { Cutout = new Rectangle(128, 32, 16, 16) }},
    };

   

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("RpgUrban/Street");
    public int NumberPartForIcon { get; } = (int)StreetPart.StreetParking;
    public Type EnumType { get; } = typeof(StreetPart);

    public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
    {
        StreetPart streetPart = (StreetPart)numberPart;
        
        if(!this.SpriteContent.ContainsKey(streetPart))
        {
            return SpriteSetup.Empty;
        }
        
        var mapTile = this.SpriteContent[streetPart];

        if (mapLayer != MapLayer.Street)
        {
            throw new WrongTextureSetup($"Layer {mapLayer} not supported in texture street");
        }
        
        return mapTile;
    }

    public bool IsLayer(MapLayer mapLayer) => mapLayer == MapLayer.Street;
    public MapLayer[] GetMapLayers() => [MapLayer.Street];
}