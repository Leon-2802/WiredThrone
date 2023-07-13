using UnityEngine;

public class PlanetConsole : MonoBehaviour
{
    [SerializeField] private MeshRenderer mRenderer;
    [SerializeField] private Material[] planetMats;
    [SerializeField] private GameObject canvasContent;
    [SerializeField] private GameObject planet00Text;
    [SerializeField] private GameObject planet01Text;
    private int currentPlanetIndex = -1;


    public void InitConsole()
    {
        canvasContent.SetActive(true);
        mRenderer.material = planetMats[0];
        currentPlanetIndex = 0;
    }

    public void SwitchPlanet()
    {
        if (currentPlanetIndex == 0)
        {
            currentPlanetIndex = 1;
            planet01Text.SetActive(true);
            planet00Text.SetActive(false);
        }
        else if (currentPlanetIndex == 1)
        {
            currentPlanetIndex = 0;
            planet01Text.SetActive(false);
            planet00Text.SetActive(true);
        }
        else if (currentPlanetIndex == -1)
        {
            return;
        }

        mRenderer.material = planetMats[currentPlanetIndex];
    }

    public void TravelToPlanet()
    {
        if (currentPlanetIndex == -1)
            return;

        Debug.Log("Traveling to planet");
    }
}
