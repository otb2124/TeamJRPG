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
        RegisterEvent(new EventPortal("Location0", new Point(10, 10), "Location1", new Point(10, 10)));
        RegisterEvent(new EventPortal("Location1", new Point(20, 10), "Location0", new Point(20, 10)));
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
        // Find the target map by name
        if(eventTrigger.type == Event.EventType.portal)
        {
            EventPortal eventtr = (EventPortal)eventTrigger;

            Map targetMap = Globals.maps.FirstOrDefault(m => m.name == eventtr.TargetMapName);
            if (targetMap != null)
            {
                Globals.currentMap = targetMap;
                foreach (var member in Globals.group.members)
                {
                    member.position = eventtr.TargetPosition.ToVector2() * Globals.tileSize;
                }
                Console.WriteLine($"Event triggered: Switching to map {eventtr.TargetMapName} at position {eventtr.TargetPosition}");
            }
            else
            {
                Console.WriteLine($"Target map '{eventtr.TargetMapName}' not found.");
            }
        }
        
    }
}
