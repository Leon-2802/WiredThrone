using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfButton : MonoBehaviour
{
    private GameObject _ifBlock;
    [SerializeField] private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        Button but = _button.GetComponent<Button>();
        _ifBlock = Resources.Load<GameObject>("BlockIf");

        but.onClick.AddListener(InstantiateIfBlock);
    }

    void InstantiateIfBlock() {
        Debug.Log("Created if block");
        Instantiate(_ifBlock);
    }
}
