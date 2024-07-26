using System;


namespace TeamJRPG
{
    public class InfoCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: info <type of info> <castType>");
                return;
            }
            else
            {
                if (args[1] == "count")
                {
                    switch (args[0])
                    {
                        case "entities":
                            Console.WriteLine("Total entities count on map: " + Globals.currentEntities.Count);
                            break;

                        case "mob":
                            int mobCount = Globals.currentEntities.FindAll(entity => entity is Mob).Count;
                            Console.WriteLine("Total mob count on map: " + mobCount);
                            break;

                        case "group":
                            int groupCount = Globals.currentEntities.FindAll(entity => entity is GroupMember).Count;
                            Console.WriteLine("Total group member count on map: " + groupCount);
                            break;

                        case "obj":
                            int objectCount = Globals.currentEntities.FindAll(entity => entity is Object).Count;
                            Console.WriteLine("Total objects count on map: " + objectCount);
                            break;

                        case "npc":
                            int npcCount = Globals.currentEntities.FindAll(entity => entity is NPC).Count;
                            Console.WriteLine("Total npc count on map: " + npcCount);
                            break;

                        default:
                            Console.WriteLine("Unknown info type. Valid types are: entities, mob, group, obj.");
                            break;
                    }
                }



            }
        }
    }
}
