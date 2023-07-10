using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberBlock : MonoBehaviour
{
    [SerializeField] private int _number;
    private TMP_InputField inputField;

    public int Value
    {
        get { return _number; }
    }
    private void Start()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(delegate { SetValue(); });
    }

    private void SetValue()
    {
        try
        {
            _number = int.Parse(inputField.text);
        } catch {
            _number = 0;
        }
    }


}