using UnityEngine;
using UnityEngine.InputSystem;

public class DebugEventcalls : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private Transform debugCompanionEventTarget;
    private InputAction debug;

    private void Awake() 
    {
        playerControls = GameManager.Instance.playerControls;
    }

    private void OnEnable()
    {
        debug = playerControls.Player.Debug;
        debug.Enable();
        debug.performed += CallDebugEvent;
    }

    private void CallDebugEvent(InputAction.CallbackContext context)
    {
        EventManager.instance.CompanionFlyToObjEvent(debugCompanionEventTarget);
    }

    private void OnDisable() 
    {
        debug.Disable();
    }
}
