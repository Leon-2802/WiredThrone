using UnityEngine;

public class TriggerCollissionHandlerBehaviour : MonoBehaviour
{
    [SerializeField] private string triggerName;
    [SerializeField] private GameObject triggerObject;
    [SerializeField] private CollisionEvent onCollision = new CollisionEvent();
    [SerializeField] private Transform destinationWhenInactive;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(triggerObject))
        {
            onCollision.Invoke(new CollisionData
            {
                //! not used at the moment... (other classes can't process this data, also it isn't necessary atm)
                collidedWith = other.gameObject,
                triggerGameObject = this.gameObject
            });


            this.transform.parent = destinationWhenInactive;
            this.gameObject.SetActive(false);
        }
    }
}
