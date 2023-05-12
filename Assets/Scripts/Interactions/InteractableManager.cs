using System;
using UnityEngine;

public enum EInteractionType { Decoration, Computer, Log, Secret, Gatherable };

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance;
    public bool interactionAvailable = false;
    public bool isOneShotInteraction = false;
    public bool isInteracting = false;
    public bool isInteractingWithCom = false;
    public event EventHandler startInteraction;
    public event EventHandler endInteraction;
    public event EventHandler startCom;
    public event EventHandler endCom;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }


    public void EnterInteractionZone(bool isOneShotInteraction)
    {
        if (interactionAvailable)
            return;

        this.isOneShotInteraction = isOneShotInteraction;
        interactionAvailable = true;
    }
    public void StartInteraction()
    {
        if (!isOneShotInteraction)
        { //One Shot Interactions have no duration, so no need to set the bool 
            isInteracting = true;
            GameManager.Instance.playerControls.Player.ClickMove.Disable();
        }
        startInteraction?.Invoke(this, EventArgs.Empty);
    }
    public void EndInteraction()
    {
        if (!isOneShotInteraction)
        {
            isInteracting = false;
            GameManager.Instance.playerControls.Player.ClickMove.Enable();
        }
        endInteraction?.Invoke(this, EventArgs.Empty);
    }
    public void LeaveInteractionZone()
    {
        isInteracting = false;
        interactionAvailable = false;
    }

    public void InvokeStartCom()
    {
        startCom.Invoke(this, EventArgs.Empty);
    }
    public void InvokeEndCom()
    {
        endCom.Invoke(this, EventArgs.Empty);
    }
}
