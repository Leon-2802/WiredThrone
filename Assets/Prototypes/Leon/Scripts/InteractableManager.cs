using UnityEngine;

public enum EInteractionType { Decoration, Computer, Log, Secret, Gatherable, NoInteraction };

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance;
    public bool interactionAvailable = false;
    public bool isInteracting = false;
    public bool isInteractingWithCompanionTarget = false; //set to true by InteractableObject that is CompanionTarget
    [SerializeField] private GameObject[] interactableObjects; //Maybe change this to a List<InteractableObject>, and add an enum with the name to compare this below
    [SerializeField] private Transform[] standingPositions;
    [SerializeField] private ClickMovement clickMovement;
    [SerializeField] private InteractionUIHandler interactionUIHandler;
    private Transform playerTarget; //Used to store playerTarget Transfom and use it as parameter later for ForceDestination() of Player Agent
    private float stoppingDistToInteractable; //Used to store stoppingDistance of Player Agent and use it as parameter later for ForceDestination() of Player Agent

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }


    public void EnterInteractionZone(EInteractionType interactionType, GameObject enteredObj, float stopDist)
    {
        if (interactionAvailable || interactionType == EInteractionType.NoInteraction)
            return;

        interactionAvailable = true;
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            if (interactableObjects[i] == enteredObj) //better change it to comparing enums
            {
                playerTarget = standingPositions[i]; //used to set forceDestination in StartInteraction()
            }
        }
        stoppingDistToInteractable = stopDist; //used to set forceDestination in StartInteraction()
    }
    public void StartInteraction()
    {
        isInteracting = true;
        interactionUIHandler.OpenInputInfo("ESC");
        clickMovement.ForceDestination(playerTarget, stoppingDistToInteractable); //Make player move to desired Pos
    }
    public void EndInteraction()
    {
        isInteracting = false;
        interactionUIHandler.OpenInputInfo("E");
        clickMovement.SetStoppingDistance(0); //Reset Values to let Player be moved by clicking again
        clickMovement.forcedDest = false; //Reset Values to let Player be moved by clicking again

        if (isInteractingWithCompanionTarget)
        {
            EventManager.instance.CompanionFlyBackToPlayerEvent(); //Let Companion Fly to Player and Follow him again
            isInteractingWithCompanionTarget = false;
        }
    }
    public void LeaveInteractionZone()
    {
        interactionAvailable = false;
    }
}
