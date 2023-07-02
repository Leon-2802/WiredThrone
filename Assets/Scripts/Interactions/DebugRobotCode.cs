using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class DebugRobotCode : Computer
{
    public UnityEvent onDebugDone;
    [SerializeField] private ECodeType codeType;
    [SerializeField] private GameObject shoulderCam;
    private bool onStayPassed = false;
    private bool startedComOnce = false;

    protected override void Start()
    {
        base.Start();

        if (codeType == ECodeType.DamagedBot00)
            DebugManager.instance.compiledBot00 += DebuggedSuccessfully;
        else
            DebugManager.instance.compiledBot01 += DebuggedSuccessfully;
    }

    protected override void StartCom(object sender, EventArgs e)
    {
        base.StartCom(sender, e);
        SwitchGameplayManager.instance.SwitchToDebugCamera();
        if (!startedComOnce)
        {
            Debug.Log("onStartCom");
            DebugManager.instance.InitInteraction(codeType);
            startedComOnce = true;
        }
    }
    protected override void ExitCom(object sender, EventArgs e)
    {
        base.ExitCom(sender, e);
        SwitchGameplayManager.instance.SwitchToMainCamera();
    }

    public void DebuggedSuccessfully(object sender, EventArgs e)
    {
        // SwitchGameplayManager.instance.SwitchToMainCamera();
        onDebugDone.Invoke();
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
    }

    private IEnumerator ResetBlendTime()
    {
        yield return new WaitForSeconds(1.5f);
        CameraController.instance.ResetBlendTime();
    }

}
