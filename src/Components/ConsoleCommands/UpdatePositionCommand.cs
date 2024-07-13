using Microsoft.Xna.Framework;
using System;


namespace TeamJRPG
{
    public class UpdatePositionCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: setpos <x> <y>");
                return;
            }

            if (float.TryParse(args[0], out float x) && float.TryParse(args[1], out float y))
            {
                Globals.player.position = new Vector2(x*Globals.tileSize.X, y*Globals.tileSize.Y);
                Console.WriteLine($"Position updated to ({x}, {y}).");
            }
            else
            {
                Console.WriteLine("Invalid coordinates.");
            }
        }
    }
}
