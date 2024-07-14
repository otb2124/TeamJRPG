using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public interface ICommand
    {
        void Execute(string[] args);
    }



    public class CommandManager
    {
        private Dictionary<string, ICommand> commands;

        public CommandManager()
        {
            


            commands = new Dictionary<string, ICommand>();

            RegisterCommand("setpos", new UpdatePositionCommand());
            RegisterCommand("gameMode", new DebugModeOnCommand());
            RegisterCommand("set", new SetCommand());


        }

        public void RegisterCommand(string name, ICommand command)
        {
            commands[name] = command;
        }

        public void ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ');
            if (parts.Length == 0) return;

            string commandName = parts[0];
            string[] args = parts.Skip(1).ToArray();

            if (commands.TryGetValue(commandName, out ICommand command))
            {
                command.Execute(args);
            }
            else
            {
                Console.WriteLine($"Command '{commandName}' not found.");
            }
        }
    }
}
