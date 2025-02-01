using System.Collections;
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
            this._adjuster = new WorldMapAdjuster(game, this.Map);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            this.Map.Update();
            
            // HUD depended content
            if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
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
                           MapLayer.Colliders, 
                           x - 1 + posX, 
                           y - 1 + posY, 
                           out var mapTile))
                    {
                        if( mapTile.AssetNumber == 0) continue;
                        
                        collidableObjects.Add(new ColliderBox2D(16, 16){ 
                            Position = mapTile.Position.TilePositionToVector()
                        });
                    }
                }
            }
            
            return collidableObjects;
        }
    }
}