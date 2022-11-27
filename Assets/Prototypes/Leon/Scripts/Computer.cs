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
        if(other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.EnterInteractionZone(interactionType, this.gameObject);
            ChangeMat(ThemeManager.instance.interactionAvailable);
            interacting = true;

            if(isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = true;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
         if(other.gameObject.GetComponent<Player>())
        {
            InteractableManager.Instance.LeaveInteractionZone();
            ChangeMat(initialMat);
            interacting = false;

            if(isCompanionTarget)
                InteractableManager.Instance.isInteractingWithCompanionTarget = false;
        }
    }
}
