using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected EInteractionType interactionType;
    [SerializeField] protected MeshRenderer meshToHighlight;
    [SerializeField] protected int materialNumber;
    protected Material initialMat;

    protected void Start() 
    {
        initialMat = new Material(meshToHighlight.materials[materialNumber]);
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.EnterInteractionZone(interactionType);
            ChangeMat(ThemeManager.instance.interactionAvailable);
        }
    }
    protected virtual void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.LeaveInteractionZone();
            ChangeMat(initialMat);
        }
    }

    protected void ChangeMat(Material mat)
    {
        Material[] matList = meshToHighlight.materials;
        matList[materialNumber] = mat;

        meshToHighlight.materials = matList;
    }
}
