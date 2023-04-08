using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject itemInspector;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemTextfield;
    [SerializeField] private GameObject inputInfo;
    [SerializeField] private TMP_Text inputTextfield;


    private void Start()
    {
        GeneralUIHandler.instance.openInspector += OpenItemInspector;
        GeneralUIHandler.instance.openInteractionInfo += SetInteractionInfo;
        GeneralUIHandler.instance.closeInspector += CloseItemInspector;
    }

    private void OpenItemInspector(object sender, GeneralUIHandler.InspectorEventArgs e)
    {
        itemImage.sprite = e._itemSprite;
        itemTextfield.text = e._itemText;
        itemInspector.SetActive(true);

    }
    private void SetInteractionInfo(object sender, GeneralUIHandler.ItemInteractionEventArgs e)
    {
        inputTextfield.text = e._inputText;
        inputInfo.SetActive(true);
    }
    private void CloseItemInspector(object sender, EventArgs e)
    {
        itemInspector.SetActive(false);
        inputInfo.SetActive(false);
    }

    private void OnDestroy()
    {
        GeneralUIHandler.instance.openInspector -= OpenItemInspector;
        GeneralUIHandler.instance.openInteractionInfo -= SetInteractionInfo;
        GeneralUIHandler.instance.closeInspector -= CloseItemInspector;
    }
}
