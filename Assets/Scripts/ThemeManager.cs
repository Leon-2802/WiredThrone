using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager instance;
    public Material interactionAvailable;
    public Material lockedDoor;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
}
