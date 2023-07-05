using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBot00 : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] targets;
    [SerializeField] private Transform[] terminalTargets;
    private int targetIndex = 0;
    private bool interacting = false;
    private bool terminalRepair = false;
    private bool initialized = false;


    private void Start()
    {
        animator.SetBool("Idle", true);
    }

    public void InitTerminalRepair()
    {
        animator.SetBool("Idle", false);
        terminalRepair = true;
        agent.SetDestination(terminalTargets[targetIndex].position);
        initialized = true;
    }

    public void Init()
    {
        animator.SetBool("Idle", false);
        agent.SetDestination(targets[targetIndex].position);
        initialized = true;
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !interacting && initialized)
        {
            interacting = true;
            Debug.Log(targetIndex);
            if (!terminalRepair)
            {
                int rand = Random.Range(0, 10);
                if (rand < 2)
                    StartCoroutine(Repair(targets[targetIndex]));
                else
                    NextTarget();
            }
            else
            {
                StartCoroutine(Repair(terminalTargets[targetIndex]));
            }
        }
    }


    void NextTarget()
    {
        targetIndex++;
        if (!terminalRepair)
        {
            if (targetIndex == targets.Length)
                targetIndex = 0;

            agent.SetDestination(targets[targetIndex].position);
        }
        else
        {
            // if end of terminalTarget array is reached, set to normal repair loop and Init
            if (targetIndex == terminalTargets.Length)
            {
                terminalRepair = false;
                targetIndex = 0;
                Init();
                interacting = false;
                return;
            }
            agent.SetDestination(terminalTargets[targetIndex].position);
        }
        interacting = false;
    }

    private IEnumerator Repair(Transform currentTarget)
    {
        currentTarget.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        currentTarget.GetChild(0).gameObject.SetActive(false);
        NextTarget();
    }
}
