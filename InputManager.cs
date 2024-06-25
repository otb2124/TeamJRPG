using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeamJRPG
{
    public class InputManager
    {

        public enum MouseButton {  Left, Right, Wheel }
        private MouseState mouseState;
        private KeyboardState keyboardState;


        public bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key); 
        }
   



        public bool IsMouseButtonClick(MouseButton button)
        {
            
            var mouseState = Mouse.GetState();
            bool isClick = false;
            if (button == MouseButton.Left)
            {
                isClick = mouseState.LeftButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Right)
            {
                isClick = mouseState.RightButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Wheel)
            {
                isClick = mouseState.MiddleButton == ButtonState.Pressed;
            }

            this.mouseState = mouseState;
            return isClick;
        }






        public void Update()
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }
    }
}
