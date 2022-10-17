using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGamePlayer : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private float speed;
    private InputAction move;
    private bool moving;


    void Start()
    {
        playerControls = GameManager.Instance.playerControls;

        move = playerControls.Player.Move;
        move.Enable();
        move.performed += Moving;
    }

    void Update()
    {
        if(!moving)
         return;
        
        //Funktioniert mal gar nicht...
        Vector3 pos = transform.position;

        pos.x += (move.ReadValue<Vector2>().x + Time.deltaTime * speed);
        pos.y += (move.ReadValue<Vector2>().y + Time.deltaTime * speed);

        transform.position = pos;
    }

    void Moving(InputAction.CallbackContext context)
    {
        moving = true;
    }

    private void OnDisable() 
    {
        move.Disable();
    }
}
