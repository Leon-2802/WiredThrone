using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;

public class BlockDragDropHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Wire wireRef;
    
    private Vector3 _clickOffset;
    private bool _dragging = false;

    private void Start() {
        wireRef = GetComponentInChildren<Wire>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData) {
        if (!_dragging) {
            _clickOffset = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - GetComponent<RectTransform>().position;
            _dragging = true;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _clickOffset;
        mousePos.z = 0;
        transform.position = mousePos;

        if (wireRef.ConnectionIn != null) {
            wireRef.UpdateWireVisuals();
        }
        if (wireRef.ConnectionOut != null) {
            Transform connectionOutPivot = wireRef.ConnectionOut.GetComponentInChildren<Wire>().BlockWirePivot;
            wireRef.UpdateWireVisuals();
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData data) {
        _dragging = false;
    }
}
