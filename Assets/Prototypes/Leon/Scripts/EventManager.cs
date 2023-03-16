using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EEvents { CompanionFlyToObj, CompanionHint, CompanionFlyBack }
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public bool companionOnTarget = false;
    public List<UnityAction<Transform>> companionFlyToObjectCallbacks = new List<UnityAction<Transform>>();
    public List<UnityAction> companionHintCallbacks = new List<UnityAction>();
    public List<UnityAction> companionBackToPlayerCallbacks = new List<UnityAction>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void Subscribe<T>(EEvents eventType, T func) where T : Delegate
    {
        switch (eventType)
        {
            case EEvents.CompanionFlyBack:
                if (func is UnityAction)
                    companionBackToPlayerCallbacks.Add(func as UnityAction);
                else
                    Debug.LogWarning("Function is not of type UnityAction!");
                break;
            case EEvents.CompanionFlyToObj:
                if (func is UnityAction<Transform>)
                    companionFlyToObjectCallbacks.Add(func as UnityAction<Transform>);
                else
                    Debug.LogWarning("Function is not of type UnityAction<Transform>!");
                break;

        }
    }
    public void UnSubscribe<T>(EEvents eventType, T func) where T : Delegate
    {
        switch (eventType)
        {
            case EEvents.CompanionFlyBack:
                if (func is UnityAction)
                    companionBackToPlayerCallbacks.Remove(func as UnityAction);
                else
                    Debug.LogWarning("Function is not of type UnityAction!");
                break;
            case EEvents.CompanionFlyToObj:
                if (func is UnityAction<Transform>)
                    companionFlyToObjectCallbacks.Remove(func as UnityAction<Transform>);
                else
                    Debug.LogWarning("Function is not of type UnityAction<Transform>!");
                break;

        }
    }

    public void CompanionFlyToObjEvent(Transform target)
    {
        foreach (UnityAction<Transform> cb in companionFlyToObjectCallbacks)
        {
            cb(target);
        }
        companionOnTarget = true;
    }
    public void CompanionFlyBackToPlayerEvent()
    {
        foreach (UnityAction cb in companionBackToPlayerCallbacks)
        {
            cb();
        }
        companionOnTarget = false;
    }

    public void EventActionDone(EEvents eventType)
    {
        switch (eventType)
        {
            case EEvents.CompanionFlyToObj:
                break;
        }
    }
}
