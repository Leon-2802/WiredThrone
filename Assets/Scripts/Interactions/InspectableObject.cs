using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    [SerializeField] protected Renderer[] rendererRefs;
    [SerializeField] protected Sprite objectImage;
    [SerializeField] protected string objectInfo;
    protected Color previousOutlineColor;
    protected float[] previousOutlineSizes;

    protected void Awake()
    {
        previousOutlineSizes = new float[rendererRefs.Length];
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!this.isActiveAndEnabled)
            return;

        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeOpenInspector(objectImage, objectInfo);

            previousOutlineColor = rendererRefs[0].materials[0].GetColor("_OutlineColor");
            for (int i = 0; i < rendererRefs.Length; i++)
            {
                previousOutlineSizes[i] = rendererRefs[i].materials[0].GetFloat("_OutlineSize");
                rendererRefs[i].materials[0].SetColor("_OutlineColor", Color.white);
                rendererRefs[i].materials[0].SetFloat("_OutlineSize", previousOutlineSizes[i] * 1.4f);
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!this.isActiveAndEnabled)
            return;

        if (other.gameObject.GetComponent<Player>())
        {
            GeneralUIHandler.instance.InvokeCloseInspector();

            for (int i = 0; i < rendererRefs.Length; i++)
            {
                rendererRefs[i].materials[0].SetColor("_OutlineColor", previousOutlineColor);
                rendererRefs[i].materials[0].SetFloat("_OutlineSize", previousOutlineSizes[i]);
            }
        }
    }
}
