using UnityEngine;

public class PlanetConsole : MonoBehaviour
{
    [SerializeField] private CutSceneManager cutSceneManager;
    [SerializeField] private MeshRenderer mRenderer;
    [SerializeField] private MeshRenderer holoRenderer;
    [SerializeField] private Material[] planetMats;
    [SerializeField] private GameObject canvasContent;
    [SerializeField] private GameObject planet00Text;
    [SerializeField] private GameObject planet01Text;
    [SerializeField] private GameObject inactiveButton;
    [SerializeField] private GameObject activeButton;
    [SerializeField] private GameObject planet00Mesh;
    [SerializeField] private GameObject planet01Mesh;
    private int currentPlanetIndex = -1;
    private bool travellingPossible = false;
    private float previousOutlineSize;


    public void InitConsole()
    {
        canvasContent.SetActive(true);
        holoRenderer.enabled = false;
        mRenderer.enabled = true;
        mRenderer.material = planetMats[0];
        currentPlanetIndex = 0;
    }

    public void SetTravellingPossible()
    {
        inactiveButton.SetActive(false);
        activeButton.SetActive(true);
        travellingPossible = true;
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
        previousOutlineSize = mRenderer.material.GetFloat("_OutlineSize");
        mRenderer.material.SetFloat("_OutlineSize", previousOutlineSize * 1.4f);
        mRenderer.material.SetColor("_OutlineColor", Color.white);
    }

    public void TravelToPlanet()
    {
        if (currentPlanetIndex == -1 || !travellingPossible)
            return;

        if (currentPlanetIndex == 0)
        {
            planet00Mesh.SetActive(true);
        }
        else if (currentPlanetIndex == 1)
        {
            planet01Mesh.SetActive(true);
        }

        cutSceneManager.InitEndCutscene();
    }
}
