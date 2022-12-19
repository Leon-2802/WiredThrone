public abstract class WeaponModule
{
    private string _name;
    private int _size;
    private bool[,] _shape;

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
}

public class FistModule : WeaponModule
{
    private string _name = "Fist Module";
    private int _size = 3;
    private bool[,] _shape = new bool[,] {
        {false, true, false},
        {true, false, true},
        {false, true, false}
    };
}