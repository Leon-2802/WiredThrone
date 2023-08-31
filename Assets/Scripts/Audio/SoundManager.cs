using UnityEngine;

public enum ESounds { SlidingDoor, Text, DoorError, Collect, RepairCompanion, Welding, Success, Click }
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource slidingDoor;
    [SerializeField] private AudioSource text;
    [SerializeField] private AudioSource doorError;
    [SerializeField] private AudioSource collect;
    [SerializeField] private AudioSource repairCompanion;
    [SerializeField] private AudioSource welding;
    [SerializeField] private AudioSource success;
    [SerializeField] private AudioSource click;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        music.Play();
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
            case ESounds.Welding:
                welding.Play();
                break;
            case ESounds.Success:
                success.Play();
                break;
            case ESounds.Click:
                click.Play();
                break;
        }
    }
}
