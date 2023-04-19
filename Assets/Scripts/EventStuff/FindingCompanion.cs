using UnityEngine;

public class FindingCompanion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            QuestManager.instance.FoundCompanion();
            Destroy(this.gameObject);
        }
    }
}
