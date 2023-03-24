using UnityEngine;

//Class is attached to an invisble wall, if player passes, Trigger-Function is executed
public class TriggerCompanionFlyEvent : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            EventManager.instance.CompanionFlyToObjEvent(target); //Trigger the CompanionFlyToObjectEvent -> leeds to all subscribed methods being called as well
            Destroy(this.gameObject); //Destroy the object, bc event should only be called once
        }
    }
}
