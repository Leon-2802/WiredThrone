using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//no cutscenes filned yet -> just has placeholder behaviour for now
public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private UnityEvent wakingUp;

    void Start()
    {
        playerAnim.SetBool("Unconscious", true);
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(3f);
        wakingUp.Invoke();
        playerAnim.SetBool("Unconscious", false);
    }
}
