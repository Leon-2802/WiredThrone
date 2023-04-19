using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClickEvents : MonoBehaviour
{
    [SerializeField] private GameObject playOptions;

    public void OnClickPlay()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            playOptions.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Spaceship");
        }
    }

    public void OnClickNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Spaceship");
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
