using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

//no cutscenes filmed yet -> just has placeholder behaviour for now
public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject companion;
    [SerializeField] private GameObject playerWakeupScene;
    [SerializeField] private GameObject companionRepairedScene;
    [SerializeField] private GameObject endScene;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TimelineAsset playerWakeupClip;
    [SerializeField] private TimelineAsset companionRepairedClip;
    [SerializeField] private TimelineAsset endClip;

    public void InitPlayerWakeUpScene()
    {
        player.SetActive(false);
        playerWakeupScene.SetActive(true);
        GameManager.Instance.playerControls.Disable();
        StartCoroutine(DisableCutscene(playerWakeupClip.duration, 0));
    }
    public void InitCompanionRepairedScene()
    {
        companion.SetActive(false);
        companionRepairedScene.SetActive(true);
        GameManager.Instance.playerControls.Disable();
        StartCoroutine(DisableCutscene(companionRepairedClip.duration, 1));
    }
    public void InitEndCutscene()
    {
        canvas.SetActive(true);
        StartCoroutine(DelayedSceneStart(1f));
    }

    IEnumerator DisableCutscene(double duration, int cutsceneIndex)
    {
        yield return new WaitForSeconds((float)duration);
        switch (cutsceneIndex)
        {
            case 0:
                playerWakeupScene.SetActive(false);
                player.SetActive(true);
                break;
            case 1:
                companionRepairedScene.SetActive(false);
                companion.SetActive(true);
                QuestManager.instance.InvokeSetQuest(QuestManager.instance.quests[1], 0);
                break;
        }
        GameManager.Instance.playerControls.Enable();
    }

    IEnumerator DelayedSceneStart(float time)
    {
        yield return new WaitForSeconds(time);
        UI.SetActive(false);
        endScene.SetActive(true);
        GameManager.Instance.playerControls.Disable();
        StartCoroutine(EndGame(endClip.duration));
    }

    IEnumerator EndGame(double duration)
    {
        yield return new WaitForSeconds((float)duration);
        SceneManager.LoadScene("MainMenu");
    }
}
