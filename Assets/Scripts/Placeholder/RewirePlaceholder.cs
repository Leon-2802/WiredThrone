using UnityEngine;
using UnityEngine.Events;

public class RewirePlaceholder : MonoBehaviour
{
    public UnityEvent rewired;

    public void InvokeRewired()
    {
        rewired.Invoke();
    }
}
