using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    [SerializeField] protected Sprite objectImage;
    [SerializeField] protected string objectInfo;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeOpenInspector(objectImage, objectInfo);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeCloseInspector();
        }
    }
}
