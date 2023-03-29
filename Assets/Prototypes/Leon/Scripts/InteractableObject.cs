using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected EInteractionType interactionType;
    [SerializeField] protected float playerStoppingDistance;
    [SerializeField] protected Transform companionStopPos;
    [SerializeField] private UnityEvent enteredInteractionZone;
    [SerializeField] private UnityEvent leftInteractionZone;

    protected Material initialMat;
    protected bool isCompanionTarget;

    protected virtual void Start()
    {
        isCompanionTarget = false;

        CompanionEvents.instance.flyToObject += OnCompanionFlyToTarget;
        CompanionEvents.instance.flyBackToPlayer += OnCompanionFlyToPlayer;
    }

    protected virtual void OnCompanionFlyToTarget(object sender, CompanionEvents.FlyToObjectEventArgs e)
    {
        if (e._target == this.companionStopPos)
            isCompanionTarget = true;
    }
    protected virtual void OnCompanionFlyToPlayer(object sender, EventArgs e)
    {
        if (isCompanionTarget)
            isCompanionTarget = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            enteredInteractionZone.Invoke();

            InteractableManager.Instance.EnterInteractionZone(interactionType,
                this.gameObject, playerStoppingDistance);

            if (isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            leftInteractionZone.Invoke();

            InteractableManager.Instance.LeaveInteractionZone();

            if (isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = false;
        }
    }

    protected void OnDestroy()
    {
        CompanionEvents.instance.flyToObject -= OnCompanionFlyToTarget;
        CompanionEvents.instance.flyBackToPlayer -= OnCompanionFlyToPlayer;
    }
}
