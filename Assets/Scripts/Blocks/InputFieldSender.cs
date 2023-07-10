using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldSender : MonoBehaviour
{
    [SerializeField] private GameObject receivingObject;

    private TMP_InputField inputField;

    private GameObject _sideBlock;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(SendValueToReceivingObject);
        receivingObject = GameObject.FindGameObjectWithTag("SideBarCanvas");
    }

    private void SendValueToReceivingObject(string value)
    {
        if (receivingObject != null)
        {
            GameObject sideBlock = gameObject.GetComponentInParent<SideConnection>().SideBlock;
            //receivingObject.SendMessage("SetName", inputField.text);
            if (sideBlock != null) {
                TMP_InputField sideBlockInput = sideBlock.GetComponentInChildren<TMP_InputField>();
                if (sideBlockInput.text != "" && sideBlockInput.text != null) {
                    receivingObject.SendMessage("SetVariable", inputField.text + " " + sideBlockInput.text);
                }
            }
        }
    }
}