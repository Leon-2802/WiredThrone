using System;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public UnityEvent playerAwake;
    public event EventHandler<SetFirstQuest> companionFound;
    public class SetFirstQuest : EventArgs
    {
        public string _text;
        public int _taskIterations;
    }
    [SerializeField] private CutSceneManager cutSceneManager;
    [SerializeField] private FindingCompanion findingCompanion;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        findingCompanion.foundCompanion += FoundCompanion;
    }

    public void InitStory()
    {
        cutSceneManager.InitPlayerWakeUpScene();
    }

    private void FoundCompanion(object sender, EventArgs e)
    {
        companionFound?.Invoke(this, new SetFirstQuest
        {
            _text = "Find 5 scrap parts",
            _taskIterations = 5
        }
        );
        findingCompanion.foundCompanion -= FoundCompanion;
    }

    private void OnDestroy()
    {
        findingCompanion.foundCompanion -= FoundCompanion;
    }
}
