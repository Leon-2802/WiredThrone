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
    public bool flyEventCalled;
    [SerializeField] private Transform eventTriggerParent;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        flyEventCalled = false;
    }

    public void CallFlyToObjectEvent(Transform target)
    {
        flyToObject?.Invoke(this, new FlyToObjectEventArgs { _target = target });
        flyEventCalled = true;
    }
    public void CallFlyBackToPlayer()
    {
        flyBackToPlayer?.Invoke(this, EventArgs.Empty);
        flyEventCalled = false;
    }
}
