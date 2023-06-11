using UnityEngine;

public enum ECodeType { DamagedBot00, DamagedBot01 }
public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    [SerializeField] private ECodeType codeType;
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
                connectionsNeeded = 8;
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
            Debug.Log("Good");
            SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);

            if (connectionsNeeded == activeConnections)
            {
                Debug.Log("Successfully debugged the code!");
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
}
