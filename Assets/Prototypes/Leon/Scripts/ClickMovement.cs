using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ParticleSystem clickEffect;
    private InputAction clickMove;
    private InputAction mousePosition;

    private void Awake() 
    {
        playerControls = new PlayerControls(); 
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

    }

    private void RightClickOnScene(InputAction.CallbackContext context)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition.ReadValue<Vector2>());
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            clickEffect.transform.position = new Vector3(hit.point.x, (hit.point.y + 0.2f), hit.point.z);
            clickEffect.Play();
            agent.SetDestination(hit.point);
        }
    }

    private void OnDisable() 
    {
        clickMove.Disable();
        mousePosition.Disable();
    }
}
