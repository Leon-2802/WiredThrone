using UnityEngine;

public class FightAreaBlock : MonoBehaviour
{
    [SerializeField] private Vector2 coordinates;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<MovingCharacter>())
        {
            ManagePositions.instance.robotPosition = coordinates;
        }
    }
}
