using System;
using UnityEngine;

public class Computer : Decoration
{
    [SerializeField] protected GameObject interactionInfo;
    [SerializeField] protected GameObject computerCam;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.isInteractingWithCom = true;
            interactionInfo.SetActive(true);
            InteractableManager.Instance.startCom += StartCom;
            InteractableManager.Instance.endCom += ExitCom;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.isInteractingWithCom = false;
            interactionInfo.SetActive(false);
            InteractableManager.Instance.startCom -= StartCom;
            InteractableManager.Instance.endCom -= ExitCom;
        }
    }

    protected virtual void StartCom(object sender, EventArgs e)
    {
        computerCam.SetActive(true);
        GameManager.Instance.playerControls.Player.Disable();
        GameManager.Instance.playerControls.Computer.Enable();
    }

    protected virtual void ExitCom(object sender, EventArgs e)
    {
        computerCam.SetActive(false);
        GameManager.Instance.playerControls.Player.Enable();
        GameManager.Instance.playerControls.Computer.Disable();
    }

    protected override void OnDestroy()
    {
        InteractableManager.Instance.endInteraction -= StartCom;
        InteractableManager.Instance.endCom -= ExitCom;
        //just in case something goes wrong ----
        computerCam.SetActive(false);
        GameManager.Instance.playerControls.Player.Enable();
        GameManager.Instance.playerControls.Computer.Disable();
        // -------------------------------------
        base.OnDestroy();
    }
}
