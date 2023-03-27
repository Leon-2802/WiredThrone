using System;
using UnityEngine;

public class CompanionEvents : MonoBehaviour
{
    public static CompanionEvents instance;
    public event EventHandler<FlyToObjectEventArgs> flyToObject;
    public class FlyToObjectEventArgs : EventArgs
    {
        public Transform _target;
    }
    public event EventHandler flyBackToPlayer;
    [SerializeField] private Transform eventTriggerParent;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void CallFlyToObjectEvent(Transform target)
    {
        flyToObject?.Invoke(this, new FlyToObjectEventArgs { _target = target });

        //all triggers need to be disabled in order to prevent multiple active events
        for (int i = 0; i < eventTriggerParent.childCount; i++)
        {
            GameObject Go = eventTriggerParent.GetChild(i).gameObject;
            Go.SetActive(false);
        }
    }
    public void CallFlyBackToPlayer()
    {
        flyBackToPlayer?.Invoke(this, EventArgs.Empty);

        //enable all triggers, that are still to be used later in game
        for (int i = 0; i < eventTriggerParent.childCount; i++)
        {
            GameObject Go = eventTriggerParent.GetChild(i).gameObject;
            Go.SetActive(true);
        }
    }
}
