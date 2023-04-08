using System;
using UnityEngine;

public class InteractableObject : InspectableObject
{
    protected EInteractionType interactionType;
    [SerializeField] protected string interactionInfo;
    [SerializeField] protected float playerStoppingDistance;
    [SerializeField] protected Transform companionStopPos;
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.EnterInteractionZone(interactionType,
                this.gameObject, playerStoppingDistance);

            GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionInfo);

            if (isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = true;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.gameObject.GetComponent<Player>())
        {
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
