using UnityEngine;

public class EnemyRobot : MovingNPC
{
    public int enemyIndex;
    void Start()
    {
        ManagePositions.instance.enemyPositions.Add(new Vector2(0,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
