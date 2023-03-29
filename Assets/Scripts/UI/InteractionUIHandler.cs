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



    public void SetItemInspectorImage(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }
    public void OpenItemInspector(string text)
    {
        itemTextfield.text = text;
        itemInspector.SetActive(true);
    }
    public void CloseItemInspector()
    {
        itemInspector.SetActive(false);
    }
    public void OpenInputInfo(string buttonToPress)
    {
        inputTextfield.text = buttonToPress;
        inputInfo.SetActive(true);
    }
    public void CloseInputInfo()
    {
        inputInfo.SetActive(false);
    }
}
