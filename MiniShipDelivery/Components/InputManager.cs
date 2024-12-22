using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components
{
    public class InputManager
    {
        private readonly float _scaledMouseMoveX;
        private readonly float _scaleMouseMoveY;

        public InputManager(float scaledMouseMoveX, float scaleMouseMoveY)
        {
            this._scaledMouseMoveX = scaledMouseMoveX;
            this._scaleMouseMoveY = scaleMouseMoveY;
        }

        public Vector2 MovementCharacter { get; private set; }
        public Vector2 MousePosition { get; private set; }

        internal void Update(GameTime gameTime)
        {
            var elapsedSec = gameTime.GetElapsedSeconds();

            this.MovementCharacter = this.GetMovement();
            
            // mouse state
            var mouseState = Mouse.GetState();
            this.MousePosition = new Vector2(
                mouseState.X * this._scaledMouseMoveX, 
                mouseState.Y * this._scaleMouseMoveY);
            
            this.MouseLeftButton = mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool MouseLeftButton { get; private set; }

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
