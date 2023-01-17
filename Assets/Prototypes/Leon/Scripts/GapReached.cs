using UnityEngine;

public class GapReached : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<Player>())
        {
            bool jumpState = playerAnimator.GetBool("Jump");
            playerAnimator.SetBool("Jump", !jumpState);
        }
    }
}
