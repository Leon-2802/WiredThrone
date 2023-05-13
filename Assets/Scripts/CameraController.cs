using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] private CinemachineBrain cmBrain;
    private float initialBlendTime;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }
    private void Start()
    {
        initialBlendTime = cmBrain.m_DefaultBlend.m_Time;
    }

    public void SetBlendTime(float t)
    {
        cmBrain.m_DefaultBlend.m_Time = t;
    }

    public void ResetBlendTime()
    {
        cmBrain.m_DefaultBlend.m_Time = initialBlendTime;
    }
}
