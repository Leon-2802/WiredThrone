using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private GameObject screenCam;
    [SerializeField] private Computer computer;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject game;
    private InputAction start;
    private InputAction exit;


    private void Start() 
    {
        playerControls = GameManager.Instance.playerControls;
        
        start = playerControls.Player.StartCom;
        start.Enable();
        start.performed += OnStartGame;

        exit = playerControls.Player.Back;
        exit.Enable();
        exit.performed += OnExitGame;
    }
    
    private void OnStartGame(InputAction.CallbackContext context)
    {
        if(!InteractableManager.Instance.isInteracting || !computer.interacting)
            return;
        
        screenCam.SetActive(true);

        startScreen.SetActive(false);
        game.SetActive(true);
    }
    private void OnExitGame(InputAction.CallbackContext context)
    {
        if(!InteractableManager.Instance.isInteracting || !computer.interacting)
            return;

        screenCam.SetActive(false);

        startScreen.SetActive(true);
        game.SetActive(false);
    }

    private void OnDisable() 
    {
        start.Disable();
        exit.Disable();
    }
}
