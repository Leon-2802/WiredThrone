using UnityEngine;

public enum ECharacterTypes { Character, MovingCharacter, NPC, Player, Robot };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerControls playerControls;
    public bool playerIsRunning = false;
    public bool menuAccessible = false;
    [SerializeField] private GameObject player;
    [SerializeField] private SaveLoadManager saveLoadManager;

    public void TogglePlayerControls(bool enable)
    {
        if (enable)
            playerControls.Player.Enable();
        else
            playerControls.Player.Disable();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        playerControls = new PlayerControls();
        playerControls.Computer.Disable(); //Computer Controls are only enabled when player starts using one
        CheckPlayerPrefs();
    }

    private void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            int progress = PlayerPrefs.GetInt("Checkpoint");

            switch (progress)
            {
                case 0:
                    QuestManager.instance.InitStory();
                    break;
                case 1:
                    saveLoadManager.LoadCheckpoint(1);
                    break;
            }
        }
        else
        {
            QuestManager.instance.InitStory();
        }
    }
}
