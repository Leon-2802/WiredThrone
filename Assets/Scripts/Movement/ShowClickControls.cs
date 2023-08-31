using System.Collections;
using UnityEngine;

public class ShowClickControls : MonoBehaviour
{
    [SerializeField] private ClickMovement clickMovement;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject controlInfo;

    void Start()
    {

    }

    public void Show()
    {
        StartCoroutine(ShowClick());
    }

    IEnumerator ShowClick()
    {
        yield return new WaitForSeconds(1.5f);
        // clickMovement.MovePlayerToDestination(destination);
        controlInfo.SetActive(true);
    }
}
