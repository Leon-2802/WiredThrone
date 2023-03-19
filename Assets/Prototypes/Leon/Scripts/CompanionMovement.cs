using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CompanionMovement : MonoBehaviour
{
    public bool movedBackToPlayer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform targetObj;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float rotateAroundOwnAxisSpeed = 20f;
    [SerializeField] private float maxDistanceToPlayer = 1f;
    private Transform eventTarget;
    private Vector3 initialPos;
    private float initalStoppingDist;
    private float currentDistanceToParent;
    private bool disableFollowPlayer; //true if companion is moving towards an object, ie. to give the player a hint
    private bool checkDistanceToPlayer; //set to true if an optional target was approached

    void Start()
    {
        agent.updateRotation = false; // Companion rotates around himself all the time, so no need to rotate him in the movement direction

        EventManager.instance.Subscribe<UnityAction>(EEvents.CompanionFlyBack, FlyBackToPlayer);
        EventManager.instance.Subscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, FlyToEventObj);

        initalStoppingDist = agent.stoppingDistance;
        movedBackToPlayer = true; //sits next to player on start
        disableFollowPlayer = false;
        checkDistanceToPlayer = false;
    }


    void Update()
    {
        transform.Rotate(Vector3.up, rotateAroundOwnAxisSpeed * Time.deltaTime, Space.Self); //rotate around y axis

        //was in use, made companion rotate around the player when idling:
        // if (movedBackToPlayer && !disableFollowPlayer && !InteractableManager.Instance.isInteracting)
        //     RotateAroundPlayer();

        if (GameManager.Instance.playerIsRunning && !disableFollowPlayer || !movedBackToPlayer && !disableFollowPlayer) //means, that if player is running and is not close to player, if statement is excuted
        {
            MoveTowardsTarget(targetObj.position, initalStoppingDist, false);
        }

        if (disableFollowPlayer) //if companion should move to another target than the player
        {
            MoveTowardsTarget(eventTarget.position, 0, true);
            if (checkDistanceToPlayer) //only true if an optional target is approached -> don't stay if Player doesn't follow the companion
            {
                CheckIfPlayerTooFar();
            }
        }
    }

    //Not in use for now, bc seems to look better without
    // void RotateAroundPlayer()
    // {
    //     transform.RotateAround(targetObj.position, transform.up, -rotSpeed * Time.deltaTime);
    // }

    void MoveTowardsTarget(Vector3 target, float stopDistance, bool eventTargeted)
    {
        movedBackToPlayer = false; //stays false until target is reached

        agent.stoppingDistance = stopDistance; //variable stopping distance

        agent.SetDestination(target); //set target as navmesh agent's destination 

        if (agent.remainingDistance <= agent.stoppingDistance) // => target is reached
        {
            if (!eventTargeted) //meaning if player was followed
            {
                movedBackToPlayer = true;
            }
            else
            {
                EventManager.instance.EventActionDone(EEvents.CompanionFlyToObj); //notify the EventManager that FlyToObj Action is done
            }
        }
    }

    //checks how the player is, after companion moved to another object
    void CheckIfPlayerTooFar()
    {
        float currentDistanceToPlayer = Vector3.Distance(transform.position, targetObj.position);

        if (currentDistanceToPlayer > maxDistanceToPlayer) //if the player is too far, call event to revert back to follwowing the player
        {
            EventManager.instance.CompanionFlyBackToPlayerEvent();
            checkDistanceToPlayer = false;
        }
    }

    //called the the CompanionFlyToObject event occurs
    void FlyToEventObj(Transform obj)
    {
        eventTarget = obj;
        disableFollowPlayer = true; //don't follow the player for time being

        //check if optional object 
        foreach (Transform target in GameManager.Instance.optionalCompanionTargets) 
        {
            if (target == obj)
            {
                checkDistanceToPlayer = true; //if optional -> set to true -> goes back to player, if he doesn't go to the optional target
                Debug.Log("optional Object");
            }
        }
    }

    //resets all arguments to default values -> follow player
    void FlyBackToPlayer()
    {
        movedBackToPlayer = false;
        disableFollowPlayer = false;
        checkDistanceToPlayer = false;
    }


    private void OnDestroy()
    {
        EventManager.instance.UnSubscribe<UnityAction>(EEvents.CompanionFlyBack, FlyBackToPlayer);
        EventManager.instance.UnSubscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, FlyToEventObj);
    }
}
