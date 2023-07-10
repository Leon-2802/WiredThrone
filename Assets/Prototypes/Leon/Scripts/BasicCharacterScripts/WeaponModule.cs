using UnityEngine;
public abstract class WeaponModule
{
    protected string _name;
    protected int _size;
    protected bool[,] _shape;

    protected int _dmg;
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

    public int Dmg
    {
        get { return _dmg; }
    }

    public static WeaponModule GetFromID(int id)
    {
        switch (id)
        {
            case 1:
                return new GunModule();
            case 2:
                return new DrillModule();
            default:
                return null;
        }
    }
}

public class GunModule : WeaponModule
{
    public GunModule()
    {
        _name = "GunModule";
        _size = 3;
        _dmg = 5;
        _shape = new bool[,] {
            {true, true, true},
            {true, false, true},
            {false, false, false}
        };
    }
}

public class DrillModule : WeaponModule
{
    public DrillModule()
    {
        _name = "DrillModule";
        _size = 3;
        _dmg = 3;
        _shape = new bool[,] {
            {false, true, false},
            {true, false, true},
            {false, true, false}
        };
    }
}
