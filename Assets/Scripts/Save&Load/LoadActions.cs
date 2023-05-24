using UnityEngine;
using UnityEngine.Events;

public class LoadActions : MonoBehaviour
{
    [SerializeField] private UnityEvent loadAction01;

    public void Checkpoint01Action()
    {
        loadAction01.Invoke();
    }
}
