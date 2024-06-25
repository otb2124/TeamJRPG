using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TeamJRPG
{
    public class InputManager
    {
        private static Point _direction;
        public static Point Direction => _direction;

        private static MouseState _lastMouseState;
        public Point MousePosition => Mouse.GetState().Position;



        public bool IsRightMouseClick()
        {
            var mouseState = Mouse.GetState();
            bool isClick = mouseState.RightButton == ButtonState.Pressed;
            _lastMouseState = mouseState;
            return isClick;
        }

        public bool IsLeftMouseClick()
        {
            var mouseState = Mouse.GetState();
            bool isClick = mouseState.LeftButton == ButtonState.Pressed;
            _lastMouseState = mouseState;
            return isClick;
        }


        public void Update()
        {
            _lastMouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) Globals.camera.Move(new Vector2(-5, 0));
            if (keyboardState.IsKeyDown(Keys.Right)) Globals.camera.Move(new Vector2(5, 0));
            if (keyboardState.IsKeyDown(Keys.Up)) Globals.camera.Move(new Vector2(0, -5));
            if (keyboardState.IsKeyDown(Keys.Down)) Globals.camera.Move(new Vector2(0, 5));
            if (keyboardState.IsKeyDown(Keys.OemPlus)) Globals.camera.Zoom(0.05f);
            if (keyboardState.IsKeyDown(Keys.OemMinus)) Globals.camera.Zoom(-0.05f);
        }
    }
}
