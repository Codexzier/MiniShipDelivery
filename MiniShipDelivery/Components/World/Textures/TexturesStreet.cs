using System;
using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures;

public class TexturesStreet(Game game) : ISpriteContent<StreetPart>, IMapEditableContent
{
    public IDictionary<StreetPart, Rectangle> SpriteContent { get; } = new Dictionary<StreetPart, Rectangle>
    {
        // { StreetPart.None, new Rectangle(0, 0, 0, 0) },
                
        { StreetPart.ZebraCrossingVerticalTop, new Rectangle(0, 0, 16, 16) },
        { StreetPart.ZebraCrossingVerticalMiddle, new Rectangle(0, 16, 16, 16) },
        { StreetPart.ZebraCrossingVerticalDown, new Rectangle(0, 32, 16, 16) },
                
        { StreetPart.StreetVerticalTop, new Rectangle(16, 0, 16, 16) },
        { StreetPart.StreetVerticalMiddle, new Rectangle(16, 16, 16, 16) },
        { StreetPart.StreetVerticalDown, new Rectangle(16, 32, 16, 16) },
                
        { StreetPart.StreetCross, new Rectangle(32, 0, 16, 16) },
        { StreetPart.Street01, new Rectangle(48, 0, 16, 16) },
        { StreetPart.StreetParking, new Rectangle(64, 0, 16, 16) },
        { StreetPart.Street02, new Rectangle(80, 0, 16, 16) },
        { StreetPart.StreetBicylceParking, new Rectangle(96, 0, 16, 16) },
                
        { StreetPart.ZebraCrossingHorizontalLeft, new Rectangle(32, 16, 16, 16) },
        { StreetPart.ZebraCrossingHorizontalMiddle, new Rectangle(48, 16, 16, 16) },
        { StreetPart.ZebraCrossingHorizontalRight, new Rectangle(64, 16, 16, 16) },
                
        { StreetPart.StreetHorizontalLeft, new Rectangle(32, 32, 16, 16) },
        { StreetPart.StreetHorizontalMiddle, new Rectangle(48, 32, 16, 16) },
        { StreetPart.StreetHorizontalRight, new Rectangle(64, 32, 16, 16) },
                
        { StreetPart.ParkingLineTopLeft, new Rectangle(80, 16, 16, 16) },
        { StreetPart.ParkingLineTopRight, new Rectangle(96, 16, 16, 16) },
        { StreetPart.ParkingLineDownLeft, new Rectangle(80, 32, 16, 16) },
        { StreetPart.ParkingLineDownRight, new Rectangle(96, 32, 16, 16) },
                
        { StreetPart.ParkingLineInnenTopLeft, new Rectangle(112, 16, 16, 16) },
        { StreetPart.ParkingLineInnenTopRight, new Rectangle(128, 16, 16, 16) },
        { StreetPart.ParkingLineInnenDownLeft, new Rectangle(112, 32, 16, 16) },
        { StreetPart.ParkingLineInnenDownRight, new Rectangle(128, 32, 16, 16) },
    };

   

    public Texture2D Texture { get; } = game.Content.Load<Texture2D>("RpgUrban/Street");
    public int NumberPartForIcon { get; } = (int)StreetPart.StreetParking;
    public Type EnumType { get; } = typeof(StreetPart);

    public Rectangle GetSprite(MapLayer mapLayer, int numberPart)
    {
        StreetPart streetPart = (StreetPart)numberPart;
        
        if(!this.SpriteContent.ContainsKey(streetPart))
        {
            return Rectangle.Empty;
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