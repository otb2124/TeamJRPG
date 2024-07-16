using Microsoft.Xna.Framework;
using System;


namespace TeamJRPG
{
    public class AddCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: add <EntityType> <int id> <int mapPositionX> <int mapPositionY>");
                return;
            }


            if (args[0] == "obj")
            {
                Globals.currentEntities.Add(new Object(new Point(int.Parse(args[2]), int.Parse(args[3])), int.Parse(args[1])));
                Console.WriteLine($"Added object to map {Globals.currentMap.name} with position {args[2]}, {args[3]}");
            }
            else if(args[0] == "mob")
            {
                Globals.currentEntities.Add(new Mob(new Point(int.Parse(args[2]), int.Parse(args[3])), int.Parse(args[1])));
                Console.WriteLine($"Added mob to map {Globals.currentMap.name} with position {args[2]}, {args[3]}");
            }
            else if (args[0] == "npc")
            {
                Globals.currentEntities.Add(new NPC(new Point(int.Parse(args[2]), int.Parse(args[3])), int.Parse(args[1])));
                Console.WriteLine($"Added NPC to map {Globals.currentMap.name} with position {args[2]}, {args[3]}");
            }

        }
    }
}
