using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components
{
    public class InputManager
    {
        public InputManager()
        {
        }

        public Vector2 MovementCharacter { get; private set; }
        public Vector2 MovementMouse { get; private set; }

        internal void Update(GameTime gameTime)
        {
            var elapsedSec = gameTime.GetElapsedSeconds();

            this.MovementCharacter = this.GetMovement();
            this.MovementMouse = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public Vector2 GetMovement()
        {
            var movement = Vector2.Zero;

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                movement.Y = -1;
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                movement.Y = 1;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                movement.X = -1;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                movement.X = 1;
            }

            return movement;
        }

        

        internal bool HasPressToClose()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        
    }
}
