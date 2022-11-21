using UnityEngine;
using TMPro;

public class CompanionHints : MonoBehaviour
{
    [SerializeField] private TMP_Text textfield;
    [SerializeField] private Transform[] targets;
    [SerializeField] private string[] targetHints;

    void Start()
    {
        EventManager.instance.SubscribeToCompanionFlyEvent(GetHintToShow);
        EventManager.instance.Subscribe(EEvents.CompanionFlyBack, HideTextfield);
    }

    private void GetHintToShow(Transform target)
    {
        for(int i = 0; i < targets.Length; i++)
        {
            if(targets[i] == target)
            {
                ShowHint(targetHints[i]);
            }
        }
    }

    private void ShowHint(string hint)
    {
        textfield.enabled = true;
        textfield.text = hint;
    }

    private void HideTextfield()
    {
        textfield.enabled = false;
    }
}
