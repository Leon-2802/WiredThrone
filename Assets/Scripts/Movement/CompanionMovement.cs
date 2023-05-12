using System;
using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
    public bool movedBackToPlayer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float rotateAroundOwnAxisSpeed = 20f;
    [SerializeField] private float maxDistanceToPlayer = 1f;
    private Transform eventTarget;
    private Vector3 initialPos;
    private float initialStoppingDist;
    private float currentDistanceToParent;
    private bool disableFollowPlayer; //true if companion is moving towards an object, ie. to give the player a hint
    private bool checkDistanceToPlayer; //set to true if an optional target was approached

    void Start()
    {
        agent.updateRotation = false; // Companion rotates around himself all the time, so no need to rotate him in the movement direction

        CompanionEvents.instance.flyToObject += FlyToEventObj;
        CompanionEvents.instance.flyBackToPlayer += FlyBackToPlayer;

        initialStoppingDist = agent.stoppingDistance;
        disableFollowPlayer = false;
        checkDistanceToPlayer = false;
    }


    void Update()
    {
        transform.Rotate(Vector3.up, rotateAroundOwnAxisSpeed * Time.deltaTime, Space.Self); //rotate around y axis

        if (GameManager.Instance.playerIsRunning && !disableFollowPlayer || !movedBackToPlayer && !disableFollowPlayer) //means, that if player is running and is not close to player, if statement is excuted
        {
            MoveTowardsTarget(player.position, initialStoppingDist, false);
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
            //maybe call an Event for EventActionDone here
        }
    }

    //checks how the player is, after companion moved to another object
    void CheckIfPlayerTooFar()
    {
        float currentDistanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (currentDistanceToPlayer > maxDistanceToPlayer) //if the player is too far, call event to revert back to following the player
        {
            CompanionEvents.instance.CallFlyBackToPlayer();
            checkDistanceToPlayer = false;
        }
    }

    //called the the CompanionFlyToObject event occurs
    void FlyToEventObj(object sender, CompanionEvents.FlyToObjectEventArgs e)
    {
        eventTarget = e._target;
        disableFollowPlayer = true; //don't follow the player for time being

        //check if optional object 
        if (e._target.gameObject.GetComponent<CompanionTarget>().optionalTarget == true)
        {
            checkDistanceToPlayer = true; //if optional -> set to true -> goes back to player, if he doesn't go to the optional target
            Debug.Log("optional Object");
        }
    }

    //resets all arguments to default values -> follow player
    void FlyBackToPlayer(object sender, EventArgs e)
    {
        movedBackToPlayer = false;
        disableFollowPlayer = false;
        checkDistanceToPlayer = false;
    }


    private void OnDestroy()
    {
        CompanionEvents.instance.flyToObject -= FlyToEventObj;
        CompanionEvents.instance.flyBackToPlayer -= FlyBackToPlayer;
    }
}
