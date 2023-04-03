using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
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

    public void PlayerWakeUp()
    {
        cutSceneManager.InitPlayerWakeUpScene();
    }

    private void FoundCompanion(object sender, EventArgs e)
    {
        Debug.Log("Found companion");
        findingCompanion.foundCompanion -= FoundCompanion;
    }

    private void OnDestroy()
    {
        findingCompanion.foundCompanion -= FoundCompanion;
    }
}
