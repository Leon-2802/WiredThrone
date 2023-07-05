using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBot01 : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private GameObject repairEffect;
    private bool enRoute = false;
    private bool movingBackToIdlePos = false;

    private void Start()
    {
        agent.SetDestination(idlePosition.position);
        enRoute = true;
        movingBackToIdlePos = true;
    }

    public void CallRobotToTarget(Transform target)
    {
        agent.SetDestination(target.position);
        enRoute = true;
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && enRoute)
        {
            if (movingBackToIdlePos)
            {
                movingBackToIdlePos = false;
            }
            else
            {
                StartCoroutine(Repair());
            }
            enRoute = false;
        }
    }

    private IEnumerator Repair()
    {
        repairEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        repairEffect.SetActive(false);
        agent.SetDestination(idlePosition.position);
        movingBackToIdlePos = true;
        enRoute = true;
    }

}
