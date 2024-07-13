using System;


namespace TeamJRPG
{
    public class SetCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: set <GroupMember.name> <hp/mana/maxhp/maxmana/strength..> <amount> ");
                return;
            }

            if (float.TryParse(args[2], out float amount))
            {
               foreach(var member in Globals.group.members)
                {
                    if(member.name == args[0])
                    {

                        switch (args[1])
                        {
                            case "hp":
                                member.currentHP = amount;
                                break;
                            case "maxhp":
                                member.maxHP = amount;
                                break;
                            case "mana":
                                member.currentMana = amount;
                                break;
                            case "maxmana":
                                member.maxMana = amount;
                                break;
                            case "str":
                                member.strength = (int)amount;
                                break;
                            case "dex":
                                member.dexterity = (int)amount;
                                break;
                            case "wis":
                                member.wisdom = (int)amount;
                                break;
                            case "exp":
                                member.currentExp = (int)amount;
                                break;
                            case "lvl":
                                member.level = (int)amount;
                                break;
                            case "speed":
                                member.defaultSpeed = (int)amount;
                                break;
                        }
                    }
                }


                Console.WriteLine($"{args[1]} set to ({amount}).");
            }
            else
            {
                Console.WriteLine("Invalid coordinates.");
            }
        }
    }
}
