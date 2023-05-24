using UnityEngine;
using System.Collections.Generic;

public enum ECharacterTypes { Character, MovingCharacter, NPC, Player, Robot };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerControls playerControls;
    public bool playerIsRunning = false;
    public bool menuAccessible = false;
    [SerializeField] private List<Robot> robots = new List<Robot>();
    [SerializeField] private GameObject player;
    [SerializeField] private SaveLoadManager saveLoadManager;
    [SerializeField] private int debugCheckpoint;
    [SerializeField] private bool debugLoadMode = false;

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
        player.SetActive(true);

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
        }
    }
}
