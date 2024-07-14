using System.Collections.Generic;
using System;
using TeamJRPG;
using Microsoft.Xna.Framework;
using System.Linq;

public class EventManager
{
    private List<Event> eventTriggers = new List<Event>();

    public void RegisterEvent(Event eventTrigger)
    {
        eventTriggers.Add(eventTrigger);
    }


    public void SetEvents()
    {
        RegisterEvent(new EventPortal("Location0", new Point(15, 0), "Location1", new Point(15, 28)));
        RegisterEvent(new EventPortal("Location1", new Point(15, 29), "Location0", new Point(15, 1)));
        RegisterEvent(new EventSpike("Location1", new Point(23, 4), 0.25f));
    }

    public void CheckEvents()
    {
        foreach (var eventTrigger in eventTriggers)
        {
            if (IsPlayerInTile(eventTrigger))
            {
                TriggerEvent(eventTrigger);
                break; // Assuming one event can be triggered at a time
            }
        }
    }

    private bool IsPlayerInTile(Event eventTrigger)
    {
        return Globals.player.GetMapPos() == eventTrigger.SourceTilePosition && Globals.currentMap.name == eventTrigger.SourceMapName;
    }





    private void TriggerEvent(Event eventTrigger)
    {
        if (eventTrigger.type == Event.EventType.portal)
        {
            EventPortal eventtr = (EventPortal)eventTrigger;

            // Find the target map by name
            Map targetMap = Globals.maps.FirstOrDefault(m => m.name == eventtr.TargetMapName);

            if (targetMap != null)
            {
                // Remove all GroupMember objects from the current map's entities list
                Globals.currentMap.entities.RemoveAll(entity => entity is GroupMember);

                // Add group members to the target map's entities list
                foreach (var member in Globals.group.members)
                {
                    targetMap.entities.Add(member);
                    member.position = eventtr.TargetPosition.ToVector2() * Globals.tileSize;
                    if (member.isPlayer)
                    {
                        Globals.player = member;
                    }
                    member.path = null;
                }

                // Update current map and entities references
                Globals.currentMap = targetMap;
                Globals.currentEntities = targetMap.entities;

                Console.WriteLine($"Event triggered: Switching to map {eventtr.TargetMapName} at position {eventtr.TargetPosition}");
            }
            else
            {
                Console.WriteLine($"Target map '{eventtr.TargetMapName}' not found.");
            }
        }
        else if(eventTrigger.type == Event.EventType.spike)
        {
            EventSpike eventtr = (EventSpike)eventTrigger;

            Globals.player.currentHP -= eventtr.damage;

            Console.WriteLine($"Event triggered: Dealed Damage {eventtr.damage}");
        }
    }




}
