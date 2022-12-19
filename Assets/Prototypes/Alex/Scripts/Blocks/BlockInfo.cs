using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlockInfo<T>
{
    private T _value;
    public T Value
    {
        get { return _value; }
        set { _value = value; }
    }

    private Blockinfotype _type;
    public Blockinfotype Type
    {
        get { return _type; }
    }

    public BlockInfo(T v, Blockinfotype t)
    {
        _value = v;
        _type = t;
    }
}
