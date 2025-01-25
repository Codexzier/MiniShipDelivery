using System;
using CodexzierGameEngine.DataModels.World;

namespace MiniShipDelivery.Components.World;

public class MissingSpriteAndCutout(int numberPart, MapLayer mapLayer)
    : Exception($"NumberPart: {numberPart} has not been cut out of {mapLayer}");