using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private UnityEvent loadCheckpoint01;
    [SerializeField] private GameObject loadSymbol;
    private void Start()
    {
        PlayerPrefs.SetInt("Checkpoint", 0);
    }
    public void SaveCheckpoint(int checkpoint)
    {
        PlayerPrefs.SetInt("Checkpoint", checkpoint);
        StartCoroutine(PlayLoadAnim());
    }
    public void LoadCheckpoint01()
    {
        loadCheckpoint01.Invoke();
        DelayedSetQuest("New Quest", 5);
    }

    IEnumerator PlayLoadAnim()
    {
        loadSymbol.SetActive(true);
        yield return new WaitForSeconds(3f);
        loadSymbol.SetActive(false);
    }
    IEnumerator DelayedSetQuest(string text, int iterations)
    {
        yield return new WaitForSeconds(1.5f);
        QuestManager.instance.InvokeSetQuest(text, iterations);
    }
}
