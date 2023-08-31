using System.Collections;
using UnityEngine;

public class UnlockJammedDoor : MonoBehaviour
{
    [SerializeField] private GameObject repairEffect;
    [SerializeField] private GameObject doorOpenedDialogue;
    [SerializeField] private SlidingDoor lockedDoor;
    private bool unlocked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CompanionAnims>() && !unlocked)
        {
            repairEffect.SetActive(true);
            SoundManager.instance.PlaySoundOneShot(ESounds.Welding);
            StartCoroutine(disableClass());
        }
    }

    IEnumerator disableClass()
    {
        yield return new WaitForSeconds(2f);
        lockedDoor.Unlock();
        repairEffect.SetActive(false);
        unlocked = true;
        doorOpenedDialogue.SetActive(true);
        this.enabled = false;
    }
}
