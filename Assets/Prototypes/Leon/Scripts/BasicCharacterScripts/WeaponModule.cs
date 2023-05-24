using UnityEngine;
public abstract class WeaponModule
{
    protected string _name;
    protected int _size;
    protected bool[,] _shape;

    public string Name
    {
        get { return _name; }
    }

    public int Size
    {
        get { return _size; }
    }
    public bool[,] Shape
    {
        get { return _shape; }
    }

    public static WeaponModule GetFromID(int id)
    {
        switch (id)
        {
            case 1:
                return new FistModule();
            case 2:
                return new BladeModule();
            default:
                return null;
        }
    }
}

public class FistModule : WeaponModule
{
    public FistModule()
    {
        _name = "FistModule";
        _size = 3;
        _shape = new bool[,] {
            {false, true, false},
            {true, false, true},
            {false, true, false}
        };
    }
}

public class BladeModule : WeaponModule
{
    public BladeModule()
    {
        _name = "BladeModule";
        _size = 3;
        _shape = new bool[,] {
            {true, true, true},
            {true, false, true},
            {false, false, false}
        };
    }
}

