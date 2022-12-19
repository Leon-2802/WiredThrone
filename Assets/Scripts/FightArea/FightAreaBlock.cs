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
            ManagePositions.instance.robotPositions[other.gameObject.GetComponent<Robot>().robotIndex] = coordinates;
        }
        else if(other.gameObject.GetComponent<EnemyRobot>())
        {
            ManagePositions.instance.enemyPositions[other.gameObject.GetComponent<EnemyRobot>().enemyIndex] = coordinates;
        }
    }
}
