using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//no cutscenes filmed yet -> just has placeholder behaviour for now
public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private UnityEvent wakingUp;

    public void InitPlayerWakeUpScene()
    {
        playerAnim.SetBool("Unconscious", true);
        StartCoroutine(WakeUp());
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(3f);
        wakingUp.Invoke();
        playerAnim.SetBool("Unconscious", false);
    }
}
