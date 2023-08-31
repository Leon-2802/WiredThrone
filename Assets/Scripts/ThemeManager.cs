using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager instance;
    public Material interactionAvailable;
    public Material lockedDoor;
    public float dialogueSpeed = 0.1f;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
}
