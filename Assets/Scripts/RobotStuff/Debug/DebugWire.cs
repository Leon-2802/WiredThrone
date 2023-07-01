using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DebugWire : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int _id = -1;
    [SerializeField] private bool falseWire = false;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _blockWirePivot;
    [SerializeField] private GameObject _connectionOut;
    [SerializeField] private GameObject _connectionIn;
    [SerializeField] private UnityEvent _onConnected;

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
        if (_camera == null)
        {
            _camera = Camera.main;
        }
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
            DebugWire wireIn = _connectionIn.GetComponentInChildren<DebugWire>();
            _connectionIn = null;

            // wireIn == wireOut of the other block.
            wireIn.ResetWireOut();
        }
    }

    private void SetWireInVisually()
    {
        if (_connectionIn != null)
        {
            _connectionIn.GetComponentInChildren<DebugWire>().SetWireOutVisually();
        }
    }

    private void SetWireOutVisually()
    {
        if (_connectionOut != null)
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");

            Vector3 outBlockPivot = _connectionOut.GetComponentInChildren<DebugWire>().BlockWirePivot.position;
            var wireDraggableRt = GetComponent<RectTransform>();

            wireDraggableRt.position = outBlockPivot;

            var wireEndRt = wireEnd.GetComponent<RectTransform>();
            startPoint = wireEndRt.position;

            // Convert positions to canvas-relative coordinates
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), outBlockPivot, _camera, out Vector2 canvasOutBlockPivot);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), startPoint, _camera, out Vector2 canvasStartPoint);

            _dist = Vector2.Distance(canvasOutBlockPivot, canvasStartPoint);
            wireEnd.transform.localScale = new Vector2(wireEnd.localScale.x, _dist / 35);

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
        Vector3 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        var wireDraggableRt = GetComponent<RectTransform>();
        wireDraggableRt.position = mousePos;

        var wireEndRt = wireEnd.GetComponent<RectTransform>();

        // Convert positions to canvas-relative coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePos, _camera, out Vector2 canvasMousePos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), wireEndRt.position, _camera, out Vector2 canvasStartPoint);

        float dist = Vector2.Distance(canvasMousePos, canvasStartPoint);

        wireEndRt.localScale = new Vector2(wireEnd.localScale.x, dist / 35);

        float angle = AngleBetweenTwoPoints(canvasMousePos, canvasStartPoint);
        wireDraggableRt.rotation = Quaternion.Euler(0, 0, angle - 90);
        wireEndRt.rotation = Quaternion.Euler(0, 0, angle - 90);
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

            if (ConnectionOut)
            {
                DebugManager.instance.CutConnetion();
            }
            _connectionOut = null;

            //Audio Feedback when no connection found
            if (SoundManager.instance)
            {
                SoundManager.instance.PlaySoundOneShot(ESounds.DoorError);
            }

            return;
        }

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.CompareTag("Block"))
            {
                DebugWire[] otherBlocks = collider.gameObject.GetComponentsInChildren<DebugWire>();
                RectTransform rt = GetComponent<RectTransform>();
                transform.position = otherBlocks[0]._blockWirePivot.position;

                // Set block connections.
                foreach (DebugWire block in otherBlocks)
                {
                    block.ResetWireIn();
                    block._connectionIn = transform.parent.transform.parent.gameObject;
                }
                _connectionOut = collider.gameObject;

                // Do not allow wires to connect to its own block.
                if (otherBlocks[0]._connectionIn == transform.IsChildOf(collider.gameObject.transform))
                {
                    ResetWireIn();
                    Debug.Log("resetting as same block conntection");
                    return;
                }

                // If in Debug-Scene, send the ids of the connected blocks to the manager
                if (DebugManager.instance)
                {
                    if (!falseWire)
                    {
                        bool success = DebugManager.instance.ConnectedBlocks(this._id, otherBlocks[0]._id);

                        // don't let blocks connect, that are not fitting
                        if (!success)
                        {
                            ResetWireOut();
                            return;
                        }
                    }
                    else
                    {
                        bool success = DebugManager.instance.ConnectedFalseWire(otherBlocks[0]._id);
                        Debug.Log("False Wire connected: " + success);

                        // don't let blocks connect, that are not fitting
                        if (!success)
                        {
                            ResetWireOut();
                            return;
                        }
                    }
                }

                UpdateWireVisuals();
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint, 1f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 100000));
    }

}


