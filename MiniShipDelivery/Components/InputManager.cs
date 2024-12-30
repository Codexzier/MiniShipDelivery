using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components
{
    public class InputManager : GameComponent
    {
        private readonly float _scaledMouseMoveX;
        private readonly float _scaleMouseMoveY;

        private bool _mouseLeftButtonReleased;
        private bool _mouseLeftButtonHasPressed;

        public InputManager(
            Game game,
            float scaledMouseMoveX, float scaleMouseMoveY) : base(game)
        {
            this._scaledMouseMoveX = scaledMouseMoveX;
            this._scaleMouseMoveY = scaleMouseMoveY;
            
            this.Inputs = new InputData();
        }

        public InputData Inputs { get; set; }

        public override void Update(GameTime gameTime)
        {
            var elapsedSec = gameTime.GetElapsedSeconds();

            this.Inputs.MovementCharacter = this.GetMovement();
            
            // mouse state
            var mouseState = Mouse.GetState();
            this.Inputs.MousePosition = new Vector2(
                mouseState.X * this._scaledMouseMoveX, 
                mouseState.Y * this._scaleMouseMoveY);

            this._mouseLeftButton = mouseState.LeftButton == ButtonState.Pressed;
            if(this._mouseLeftButton && !this._mouseLeftButtonHasPressed)
            {
                this._mouseLeftButtonHasPressed = true;
            }
            if (this._mouseLeftButtonHasPressed && mouseState.LeftButton == ButtonState.Released)
            {
                this._mouseLeftButtonReleased = true;
                this._mouseLeftButtonHasPressed = false;
            }
            
            this.Inputs.MouseRightButton = mouseState.RightButton == ButtonState.Pressed;
        }

        private bool _mouseLeftButton;


        private Vector2 GetMovement()
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

        internal void ResetMouseLeftButtonReleased()
        {
            this._mouseLeftButtonReleased = false;
        }

        internal bool GetMouseLeftButtonReleasedState()
        {
            if (this._mouseLeftButtonReleased)
            {
                this._mouseLeftButtonReleased = false;
                return true;
            }

            return false;
        }
    }
}
