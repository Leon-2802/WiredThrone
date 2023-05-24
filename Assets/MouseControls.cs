using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private float _currentScaleFactor = 1f;
    [SerializeField] private float _scaleFactor = .001f;
    [SerializeField] private float _minScale = .53f;
    [SerializeField] private Vector2 _clickedRectPos;
    [SerializeField] private bool _rightMouseClicked = false;
    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }


    private void Update()
    {

        _currentScaleFactor += Mouse.current.scroll.y.ReadValue() * _scaleFactor;
        if (_currentScaleFactor < _minScale) _currentScaleFactor = _minScale;
        _rect.localScale = new Vector3(_currentScaleFactor, _currentScaleFactor, _currentScaleFactor);

        if (Mouse.current.rightButton.isPressed)
        {
            if (!_rightMouseClicked)
            {
                _rightMouseClicked = true;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), Camera.main, out _clickedRectPos);
            }

            Vector2 canvasMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), Camera.main, out canvasMousePos);
            Vector2 movementOffset = canvasMousePos - _clickedRectPos;
            _rect.position += (Vector3)movementOffset;
        }
        else
        {
            _rightMouseClicked = false;
        }
    }
}
