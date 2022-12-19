using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockInit : MonoBehaviour
{
    [SerializeField] private BlockIf _ifblock;
    [SerializeField] private BlockInfo<int> _leftBlock;
    [SerializeField] private BlockInfo<int> _rightBlock;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _leftBlockObject;

    [SerializeField] private GameObject _rightBlockObject;

    public GameObject LeftBlock
    {
        set { _leftBlockObject = value; }
    }

    public GameObject RightBlock
    {
        set { _rightBlockObject = value; }
    }
    void Start()
    {
        _leftBlock = new BlockInfo<int>(0, Blockinfotype.IntBlock);
        _rightBlock = new BlockInfo<int>(0, Blockinfotype.IntBlock);
        _ifblock = new BlockIf(_leftBlock, Operator.Equal, _rightBlock);

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            var child = transform.GetChild(0).GetChild(i);
            if (child.CompareTag("IfOperator"))
            {
                _dropdown = child.gameObject.GetComponent<TMP_Dropdown>();
                _text = _dropdown.transform.GetChild(0).GetComponent<TMP_Text>();
            }
        }
    }

    void Update()
    {
        if (_leftBlockObject != null)
        {
            _leftBlock.Value = _leftBlockObject.GetComponentInParent<IntBlock>().Value;        
        }
        else
        {
            _leftBlock.Value = 0;
        }
        if (_rightBlockObject != null)
        {
            _rightBlock.Value = _rightBlockObject.GetComponentInParent<IntBlock>().Value;
        }
        else
        {
            _rightBlock.Value = 0;
        }
        _ifblock.Logic.SetOperatorFromString(_dropdown.options[_dropdown.value].text);

        //_dropdown.transform.GetChild(0).GetComponent<TMP_Text>().text = _dropdown.options[_dropdown.value].text;
        if (_ifblock.Logic.IsSet())
        {
            Debug.Log(this.name + " " + _leftBlock.Value + " " + _ifblock.Logic.GetOperator + " " + _rightBlock.Value + " is " + _ifblock.Logic.ParseInput());
        }
    }
}
