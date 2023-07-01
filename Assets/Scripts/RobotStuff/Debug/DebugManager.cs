using UnityEngine;
using UnityEngine.Events;

public enum ECodeType { DamagedBot00, DamagedBot01 }
public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    [SerializeField] private ECodeType codeType;
    [SerializeField] private GameObject compileStatusInfo;
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
                compileStatusInfo.SetActive(true);
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
}
