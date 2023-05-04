using UnityEngine;

public enum ESounds { SlidingDoor, Text }
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource slidingDoor;
    [SerializeField] private AudioSource text;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void PlaySoundOneShot(ESounds soundName)
    {
        switch (soundName)
        {
            case ESounds.SlidingDoor:
                slidingDoor.Play();
                break;

            case ESounds.Text:
                text.Play();
                break;
        }
    }
}
