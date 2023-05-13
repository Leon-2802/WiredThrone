using System.Collections;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ECharacterTypes acceptedCharacterType;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private bool locked;
    private int timesOpened;
    private Material standardMaterial;

    private void Start()
    {
        timesOpened = 0; //if several characters pass the door, it should only close as soon as all of them left the door area
        standardMaterial = meshRenderer.materials[0];
    }

    public void Unlock()
    {
        locked = false;
        Open();
    }

    public void SetLocked(bool locked)
    {
        this.locked = locked;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovingCharacter>())
        {
            if (locked)
            {
                meshRenderer.material = ThemeManager.instance.lockedDoor;
                StartCoroutine(ShowStandardMat());
                SoundManager.instance.PlaySoundOneShot(ESounds.DoorError);
                return;
            }

            Open();
        }
    }

    private void Open()
    {
        timesOpened++;

        if (timesOpened == 1)
        {
            anim.SetBool("open", true);
            SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MovingCharacter>() && !locked)
        {
            Close();
        }
    }

    private void Close()
    {
        timesOpened--;

        if (timesOpened == 0)
        {
            anim.SetBool("open", false);
            SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
        }

    }

    private IEnumerator ShowStandardMat()
    {
        yield return new WaitForSeconds(1f);
        meshRenderer.material = standardMaterial;
    }
}
