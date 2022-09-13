using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESounds {SlidingDoor}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource slidingDoor;
    
    private void Awake() 
    {
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else
        instance = this;
    }

    public void PlaySoundOneShot(ESounds soundName)
    {
        switch(soundName)
        {
            case ESounds.SlidingDoor:
                slidingDoor.Play();
                break;
        }
    }
}
