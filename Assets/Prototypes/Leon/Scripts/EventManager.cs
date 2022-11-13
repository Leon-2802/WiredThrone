using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EEvents {CompanionFlyToObj, CompanionHint}
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public List<UnityAction<Transform>> companionFlyToObjectCallbacks = new List<UnityAction<Transform>>();
    public List<UnityAction> companionHintCallbacks = new List<UnityAction>();

    private void Awake() 
    {
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else    
            instance = this;
    }

    public void SubscribeToCompanionFlyEvent(UnityAction<Transform> func) 
    {
        companionFlyToObjectCallbacks.Add(func);
    }

    public void CompanionFlyToObjEvent(Transform target)
    {
        foreach(UnityAction<Transform> cb in companionFlyToObjectCallbacks)
        {
            cb(target);
        }
    }

    public void EventActionDone(EEvents eventType)
    {
        switch(eventType)
        {
            case EEvents.CompanionFlyToObj:
                Debug.Log("Event Object Reached");
                break;
        }
    }
}
