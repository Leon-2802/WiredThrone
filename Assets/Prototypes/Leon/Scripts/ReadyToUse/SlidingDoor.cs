using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ECharacterTypes acceptedCharacterType;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<MovingCharacter>())
        {
            anim.SetBool("open", true);
            SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<MovingCharacter>())
        {
            anim.SetBool("open", false);
            SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
        }
    }
}
