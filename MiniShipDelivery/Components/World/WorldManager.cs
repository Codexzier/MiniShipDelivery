using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Objects;
using MiniShipDelivery.Components.World.Sprites;

namespace MiniShipDelivery.Components.World
{
    public class WorldManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly WorldMapAdjuster _adjuster;
        
        public readonly WorldMap Map;
        
        public WorldManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            
            WorldMapHelper.SetMapTextures(new WorldMapSprites(game));
            
            this.Map = new WorldMap();
            this._adjuster = new WorldMapAdjuster(this.Map);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            this.Map.Update();
            
            // HUD depended content
            if(GlobalGameParameters.HudView != HudOptionView.MapEditor) return;
            if(WorldMapAdjuster.SelectedNumberPart < 0) return;
            
            this._adjuster.UpdateSetMapTile();
        }
       
        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix();
            
            this.Map.DrawAllLayers(this._spriteBatch);
            this._adjuster.Draw(this._spriteBatch);
            
            this._spriteBatch.End();
        }
        
        private Vector2[] _chunkNeighbors =new Vector2[]
        {
            new (0, 0),
            new (-1, 0), 
            new (1, 0), 
            new (0, -1), 
            new (0, 1)
        };

        public List<ColliderBox2D> GetCollidableObjects(int x, int y)
        {
            var collidableObjects = new List<ColliderBox2D>();
            
            // check all collidable objects around the player
            for (int posY = 0; posY < 3; posY++)
            {
                for (int posX = 0; posX < 3; posX++)
                {
                    // pick on top fo the field.
                    if(this.Map.TryTilemap(
                           ApplicationBus.Instance.MapChunkIndex,
                           MapLayer.Colliders, 
                           x - 1 + posX, 
                           y - 1 + posY, 
                           out var mapTile))
                    {
                        if( mapTile.AssetNumber == 0) continue;

                        var colliderBox2D = GetColliderBox2DByAssetNumber(mapTile);

                        if (collidableObjects == null)
                        {
                            throw new System.Exception(
                                "ColliderBox2D not implemented for AssetNumber: " + 
                                mapTile.AssetNumber);
                        }
                        
                        collidableObjects.Add(colliderBox2D);
                    }
                }
            }
            
            return collidableObjects;
        }

        private static ColliderBox2D GetColliderBox2DByAssetNumber(MapTile mapTile)
        {
            ColliderBox2D colliderBox2D = null;
            switch (mapTile.AssetNumber)
            {
                case 1:
                {
                    colliderBox2D = new ColliderBox2D(16, 16,
                        mapTile.Position.TilePositionToVector() 
                        + ApplicationBus.Instance.MapChunkPosition,
                        0,
                        0);
                    break;
                }
                case 2:
                {
                    colliderBox2D = new ColliderBox2D(16, 8,
                        mapTile.Position.TilePositionToVector() 
                        + ApplicationBus.Instance.MapChunkPosition,
                        0,
                        0);
                    break;
                }
                case 3:
                {
                    colliderBox2D = new ColliderBox2D(16, 8,
                        mapTile
                            .Position
                            .TilePositionToVector() + new Vector2(0, 8)
                                                    + ApplicationBus.Instance.MapChunkPosition,
                        0,
                        0);
                    break;
                }
            }

            return colliderBox2D;
        }
    }
}