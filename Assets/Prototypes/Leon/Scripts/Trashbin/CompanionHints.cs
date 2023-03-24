using UnityEngine;
using UnityEngine.Events;
using TMPro;

//Liegt in Prefab "IngameUI"
public class CompanionHints : MonoBehaviour
{
    [SerializeField] private TMP_Text textfield;
    [SerializeField] private Transform[] targets; //all targets the companion can have in the scene
    [SerializeField] private string[] targetHints; //hints have to be placed at the same index as their target object

    void Start()
    {
        EventManager.instance.Subscribe<UnityAction>(EEvents.CompanionFlyBack, HideTextfield);
        EventManager.instance.Subscribe<UnityAction<Transform>>(EEvents.CompanionFlyToObj, GetHintToShow); 
    }

    //called each time Companion flies to an Object, in which case he may want to display a hint
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
