using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeamJRPG
{
    public class InputManager
    {
        public enum MouseButton { Left, Right, Wheel }
        public MouseState currentMouseState;
        public MouseState previousMouseState;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        public InputManager()
        {
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsMouseButtonClick(MouseButton button)
        {
            bool wasReleased = false;

            if (button == MouseButton.Left)
            {
                wasReleased = previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released;
            }
            else if (button == MouseButton.Right)
            {
                wasReleased = previousMouseState.RightButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Released;
            }
            else if (button == MouseButton.Wheel)
            {
                wasReleased = previousMouseState.MiddleButton == ButtonState.Pressed && currentMouseState.MiddleButton == ButtonState.Released;
            }

            return wasReleased;
        }


        public bool IsMouseButtonHold(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => currentMouseState.LeftButton == ButtonState.Pressed,
                MouseButton.Right => currentMouseState.RightButton == ButtonState.Pressed,
                MouseButton.Wheel => currentMouseState.MiddleButton == ButtonState.Pressed,
                _ => false,
            };
        }

        public bool IsMouseButtonReleased(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => currentMouseState.LeftButton == ButtonState.Released,
                MouseButton.Right => currentMouseState.RightButton == ButtonState.Released,
                MouseButton.Wheel => currentMouseState.MiddleButton == ButtonState.Released,
                _ => false,
            };
        }



        public Vector2 GetCursorPos()
        {
            return new Vector2(Globals.inputManager.currentMouseState.X - Globals.camera.viewport.Width / 2, Globals.inputManager.currentMouseState.Y - Globals.camera.viewport.Height / 2);
        }



        public bool IsKeyPressedAndReleased(Keys key)
        {
            return previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
        }


        public bool CheckPlayerInGameInput()
        {
            bool isInput = IsKeyPressed(Keys.W) || IsKeyPressed(Keys.A) || IsKeyPressed(Keys.S) || IsKeyPressed(Keys.D);
            return isInput;
        }


        public bool CheckPlayerActivity()
        {
            bool mouseMoved = currentMouseState.X != previousMouseState.X || currentMouseState.Y != previousMouseState.Y;
            bool mouseClicked = IsMouseButtonClick(MouseButton.Left) || IsMouseButtonClick(MouseButton.Right) || IsMouseButtonClick(MouseButton.Wheel);
            bool keyPressed = currentKeyboardState.GetPressedKeys().Length > 0;

            return mouseMoved || mouseClicked || keyPressed;
        }


        public bool CheckPlayerInput() 
        {
            bool mouseClicked = IsMouseButtonClick(MouseButton.Left) || IsMouseButtonClick(MouseButton.Right) || IsMouseButtonClick(MouseButton.Wheel);
            bool keyPressed = currentKeyboardState.GetPressedKeys().Length > 0;
            return mouseClicked || keyPressed;
        }


        public void Update()
        {
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;

            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }
    }
}
