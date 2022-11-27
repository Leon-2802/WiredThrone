using UnityEngine;

public enum EInteractionType {Decoration, Computer, Log, Secret};

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private GameObject[] interactableObjects;
    public static InteractableManager Instance;
    public bool interactionAvailable = false;
    public bool isInteracting = false;
    public bool isInteractingWithCompanionTarget = false;
    [SerializeField] private MovePlayerToInteraction movePlayerToInteraction;
    [SerializeField] private ClickMovement clickMovement;
    private GameObject currentObj;

    private void Awake() 
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }


    public void EnterInteractionZone(EInteractionType interactionType, GameObject enteredObj)
    {
        if(interactionAvailable)
            return;

        interactionAvailable = true;
        foreach(GameObject obj in interactableObjects)
        {
            if(obj == enteredObj)
            {
                currentObj = obj;
            }
        }
        Debug.Log("Press 'E' to interact");
    }
    public void StartInteraction()
    {
        isInteracting = true;
        Debug.Log("Press 'esc' to exit");
        clickMovement.ForceDestination(currentObj.transform);
    }
    public void EndInteraction()
    {
        isInteracting = false;

        if(isInteractingWithCompanionTarget)
        {
            EventManager.instance.CompanionFlyBackToPlayerEvent();
            isInteractingWithCompanionTarget = false;
        }
    }
    public void LeaveInteractionZone()
    {
        interactionAvailable = false;
        currentObj = null;
    }
}
