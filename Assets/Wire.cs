using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IDragHandler
{
    [SerializeField] private Transform _blockWirePivot;
    private Vector3 startPoint;

    public RectTransform wireEnd;
    private float initialScaleY;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("mouse drag");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        transform.position = mousePos;
        Vector3 direction = mousePos - startPoint;
        //transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(mousePos, startPoint);
        wireEnd.transform.localScale = new Vector2(wireEnd.localScale.x, dist * 0.35f);  // Dunno why .35f, but it kind of works for the scale

        float angle = AngleBetweenTwoPoints(transform.position, startPoint);
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        wireEnd.transform.rotation = Quaternion.Euler(0, 0, angle - 90);


    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }

     private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block") && other.gameObject != this) {
            if (!Mouse.current.IsPressed()) {
                transform.position = other.gameObject.GetComponentInChildren<Wire>()._blockWirePivot.position;
            }
        }
     }
}

