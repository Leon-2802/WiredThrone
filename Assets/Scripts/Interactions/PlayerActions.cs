using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private GameObject shoulderCam;
    [SerializeField] private GameObject inGameMenu;
    private InputAction interact;
    private InputAction openMenu;
    private InputAction toggleQuestUI;
    private InputAction startCom;
    private InputAction exitCom;

    void OnEnable()
    {
        playerControls = GameManager.Instance.playerControls;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;

        openMenu = playerControls.Menu.ToggleMenu;
        openMenu.Enable();
        openMenu.performed += ToggleInGameMenu;

        toggleQuestUI = playerControls.Player.ToggleQuestUI;
        toggleQuestUI.Enable();
        toggleQuestUI.performed += ToggleQuestUI;

        startCom = playerControls.Player.StartCom;
        startCom.Enable();
        startCom.performed += StartCom;

        exitCom = playerControls.Computer.Exit;
        exitCom.Enable();
        exitCom.performed += ExitCom;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (InteractableManager.Instance.interactionAvailable == false)
            return;

        if (!InteractableManager.Instance.isInteracting)
        {
            InteractableManager.Instance.StartInteraction();
        }
        else
        {
            shoulderCam.SetActive(false);
            InteractableManager.Instance.EndInteraction();
        }
    }
    public void ActivateShoulderCam()
    {
        shoulderCam.SetActive(true);
    }

    private void ToggleInGameMenu(InputAction.CallbackContext context)
    {
        GameManager.Instance.TogglePlayerControls(inGameMenu.activeInHierarchy);
        inGameMenu.SetActive(!inGameMenu.activeInHierarchy);
    }

    private void ToggleQuestUI(InputAction.CallbackContext context)
    {
        GeneralUIHandler.instance.ToggleQuestView();
    }

    private void StartCom(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.lockedToPc)
            InteractableManager.Instance.InvokeStartCom();
    }
    private void ExitCom(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.lockedToPc)
            InteractableManager.Instance.InvokeEndCom();
    }

    private void OnDisable()
    {
        interact.Disable();
        openMenu.Disable();
        toggleQuestUI.Disable();
        startCom.Disable();
        exitCom.Disable();
    }
}
