using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly List<ICollider> _colliders = new();
        private readonly WorldManager _world;
        private readonly CharacterManager _characterManager;
        private List<ColliderBox2D> _worldColliders;

        public ColliderManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._characterManager = game.GetComponent<CharacterManager>();
            this._world = game.GetComponent<WorldManager>();
            
            this._colliders.Add(this._characterManager.Player);
            foreach (var npc in this._characterManager.CharacterNpCs)
            {
                this._colliders.Add(npc);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // reset all collisions
            foreach (var collider in this._colliders)
            {
                collider.ClearCollisions();
            }

            foreach (var collider in this._colliders)
            {
                foreach (var otherCollider in this._colliders
                             .Where(otherCollider => collider != otherCollider)
                             .Where(otherCollider => collider.Collider.Intersects(otherCollider.Collider)))
                {
                    collider.OnCollision(otherCollider);
                }
            }
            
            var playerPosition = this._characterManager.Player.Collider.Position;
            var x = ((int)playerPosition.X + 8) / 16;
            var y = ((int)playerPosition.Y + 8) / 16;
            
            this._worldColliders = this._world.GetCollidableObjects(x, y);
            
            foreach (var collider in this._worldColliders)
            {
                if (this._characterManager.Player.Collider.Intersects(collider))
                {
                    var resetX = (int)this._characterManager.Player.LastPosition.X - 
                                 (int)this._characterManager.Player.Collider.Position.X;
                    var resetY = (int)this._characterManager.Player.LastPosition.Y -
                                 (int)this._characterManager.Player.Collider.Position.Y;
                    
                    // TODO: Prüfen für negative laufrichtung
                    
                    var round = new Vector2(
                        (int)this._characterManager.Player.LastPosition.X, 
                        (int)this._characterManager.Player.LastPosition.Y);
                    
                    this._characterManager.Player.Collider.Position = round;
                    this._characterManager.Player.IsCollide = true;
                }
                else
                {
                    this._characterManager.Player.IsCollide = false;
                }
            }
        }
        
        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix();

            foreach (var collider in this._worldColliders)
            {
                this._spriteBatch.DrawRectangle(
                    collider.Position, 
                    new SizeF(16f, 16f),
                    Color.Orange, 
                    1f);
            }
            
            this._spriteBatch.DrawRectangle(
                this._characterManager.Player.Collider.Position, 
                new SizeF(16f, 16f),
                Color.Red, 
                1f);
            
            this._spriteBatch.End();
        }
    }
}