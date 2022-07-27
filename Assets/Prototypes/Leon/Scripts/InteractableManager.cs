using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public enum InteractionType {Decoration, Computer, Log, Secret};
    public static InteractableManager Instance;
    public bool interactionAvailable = false;
    public bool isInteracting = false;

    private void Awake() 
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    public void EnterInteractionZone(InteractionType interactionType)
    {
        if(interactionAvailable)
            return;

        interactionAvailable = true;
        Debug.Log("Press 'E' to interact");
    }
    public void startInteraction()
    {
        isInteracting = true;
        Debug.Log("Press 'esc' to exit");
    }
    public void endInteraction()
    {
        isInteracting = false;
    }
    public void LeaveInteractionZone()
    {
        interactionAvailable = false;
    }
}
