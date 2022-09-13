using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInteractionType {Decoration, Computer, Log, Secret};

public class InteractableManager : MonoBehaviour
{
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
    public void EnterInteractionZone(EInteractionType interactionType)
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
