using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    [SerializeField] protected Renderer rendererRef;
    [SerializeField] protected Sprite objectImage;
    [SerializeField] protected string objectInfo;
    protected Color previousOutlineColor;
    protected float previousOutlineSize;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!this.isActiveAndEnabled)
            return;

        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeOpenInspector(objectImage, objectInfo);

            previousOutlineColor = rendererRef.materials[0].GetColor("_OutlineColor");
            rendererRef.materials[0].SetColor("_OutlineColor", Color.white);
            previousOutlineSize = rendererRef.materials[0].GetFloat("_OutlineSize");
            rendererRef.materials[0].SetFloat("_OutlineSize", 8);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!this.isActiveAndEnabled)
            return;

        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeCloseInspector();

            rendererRef.materials[0].SetColor("_OutlineColor", previousOutlineColor);
            rendererRef.materials[0].SetFloat("_OutlineSize", previousOutlineSize);
        }
    }
}
