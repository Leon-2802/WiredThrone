using UnityEngine;

public class TestRobot02 : MonoBehaviour
{
    [SerializeField] private float countdown = 2;
    [SerializeField] private Robot robot;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            ManagePositions.instance.SetTranslationTarget(this.gameObject, robot.robotIndex, 0);
            countdown = 100;
        }
    }
}
