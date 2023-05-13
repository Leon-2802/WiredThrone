using System;
using System.Collections;
using UnityEngine;

public class Rewire : Computer
{
    [SerializeField] private GameObject shoulderCam;
    [SerializeField] private GameObject dialog;
    private bool dialogPlayed = false;

    public void Finished()
    {
        unavailable = true;
        InteractableManager.Instance.InvokeEndCom();
        InteractableManager.Instance.EndInteraction();
        CameraController.instance.SetBlendTime(0);
        shoulderCam.SetActive(false);
        interactionInfo.SetActive(false);
        StartCoroutine(ResetBlendTime());
    }

    protected override void StartCom(object sender, EventArgs e)
    {
        if (!unavailable)
        {
            // if (!dialogPlayed)
            // {
            //     dialog.SetActive(true);
            //     dialogPlayed = true;
            // }
            base.StartCom(sender, e);
        }

    }

    private IEnumerator ResetBlendTime()
    {
        yield return new WaitForSeconds(1.5f);
        CameraController.instance.ResetBlendTime();
    }
}
