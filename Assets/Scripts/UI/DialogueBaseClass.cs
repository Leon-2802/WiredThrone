using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueBaseClass : MonoBehaviour
{
    public bool finished { get; private set; }
    protected IEnumerator WriteText(string input, TMP_Text textHolder, Color textColor, float delay, float delayBetweenLines)
    {
        textHolder.color = textColor;

        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            SoundManager.instance.PlaySoundOneShot(ESounds.Text);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delayBetweenLines);
        textHolder.text = "";
        finished = true;
    }
}