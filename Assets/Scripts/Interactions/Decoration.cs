using System;
using UnityEngine;
using UnityEngine.Events;

public class Decoration : InteractableObject
{
    public UnityEvent interactionStarted;
    public UnityEvent interactionEnded;
    [SerializeField] protected string interactionEndInfo;
    [SerializeField] protected Transform playerStandingPos;
    [SerializeField] protected float playerStoppingDistance;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.endInteraction += OnEndInteraction;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.endInteraction -= OnEndInteraction;
        }
    }

    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        GameManager.Instance.playerControls.Player.ClickMove.Disable(); //Keep Player locked during interaction
        base.OnStartInteraction(sender, e);

        GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionIcon, interactionButton, interactionEndInfo);
        GameManager.Instance.playerClickMovement.ForceDestination(playerStandingPos, playerStoppingDistance); //Make player move to desired Pos
        interactionStarted.Invoke();
    }
    protected virtual void OnEndInteraction(object sender, EventArgs e)
    {
        GameManager.Instance.playerControls.Player.ClickMove.Enable();
        GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionIcon, interactionButton, interactionText);

        GameManager.Instance.playerClickMovement.SetStoppingDistance(0); //Reset Values to let Player be moved by clicking again
        GameManager.Instance.playerClickMovement.forcedDest = false; //Reset Values to let Player be moved by clicking again
        interactionEnded.Invoke();
    }

    protected override void OnDestroy()
    {
        InteractableManager.Instance.endInteraction -= OnEndInteraction;
        base.OnDestroy();
    }
}
