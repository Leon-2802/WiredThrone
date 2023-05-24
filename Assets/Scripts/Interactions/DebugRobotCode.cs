using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class DebugRobotCode : Computer
{
    public UnityEvent onDebugDone;
    [SerializeField] private int index;
    [SerializeField] private GameObject shoulderCam;
    private bool onStayPassed = false;

    public void DebuggedSuccessfully()
    {
        QuestManager.instance.DebuggedOneWokerBot(index);
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
