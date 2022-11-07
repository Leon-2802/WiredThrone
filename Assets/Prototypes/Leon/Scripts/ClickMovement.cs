using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem clickEffect;
    private InputAction clickMove;
    private InputAction mousePosition;
    private Vector3 moveDirection;
    private Quaternion lastRot;

    private void Awake() 
    {
        playerControls = GameManager.Instance.playerControls;
    }
    private void Start() 
    {
        agent.updateRotation = false;
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
            clickEffect.transform.position = new Vector3(hit.point.x, (hit.point.y + 0.2f), hit.point.z);
            clickEffect.Play();
            agent.SetDestination(hit.point);

            animator.SetBool("Running", true);
            GameManager.Instance.playerIsRunning = true;
            // transform.forward = hit.point;
        }
    }

    private void OnDisable() 
    {
        clickMove.Disable();
        mousePosition.Disable();
    }
}
