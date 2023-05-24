using UnityEngine;

public class TriggerPlayerReachedTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            CompanionEvents.instance.CallFlyBackToPlayer();
            Destroy(this.gameObject); //event should only be called once
        }
    }
}
