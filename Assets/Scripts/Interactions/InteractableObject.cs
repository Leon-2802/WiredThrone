using System;
using UnityEngine;

public class InteractableObject : InspectableObject
{
    [SerializeField] protected bool isOneShotInteraction;
    [SerializeField] protected Sprite interactionIcon;
    [SerializeField] protected string interactionButton;
    [SerializeField] protected string interactionText;
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

        if (!this.isActiveAndEnabled)
            return;

        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.EnterInteractionZone(isOneShotInteraction);
            InteractableManager.Instance.startInteraction += OnStartInteraction;
            GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionIcon, interactionButton, interactionText);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (!this.isActiveAndEnabled)
            return;

        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.LeaveInteractionZone();
            InteractableManager.Instance.startInteraction -= OnStartInteraction;

            //! InteractionInfo closed in base class

            if (isCompanionTarget)
            {
                CompanionEvents.instance.CallFlyBackToPlayer(); //Let Companion Fly to Player and Follow him again
            }
        }
    }

    protected virtual void OnStartInteraction(object sender, EventArgs e)
    {
    }

    protected virtual void OnDestroy()
    {
        CompanionEvents.instance.flyToObject -= OnCompanionFlyToTarget;
        CompanionEvents.instance.flyBackToPlayer -= OnCompanionFlyToPlayer;

        InteractableManager.Instance.startInteraction -= OnStartInteraction;

        if (isCompanionTarget)
        {
            CompanionEvents.instance.CallFlyBackToPlayer(); //Let Companion Fly to Player and Follow him again
        }
    }
}
