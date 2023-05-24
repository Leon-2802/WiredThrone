using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnEnter : MonoBehaviour
{
    [SerializeField] private UnityEvent myEvent;
    [SerializeField] private bool companionFlyEvent;
    [SerializeField] private bool triggerByAnyMovingCharacter;

    private void OnTriggerEnter(Collider other)
    {
        if (companionFlyEvent && CompanionEvents.instance.flyEventCalled)
            return;

        if (triggerByAnyMovingCharacter)
        {
            if (other.gameObject.GetComponent<MovingCharacter>())
            {
                myEvent.Invoke();
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (other.gameObject.GetComponent<Player>())
            {
                myEvent.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}
