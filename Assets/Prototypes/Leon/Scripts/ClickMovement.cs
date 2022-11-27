using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private PlayerInteractions playerInteractions;
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem clickEffect;
    private InputAction clickMove;
    private InputAction mousePosition;
    private Vector3 moveDirection;
    private Quaternion lastRot;
    private Transform forcedDestination;
    private bool forcedDest;

    private void Awake() 
    {
        playerControls = GameManager.Instance.playerControls;
    }
    private void Start() 
    {
        agent.updateRotation = false;
        forcedDest = false;
    }
    private void OnEnable() 
    {
        clickMove = playerControls.Player.ClickMove;
        clickMove.Enable();
        clickMove.performed += RightClickOnScene;

        mousePosition = playerControls.Player.MousePosition;
        mousePosition.Enable();
    }

    void Update()
    {
        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // animator.speed = agent.velocity.sqrMagnitude;
                if (!agent.hasPath || agent.velocity.sqrMagnitude <= Mathf.Epsilon)
                {
                    animator.SetBool("Running", false);
                    GameManager.Instance.playerIsRunning = false;
                    if(forcedDest) 
                    {
                        forcedDest = false;
                        playerInteractions.ActivateShoulderCam();
                    }
                }
            }
        }
    }
    
    void LateUpdate() 
    {
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            lastRot = transform.rotation;
        }
        else {
            transform.rotation = lastRot;
        }
    }

    private void RightClickOnScene(InputAction.CallbackContext context)
    {
        if(InteractableManager.Instance.isInteracting)
            return;

        Ray ray = cam.ScreenPointToRay(mousePosition.ReadValue<Vector2>());
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            // if(hit.point.y > 0.5f)
            //     return;

            clickEffect.transform.position = new Vector3(hit.point.x, (0.2f), hit.point.z);
            clickEffect.Play();
            agent.SetDestination(hit.point);

            animator.SetBool("Running", true);
            GameManager.Instance.playerIsRunning = true;
        }
    }

    public void ForceDestination(Transform dest)
    {
        agent.SetDestination(dest.position);
        animator.SetBool("Running", true);

        forcedDest = true; 
        forcedDestination = dest;
    }

    private void OnDisable() 
    {
        clickMove.Disable();
        mousePosition.Disable();
    }
}
