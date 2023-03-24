using System.Collections;
using UnityEngine;
using TMPro;

public class ClearUIHint : MonoBehaviour
{
    [SerializeField] private TMP_Text textfield;
    public void ClearAfterSeconds(float seconds)
    {
        StartCoroutine(Clear(seconds));
    }

    IEnumerator Clear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        textfield.text = "";
    }
}
