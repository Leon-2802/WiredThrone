using System;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public string[] quests;
    public int finishedQuests = 0;
    public UnityEvent companionFound;
    public UnityEvent scrapCollected;
    public UnityEvent repairedCompanion;
    public UnityEvent reachedWorkerBots;
    public UnityEvent debuggedWorkerBots;
    public event EventHandler<SetQuestText> setQuest;
    public class SetQuestText : EventArgs
    {
        public string _text;
        public int _taskIterations;
    }
    public event EventHandler<TaskSteps> taskStepDone;
    public class TaskSteps : EventArgs
    {
        public int _stepCount;
    }
    [SerializeField] private CutSceneManager cutSceneManager;
    private int scrapPartsCollected = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void InitStory()
    {
        cutSceneManager.InitPlayerWakeUpScene();
    }

    public void FoundCompanion()
    {
        InvokeSetQuest("Find 5 scrap parts", 5);
        companionFound.Invoke();
    }

    public void CollectedScrapPart()
    {
        scrapPartsCollected++;
        taskStepDone.Invoke(this, new TaskSteps
        {
            _stepCount = scrapPartsCollected
        });

        if (scrapPartsCollected >= 5)
        {
            InvokeSetQuest("Go back to the damaged robot and repair it", 0);
            scrapCollected.Invoke();
        }
    }

    public void RepairedCompanion()
    {
        finishedQuests++;
        repairedCompanion.Invoke();
    }

    public void ReachedWorkerBots()
    {
        finishedQuests++;
        reachedWorkerBots.Invoke();
        InvokeSetQuest("Debug the two worker robots", 2);
    }
    public void DebuggedOneWokerBot(int counter)
    {
        taskStepDone.Invoke(this, new TaskSteps
        {
            _stepCount = counter
        });
    }

    public void DebuggedWorkerBots()
    {
        debuggedWorkerBots.Invoke();
        InvokeSetQuest("New Task", 0);
    }

    public void InvokeSetQuest(string text, int taskIterations)
    {
        setQuest.Invoke(this, new SetQuestText
        {
            _text = text,
            _taskIterations = taskIterations
        });
    }
}
