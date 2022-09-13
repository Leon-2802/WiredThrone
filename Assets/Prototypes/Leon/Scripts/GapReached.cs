using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapReached : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            playerAnimator.SetBool("Jump", true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            playerAnimator.SetBool("Jump", false);
        }
    }
}
