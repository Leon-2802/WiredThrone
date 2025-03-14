using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);

    }
}