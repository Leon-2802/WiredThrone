using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _blockWirePivot;
    [SerializeField] private GameObject _connectionOut;
    [SerializeField] private GameObject _connectionIn;
    private float _dist;
    private Vector3 startPoint;

    public RectTransform wireEnd;

    public Transform BlockWirePivot
    {
        get { return _blockWirePivot; }
    }

    public GameObject ConnectionOut
    {
        get { return _connectionOut; }
    }

    public GameObject ConnectionIn
    {
        get { return _connectionIn; }
    }

    private bool _dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        _dist = 0f;
    }

    public void ResetWireOut()
    {
        _connectionOut = null;
        startPoint = wireEnd.GetComponent<RectTransform>().position;
        transform.position = startPoint;
        wireEnd.transform.localScale = Vector2.one;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        wireEnd.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void ResetWireIn()
    {
        if (_connectionIn != null)
        {
            Wire wireIn = _connectionIn.GetComponentInChildren<Wire>();
            _connectionIn = null;

            // wireIn == wireOut of the other block.
            wireIn.ResetWireOut();
        }
    }

    private void SetWireInVisually()
    {
        if (_connectionIn != null)
        {
            _connectionIn.GetComponentInChildren<Wire>().SetWireOutVisually();
        }
    }

    private void SetWireOutVisually()
    {
        if (_connectionOut != null)
        {
            Vector3 outBlockPivot = _connectionOut.GetComponentInChildren<Wire>().BlockWirePivot.position;
            var wireDraggableRt = GetComponent<RectTransform>();

            wireDraggableRt.position = outBlockPivot;

            var wireEndRt = wireEnd.GetComponent<RectTransform>();
            startPoint = wireEndRt.position;
            _dist = Vector2.Distance(wireDraggableRt.position, startPoint);
            wireEnd.transform.localScale = new Vector2(wireEnd.localScale.x, _dist / 30);

            float angle = AngleBetweenTwoPoints(transform.position, startPoint);
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            wireEnd.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    public void UpdateWireVisuals()
    {
        SetWireInVisually();
        SetWireOutVisually();

    }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");

        // Store mouse position and calculate distance to source of the wire.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        var wireDraggableRt = GetComponent<RectTransform>();
        wireDraggableRt.position = mousePos;

        var wireEndRt = wireEnd.GetComponent<RectTransform>();

        startPoint = wireEndRt.position;
        _dist = Vector2.Distance(mousePos, startPoint);

        wireEndRt.localScale = new Vector2(wireEnd.localScale.x, _dist / 30);

        float angle = AngleBetweenTwoPoints(mousePos, startPoint);
        wireDraggableRt.rotation = Quaternion.Euler(0, 0, angle - 90);
        wireEndRt.rotation = Quaternion.Euler(0, 0, angle - 90);
        // startPoint = transform.parent.position;  // Wire Source
        // _dist = Vector2.Distance(mousePos, startPoint);

        // transform.position = mousePos;
        // //startPoint = new Vector3(wireEndRt.position.x, wireEndRt.position.y, 0);

        // RectTransform wireEndRt = wireEnd.GetComponent<RectTransform>();

        // Vector3 direction = mousePos - startPoint;
        // transform.right = direction * transform.lossyScale.x;


        // wireEnd.transform.localScale = new Vector2(wireEnd.localScale.x, _dist / 2);  // Dunno why .35f, but it kind of works for the scale

        // RectTransform rt = GetComponent<RectTransform>();
        // rt.localPosition = new Vector3(rt.anchoredPosition.x, rt.anchoredPosition.y, 0);

        // wireEndRt.localPosition = new Vector3(wireEndRt.localPosition.x, wireEnd.localPosition.y, 0);

        // float angle = AngleBetweenTwoPoints(transform.position, startPoint);
        // transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        // wireEnd.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData data)
    {
        Debug.Log("Drag end");
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 100000));

        if (colliders.Length == 0)
        {
            Debug.Log("No connection");
            //ResetWireOut();
            _connectionOut = null;
            return;
        }

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.CompareTag("Block"))
            {
                Wire otherBlock = collider.gameObject.GetComponentInChildren<Wire>();
                RectTransform rt = GetComponent<RectTransform>();
                transform.position = otherBlock._blockWirePivot.position;

                // Set block connections.
                otherBlock.ResetWireIn();
                otherBlock._connectionIn = transform.parent.transform.parent.gameObject;
                _connectionOut = collider.gameObject;

                // Do not allow wires to connect to its own block.
                if (otherBlock._connectionIn == transform.IsChildOf(collider.gameObject.transform))
                {
                    ResetWireIn();
                    Debug.Log("resetting as same block conntection");
                    return;
                }
                UpdateWireVisuals();
                // Rotate wire.
                // startPoint = new Vector3(transform.parent.position.x, transform.parent.position.y, 0);
                // _dist = Vector2.Distance(transform.position, startPoint);
                // wireEnd.transform.localScale = new Vector2(wireEnd.localScale.x, _dist * 0.24f);
                // float angle = AngleBetweenTwoPoints(transform.position, startPoint);
                // transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                // wireEnd.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint, 1f);
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // mousePos.z = 2;
        // Gizmos.DrawSphere(mousePos, 20f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 100000));
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Block"))// && !transform.IsChildOf(other.gameObject.transform))
    //     {
    //         Wire otherBlock = other.gameObject.GetComponentInChildren<Wire>();
    //         //if (!Mouse.current.IsPressed())
    //         //{
    //         RectTransform rt = GetComponent<RectTransform>();
    //         transform.position = otherBlock._blockWirePivot.position;
    //         if (otherBlock._connectionOut != null && otherBlock._connectionOut != transform.IsChildOf(other.gameObject.transform))
    //         {
    //             otherBlock.ResetWireIn();
    //         }
    //         otherBlock._connectionIn = transform.parent.gameObject.transform.parent.gameObject;
    //         _connectionOut = other.gameObject;
    //         //}
    //     }
    // }

}


