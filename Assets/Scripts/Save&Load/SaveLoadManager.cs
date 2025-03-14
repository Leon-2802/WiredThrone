using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private UnityEvent loadCheckpointGeneral;
    [SerializeField] private UnityEvent loadCheckpoint01;
    [SerializeField] private UnityEvent loadCheckpoint02;
    [SerializeField] private UnityEvent loadCheckpoint03;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private GameObject companion;
    [SerializeField] private GameObject loadSymbol;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Checkpoint"))
        {
            PlayerPrefs.SetInt("Checkpoint", 0);
        }
    }
    public void SaveCheckpoint(int checkpoint)
    {
        // Checkpoint 01 in QuestManager.RepairedCompanion() Event
        // Checkpoint 02 in QuestManager.TerminalRepaired() Event
        StartCoroutine(PlayLoadAnim());
        PlayerPrefs.SetInt("Checkpoint", checkpoint);
        // SavePlayerPos(player.transform);
        // SaveCompanionPos(companion.transform);
    }
    public void LoadCheckpoint(int num)
    {
        QuestManager.instance.finishedQuests = num;
        switch (num)
        {
            case 1:
                StartCoroutine(DelayedSetQuest(QuestManager.instance.quests[1], 0));
                QuestManager.instance.finishedQuests = 1;
                loadCheckpointGeneral.Invoke();
                loadCheckpoint01.Invoke();
                break;
            case 2:
                StartCoroutine(DelayedSetQuest(QuestManager.instance.quests[3], 0));
                QuestManager.instance.finishedQuests = 3;
                loadCheckpointGeneral.Invoke();
                loadCheckpoint02.Invoke();
                break;
            case 3:
                StartCoroutine(DelayedSetQuest(QuestManager.instance.quests[5], 0));
                QuestManager.instance.finishedQuests = 5;
                loadCheckpointGeneral.Invoke();
                loadCheckpoint03.Invoke();
                break;

        }
        LoadPlayerPos();
        LoadCompanionPos();
    }

    public void SavePlayerPos(Transform pos)
    {
        PlayerPrefs.SetFloat("PlayerPosX", pos.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", pos.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", pos.position.z);
    }
    public void SaveCompanionPos(Transform pos)
    {
        PlayerPrefs.SetFloat("CompanionPosX", pos.position.x);
        PlayerPrefs.SetFloat("CompanionPosY", pos.position.y);
        PlayerPrefs.SetFloat("CompanionPosZ", pos.position.z);
    }

    private void LoadPlayerPos()
    {
        if (!PlayerPrefs.HasKey("PlayerPosX"))
            return;

        playerAgent.enabled = false;

        Vector3 pos = new Vector3(
            PlayerPrefs.GetFloat("PlayerPosX"),
            PlayerPrefs.GetFloat("PlayerPosY"),
            PlayerPrefs.GetFloat("PlayerPosZ")
        );
        player.transform.position = pos;
        playerAgent.enabled = true;
    }

    private void LoadCompanionPos()
    {
        if (!PlayerPrefs.HasKey("CompanionPosX"))
            return;

        Vector3 pos = new Vector3(
            PlayerPrefs.GetFloat("CompanionPosX"),
            PlayerPrefs.GetFloat("CompanionPosY"),
            PlayerPrefs.GetFloat("CompanionPosZ")
        );
        companion.transform.position = pos;
    }

    IEnumerator PlayLoadAnim()
    {
        loadSymbol.SetActive(true);
        yield return new WaitForSeconds(3f);
        loadSymbol.SetActive(false);
    }
    IEnumerator DelayedSetQuest(string text, int iterations)
    {
        yield return new WaitForSeconds(0.1f);
        QuestManager.instance.InvokeSetQuest(text, iterations);
    }
}
