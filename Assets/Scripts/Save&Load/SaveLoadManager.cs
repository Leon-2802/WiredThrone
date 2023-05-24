using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private UnityEvent loadCheckpoint01;
    [SerializeField] private UnityEvent loadCheckpoint02;
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
        StartCoroutine(PlayLoadAnim());
        PlayerPrefs.SetInt("Checkpoint", checkpoint);
        SavePlayerPos();
        SaveCompanionPos();
    }
    public void LoadCheckpoint(int num)
    {
        QuestManager.instance.finishedQuests = num;
        switch (num)
        {
            case 1:
                StartCoroutine(DelayedSetQuest(QuestManager.instance.quests[1], 0));
                loadCheckpoint01.Invoke();
                break;
            case 2:
                StartCoroutine(DelayedSetQuest(QuestManager.instance.quests[1], 0));
                loadCheckpoint02.Invoke();
                break;
        }
        LoadPlayerPos();
        LoadCompanionPos();
    }

    private void SavePlayerPos()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
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

    private void SaveCompanionPos()
    {
        PlayerPrefs.SetFloat("CompanionPosX", companion.transform.position.x);
        PlayerPrefs.SetFloat("CompanionPosY", companion.transform.position.y);
        PlayerPrefs.SetFloat("CompanionPosZ", companion.transform.position.z);
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
