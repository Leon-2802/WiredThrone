using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CompanionHints : MonoBehaviour
{
    [SerializeField] private TMP_Text textfield;
    [SerializeField] private Transform[] targets;
    [SerializeField] private string[] targetHints;

    void Start()
    {
        EventManager.instance.Subscribe<UnityAction>(EEvents.CompanionFlyBack, HideTextfield);
        EventManager.instance.Subscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, GetHintToShow);
    }

    private void GetHintToShow(Transform target)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == target)
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


    private void OnDestroy()
    {
        EventManager.instance.UnSubscribe<UnityAction>(EEvents.CompanionFlyBack, HideTextfield);
        EventManager.instance.UnSubscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, GetHintToShow);
    }
}
