using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;

public class BlockDragDropHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    [SerializeField] private Wire wireRef;
    [SerializeField] private RectTransform _sideConnection;
    [SerializeField] private Camera _blocksCam;

    public RectTransform SideConnection
    {
        get { return _sideConnection; }
    }
    private Vector3 _clickOffset;
    private bool _dragging = false;

    private void Start()
    {
        wireRef = GetComponentInChildren<Wire>();
        _blocksCam = GameObject.FindGameObjectWithTag("BlockCam").GetComponent<Camera>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("lool");
        if (wireRef != null)
        {
            if (Mouse.current.middleButton.IsPressed())
            {
                wireRef.ResetWireOut();
            }
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!_dragging)
        {
            _clickOffset = _blocksCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - GetComponent<RectTransform>().position;
            _dragging = true;
        }
        Vector3 mousePos = _blocksCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _clickOffset;
        mousePos.z = 0;
        transform.position = mousePos;

        if (wireRef != null)
        {
            if (wireRef.ConnectionIn != null)
            {
                wireRef.UpdateWireVisuals();
            }
            if (wireRef.ConnectionOut != null)
            {
                Transform connectionOutPivot = wireRef.ConnectionOut.GetComponentInChildren<Wire>().BlockWirePivot;
                wireRef.UpdateWireVisuals();
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData data)
    {
        _dragging = false;

        if (wireRef != null) {
            wireRef.UpdateWireVisuals();
        }

        if (_sideConnection != null)
        {

            Collider[] colliders = Physics.OverlapBox(_sideConnection.transform.position, new Vector3(1, 1, 100000));
            if (colliders.Length <= 1)
            {
                SideConnection sideComponent = GetComponent<SideConnection>();
                if (sideComponent != null && sideComponent.Side == Side.right && sideComponent.SideBlock != null)
                {
                    SideConnection sideBlock = sideComponent.SideBlock.GetComponent<SideConnection>();
                    if (sideBlock != null) sideBlock.RemoveSideBlock();
                    sideComponent.RemoveSideBlock();
                }
                return;
            }
            else
            {
                foreach (Collider collider in colliders)
                {
                    if (Wire.BlockTag(collider.tag))
                    {
                        RectTransform sideCon = collider.gameObject.GetComponent<BlockDragDropHandler>().SideConnection;
                        if (sideCon != null)
                        {
                            SideConnection sideBlock = this.gameObject.GetComponent<SideConnection>();
                            sideBlock.SetSideBlock(collider.gameObject);
                            sideBlock.SideBlock.GetComponent<SideConnection>().SetPosition();
                        }
                    }
                }
            }
        }
    }
}
