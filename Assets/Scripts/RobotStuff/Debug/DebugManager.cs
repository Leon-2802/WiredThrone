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
    [SerializeField] private ECodeType currentCodeType;
    [SerializeField] private GameObject bot00Blocks;
    [SerializeField] private GameObject bot01Blocks;
    [SerializeField] private GameObject blockInteraction;
    [SerializeField] private GameObject bot00Dialogue;
    [SerializeField] private GameObject bot01Dialogue;
    [SerializeField] private GameObject compileInfo;
    [SerializeField] private GameObject compileStatusInfo;
    [SerializeField] private GameObject finishedCompileInfo;
    [SerializeField] private ProgressBar compileProgressBar;
    [SerializeField] private UnityEvent onCompiledCode00;
    [SerializeField] private UnityEvent onCompiledCode01;

    private int connectionsNeeded = -1;
    private int falseWireTargetId = -1;
    private int activeConnections = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void InitInteraction(ECodeType codeType)
    {
        GameManager.Instance.lockedToPc = true;
        currentCodeType = codeType;
        switch (codeType)
        {
            case ECodeType.DamagedBot00:
                bot00Blocks.SetActive(true);
                connectionsNeeded = 7;
                falseWireTargetId = 5;
                bot00Dialogue.SetActive(true);
                break;
            case ECodeType.DamagedBot01:
                compileStatusInfo.SetActive(true); // been set to false after debugging bot00
                bot01Blocks.SetActive(true);
                connectionsNeeded = 7;
                falseWireTargetId = 4;
                bot01Dialogue.SetActive(true);
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
                blockInteraction.SetActive(true);
                compileInfo.SetActive(true);
                if (currentCodeType == ECodeType.DamagedBot00)
                {
                    StartCoroutine(compileProgressBar.RunTimerProgress(2f, ESounds.Success, onCompiledCode00));
                    Debug.Log("Bot00 compiled successfully");
                }
                else if (currentCodeType == ECodeType.DamagedBot01)
                {
                    StartCoroutine(compileProgressBar.RunTimerProgress(2f, ESounds.Success, onCompiledCode01));
                    Debug.Log("Bot01 compiled successfully");
                }
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
        if (targetId == falseWireTargetId)
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
        activeConnections = 0;
        GameManager.Instance.lockedToPc = false;
        bot00Blocks.SetActive(false);
        compileInfo.SetActive(false);
        finishedCompileInfo.SetActive(false);
        compiledBot00.Invoke(this, EventArgs.Empty);
    }
    private void InvokeCompiledBot01()
    {
        activeConnections = 0;
        GameManager.Instance.lockedToPc = false;
        bot01Blocks.SetActive(false);
        compileInfo.SetActive(false);
        finishedCompileInfo.SetActive(false);
        compiledBot01.Invoke(this, EventArgs.Empty);
    }
}
