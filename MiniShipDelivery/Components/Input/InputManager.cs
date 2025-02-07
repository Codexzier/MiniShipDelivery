using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Input
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

        private const int PlayerIndex = 0;


        private const float ScaledMouseMovingX = GlobaleGameParameters.ScreenWidth / (float)GlobaleGameParameters.PreferredBackBufferWidth;

        private const float ScaleMouseMovingY = GlobaleGameParameters.ScreenHeight / (float)GlobaleGameParameters.PreferredBackBufferHeight;

        public InputData Inputs { get; } = new();
        private readonly InputTextController _inputTextController = new();

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
            
            // keyboard states
            this.UpdateKeyboardPressed(gameTime);
            
            this._inputTextController.Update();
        }
        
        private void UpdateKeyboardPressed(GameTime gameTime)
        {
            if(GlobaleGameParameters.DialogState.DialogOn &&
               GlobaleGameParameters.DialogState.DialogExit )
            {
                if (gameTime.TotalGameTime.TotalSeconds > this._dialogExitTime + 1)
                {
                    GlobaleGameParameters.DialogState.DialogExit = false;
                    GlobaleGameParameters.DialogState.DialogOn = false;
                }
                return;
            }
            
            this._dialogExitTime = gameTime.TotalGameTime.TotalSeconds;
            
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) &&
                !GlobaleGameParameters.DialogState.DialogExit)
            {
                GlobaleGameParameters.DialogState.DialogOn = true;
            }

            if (!GlobaleGameParameters.DialogState.DialogOn) return;
            if(!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter)) return;

            foreach (var keyValuePair in this._dictionary)
            {
                if (!GlobaleGameParameters.DialogState.LetterIsPressed && keyboardState.IsKeyDown(keyValuePair.Key))
                {
                    GlobaleGameParameters.DialogState.DialogLetter = keyValuePair.Value;
                    GlobaleGameParameters.DialogState.LetterIsPressed = true;
                    GlobaleGameParameters.DialogState.KeyIsPressed = keyValuePair.Key;
                    return;
                }
            }
            
            if(GlobaleGameParameters.DialogState.LetterIsPressed && 
               keyboardState.IsKeyUp(GlobaleGameParameters.DialogState.KeyIsPressed))
            {
                GlobaleGameParameters.DialogState.LetterIsPressed = false;
            }
        }

        private Vector2 GetMovement()
        {
            if(GlobaleGameParameters.DialogState.DialogOn) return Vector2.Zero;
            
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
        
        private readonly IDictionary<Keys, string> _dictionary = new Dictionary<Keys, string>
        {
            {  Keys.A, "A" },
            {  Keys.B, "B" },
            {  Keys.C, "C" },
            {  Keys.D, "D" },
            {  Keys.E, "E" },
            {  Keys.F, "F" },
            {  Keys.G, "G" },
            {  Keys.H, "H" },
            {  Keys.I, "I" },
            {  Keys.J, "J" },
            {  Keys.K, "K" },
            {  Keys.L, "L" },
            {  Keys.M, "M" },
            {  Keys.N, "N" },
            {  Keys.O, "O" },
            {  Keys.P, "P" },
            {  Keys.Q, "Q" },
            {  Keys.R, "R" },
            {  Keys.S, "S" },
            {  Keys.T, "T" },
            {  Keys.U, "U" },
            {  Keys.V, "V" },
            {  Keys.W, "W" },
            {  Keys.X, "X" },
            {  Keys.Y, "Y" },
            {  Keys.Z, "Z" },
            {  Keys.Space, " " },
            {  Keys.Back, "BACK" },
            {  Keys.Enter, "ENTER" },
            
            // numbers
            {  Keys.D0, "0" },
            {  Keys.D1, "1" },
            {  Keys.D2, "2" },
            {  Keys.D3, "3" },
            {  Keys.D4, "4" },
            {  Keys.D5, "5" },
            {  Keys.D6, "6" },
            {  Keys.D7, "7" },
            {  Keys.D8, "8" },
            {  Keys.D9, "9" }
        };

        private double _dialogExitTime;
    }
}
