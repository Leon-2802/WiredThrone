using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] private UnityEvent dialogEnded;
    private void Awake()
    {
        StartCoroutine(DialogueSequence());
    }


    private IEnumerator DialogueSequence()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Deactivate();
            transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
        }
        dialogEnded.Invoke();
        this.gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
