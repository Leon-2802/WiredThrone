using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class IntBlock : MonoBehaviour
{
    [SerializeField] private string _variableName;
    [SerializeField] private int _value;
    [SerializeField] private TMP_InputField _varText;
    [SerializeField] private TMP_InputField _numText;
    
    public void Update() {
        if (_varText != null) {
            _variableName = _varText.text;
        }
        if (_numText != null && _numText.text != "") {

            _value = int.Parse( _numText.text);
        }
    }
}
