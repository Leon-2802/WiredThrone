using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuClickEvents : MonoBehaviour
{
    [SerializeField] private GameObject playOptions;
    [SerializeField] private GameObject warning;

    public void OnClickPlay()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            playOptions.SetActive(!playOptions.activeInHierarchy);
        }
        else
        {
            SceneManager.LoadScene("Spaceship");
        }
    }

    public void OnClickNewGame()
    {
        warning.SetActive(true);
    }
    public void OnConfirmNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Spaceship");

    }
    public void OnCancelNewGame()
    {
        warning.SetActive(false);
    }

    public void OnClickContinue()
    {
        SceneManager.LoadScene("Spaceship");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
