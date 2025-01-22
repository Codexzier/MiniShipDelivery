using System;
using CodexzierGameEngine.DataModels.World;

namespace MiniShipDelivery.Components.World;

public class MissingMapTexturesAndCutout(int numberPart, MapLayer mapLayer)
    : Exception($"Numberpart: {numberPart} has not been cut out of {mapLayer}");