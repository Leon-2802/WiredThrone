using UnityEngine;

public enum ESounds { SlidingDoor, Text, DoorError, Collect, RepairCompanion }
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource slidingDoor;
    [SerializeField] private AudioSource text;
    [SerializeField] private AudioSource doorError;
    [SerializeField] private AudioSource collect;
    [SerializeField] private AudioSource repairCompanion;

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

            case ESounds.DoorError:
                doorError.Play();
                break;

            case ESounds.Collect:
                collect.Play();
                break;

            case ESounds.RepairCompanion:
                repairCompanion.Play();
                break;
        }
    }
}
