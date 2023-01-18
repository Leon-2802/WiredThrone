using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
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
    private bool disableFollowPlayer;
    private bool checkDistanceToPlayer;
    private bool playerIsRunning;
    private bool movedBackToPlayer;


    void Start()
    {
        agent.updateRotation = false;

        EventManager.instance.SubscribeToCompanionFlyEvent(FlyToEventObj);
        EventManager.instance.Subscribe(EEvents.CompanionFlyBack, FlyBackToPlayer);

        initalStoppingDist = agent.stoppingDistance;
        movedBackToPlayer = true;
        disableFollowPlayer = false;
        checkDistanceToPlayer = false;
    }


    void Update()
    {
        transform.Rotate(Vector3.up, rotateAroundOwnAxisSpeed * Time.deltaTime, Space.Self);

        playerIsRunning = GameManager.Instance.playerIsRunning;


        if(movedBackToPlayer && !disableFollowPlayer && !InteractableManager.Instance.isInteracting)
            RotateAroundPlayer();   

        if(playerIsRunning && !disableFollowPlayer || !movedBackToPlayer && !disableFollowPlayer)
            MoveTowardsTarget(targetObj.position, initalStoppingDist, false);

        if(disableFollowPlayer)
        {
            MoveTowardsTarget(eventTarget.position, 0, true);
            if(checkDistanceToPlayer)
            {
                CheckIfPlayerTooFar();
            }
        }
    }

    void RotateAroundPlayer()
    {
        // transform.RotateAround(targetObj.position, transform.up, -rotSpeed * Time.deltaTime);
    }

    void MoveTowardsTarget(Vector3 target, float stopDistance, bool eventTargeted)
    {
        movedBackToPlayer = false;

        // currentDistanceToParent = Vector3.Distance(transform.position, target);

        // if(currentDistanceToParent > stopDistance)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        //     transform.position = new Vector3(transform.position.x, initialPos.y, transform.position.z);
        // }
        agent.stoppingDistance = stopDistance;

        agent.SetDestination(target);

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!eventTargeted)
                movedBackToPlayer = true;
            else
                EventManager.instance.EventActionDone(EEvents.CompanionFlyToObj);
        }
    }

    void CheckIfPlayerTooFar()
    {
        float currentDistanceToPlayer = Vector3.Distance(transform.position, targetObj.position);

        if(currentDistanceToPlayer > maxDistanceToPlayer)
        {
            EventManager.instance.CompanionFlyBackToPlayerEvent();
            checkDistanceToPlayer = false;
        }
    }

    void FlyToEventObj(Transform obj)
    {
        eventTarget = obj;
        disableFollowPlayer = true;

        foreach(Transform target in GameManager.Instance.optionalCompanionTargets)
        {
            if(target == obj)
            {
                checkDistanceToPlayer = true;
                Debug.Log("optional Object");
            }
        }
    }

    void FlyBackToPlayer()
    {
        movedBackToPlayer = false;
        disableFollowPlayer = false;
        checkDistanceToPlayer = false;
    }
}
