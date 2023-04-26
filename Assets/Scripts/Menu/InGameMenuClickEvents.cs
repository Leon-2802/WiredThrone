using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuClickEvents : MonoBehaviour
{
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject warning;

    public void OnClickResume()
    {
        inGameMenu.SetActive(false);
    }

    public void OnClickQuit()
    {
        warning.SetActive(true);
    }
    public void OnConfirmQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnCancelQuit()
    {
        warning.SetActive(false);
    }
}
