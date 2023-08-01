using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProgramBotsPC : Computer
{
    [SerializeField] private UnityEvent onProgrammingDone;
    [SerializeField] private GameObject shoulderCam;
    private bool onStayPassed = false;
    private bool startedComOnce = false;

    protected override void StartCom(object sender, EventArgs e)
    {
        base.StartCom(sender, e);
        if (!startedComOnce)
        {
            SwitchGameplayManager.instance.onProgrammingFinished += OnPogrammingDone;
            startedComOnce = true;
            SwitchGameplayManager.instance.SwitchToProgrammingGameplay();
        }
    }

    protected override void ExitCom(object sender, EventArgs e)
    {
        base.ExitCom(sender, e);
    }

    private void OnPogrammingDone(object sender, EventArgs e)
    {
        onProgrammingDone.Invoke();
        SwitchGameplayManager.instance.onProgrammingFinished -= OnPogrammingDone;
        Debug.Log("Programming finished");
        Finished();
    }

    public void Finished()
    {
        unavailable = true;
        InteractableManager.Instance.InvokeEndCom();
        InteractableManager.Instance.EndInteraction();
        CameraController.instance.SetBlendTime(0);
        shoulderCam.SetActive(false);
        interactionInfo.SetActive(false);
        InteractableManager.Instance.LeaveInteractionZone();
        InteractableManager.Instance.isInteractingWithCom = false;
        InteractableManager.Instance.startInteraction -= OnStartInteraction;
        InteractableManager.Instance.endInteraction -= OnEndInteraction;
        InteractableManager.Instance.DeselectInteractable(rendererRefs, previousOutlineSizes[0]);
        StartCoroutine(ResetBlendTime());
    }

    //if player is standing in collider, while onlyInspect is set to false, 
    //the actions of OnTriggerEnter should still be called
    private void OnTriggerStay(Collider other)
    {
        if (!onlyInspect && !onStayPassed && other.gameObject.GetComponent<Player>())
        {
            base.OnTriggerEnter(other);
            onStayPassed = true;
        }
        else if (onlyInspect && onStayPassed && other.gameObject.GetComponent<Player>())
        {
            base.OnTriggerExit(other);
            onStayPassed = false;
        }
    }

    private IEnumerator ResetBlendTime()
    {
        yield return new WaitForSeconds(1.5f);
        CameraController.instance.ResetBlendTime();
        this.gameObject.SetActive(false);
    }
}
