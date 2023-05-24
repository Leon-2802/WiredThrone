using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ModuleSelect : MonoBehaviour
{
    [SerializeField] private int _moduleId;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _moduleSelectionPanel;

    public int ModuleID {
        get { return _moduleId; }
    }

    private void Start() {
        _moduleSelectionPanel.SetActive(false);
        _moduleId = -1;
    }

    public void ToggleModuleSelection() {
        _moduleSelectionPanel.SetActive(!_moduleSelectionPanel.activeSelf);
    }

    public void SetModuleId(int id) {

            _moduleId = id;
            _text.text = id.ToString();
    }
}
