using UnityEngine;

public class ActivateOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToActivate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            gameObjectToActivate.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
