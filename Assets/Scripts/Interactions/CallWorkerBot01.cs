using System;
using UnityEngine;
using UnityEngine.Events;

public class CallWorkerBot01 : Collectible
{
    [SerializeField] private UnityEvent onRepaired;
    [SerializeField] private WorkerBot01 workerBot01;
    [SerializeField] private Transform target;

    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        workerBot01.CallRobotToTarget(target);
        workerBot01.repairFinised += RepairDone;
        base.OnStartInteraction(sender, e);
    }

    private void RepairDone(object sender, EventArgs e)
    {
        onRepaired.Invoke();
        workerBot01.repairFinised -= RepairDone;
    }
}
