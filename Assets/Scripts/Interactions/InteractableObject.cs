using System;
using UnityEngine;

public class InteractableObject : InspectableObject
{
    protected EInteractionType interactionType;
    [SerializeField] protected string interactionStartInfo;
    [SerializeField] protected string interactionEndInfo;
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
            InteractableManager.Instance.EnterInteractionZone();
            InteractableManager.Instance.startInteraction += OnStartInteraction;
            InteractableManager.Instance.endInteraction += OnEndInteraction;
            GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionStartInfo);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.LeaveInteractionZone();
            InteractableManager.Instance.startInteraction -= OnStartInteraction;
            InteractableManager.Instance.endInteraction -= OnEndInteraction;
            //! InteractionInfo closed in base class
        }
    }

    protected virtual void OnStartInteraction(object sender, EventArgs e)
    {
        GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionEndInfo);
    }
    protected virtual void OnEndInteraction(object sender, EventArgs e)
    {
        GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionStartInfo);
        if (isCompanionTarget)
        {
            CompanionEvents.instance.CallFlyBackToPlayer(); //Let Companion Fly to Player and Follow him again
        }
    }

    protected void OnDestroy()
    {
        CompanionEvents.instance.flyToObject -= OnCompanionFlyToTarget;
        CompanionEvents.instance.flyBackToPlayer -= OnCompanionFlyToPlayer;

        InteractableManager.Instance.startInteraction -= OnStartInteraction;
        InteractableManager.Instance.endInteraction -= OnEndInteraction;

        if (isCompanionTarget)
        {
            CompanionEvents.instance.CallFlyBackToPlayer(); //Let Companion Fly to Player and Follow him again
        }
    }
}
