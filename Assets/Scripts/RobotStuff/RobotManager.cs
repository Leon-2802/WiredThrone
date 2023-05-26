using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public static RobotManager instance;
    [SerializeField] private List<Robot> robots = new List<Robot>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public List<Robot> GetRobots
    {
        get { return robots; }
    }
    public void AddRobot(Robot r)
    {
        robots.Add(r);
    }
}
