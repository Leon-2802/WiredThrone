using UnityEngine;
using UnityEngine.Events;

public enum Events {CompanionFlyToObj, CompanionHint}
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public UnityAction<Transform>[] companionFlyToObjectCallbacks;
    public UnityAction[] companionHintCallbacks;

    private void Awake() 
    {
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else    
            instance = this;
    }

    public void CompanionFlyToObjEvent(Transform target)
    {
        foreach(UnityAction<Transform> cb in companionFlyToObjectCallbacks)
        {
            cb(target);
        }
    }
}
