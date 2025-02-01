using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.GameMenuMap;

public class GameMenuMapOptions : BaseMenu
{
    private readonly CameraManager _camera;
    private readonly WorldManager _map;

    public GameMenuMapOptions(Game game)
        : base(
            game,
            new Vector2(GlobaleGameParameters.ScreenWidthHalf - 100, GlobaleGameParameters.ScreenHeightHalf - 60),
            new Size(200, 120))
    {
        this._camera = game.GetComponent<CameraManager>();
        this._map = game.GetComponent<WorldManager>();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        var tileSize = 3;

        if (this._map.Map.MiniMapChunks.Any())
        {
            foreach (var mapChunk in this._map.Map.MiniMapChunks)
            {
                for (int indexY = 0; indexY < mapChunk.MiniMap.Length; indexY++)
                {
                    for (int indexX = 0; indexX < mapChunk.MiniMap[indexY].Length; indexX++)
                    {
                        spriteBatch.FillRectangle(
                            new RectangleF(
                                this.Position.X + this._camera.Camera.Position.X + (this.Size.Width / 2) + (indexX * tileSize), 
                                this.Position.Y + this._camera.Camera.Position.Y + (this.Size.Height / 2) + (indexY * tileSize), 
                                tileSize, 
                                tileSize), 
                            mapChunk.MiniMap[indexY][indexX]);
                    }
                }
            }
        }
        
        
        spriteBatch.DrawRectangle(
            new RectangleF(
                this.Position.X + this._camera.Camera.Position.X, 
                this.Position.Y + this._camera.Camera.Position.Y, 
                this.Size.Width, 
                this.Size.Height), 
            Color.Chocolate);
    }
}