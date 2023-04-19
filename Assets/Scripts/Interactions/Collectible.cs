using System;
using UnityEngine;

public class Collectible : InteractableObject
{
    [SerializeField] private bool destroyOnCollect = true;

    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        if (!this.isActiveAndEnabled)
            return;


        base.OnStartInteraction(sender, e);

        InteractableManager.Instance.EndInteraction();
        InteractableManager.Instance.LeaveInteractionZone();
        GeneralUIHandler.instance.InvokeCloseInspector();

        if (destroyOnCollect)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
