using UnityEngine;
using System;
using System.Collections.Generic;
public abstract class Block
{
    public abstract object GetValue();
    public abstract object GetName();
    public abstract new object GetType();
    public abstract object GetMoveVector();
    public abstract object GetSteps();
    public abstract override string ToString();
}

public class Block<T> : Block
{
    private T _value; 
    private string _name;
    private System.Type _type;
    private Vector2 _moveVector;
    private int _steps;

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

    public Block(string name, T value, int steps) : this(name, value)
    {
        _steps = steps;
    }

    public override object GetValue()
    {
        return _value;
    }

    public override object GetName()
    {
        return _name;
    }

    public override object GetMoveVector()
    {
        return _moveVector;
    }

    public override object GetType()
    {
        return _type;
    }

    public override object GetSteps()
    {
        return _steps;
    }

    public override string ToString()
    {
        Debug.Log("Lmao");
        return _name + " " + _type + " " + _steps;
    }
}