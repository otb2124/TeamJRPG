using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeamJRPG
{
    public class InputManager
    {
        public enum MouseButton { Left, Right, Wheel }
        private MouseState currentMouseState;
        private MouseState previousMouseState;
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
            bool isClick = false;
            if (button == MouseButton.Left)
            {
                isClick = currentMouseState.LeftButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Right)
            {
                isClick = currentMouseState.RightButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Wheel)
            {
                isClick = currentMouseState.MiddleButton == ButtonState.Pressed;
            }

            return isClick;
        }

        public bool IsKeyPressedAndReleased(Keys key)
        {
            return previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
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
