using UnityEngine;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    [SerializeField] private TMP_Text textHolder;

    [Header("Text Options")]
    [SerializeField] private string input;
    [SerializeField] private Color textColor;

    [Header("Time Parameters")]
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float delayBetweenLines = 1f;


    private void Start()
    {
        StartCoroutine(WriteText(input, textHolder, textColor, delay, delayBetweenLines));
    }
}
