using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using MiniShipDelivery.Components.Character;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderManager : GameComponent
    {
        private readonly List<ICollider> _colliders = new();

        public ColliderManager(Game game, CharacterManager characterManager) : base(game)
        {
            this._colliders.Add(characterManager.Player);
            foreach (var npc in characterManager.CharacterNpCs)
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
        }
    }
}