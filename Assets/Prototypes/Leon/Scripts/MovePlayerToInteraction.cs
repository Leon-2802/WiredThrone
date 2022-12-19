using UnityEngine;

public class MovePlayerToInteraction : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToInteraction(Transform target)
    {
        player.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        player.position = new Vector3(player.position.x, initialPos.y, player.position.z);
    }
}
