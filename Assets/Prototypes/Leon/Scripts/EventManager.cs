using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EEvents {CompanionFlyToObj, CompanionHint, CompanionFlyBack}
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public bool companionOnTarget = false;
    public List<UnityAction<Transform>> companionFlyToObjectCallbacks = new List<UnityAction<Transform>>();
    public List<UnityAction> companionHintCallbacks = new List<UnityAction>();
    public List<UnityAction> companionBackToPlayerCallbacks = new List<UnityAction>();

    private void Awake() 
    {
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else    
            instance = this;
    }

    public void Subscribe(EEvents eventType, UnityAction func)
    {
        switch(eventType)
        {
            case EEvents.CompanionFlyBack:
                companionBackToPlayerCallbacks.Add(func);
                break;
        }
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
        companionOnTarget = true;
    }
    public void CompanionFlyBackToPlayerEvent()
    {
        foreach(UnityAction cb in companionBackToPlayerCallbacks)
        {
            cb();
        }
        companionOnTarget = false;
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
