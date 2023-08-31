using System;
using UnityEngine;

public class Computer : Decoration
{
    protected bool unavailable = false;

    [SerializeField] protected GameObject interactionInfo;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.GetComponent<Player>())
        {
            if (!unavailable && this.enabled && !onlyInspect)
                interactionInfo.SetActive(true);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.GetComponent<Player>())
        {
            if (!unavailable && this.enabled && !onlyInspect)
                interactionInfo.SetActive(false);
        }
    }

    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        base.OnStartInteraction(sender, e);
        GameManager.Instance.playerControls.Computer.Disable(); // make sure controls are disabled, as long as com is not used yet
        InteractableManager.Instance.isInteractingWithCom = true;
        InteractableManager.Instance.startCom += StartCom;
        InteractableManager.Instance.endCom += ExitCom;
    }

    protected override void OnEndInteraction(object sender, EventArgs e)
    {
        base.OnEndInteraction(sender, e);
        InteractableManager.Instance.isInteractingWithCom = false;
        InteractableManager.Instance.startCom -= StartCom;
        InteractableManager.Instance.endCom -= ExitCom;
        CameraController.instance.ResetBlendTime(); //Safety measure, in case Computer was not exited properly
    }

    protected virtual void StartCom(object sender, EventArgs e)
    {
        GameManager.Instance.playerControls.Player.Disable();
        GameManager.Instance.playerControls.Computer.Enable();
    }

    protected virtual void ExitCom(object sender, EventArgs e)
    {
        GameManager.Instance.playerControls.Player.Enable();
        GameManager.Instance.playerControls.Player.ClickMove.Disable(); // Keep ClickMovement disabled, so player can't move while interacting with computer
        GameManager.Instance.playerControls.Computer.Disable();
    }

    protected override void OnDestroy()
    {
        InteractableManager.Instance.endInteraction -= StartCom;
        InteractableManager.Instance.endCom -= ExitCom;
        GameManager.Instance.playerControls.Player.Enable();
        GameManager.Instance.playerControls.Computer.Disable();
        // -------------------------------------
        base.OnDestroy();
    }
}
