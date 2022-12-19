using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class IntBlock : MonoBehaviour
{
    [SerializeField] private Block<int> _block;
    [SerializeField] private TMP_InputField _varText;
    [SerializeField] private TMP_InputField _numText;

    public int Value
    {
        get { return _block.Value; }
    }

    private void Start() {
        _block = new Block<int>("", 0);    
    }


    public void Update()
    {
        if (_varText != null)
        {
            _block.Name = _varText.text;
        }
        if (_numText != null && _numText.text != "")
        {

            _block.Value = int.Parse(_numText.text);
        }
    }
}
