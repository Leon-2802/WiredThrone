using UnityEngine;

public class TriggerCompanionFlyEvent : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() && !EventManager.instance.companionOnTarget)
        {
            EventManager.instance.CompanionFlyToObjEvent(target);
            Destroy(this.gameObject);
        }
    }
}
