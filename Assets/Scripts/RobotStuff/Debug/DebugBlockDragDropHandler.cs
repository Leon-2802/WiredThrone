using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;

public class DebugBlockDragDropHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private DebugWire wireRef;
    [SerializeField] private Camera cameraRef;

    private Vector3 _clickOffset;
    private bool _dragging = false;

    private void Start()
    {
        cameraRef = DebugManager.instance.cameraRef;
    }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!_dragging)
        {
            _clickOffset = cameraRef.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - GetComponent<RectTransform>().position;
            _dragging = true;
        }
        Vector3 mousePos = cameraRef.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _clickOffset;
        mousePos.z = 0;
        transform.position = mousePos;

        if (wireRef.ConnectionIn != null)
        {
            wireRef.UpdateWireVisuals();
        }
        if (wireRef.ConnectionOut != null)
        {
            Transform connectionOutPivot = wireRef.ConnectionOut.GetComponentInChildren<DebugWire>().BlockWirePivot;
            wireRef.UpdateWireVisuals();
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData data)
    {
        _dragging = false;
    }
}
