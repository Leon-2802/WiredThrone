using UnityEngine;

public class SetObjectsInteractable : MonoBehaviour
{
    [SerializeField] private InteractableObject[] objects;

    public void SetInteractable()
    {
        foreach (ScrapPart obj in objects)
        {
            obj.SetOnlyInspect(false);
        }
    }
}
