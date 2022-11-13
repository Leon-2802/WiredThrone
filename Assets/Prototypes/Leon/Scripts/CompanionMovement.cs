using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    
    [SerializeField] private Transform targetObj;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float moveStepsPerCall = 0.01f;
    [SerializeField] private float rotateAroundOwnAxisSpeed = 20f;
    private Transform eventTarget;
    private Vector3 initialPos;
    private float initialDistanceToParent;
    private float currentDistanceToParent;
    private bool disableFollowPlayer;
    private bool playerIsRunning;
    private bool movedBackToPlayer;


    void Start()
    {
        EventManager.instance.SubscribeToCompanionFlyEvent(FlyToEventObj);

        initialDistanceToParent = Vector3.Distance(transform.position, targetObj.position);
        initialPos = transform.position;
        movedBackToPlayer = true;
        disableFollowPlayer = false;
    }


    void Update()
    {
        transform.Rotate(Vector3.up, rotateAroundOwnAxisSpeed * Time.deltaTime, Space.Self);

        playerIsRunning = GameManager.Instance.playerIsRunning;


        if(movedBackToPlayer && !disableFollowPlayer)
            RotateAroundPlayer();   

        if(playerIsRunning && !disableFollowPlayer || !movedBackToPlayer && !disableFollowPlayer)
            MoveTowardsTarget(targetObj.position, initialDistanceToParent, false);

        if(disableFollowPlayer)
        {
            MoveTowardsTarget(eventTarget.position, initialDistanceToParent, true);
        }
    }

    void RotateAroundPlayer()
    {
        transform.RotateAround(targetObj.position, transform.up, -rotSpeed * Time.deltaTime);
    }

    void MoveTowardsTarget(Vector3 target, float stopDistance, bool eventTargeted)
    {
        movedBackToPlayer = false;

        currentDistanceToParent = Vector3.Distance(transform.position, target);

        if(currentDistanceToParent > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveStepsPerCall);
            transform.position = new Vector3(transform.position.x, initialPos.y, transform.position.z);
        }
        
        else
        {
            if(!eventTargeted)
                movedBackToPlayer = true;
            else
                EventManager.instance.EventActionDone(EEvents.CompanionFlyToObj);
        }
    }

    void FlyToEventObj(Transform obj)
    {
        eventTarget = obj;
        disableFollowPlayer = true;
    }
}
