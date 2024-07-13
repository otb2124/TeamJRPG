using System;


namespace TeamJRPG
{
    public class DebugModeOnCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: gameMode <0/1/2> ");
                return;
            }

            if (int.TryParse(args[0], out int condition))
            {
                switch (condition)
                {
                    case 0:
                        Globals.currentGameMode = Globals.GameMode.playmode;
                        break;
                    case 1:
                        Globals.currentGameMode = Globals.GameMode.debugmode;
                        break;
                }
                
                Console.WriteLine($"Gamemode set to ({condition}).");
            }
            else
            {
                Console.WriteLine("Invalid coordinates.");
            }
        }
    }
}
