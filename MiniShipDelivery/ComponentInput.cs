using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace MiniShipDelivery
{
    public class ComponentInput
    {
        private CharacterPlayer _player;

        public ComponentInput(CharacterPlayer player)
        {
            this._player = player;
        }

        public Vector2 GetMovement()
        {
            var movement = new Vector2(0, 0);

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                movement.Y = -1;
            }

            if(keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                movement.Y = 1;
            }

            if(keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                movement.X = -1;
            }

            if(keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                movement.X = 1;
            }

            return movement * this._player.Speed;
        }

        internal bool HasPressToClose()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        internal void Update(GameTime gameTime)
        {
            var elapsedSec = gameTime.GetElapsedSeconds();

            var movement = this.GetMovement();
            this._player.Position += movement * elapsedSec;
            this._player.Direction = movement;
        }
    }
}
