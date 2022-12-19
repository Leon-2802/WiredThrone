using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInit<T> : MonoBehaviour
{
    [SerializeField] private BlockIf<int> _ifblock;
    [SerializeField] private BlockInfo<int> _r;

    [SerializeField] private int _valueLeft;
    [SerializeField] private int _valueRight;
    [SerializeField] private Operator _op;
    
    // Start is called before the first frame update
    void Start()
    {
        _ifblock = new BlockIf<int>(new BlockInfo<int>(_valueLeft, Blockinfotype.IntBlock), _op, new BlockInfo<int>(_valueRight, Blockinfotype.IntBlock));
    }

    // Update is called once per frame
    void Update()
    {
        if (_ifblock.IsSet())
        {
            Debug.Log(_ifblock.ParseInput());
        }
    }
}
