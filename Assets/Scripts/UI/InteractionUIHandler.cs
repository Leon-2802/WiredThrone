using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject itemInspector;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemTextfield;
    [SerializeField] private GameObject interactionInfo;
    [SerializeField] private Image interactionIcon;
    [SerializeField] private TMP_Text interactionText;


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
        interactionIcon.sprite = e._interactionSprite;
        interactionText.text = "[" + e._interactionButton + "] " + e._interactionText;
        interactionInfo.SetActive(true);
    }
    private void CloseItemInspector(object sender, EventArgs e)
    {
        itemInspector.SetActive(false);
        interactionInfo.SetActive(false);
    }

    private void OnDestroy()
    {
        GeneralUIHandler.instance.openInspector -= OpenItemInspector;
        GeneralUIHandler.instance.openInteractionInfo -= SetInteractionInfo;
        GeneralUIHandler.instance.closeInspector -= CloseItemInspector;
    }
}
