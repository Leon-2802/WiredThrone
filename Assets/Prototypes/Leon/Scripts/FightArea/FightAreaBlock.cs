using UnityEngine;

public class FightAreaBlock : MonoBehaviour
{
    [SerializeField] private Vector2 coordinates;

    private void Start() 
    {
        coordinates = new Vector2(transform.position.x, this.gameObject.GetComponentInParent<Transform>().position.z);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<Robot>())
        {
            ManagePositions.instance.robotPosition = coordinates;
        }
    }
}
