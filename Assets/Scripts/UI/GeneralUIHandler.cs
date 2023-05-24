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
        public Sprite _interactionSprite;
        public string _interactionButton;
        public string _interactionText;
    }
    [SerializeField] private GameObject taskField;
    [SerializeField] private TMP_Text taskTextField;
    private string taskText;
    private int taskIterations;
    private bool questViewActive;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        questViewActive = false;
        QuestManager.instance.setQuest += SetQuestText;
        QuestManager.instance.taskStepDone += OnTaskStepDone;
    }

    public void SetQuestText(object sender, QuestManager.SetQuestText e)
    {
        taskText = e._text;
        taskTextField.text = taskText;
        if (e._taskIterations > 0)
        {
            taskIterations = e._taskIterations;
            taskTextField.text = taskText + " (" + 0 + "/" + taskIterations + ")";
        }

        if (!questViewActive)
            ToggleQuestView();
    }
    public void ToggleQuestView()
    {
        questViewActive = !questViewActive;
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
    public void InvokeOpenInteractionInfo(Sprite interactionIcon, string interactionButton, string interactionInfo)
    {
        openInteractionInfo.Invoke(this, new ItemInteractionEventArgs
        {
            _interactionSprite = interactionIcon,
            _interactionButton = interactionButton,
            _interactionText = interactionInfo
        });
    }

    private void OnTaskStepDone(object sender, QuestManager.TaskSteps e)
    {
        taskTextField.text = taskText + " (" + e._stepCount + "/" + taskIterations + ")";
    }

    private void OnDestroy()
    {
        QuestManager.instance.setQuest -= SetQuestText;
        QuestManager.instance.taskStepDone -= OnTaskStepDone;
    }
}
