using System;
using UnityEngine;

public class Collectible : InteractableObject
{
    [SerializeField] private bool destroyOnCollect = true;
    [SerializeField] private ESounds collectSound;

    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        if (!this.isActiveAndEnabled)
            return;


        base.OnStartInteraction(sender, e);

        SoundManager.instance.PlaySoundOneShot(collectSound);

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
