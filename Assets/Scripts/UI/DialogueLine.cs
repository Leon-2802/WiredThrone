using UnityEngine;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    public bool debugSkip = false;
    [SerializeField] private TMP_Text textHolder;

    [Header("Text Options")]
    [SerializeField] private string input;

    [Header("Time Parameters")]
    private float delay = 0.1f;
    [SerializeField] private float delayBetweenLines = 1.5f;


    private void Start()
    {
        delay = ThemeManager.instance.dialogueSpeed;
        StartCoroutine(WriteText(input, textHolder, textHolder.color, delay, delayBetweenLines));
    }
}
