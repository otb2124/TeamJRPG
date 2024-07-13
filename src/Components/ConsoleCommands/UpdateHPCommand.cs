using System;


namespace TeamJRPG
{
    public class UpdateHPCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: sethp <amount> ");
                return;
            }

            if (float.TryParse(args[0], out float amount))
            {
                Globals.player.currentHP = amount;

                Console.WriteLine($"CurrentHP set to ({amount}).");
            }
            else
            {
                Console.WriteLine("Invalid coordinates.");
            }
        }
    }
}
