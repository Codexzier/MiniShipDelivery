using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Emote
{
    [Obsolete("Bin noch nicht sicher, ob ich das noch brauche.")]
    public class EmoteManager(AssetManager assetManager)
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 position, EmotePart tilemapPart)
        {
            // draw the emote
            //assetManager.Draw(spriteBatch, new Vector2(position.X, position.Y - 16), tilemapPart);
        }
    }
}