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
    }
    public event EventHandler closeInspector;
    public event EventHandler<ItemInteractionEventArgs> openInteractionInfo;
    public class ItemInteractionEventArgs : EventArgs
    {
        public string _inputText;
    }
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

    public void InvokeOpenInspector(Sprite itemSprite, string itemText)
    {
        openInspector.Invoke(this, new InspectorEventArgs
        {
            _itemSprite = itemSprite,
            _itemText = itemText,
        });
    }
    public void InvokeCloseInspector()
    {
        closeInspector.Invoke(this, EventArgs.Empty);
    }
    public void InvokeOpenInteractionInfo(string interactionInfo)
    {
        openInteractionInfo.Invoke(this, new ItemInteractionEventArgs
        {
            _inputText = interactionInfo
        });
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
