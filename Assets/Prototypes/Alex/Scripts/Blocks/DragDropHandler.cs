using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DragDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
{
    private Canvas _canvas;
    private Camera _cam;
    [SerializeField] private Vector3 _dragOffset;
    private void Awake()
    {
        _canvas = GameObject.FindGameObjectWithTag("ProgrammingBlockCanvas").GetComponent<Canvas>();
        _cam = GameObject.FindGameObjectWithTag("BlockCam").GetComponent<Camera>();
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name);
        _dragOffset = transform.position - _cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
        _dragOffset.z = 0;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector3 newPos = _cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
        newPos.z = 0;
        transform.parent.position = newPos + _dragOffset;

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision of " + this.gameObject.name + " with " + other.gameObject.tag);

        if (!Mouse.current.IsPressed())
        {
            switch (other.gameObject.tag)
            {
                case "IfLeft":
                    other.gameObject.GetComponentInParent<BlockInit>().LeftBlock = this.gameObject;
                    other.gameObject.transform.parent.localScale = new Vector3(GetComponentInParent<BoxCollider>().bounds.size.x / 2, 1, 1);
                    transform.parent.position = other.gameObject.transform.parent.position;
                    break;
                case "IfRight":
                    other.gameObject.GetComponentInParent<BlockInit>().RightBlock = this.gameObject;
                    other.gameObject.transform.parent.localScale = new Vector3(GetComponentInParent<BoxCollider>().bounds.size.x / 2, 1, 1);
                    transform.parent.position = other.gameObject.transform.parent.position;
                    break;
            }
        }
        else
        {
            switch (other.gameObject.tag)
            {
                case "IfLeft":
                    other.gameObject.GetComponentInParent<BlockInit>().LeftBlock = null;
                    other.gameObject.transform.parent.localScale = Vector3.one;
                    break;
                case "IfRight":
                    other.gameObject.GetComponentInParent<BlockInit>().RightBlock = null;
                    other.gameObject.transform.parent.localScale = Vector3.one;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision end with " + other.gameObject.tag);
        switch (other.gameObject.tag)
        {
            case "IfLeft":
                other.gameObject.GetComponentInParent<BlockInit>().LeftBlock = null;
                other.gameObject.transform.parent.localScale = Vector3.one;
                break;
            case "IfRight":
                other.gameObject.GetComponentInParent<BlockInit>().RightBlock = null;
                other.gameObject.transform.parent.localScale = Vector3.one;
                break;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }
}
