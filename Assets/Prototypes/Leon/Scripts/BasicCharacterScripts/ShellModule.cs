using UnityEngine;
public abstract class ShellModule
{
    protected string _name;
    protected int _health;
    protected int _shellHealth;

    public string Name
    {
        get { return _name; }
    }

    public int Health
    {
        get { return _health; }
    }
    public int ShellHealth
    {
        get { return _shellHealth; }
    }

    public static ShellModule GetFromID(int id)
    {
        switch (id)
        {
            case 1:
                return new HealthModule();
            case 2:
                return new ShellHealthModule();
            default:
                return null;
        }
    }
}

public class HealthModule : ShellModule
{
    public HealthModule()
    {
        _name = "HealthModule";
        _health = 100;
        _shellHealth = 0;
    }
}

public class ShellHealthModule : ShellModule
{
    public ShellHealthModule()
    {
        _name = "ShellHealthModule";
        _health = 0;
        _shellHealth = 100;
    }
}