using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueBaseClass : MonoBehaviour
{
    protected IEnumerator WriteText(string input, TMP_Text textHolder, Color textColor, float delay)
    {
        textHolder.color = textColor;

        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            SoundManager.instance.PlaySoundOneShot(ESounds.Text);
            yield return new WaitForSeconds(delay);
        }
    }
}