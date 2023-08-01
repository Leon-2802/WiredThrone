using System;
using System.Collections;
using UnityEngine;

public class SwitchGameplayManager : MonoBehaviour
{
    public static SwitchGameplayManager instance;
    public bool debuggingDone = false; // set to true inside QuestManager.DebuggedWorkerBots()
    public event EventHandler onProgrammingFinished;
    [SerializeField] private Camera mainCamera;
    private Camera debugScreenCamera;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        GameObject debugCamObj = GameObject.FindGameObjectWithTag("DebugCam");
        if (debugCamObj)
            debugScreenCamera = debugCamObj.GetComponent<Camera>();
    }


    public void SwitchToDebugCamera()
    {
        if (!debuggingDone) // block this function when debugging is done, bc for some reason it gets called still after the robots are disabled
        {
            mainCamera.enabled = false;
            debugScreenCamera.enabled = true;
        }
    }
    public void SwitchToMainCamera()
    {
        debugScreenCamera.enabled = false;
        mainCamera.enabled = true;
    }

    public void SwitchToProgrammingGameplay()
    {
        GameManager.Instance.lockedToPc = true;
        // StartCoroutine(DelayedProgrammingFinished());
    }
    public void ProgrammingFinished()
    {
        GameManager.Instance.lockedToPc = false;
        onProgrammingFinished.Invoke(this, EventArgs.Empty);
    }


    IEnumerator DelayedProgrammingFinished()
    {
        yield return new WaitForSeconds(2f);
        ProgrammingFinished();
    }
}
