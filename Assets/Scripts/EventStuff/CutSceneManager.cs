using System.Collections;
using UnityEngine;

//no cutscenes filmed yet -> just has placeholder behaviour for now
public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

    public void InitPlayerWakeUpScene()
    {
        playerAnim.SetBool("Unconscious", true);
        StartCoroutine(WakeUp());
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(3f);
        playerAnim.SetBool("Unconscious", false);
        yield return new WaitForSeconds(1.5f);
        QuestManager.instance.playerAwake.Invoke();
    }
}
