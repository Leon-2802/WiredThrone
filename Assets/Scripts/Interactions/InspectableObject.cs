using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    [SerializeField] protected new Renderer renderer;
    [SerializeField] protected Sprite objectImage;
    [SerializeField] protected string objectInfo;
    protected Color previousOutlineColor;
    protected float previousOutlineSize;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeOpenInspector(objectImage, objectInfo);

            previousOutlineColor = renderer.materials[0].GetColor("_OutlineColor");
            renderer.materials[0].SetColor("_OutlineColor", Color.white);
            previousOutlineSize = renderer.materials[0].GetFloat("_OutlineSize");
            renderer.materials[0].SetFloat("_OutlineSize", 8);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeCloseInspector();

            renderer.materials[0].SetColor("_OutlineColor", previousOutlineColor);
            renderer.materials[0].SetFloat("_OutlineSize", previousOutlineSize);
        }
    }
}
