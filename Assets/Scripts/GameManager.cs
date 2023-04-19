using UnityEngine;

public enum ECharacterTypes { Character, MovingCharacter, NPC, Player, Robot };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerControls playerControls;
    public bool playerIsRunning = false;
    [SerializeField] private SaveLoadManager saveLoadManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        CheckPlayerPrefs();
    }

    private void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            int progress = PlayerPrefs.GetInt("Checkpoint");
            Debug.Log(progress);

            switch (progress)
            {
                case 0:
                    QuestManager.instance.InitStory();
                    break;
                case 1:
                    saveLoadManager.LoadCheckpoint01();
                    break;
            }
        }
        else
        {
            QuestManager.instance.InitStory();
        }
    }
}
