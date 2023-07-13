using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ECharacterTypes acceptedCharacterType;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private bool locked;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private NavMeshObstacle meshObstacle;
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
        boxCollider.enabled = false;
        meshObstacle.enabled = false;
        Open(true);
    }

    public void SetLocked(bool locked)
    {
        this.locked = locked;
        boxCollider.enabled = locked;
        meshObstacle.enabled = locked;
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

            bool playSound;
            if (other.gameObject.GetComponent<Player>() || other.gameObject.GetComponent<CompanionAnims>())
                playSound = true;
            else
                playSound = false;

            Open(playSound);
        }
    }

    private void Open(bool sound)
    {
        timesOpened++;

        if (timesOpened == 1)
        {
            anim.SetTrigger("open");
            if (sound)
                SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MovingCharacter>() && !locked)
        {
            bool playSound;
            if (other.gameObject.GetComponent<Player>() || other.gameObject.GetComponent<CompanionAnims>())
                playSound = true;
            else
                playSound = false;

            Close(playSound);
        }
    }

    private void Close(bool sound)
    {
        timesOpened--;

        if (timesOpened == 0)
        {
            anim.SetTrigger("close");
            if (sound)
                SoundManager.instance.PlaySoundOneShot(ESounds.SlidingDoor);
        }

    }

    private IEnumerator ShowStandardMat()
    {
        yield return new WaitForSeconds(1f);
        meshRenderer.material = standardMaterial;
    }
}
