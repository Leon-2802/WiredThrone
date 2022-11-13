using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    
    [SerializeField] private float rotSpeed;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private float moveStepsPerCall = 0.01f;
    private Vector3 initialPos;
    private float initialDistanceToParent;
    private float currentDistanceToParent;
    private bool playerIsRunning;
    private bool movedBackToPlayer;


    void Start()
    {
        initialDistanceToParent = Vector3.Distance(transform.position, pivotPoint.position);
        initialPos = transform.position;
        movedBackToPlayer = true;
    }


    void Update()
    {
        playerIsRunning = GameManager.Instance.playerIsRunning;

        if(movedBackToPlayer)
            RotateAroundPlayer();   

        if(playerIsRunning || !movedBackToPlayer)
            MoveTowardsTarget(pivotPoint.position, initialDistanceToParent);
    }

    void RotateAroundPlayer()
    {
        transform.RotateAround(pivotPoint.position, transform.up, -rotSpeed * Time.deltaTime);
    }

    void MoveTowardsTarget(Vector3 target, float stopDistance)
    {
        movedBackToPlayer = false;

        currentDistanceToParent = Vector3.Distance(transform.position, pivotPoint.position);

        if(currentDistanceToParent > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveStepsPerCall);
            transform.position = new Vector3(transform.position.x, initialPos.y, transform.position.z);

            if(playerIsRunning)
                transform.rotation = pivotPoint.rotation; //update rotation to player rotation while he is moving
        }
        
        else
            movedBackToPlayer = true;
    }
}
