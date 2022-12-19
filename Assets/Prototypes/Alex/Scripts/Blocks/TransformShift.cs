using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformShift : MonoBehaviour
{
    [SerializeField] private Transform _leftPivot;
    [SerializeField] private Transform _middlePivot;
    [SerializeField] private Transform _rightPivot;
    [SerializeField] private Transform _leftIfTransform;
    [SerializeField] private Transform _operatorTransform;
    [SerializeField] private Transform _rightIfTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _operatorTransform.position = _middlePivot.position;
        _rightIfTransform.position = _rightPivot.position;
    }
}
