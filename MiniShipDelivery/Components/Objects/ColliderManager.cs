using System;
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
            
            var forecast = this._characterManager.Player.Collider;
            var direction = this._characterManager.Player.Direction;
            
            foreach (var collider in this._worldColliders)
            {
                if (forecast.Intersects(collider) &&
                    !this._characterManager.Player.IsCollide)
                {
                    bool hasSet = false;

                    Vector2 resetPosition = direction switch
                    {
                        {X: > 0, Y: 0} => new Vector2(collider.Position.X - 16, forecast.Position.Y),
                        {X: < 0, Y: 0} => new Vector2(collider.Position.X + 16, forecast.Position.Y),
                        {X: 0, Y: > 0} => new Vector2(forecast.Position.X, collider.Position.Y - 16),
                        {X: 0, Y: < 0} => new Vector2(forecast.Position.X, collider.Position.Y + 16),
                        _ => Vector2.Zero
                    };

                    if (resetPosition != Vector2.Zero)
                    {
                        this._characterManager.Player.Collider.Position = resetPosition;
                    }
                    
                    
                    // if (direction.X > 0)
                    // {
                    //     //this._characterManager.Player.Collider.Position = new Vector2(collider.Position.X - 16, forecast.Position.Y);
                    //     this._directions[0](forecast.Position, collider, this._characterManager.Player.Collider);
                    //     hasSet = true;
                    // }
                    //
                    // if (direction.X < 0 && !hasSet)
                    // {
                    //     //this._characterManager.Player.Collider.Position = new Vector2(collider.Position.X + 16, forecast.Position.Y);
                    //     this._directions[1](forecast.Position, collider, this._characterManager.Player.Collider);
                    //     hasSet = true;
                    // }
                    //
                    // if (direction.Y > 0 && !hasSet)
                    // {
                    //     //this._characterManager.Player.Collider.Position = new Vector2(forecast.Position.X, collider.Position.Y - 16);
                    //     this._directions[2](forecast.Position, collider, this._characterManager.Player.Collider);
                    //     hasSet = true;
                    // }
                    //
                    // if (direction.Y < 0 && !hasSet)
                    // {
                    //     //this._characterManager.Player.Collider.Position = new Vector2(forecast.Position.X, collider.Position.Y + 16);
                    //     this._directions[3](forecast.Position, collider, this._characterManager.Player.Collider);
                    // }
                    
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