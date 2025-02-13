using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Input
{
    public class InputManager : GameComponent
    {
        private bool _mouseLeftButton;
        private Vector2 _mouseLeftButtonHasPressedPosition = Vector2.Zero;
        private bool _mouseLeftButtonReleased;
        private bool _mouseLeftButtonHasPressed;
        
        private bool _mouseRightButton;
        private Vector2 _mouseRightButtonHasPressedPosition = Vector2.Zero;
        private bool _mouseRightButtonReleased;
        private bool _mouseRightButtonHasPressed;

        private const int PlayerIndex = 0;

        private const float ScaledMouseMovingX = GlobalGameParameters.ScreenWidth / (float)GlobalGameParameters.PreferredBackBufferWidth;
        private const float ScaleMouseMovingY = GlobalGameParameters.ScreenHeight / (float)GlobalGameParameters.PreferredBackBufferHeight;

        private ApplicationBus Bus => ApplicationBus.Instance;

        private readonly InputTextController _inputTextController;
        private double _lastStart;

        public InputManager(Game game) : base(game)
        {
            ((InputData)this.Bus.Inputs).GetMouseButtonReleasedStateLeft = this.GetMouseLeftButtonReleasedState;   
            ((InputData)this.Bus.Inputs).GetMouseButtonReleasedStateRight = this.GetMouseRightButtonReleasedState;

            this._inputTextController = new InputTextController(game);
        }

        public override void Update(GameTime gameTime)
        {
            if(this.HasPressToClose()) this.Game.Exit();

            ((InputData)this.Bus.Inputs).MovementCharacter = this.GetMovement();
            
            // mouse state
            var mouseState = Mouse.GetState();
            ((InputData)this.Bus.Inputs).MousePosition = new Vector2(
                mouseState.X * ScaledMouseMovingX, 
                mouseState.Y * ScaleMouseMovingY);

            // mouse buttons left
            this._mouseLeftButton = mouseState.LeftButton == ButtonState.Pressed;
            if(this._mouseLeftButton && !this._mouseLeftButtonHasPressed)
            {
                this._mouseLeftButtonHasPressed = true;
                this._mouseLeftButtonHasPressedPosition = this.Bus.Inputs.MousePosition;
            }
            if (this._mouseLeftButtonHasPressed && mouseState.LeftButton == ButtonState.Released)
            {
                this._mouseLeftButtonReleased = true;
                this._mouseLeftButtonHasPressed = false;
            }
            
            // mouse buttons right
            this._mouseRightButton = mouseState.RightButton == ButtonState.Pressed;
            if(this._mouseRightButton && !this._mouseRightButtonHasPressed)
            {
                this._mouseRightButtonHasPressed = true;
                this._mouseRightButtonHasPressedPosition = this.Bus.Inputs.MousePosition;
            }
            if (this._mouseRightButtonHasPressed && mouseState.RightButton == ButtonState.Released)
            {
                this._mouseRightButtonReleased = true;
                this._mouseRightButtonHasPressed = false;
            }
            
            // keyboard states
            this.UpdateKeyboardPressed(gameTime);
        }
        
        private void UpdateKeyboardPressed(GameTime gameTime)
        {
            if(this.Bus.TextMessage.IsOn && this.Bus.TextMessage.CanLeave)
            {
                if(this._lastStart + 500 > gameTime.TotalGameTime.TotalMilliseconds) return;
                
                this.Bus.TextMessage.IsOn = false;
                this.Bus.TextMessage.CanLeave = false;
                return;
            }
            this._lastStart = gameTime.TotalGameTime.TotalMilliseconds;
            
            if(this.Bus.TextMessage.IsOn) return;
            
            var keyboardState = Keyboard.GetState();
            
            if(keyboardState.IsKeyDown(Keys.Enter))
            {
                this.Bus.TextMessage.IsOn = true;
            }
        }

        private Vector2 GetMovement()
        {
            if(this.Bus.TextMessage.IsOn) return Vector2.Zero;
            
            var movement = GamePad.GetState(PlayerIndex).ThumbSticks.Left;
            var keyboardState = Keyboard.GetState();

            var hasSet = false;
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                movement.Y = 1;
                hasSet = true;
            }

            if (!hasSet && (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)))
            {
                movement.Y = -1;
                hasSet = true;
            }

            if (!hasSet && (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)))
            {
                movement.X = -1;
                hasSet = true;
            }

            if (!hasSet && (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)))
            {
                movement.X = 1;
            }

            return new Vector2(movement.X, -movement.Y);
        }

        private bool HasPressToClose()
        {
            return GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
        
        internal bool GetMouseLeftButtonReleasedState(Vector2 position, SizeF sizeArea, string button)
        {
            if (!this._mouseLeftButtonReleased) return false;
            
            this._mouseLeftButtonReleased = false;
                
            return this.IsMouseInRangeLastAndNowPosition(position, sizeArea, button);
        }

        private bool GetMouseRightButtonReleasedState(Vector2 position, SizeF sizeArea, string button)
        {
            if (!this._mouseRightButtonReleased) return false;
            
            this._mouseRightButtonReleased = false;
                
            return this.IsMouseInRangeLastAndNowPosition(position, sizeArea, button);
        }

        private bool IsMouseInRangeLastAndNowPosition(Vector2 position, SizeF size, string button)
        {
            var wasInRange1 = this._mouseLeftButtonHasPressedPosition.X > position.X && 
                             this._mouseLeftButtonHasPressedPosition.Y > position.Y &&
                             this._mouseLeftButtonHasPressedPosition.X < position.X + size.Width &&
                             this._mouseLeftButtonHasPressedPosition.Y < position.Y + size.Height;
            
            var wasInRange2 = this._mouseRightButtonHasPressedPosition.X > position.X && 
                             this._mouseRightButtonHasPressedPosition.Y > position.Y &&
                             this._mouseRightButtonHasPressedPosition.X < position.X + size.Width &&
                             this._mouseRightButtonHasPressedPosition.Y < position.Y + size.Height;
            
            var wasInRange = wasInRange1 || wasInRange2;
            
            Debug.WriteLine($"Button: {button}");
            Debug.WriteLine($"Was in range {wasInRange}, Position {position}, Size {size}");
            var actualInRange = this.Bus.Inputs.MousePosition.X > position.X &&
                                this.Bus.Inputs.MousePosition.Y > position.Y &&
                                this.Bus.Inputs.MousePosition.X < position.X + size.Width &&
                                this.Bus.Inputs.MousePosition.Y < position.Y + size.Height;
            
            Debug.WriteLine($"Actual in range {actualInRange}, Position {position}, Size {size}");
            
            return wasInRange && actualInRange;
        }
    }
}
