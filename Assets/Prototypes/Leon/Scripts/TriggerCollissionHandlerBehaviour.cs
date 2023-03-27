using UnityEngine;
using UnityEngine.Events;

public class TriggerCollissionHandlerBehaviour : MonoBehaviour
{
    [SerializeField] private string triggerName;
    [SerializeField] private GameObject triggerObject;
    [SerializeField] private Transform destinationWhenInactive;
    [SerializeField] private UnityEvent collisionEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(triggerObject))
        {
            collisionEvent.Invoke();

            this.transform.parent = destinationWhenInactive;
            this.gameObject.SetActive(false);
        }
    }
}
