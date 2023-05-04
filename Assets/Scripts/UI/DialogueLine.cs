using UnityEngine;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    [SerializeField] private TMP_Text textHolder;

    [Header("Text Options")]
    [SerializeField] private string input;
    [SerializeField] private Color textColor;

    [Header("Time Parameters")]
    [SerializeField] private float delay;


    private void Awake()
    {
        StartCoroutine(WriteText(input, textHolder, textColor, delay));
    }
}
