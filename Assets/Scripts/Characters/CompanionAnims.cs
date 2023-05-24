using UnityEngine;

public class CompanionAnims : MonoBehaviour
{
    [SerializeField] private GameObject screen;

    public void EnableScreen()
    {
        screen.SetActive(true);
    }

    public void DisableScreen()
    {
        screen.SetActive(false);
    }
}
