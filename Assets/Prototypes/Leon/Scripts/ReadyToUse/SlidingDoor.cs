using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ECharacterTypes acceptedCharacterType;
    private int timesOpened;

    private void Start() 
    {
        timesOpened = 0;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<MovingCharacter>())
        {
            timesOpened++;

            if(timesOpened == 1)
            {
                anim.SetBool("open", true); 
                SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<MovingCharacter>())
        {
            timesOpened--;

            if(timesOpened == 0) 
            {
                anim.SetBool("open", false);
                SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
            }
        }
    }
}
