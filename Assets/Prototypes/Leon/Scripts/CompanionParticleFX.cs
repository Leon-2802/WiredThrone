using UnityEngine;
using UnityEngine.AI;

public class CompanionParticleFX : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject rightFootEmissionsGO;
    [SerializeField] private ParticleSystem leftFootEmissionsPS;


    void Update()
    {
        if (!agent.hasPath || agent.velocity.sqrMagnitude <= Mathf.Epsilon)
        {
            rightFootEmissionsGO.SetActive(false);
            var main = leftFootEmissionsPS.main;
            main.startLifetime = 0.1f;
        }
        else
        {
            rightFootEmissionsGO.SetActive(true);
            var main = leftFootEmissionsPS.main;
            main.startLifetime = 0.15f;
        }
    }
}
