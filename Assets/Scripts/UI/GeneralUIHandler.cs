using System;
using UnityEngine;
using TMPro;

public class GeneralUIHandler : MonoBehaviour
{
    public static GeneralUIHandler instance;
    public event EventHandler<InspectorEventArgs> openInspector;
    public class InspectorEventArgs : EventArgs
    {
        public Sprite _itemSprite;
        public string _itemText;
        public string _interactionInfo;
    }
    public event EventHandler closeInspector;
    [SerializeField] private GameObject taskField;
    [SerializeField] private TMP_Text taskTextField;
    [SerializeField] private GameObject hideQuestText;
    [SerializeField] private GameObject showQuestText;
    private string taskText;
    private int taskCount;
    private bool questViewActive = false;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        QuestManager.instance.companionFound += FirstQuestUI;
    }

    public void SetQuestText(string text, int iterations)
    {
        taskText = text;
        taskTextField.text = taskText;
        if (iterations > 0)
        {
            taskCount = iterations;
            taskTextField.text = taskText + " (0/" + iterations + ")";
        }
        ToggleQuestView();
    }
    public void ToggleQuestView()
    {
        questViewActive = !questViewActive;
        if (questViewActive)
        {
            showQuestText.SetActive(false);
            hideQuestText.SetActive(true);
        }
        else
        {
            showQuestText.SetActive(true);
            hideQuestText.SetActive(false);
        }

        taskField.SetActive(questViewActive);
    }

    public void InvokeOpenInspector(Sprite itemSprite, string itemText, string interactionInfo)
    {
        openInspector.Invoke(this, new InspectorEventArgs
        {
            _itemSprite = itemSprite,
            _itemText = itemText,
            _interactionInfo = interactionInfo
        });
    }
    public void InvokeCloseInspector()
    {
        closeInspector.Invoke(this, EventArgs.Empty);
    }

    private void FirstQuestUI(object sender, QuestManager.SetFirstQuest e)
    {
        SetQuestText(e._text, e._taskIterations);
        QuestManager.instance.companionFound -= FirstQuestUI;
    }


    private void OnDestroy()
    {
        QuestManager.instance.companionFound -= FirstQuestUI;
    }
}
