using UnityEngine;

public class SwitchGameplayManager : MonoBehaviour
{
    public static SwitchGameplayManager instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera debugScreenCamera;

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
        mainCamera.enabled = false;
        debugScreenCamera.enabled = true;
    }
    public void SwitchToMainCamera()
    {
        debugScreenCamera.enabled = false;
        mainCamera.enabled = true;
    }
}
