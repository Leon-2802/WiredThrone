using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

//no cutscenes filmed yet -> just has placeholder behaviour for now
public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWakeupScene;
    [SerializeField] private TimelineAsset playerWakeupClip;

    public void InitPlayerWakeUpScene()
    {
        player.SetActive(false);
        playerWakeupScene.SetActive(true);
        StartCoroutine(DisableCutscene(playerWakeupClip.duration, 0));
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
        }
    }
}
