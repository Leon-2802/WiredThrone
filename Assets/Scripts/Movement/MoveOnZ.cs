using UnityEngine;

public class MoveOnZ : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool move = false;

    private void Start()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y,
                (this.transform.position.z + Time.deltaTime * speed)
            );
        }
    }
}
