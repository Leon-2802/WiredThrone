using UnityEngine;
using TMPro;

public class LogReader : MonoBehaviour
{
    [SerializeField] private TMP_Text headerTextField;
    [SerializeField] private string[] headings;
    [SerializeField] private GameObject[] logtexts;
    [SerializeField] private RectTransform[] headerBoxes;
    private float initialXPos;


    private void Start()
    {
        initialXPos = headerBoxes[0].position.x;
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
            rect.position = new Vector3(initialXPos, rect.position.y, 0);
        }

        logtexts[index].SetActive(true);
        headerBoxes[index].position = new Vector3((initialXPos * 1.1f), headerBoxes[index].position.y, 0);
    }
}
