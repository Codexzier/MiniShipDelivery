using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Helpers;

public static class SimpleThinksHelper
{
    public static CameraManager CameraManagerInstance { get; set; }
    public static Color BoolToColor(bool value) => value ? Color.LightGray : Color.Transparent;
    

    public static void BeginWithCameraViewMatrix(
        this SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(
            transformMatrix: CameraManagerInstance.Camera.GetViewMatrix(),
            samplerState: SamplerState.PointClamp);
    }

    public static Vector2 TilePositionToVector(this TilePosition tilePosition)
    {
        return new Vector2(tilePosition.X, tilePosition.Y);
    }
}