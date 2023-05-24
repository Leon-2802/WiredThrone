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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        playerControls = new PlayerControls();
        player.SetActive(true);
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

    public List<Robot> GetRobots
    {
        get { return robots; }
    }
    public void AddRobot(Robot r)
    {
        robots.Add(r);
    }
}
