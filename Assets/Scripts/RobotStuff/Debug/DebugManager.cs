using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum ECodeType { DamagedBot00, DamagedBot01 }
public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    public Camera cameraRef;
    public event EventHandler compiledBot00;
    public event EventHandler compiledBot01;
    [SerializeField] private ECodeType codeType;
    [SerializeField] private GameObject blockInteraction;
    [SerializeField] private GameObject bot00Dialogue;
    [SerializeField] private GameObject compileInfo;
    [SerializeField] private GameObject finishedCompileInfo;
    [SerializeField] private ProgressBar compileProgressBar;
    [SerializeField] private UnityEvent onCompiledCode;

    private int connectionsNeeded = -1;
    private int activeConnections = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        switch (codeType)
        {
            case ECodeType.DamagedBot00:
                connectionsNeeded = 7;
                break;
            case ECodeType.DamagedBot01:
                connectionsNeeded = 8;
                break;
        }
    }

    public void InitInteraction(ECodeType codeType)
    {
        GameManager.Instance.lockedToPc = true;
        switch (codeType)
        {
            case ECodeType.DamagedBot00:
                connectionsNeeded = 7;
                bot00Dialogue.SetActive(true);
                break;
            case ECodeType.DamagedBot01:
                connectionsNeeded = 8;
                break;
        }
    }

    public bool ConnectedBlocks(int originId, int targetId)
    {
        if ((targetId - originId) == 1)
        {
            activeConnections++;
            Debug.Log("Active Connections: " + activeConnections);
            SoundManager.instance.PlaySoundOneShot(ESounds.Click);

            if (connectionsNeeded == activeConnections)
            {
                Debug.Log("Successfully debugged the code!");
                blockInteraction.SetActive(true);
                compileInfo.SetActive(true);
                StartCoroutine(compileProgressBar.RunTimerProgress(2f, ESounds.Success, onCompiledCode));
            }

            return true;
        }

        else
        {
            Debug.Log("Wrong block");
            SoundManager.instance.PlaySoundOneShot(ESounds.DoorError);
            return false;
        }
    }

    public bool ConnectedFalseWire(int targetId)
    {
        if (targetId == 5)
        {
            activeConnections++;
            SoundManager.instance.PlaySoundOneShot(ESounds.Click);
            Debug.Log("Active Connections: " + activeConnections);
            return true;
        }
        else
        {
            SoundManager.instance.PlaySoundOneShot(ESounds.DoorError);
            return false;
        }
    }

    public void CutConnetion()
    {
        activeConnections--;
        Debug.Log("Active connections: " + activeConnections);
    }

    public void CallCompileEvent(bool bot00)
    {
        StartCoroutine(DelayedInvoke(bot00));
    }
    private IEnumerator DelayedInvoke(bool bot00)
    {
        yield return new WaitForSeconds(1f);
        if (bot00)
            InvokeCompiledBot00();
        else
            InvokeCompiledBot01();
    }
    private void InvokeCompiledBot00()
    {
        GameManager.Instance.lockedToPc = false;
        compileInfo.SetActive(false);
        finishedCompileInfo.SetActive(false);
        compiledBot00.Invoke(this, EventArgs.Empty);
    }
    private void InvokeCompiledBot01()
    {
        GameManager.Instance.lockedToPc = false;
        compileInfo.SetActive(false);
        finishedCompileInfo.SetActive(false);
        compiledBot01.Invoke(this, EventArgs.Empty);
    }
}
