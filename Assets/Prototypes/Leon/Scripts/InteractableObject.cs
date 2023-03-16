using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected EInteractionType interactionType;
    [SerializeField] protected float playerStoppingDistance;
    [SerializeField] protected MeshRenderer meshToHighlight;
    [SerializeField] protected int materialNumber;
    [SerializeField] protected Transform companionStopPos;
    protected Material initialMat;
    protected bool isCompanionTarget;

    protected virtual void Start()
    {
        initialMat = new Material(meshToHighlight.materials[materialNumber]);
        isCompanionTarget = false;

        EventManager.instance.Subscribe<UnityAction>(EEvents.CompanionFlyBack, OnCompanionFlyToPlayer);
        EventManager.instance.Subscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, OnCompanionFlyToTarget);
    }

    protected virtual void OnCompanionFlyToTarget(Transform target)
    {
        if (target == this.companionStopPos)
            isCompanionTarget = true;
    }
    protected virtual void OnCompanionFlyToPlayer()
    {
        if (isCompanionTarget)
            isCompanionTarget = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.EnterInteractionZone(interactionType,
                this.gameObject, playerStoppingDistance);
            ChangeMat(ThemeManager.instance.interactionAvailable);

            if (isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.LeaveInteractionZone();
            ChangeMat(initialMat);

            if (isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = false;
        }
    }

    protected void ChangeMat(Material mat)
    {
        Material[] matList = meshToHighlight.materials;
        matList[materialNumber] = mat;

        meshToHighlight.materials = matList;
    }


    protected void OnDestroy()
    {
        EventManager.instance.UnSubscribe<UnityAction>(EEvents.CompanionFlyBack, OnCompanionFlyToPlayer);
        EventManager.instance.UnSubscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, OnCompanionFlyToTarget);
    }
}
