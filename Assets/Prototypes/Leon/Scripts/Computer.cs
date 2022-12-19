using UnityEngine;

public class Computer : InteractableObject
{
    public bool interacting;

    protected override void Start()
    {
        base.Start();

        interacting = false;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject.GetComponent<Player>())
        {
            interacting = true;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(other.gameObject.GetComponent<Player>())
        {
            interacting = false;
        }
    }
}
