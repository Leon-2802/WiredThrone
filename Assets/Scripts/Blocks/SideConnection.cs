using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    left,
    right
}
public class SideConnection : MonoBehaviour
{
    [SerializeField] private GameObject _sideBlock;
    [SerializeField] private Transform _pivot;
    [SerializeField] private Side _side;

    public GameObject SideBlock
    {
        get { return _sideBlock; }
    }

    public Side Side
    {
        get { return _side; }
    }

    public Transform Pivot
    {
        get { return _pivot; }
    }

    public void SetSideBlock(GameObject block)
    {
        Debug.Log("Setting");
        if (_sideBlock == null)
        {
            _sideBlock = block;
            _sideBlock.GetComponent<SideConnection>().SetSideBlock(this.gameObject);
            SetPosition();
        }
    }

    public void RemoveSideBlock()
    {
        if (_sideBlock != null)
        {
            _sideBlock.transform.parent = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            _sideBlock = null;
        }
    }

    public void SetPosition() {
        switch (_side)
        {
            case Side.left:

                break;
            case Side.right:
                Vector3 sidePos = _sideBlock.GetComponent<SideConnection>().Pivot.position;
                GetComponent<RectTransform>().position = new Vector2(sidePos.x, sidePos.y);
                transform.parent = _sideBlock.transform;
                break;

        }
    }
}
