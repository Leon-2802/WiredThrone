using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block
{
}

public class Block<T> : Block
{
    private T _value;
    private string _name;
    private System.Type _type;
    private Vector2 _moveVector;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public System.Type Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public T Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public Vector2 MoveVector
    {
        get { return _moveVector; }
    }

    public Block(string name, T value)
    {
        _name = name;
        _value = value;
        _type = value.GetType();
        _moveVector = Vector2.zero;
    }

    public Block(string name, T value, Vector2 position) : this(name, value)
    {
        _moveVector = position;
    }
}