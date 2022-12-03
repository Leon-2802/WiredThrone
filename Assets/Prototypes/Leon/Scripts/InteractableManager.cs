using UnityEngine;

public enum EInteractionType {Decoration, Computer, Log, Secret};

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private GameObject[] interactableObjects;
    [SerializeField] private Transform[] standingPositions;
    public static InteractableManager Instance;
    public bool interactionAvailable = false;
    public bool isInteracting = false;
    public bool isInteractingWithCompanionTarget = false;
    [SerializeField] private MovePlayerToInteraction movePlayerToInteraction;
    [SerializeField] private ClickMovement clickMovement;
    private Transform playerTarget;
    private float stoppingDistToInteractable;

    private void Awake() 
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }


    public void EnterInteractionZone(EInteractionType interactionType, GameObject enteredObj, float stopDist)
    {
        if(interactionAvailable)
            return;

        interactionAvailable = true;
        for(int i = 0; i < interactableObjects.Length; i++)
        {
            if(interactableObjects[i] == enteredObj)
            {
                playerTarget = standingPositions[i];
                Debug.Log(playerTarget);
            }
        }
        stoppingDistToInteractable = stopDist;
        Debug.Log("Press 'E' to interact");
    }
    public void StartInteraction()
    {
        isInteracting = true;
        Debug.Log("Press 'esc' to exit");
        clickMovement.ForceDestination(playerTarget, stoppingDistToInteractable);
    }
    public void EndInteraction()
    {
        isInteracting = false;
        clickMovement.SetStoppingDistance(0);
        clickMovement.forcedDest = false;

        if(isInteractingWithCompanionTarget)
        {
            EventManager.instance.CompanionFlyBackToPlayerEvent();
            isInteractingWithCompanionTarget = false;
        }
    }
    public void LeaveInteractionZone()
    {
        interactionAvailable = false;
    }
}
