using UnityEngine;

public class EndCutsceneCanvasController : MonoBehaviour
{
    [SerializeField] private Animator backgroundAnimator;

    void Start()
    {
        backgroundAnimator.SetTrigger("Fade");
    }
}
