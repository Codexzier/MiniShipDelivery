using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.GameMenuMap;

public class GameMenuMapOptions(Game game) : BaseMenu(game,
    new Vector2(
        GlobaleGameParameters.ScreenWidthHalf - 100, 
        GlobaleGameParameters.ScreenHeightHalf - 60),
    new SizeF(200, 120))
{
    private readonly WorldManager _map = game.GetComponent<WorldManager>();
    private readonly Vector2 _center = new(100, 60);
    private readonly SizeF _tileSize = new(3, 3);

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        DrawBaseFrame(spriteBatch, MenuFrameType.Type1);

        if (this._map.Map.MiniMapChunks.Any())
        {
            foreach (var mapChunk in this._map.Map.MiniMapChunks)
            {
                for (int indexY = 0; indexY < mapChunk.MiniMap.Length; indexY++)
                {
                    for (int indexX = 0; indexX < mapChunk.MiniMap[indexY].Length; indexX++)
                    {
                        var tilePosition = new Vector2(
                            indexX * this._tileSize.Width - 15, 
                            indexY * this._tileSize.Height - 15);
                        
                        spriteBatch.FillRectangle(
                            this.Position + this.Bus.Camera.GetPosition() + this._center + tilePosition,
                            this._tileSize,
                            mapChunk.MiniMap[indexY][indexX],
                            1f);
                    }
                }
            }
        }
        
        spriteBatch.FillRectangle(
            this.Position + this.Bus.Camera.GetPosition() + this._center,
            new SizeF(2, 2),
            Color.Blue);
    }
}