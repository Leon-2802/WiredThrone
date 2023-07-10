using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public static RobotManager instance;
    [SerializeField] private List<Robot> allyRobots = new List<Robot>();
    [SerializeField] private List<EnemyRobot> enemyRobots = new List<EnemyRobot>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        if (ManagePositions.instance)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyRobot");
            GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");

            foreach(var e in enemies) {
                enemyRobots.Add(e.GetComponent<EnemyRobot>());
            }
            foreach(var e in robots) {
                allyRobots.Add(e.GetComponent<Robot>());
                e.transform.position = new Vector3(7, 0.868f, -14);
            }
        }
    }

    public List<Robot> GetRobots
    {
        get { return allyRobots; }
    }

    public List<EnemyRobot> GetEnemyRobots
    {
        get { return enemyRobots; }
    }

    public void AddRobot(Robot r)
    {
        allyRobots.Add(r);
    }

    public void AddEnemyRobot(EnemyRobot r)
    {
        enemyRobots.Add(r);
    }
}
