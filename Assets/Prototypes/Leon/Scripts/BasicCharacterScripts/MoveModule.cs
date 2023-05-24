using UnityEngine;
public abstract class MoveModule
{
    protected string _name;
    protected int _maxDistance;

    public string Name
    {
        get { return _name; }
    }

    public int MaxDistance
    {
        get { return _maxDistance; }
    }
    public static MoveModule GetFromID(int id)
    {
        switch (id)
        {
            case 1:
                return new LegModule();
            case 2:
                return new RocketModule();
            default:
                return null;
        }
    }
}

public class LegModule : MoveModule
{
    public LegModule()
    {
        _name = "LegModule";
        _maxDistance = 2;
    }
}

public class RocketModule : MoveModule
{
    public RocketModule()
    {
        _name = "RocketModule";
        _maxDistance = 4;
    }
}
