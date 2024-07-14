using Microsoft.Xna.Framework;

public class Event
{
    public string SourceMapName { get; set; }
    public Point SourceTilePosition { get; set; }


    public enum EventType { portal, spike }
    public EventType type { get; set; }

    public Event(string sourceMapName, Point sourceTilePosition)
    {
        SourceMapName = sourceMapName;
        SourceTilePosition = sourceTilePosition;
    }
}


public class EventSpike : Event
{
    public float damage;
    public EventSpike(string sourceMapName, Point sourceTilePosition, float damage) : base(sourceMapName, sourceTilePosition)
    {
        this.type = EventType.spike;
        this.damage = damage;
    }
}



public class EventPortal : Event
{
    public string TargetMapName { get; set; }
    public Point TargetPosition { get; set; }


    public EventPortal(string sourceMapName, Point sourceTilePosition, string targeMapName, Point targetTilePosition) : base(sourceMapName, sourceTilePosition)
    {
        this.type = EventType.portal;
        this.TargetMapName = targeMapName;
        this.TargetPosition = targetTilePosition;
    }
}
