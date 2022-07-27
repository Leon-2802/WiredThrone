using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected InteractableManager.InteractionType interactionType;
    [SerializeField] protected MeshRenderer mesh;
    [SerializeField] protected int materialNumber;
    protected Material initialMat;

    protected void Start() 
    {
        initialMat = new Material(mesh.materials[materialNumber]);
    }
    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            InteractableManager.Instance.EnterInteractionZone(interactionType);
            ChangeMat(ThemeManager.instance.interactionAvailable);
        }
    }
    protected virtual void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            InteractableManager.Instance.LeaveInteractionZone();
            ChangeMat(initialMat);
        }
    }

    protected void ChangeMat(Material mat)
    {
        Material[] matList = mesh.materials;
        matList[materialNumber] = mat;

        mesh.materials = matList;
    }
}
