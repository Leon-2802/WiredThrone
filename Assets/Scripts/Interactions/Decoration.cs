using System;
using UnityEngine;

public class Decoration : InteractableObject
{
    [SerializeField] protected Transform playerStandingPos;
    [SerializeField] protected float playerStoppingDistance;
    [SerializeField] protected ClickMovement clickMovement;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        base.OnStartInteraction(sender, e);

        clickMovement.ForceDestination(playerStandingPos, playerStoppingDistance); //Make player move to desired Pos
    }
    protected override void OnEndInteraction(object sender, EventArgs e)
    {
        base.OnEndInteraction(sender, e);

        clickMovement.SetStoppingDistance(0); //Reset Values to let Player be moved by clicking again
        clickMovement.forcedDest = false; //Reset Values to let Player be moved by clicking again
    }
}
