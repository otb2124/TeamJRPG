using System;


namespace TeamJRPG
{
    public class SaveCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 1)
            {
                Globals.mapReader.WriteMaps(Globals.currentSaveName);
                Console.WriteLine($"GameData saved to ({Globals.currentSaveName}).");
            }
            else if (args.Length == 1)
            {
                if (args[0] == "config")
                {
                    Globals.mapReader.WriteConfig();
                    Console.WriteLine($"Config saved.");
                }
                else
                {
                    Globals.mapReader.WriteMaps(args[0]);
                    Console.WriteLine($"GameData saved to ({args[0]}).");
                }
            }
            else
            {
                Console.WriteLine($"Usage: save OR save <string path> OR save <string config>");
            }





        }
    }
}
