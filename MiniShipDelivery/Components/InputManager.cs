using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.HUD.Controls;
using MonoGame.Extended;

namespace MiniShipDelivery.Components
{
    public class InputManager(
        Game game)
        : GameComponent(game)
    {
        private bool _mouseLeftButtonReleased;
        private bool _mouseLeftButtonHasPressed;

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
            
            this.Inputs.MouseRightButton = mouseState.RightButton == ButtonState.Pressed;
        }

        private bool _mouseLeftButton;
        private Vector2 _mouseLeftButtonHasPressedPosition = Vector2.Zero;


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

        
        internal bool GetMouseLeftButtonReleasedState(Vector2 position, SizeF sizeArea, UiMenuMainPart menuMainPart)
        {
            if (this._mouseLeftButtonReleased)
            {
                this._mouseLeftButtonReleased = false;
                
                Debug.WriteLine($"Menu Main Part: {menuMainPart}");
                return IsMouseInRangeLastAndNowPosition(position, sizeArea);
            }

            return false;
        }

        internal bool IsMouseInRangeLastAndNowPosition(Vector2 position, SizeF size)
        {
            var wasInRange = this._mouseLeftButtonHasPressedPosition.X > position.X && 
                             this._mouseLeftButtonHasPressedPosition.Y > position.Y &&
                             this._mouseLeftButtonHasPressedPosition.X < position.X + size.Width &&
                             this._mouseLeftButtonHasPressedPosition.Y < position.Y + size.Height;
            
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
