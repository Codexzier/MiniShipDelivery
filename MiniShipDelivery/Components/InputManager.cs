using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components
{
    public class InputManager(
        Game game)
        : GameComponent(game)
    {
        private bool _mouseLeftButton;
        private Vector2 _mouseLeftButtonHasPressedPosition = Vector2.Zero;
        private bool _mouseLeftButtonReleased;
        private bool _mouseLeftButtonHasPressed;
        
        private bool _mouseRightButton;
        private Vector2 _mouseRightButtonHasPressedPosition = Vector2.Zero;
        private bool _mouseRightButtonReleased;
        private bool _mouseRightButtonHasPressed;
        
        private readonly int _playerIndex = 0;
        

        private const float ScaledMouseMovingX = GlobaleGameParameters.ScreenWidth / (float)GlobaleGameParameters.PreferredBackBufferWidth;

        private const float ScaleMouseMovingY = GlobaleGameParameters.ScreenHeight / (float)GlobaleGameParameters.PreferredBackBufferHeight;

        public InputData Inputs { get; } = new();

        public override void Update(GameTime gameTime)
        {
            if(this.HasPressToClose()) this.Game.Exit();

            this.Inputs.MovementCharacter = this.GetMovement();
            
            // mouse state
            var mouseState = Mouse.GetState();
            this.Inputs.MousePosition = new Vector2(
                mouseState.X * ScaledMouseMovingX, 
                mouseState.Y * ScaleMouseMovingY);

            // mouse buttons left
            this._mouseLeftButton = mouseState.LeftButton == ButtonState.Pressed;
            if(this._mouseLeftButton && !this._mouseLeftButtonHasPressed)
            {
                this._mouseLeftButtonHasPressed = true;
                this._mouseLeftButtonHasPressedPosition = this.Inputs.MousePosition;
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
                this._mouseRightButtonHasPressedPosition = this.Inputs.MousePosition;
            }
            if (this._mouseRightButtonHasPressed && mouseState.RightButton == ButtonState.Released)
            {
                this._mouseRightButtonReleased = true;
                this._mouseRightButtonHasPressed = false;
            }
            
            this.Inputs.MouseRightButton = mouseState.RightButton == ButtonState.Pressed;
        }

        private Vector2 GetMovement()
        {
            var movement = GamePad.GetState(this._playerIndex).ThumbSticks.Left;

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
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        
        internal bool GetMouseLeftButtonReleasedState(Vector2 position, SizeF sizeArea)
        {
            if (!this._mouseLeftButtonReleased) return false;
            
            this._mouseLeftButtonReleased = false;
                
            return this.IsMouseInRangeLastAndNowPosition(position, sizeArea);
        }
        
        internal bool GetMouseRightButtonReleasedState(Vector2 position, SizeF sizeArea)
        {
            if (!this._mouseRightButtonReleased) return false;
            
            this._mouseRightButtonReleased = false;
                
            return this.IsMouseInRangeLastAndNowPosition(position, sizeArea);
        }

        private bool IsMouseInRangeLastAndNowPosition(Vector2 position, SizeF size)
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
            
            Debug.WriteLine($"Was in range {wasInRange}, Position {position}, Size {size}");
            var actualInRange = this.Inputs.MousePosition.X > position.X &&
                                this.Inputs.MousePosition.Y > position.Y &&
                                this.Inputs.MousePosition.X < position.X + size.Width &&
                                this.Inputs.MousePosition.Y < position.Y + size.Height;
            
            Debug.WriteLine($"Actual in range {actualInRange}, Position {position}, Size {size}");
            
            return wasInRange && actualInRange;
        }
        
        private bool IsMouseInRangeLastAndNowPositionRightClick(Vector2 position, SizeF size)
        {
            var wasInRange = this._mouseRightButtonHasPressedPosition.X > position.X && 
                             this._mouseRightButtonHasPressedPosition.Y > position.Y &&
                             this._mouseRightButtonHasPressedPosition.X < position.X + size.Width &&
                             this._mouseRightButtonHasPressedPosition.Y < position.Y + size.Height;
            
            Debug.WriteLine($"Was in range {wasInRange}, Position {position}, Size {size}");
            var actualInRange = this.Inputs.MousePosition.X > position.X &&
                                this.Inputs.MousePosition.Y > position.Y &&
                                this.Inputs.MousePosition.X < position.X + size.Width &&
                                this.Inputs.MousePosition.Y < position.Y + size.Height;
            
            Debug.WriteLine($"Actual in range {actualInRange}, Position {position}, Size {size}");
            
            return wasInRange && actualInRange;
        }
    }
}
