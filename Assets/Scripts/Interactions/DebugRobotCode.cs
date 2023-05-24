using UnityEngine;
public class DebugRobotCode : Computer
{
    private bool onStayPassed = false;
    private void OnTriggerStay(Collider other)
    {
        if (!onlyInspect && !onStayPassed && other.gameObject.GetComponent<Player>())
        {
            Debug.Log("Test onStay");
            base.OnTriggerEnter(other);
            onStayPassed = true;
        }
    }
}
