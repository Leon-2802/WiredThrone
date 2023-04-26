using System;
using UnityEngine;

public enum EInteractionType { Decoration, Computer, Log, Secret, Gatherable };

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance;
    public bool interactionAvailable = false;
    public bool isOneShotInteraction = false;
    public bool isInteracting = false;
    public event EventHandler startInteraction;
    public event EventHandler endInteraction;

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
        if (!isOneShotInteraction) //One Shot Interactions have no duration, so no need to set the bool
            isInteracting = true;
        startInteraction?.Invoke(this, EventArgs.Empty);
    }
    public void EndInteraction()
    {
        if (!isOneShotInteraction)
            isInteracting = false;
        endInteraction?.Invoke(this, EventArgs.Empty);
    }
    public void LeaveInteractionZone()
    {
        isInteracting = false;
        interactionAvailable = false;
    }
}
