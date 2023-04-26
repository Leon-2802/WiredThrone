using UnityEngine;
using TMPro;

public class ManageQuestLog : MonoBehaviour
{
    [SerializeField] private GameObject[] questEntries;
    [SerializeField] private GameObject currentQuest;
    [SerializeField] private TMP_Text currentQuestText;

    public void SetCurrentQuest(int index)
    {
        currentQuestText.text = QuestManager.instance.quests[index];
        if (!currentQuest.activeInHierarchy)
            currentQuest.SetActive(true);

        RefreshFinishedQuests();
    }
    private void RefreshFinishedQuests()
    {
        for (int i = 0; i < QuestManager.instance.finishedQuests; i++)
        {
            questEntries[i].SetActive(true);
        }
    }
}
