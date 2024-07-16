using System;


namespace TeamJRPG
{
    public class SystemExitCommand : ICommand
    {
        public void Execute(string[] args)
        {

            Globals.Exit();
            
        }
    }
}
