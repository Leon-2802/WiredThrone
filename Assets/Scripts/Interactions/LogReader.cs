using UnityEngine;
using TMPro;

public class LogReader : MonoBehaviour
{
    [SerializeField] private TMP_Text headerTextField;
    [SerializeField] private string[] headings;
    [SerializeField] private GameObject[] logtexts;
    [SerializeField] private RectTransform[] headerBoxes;


    private void Start()
    {
        SelectLog(0);
    }

    public void SelectLog(int index)
    {
        headerTextField.text = headings[index];

        foreach (GameObject log in logtexts)
        {
            log.SetActive(false);
        }
        foreach (RectTransform rect in headerBoxes)
        {
            rect.position = new Vector3(144, rect.position.y, 0);
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 393.2637f);
        }

        logtexts[index].SetActive(true);
        headerBoxes[index].position = new Vector3(169.0198f, headerBoxes[index].position.y, 0);
        headerBoxes[index].sizeDelta = new Vector2(headerBoxes[index].sizeDelta.x, 423.3031f);
    }
}
