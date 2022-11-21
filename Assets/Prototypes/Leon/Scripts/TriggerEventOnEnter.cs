using UnityEngine;

public class TriggerEventOnEnter : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            EventManager.instance.CompanionFlyToObjEvent(target);
            Destroy(this.gameObject);
        }
    }
}
