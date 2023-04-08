using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private GameObject shoulderCam;
    private InputAction interact;
    private InputAction leaveInteraction;
    private InputAction toggleQuestUI;
    private Vector3 moveDirection;

    void Start()
    {
        playerControls = GameManager.Instance.playerControls;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;

        leaveInteraction = playerControls.Player.Back;
        leaveInteraction.Enable();
        leaveInteraction.performed += LeaveInteraction;

        toggleQuestUI = playerControls.Player.ToggleQuestUI;
        toggleQuestUI.Enable();
        toggleQuestUI.performed += ToggleQuestUI;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (InteractableManager.Instance.isInteracting || InteractableManager.Instance.interactionAvailable == false)
            return;

        InteractableManager.Instance.StartInteraction();
    }
    public void ActivateShoulderCam()
    {
        shoulderCam.SetActive(true);
    }

    private void LeaveInteraction(InputAction.CallbackContext context)
    {
        if (InteractableManager.Instance.isInteracting == false || InteractableManager.Instance.interactionAvailable == false)
            return;

        shoulderCam.SetActive(false);
        InteractableManager.Instance.EndInteraction();
    }

    private void ToggleQuestUI(InputAction.CallbackContext context)
    {
        GeneralUIHandler.instance.ToggleQuestView();
    }

    private void OnDisable()
    {
        interact.Disable();
        leaveInteraction.Disable();
        toggleQuestUI.Disable();
    }
}
