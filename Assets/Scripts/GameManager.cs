using UnityEngine;

public enum ECharacterTypes { Character, MovingCharacter, NPC, Player, Robot };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerControls playerControls;
    public bool playerIsRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        LevelManager.instance.PlayerWakeUp();
    }

}