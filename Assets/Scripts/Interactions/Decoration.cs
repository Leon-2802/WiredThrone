using System;
using UnityEngine;

public class Decoration : InteractableObject
{
    [SerializeField] protected string interactionEndInfo;
    [SerializeField] protected Transform playerStandingPos;
    [SerializeField] protected float playerStoppingDistance;
    [SerializeField] protected ClickMovement clickMovement;

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
        base.OnStartInteraction(sender, e);

        GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionEndInfo);
        clickMovement.ForceDestination(playerStandingPos, playerStoppingDistance); //Make player move to desired Pos
    }
    protected virtual void OnEndInteraction(object sender, EventArgs e)
    {
        GeneralUIHandler.instance.InvokeOpenInteractionInfo(interactionStartInfo);


        clickMovement.SetStoppingDistance(0); //Reset Values to let Player be moved by clicking again
        clickMovement.forcedDest = false; //Reset Values to let Player be moved by clicking again
    }

    protected override void OnDestroy()
    {
        InteractableManager.Instance.endInteraction -= OnEndInteraction;
        base.OnDestroy();
    }
}
