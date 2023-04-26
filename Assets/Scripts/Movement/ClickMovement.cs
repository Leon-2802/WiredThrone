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
    public bool forcedDest;
    private float initalStoppingDistance;

    private void OnEnable()
    {
        /*<------ enable controls*/
        playerControls = GameManager.Instance.playerControls;

        clickMove = playerControls.Player.ClickMove;
        clickMove.Enable();
        clickMove.performed += RightClickOnScene;

        mousePosition = playerControls.Player.MousePosition;
        mousePosition.Enable();
        /*-------->*/


        forcedDest = false; //only true when player is moved towards an object
        initalStoppingDistance = agent.stoppingDistance; //stopping distance will be changed later when player is moved towards an object
    }

    void Update()
    {
        if (!agent.pathPending) // Check if we've reached the destination
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude <= Mathf.Epsilon) //no path active and velocity beflow a very small amount
                {
                    animator.SetBool("Running", false);
                    GameManager.Instance.playerIsRunning = false;
                    if (forcedDest)
                    {
                        //Switch to shoulder cam and rotate player according to the rotation of the destination the player was forced towards
                        playerInteractions.ActivateShoulderCam();
                        Vector3 finalRot = new Vector3(forcedDestination.eulerAngles.x, forcedDestination.eulerAngles.y,
                            forcedDestination.eulerAngles.z);
                        transform.rotation = Quaternion.Euler(finalRot);
                    }
                }
            }
        }
    }

    private void RightClickOnScene(InputAction.CallbackContext context)
    {
        if (InteractableManager.Instance.isInteracting)
            return;

        Ray ray = cam.ScreenPointToRay(mousePosition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            clickEffect.transform.position = new Vector3(hit.point.x, (0.2f), hit.point.z);
            clickEffect.Play();
            agent.SetDestination(hit.point);

            animator.SetBool("Running", true);
            GameManager.Instance.playerIsRunning = true;
        }
    }

    public void ForceDestination(Transform dest, float stoppingDist)
    {
        agent.SetDestination(dest.position);
        animator.SetBool("Running", true);

        forcedDest = true;  //Will be reset in InteractableManager.EndInteraction()
        forcedDestination = dest;
        agent.stoppingDistance = stoppingDist;
    }
    public void SetStoppingDistance(float value)
    {
        agent.stoppingDistance = value;
        agent.ResetPath();
    }

    private void OnDisable()
    {
        if (clickMove.enabled)
            clickMove.Disable();
        if (mousePosition.enabled)
            mousePosition.Disable();
    }
}
