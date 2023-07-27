using UnityEngine;
using UnityEngine.SceneManagement;

public enum ECharacterTypes { Character, MovingCharacter, NPC, Player, Robot };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerControls playerControls;
    public ClickMovement playerClickMovement;
    public bool playerIsRunning = false;
    public bool menuAccessible = false;
    public bool lockedToPc = false; // set to true, if player has to do some task on a pc before proceeding with the normal gameplay (ie. debugging the code of the repair bots)
    [SerializeField] private GameObject player;
    [SerializeField] private SaveLoadManager saveLoadManager;
    [SerializeField] private int debugCheckpoint;
    [SerializeField] private bool debugLoadMode = false;
    [SerializeField] private bool debugSceneLoaded = false;

    public void TogglePlayerControls(bool enable)
    {
        if (enable)
        {
            playerControls.Player.Enable();
            playerClickMovement.enabled = true;
        }
        else
        {
            playerControls.Player.Disable();
            playerClickMovement.enabled = false;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        // Load the Debug Scene only if it is not already loaded (ie. after coming from Main Menu)
        if (!debugSceneLoaded)
            SceneManager.LoadScene("DebugRobots", LoadSceneMode.Additive);

        playerControls = new PlayerControls();
        playerControls.Computer.Disable(); //Computer Controls are only enabled when player starts using one
        player.SetActive(true);
    }

    private void Start()
    {
        if (debugLoadMode)
            LoadDebugCheckpoint();
        else
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
                case 2:
                    saveLoadManager.LoadCheckpoint(2);
                    break;
                case 3:
                    saveLoadManager.LoadCheckpoint(3);
                    break;
            }
        }
        else
        {
            QuestManager.instance.InitStory();
        }
    }

    private void LoadDebugCheckpoint()
    {
        switch (debugCheckpoint)
        {
            case 0:
                QuestManager.instance.InitStory();
                break;
            case 1:
                saveLoadManager.LoadCheckpoint(1);
                break;
            case 2:
                saveLoadManager.LoadCheckpoint(2);
                break;
        }
    }

    public void DestroyGameObject(GameObject go)
    {
        Destroy(go);
    }
}
