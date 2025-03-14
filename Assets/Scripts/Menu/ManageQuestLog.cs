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
            questEntries[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        // -> "Finished Quests" appears always on top of the list 
        if (QuestManager.instance.finishedQuests > 0)
            questEntries[QuestManager.instance.finishedQuests - 1].transform.GetChild(0).gameObject.SetActive(true);
    }
}
