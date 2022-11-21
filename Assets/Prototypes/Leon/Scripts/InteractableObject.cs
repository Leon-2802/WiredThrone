using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected EInteractionType interactionType;
    [SerializeField] protected MeshRenderer meshToHighlight;
    [SerializeField] protected int materialNumber;
    protected Material initialMat;
    protected bool isCompanionTarget;

    protected virtual void Start() 
    {
        initialMat = new Material(meshToHighlight.materials[materialNumber]);
        isCompanionTarget = false;

        EventManager.instance.SubscribeToCompanionFlyEvent(OnCompanionFlyToTarget);
        EventManager.instance.Subscribe(EEvents.CompanionFlyBack, OnCompanionFlyToPlayer);
    }

    protected virtual void OnCompanionFlyToTarget(Transform target)
    {
        if(target == this .gameObject.transform)
            isCompanionTarget = true;
    }
    protected virtual void OnCompanionFlyToPlayer()
    {
        if(isCompanionTarget)
            isCompanionTarget = false;
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.EnterInteractionZone(interactionType);
            ChangeMat(ThemeManager.instance.interactionAvailable);

            if(isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.LeaveInteractionZone();
            ChangeMat(initialMat);

            if(isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = false;
        }
    }

    protected void ChangeMat(Material mat)
    {
        Material[] matList = meshToHighlight.materials;
        matList[materialNumber] = mat;

        meshToHighlight.materials = matList;
    }
}
