using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private Rigidbody rb;
    //private float moveSpeed = 5f;

    private InputAction clickMove;

    private void Awake() 
    {
        playerControls = new PlayerControls(); 
    }
    private void OnEnable() 
    {
        clickMove = playerControls.Player.ClickMove;
        clickMove.Enable();
        clickMove.performed += RightClickOnScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RightClickOnScene(InputAction.CallbackContext context)
    {
        Debug.Log("Click");
    }

    private void OnDisable() 
    {
        clickMove.Disable();
    }
}
