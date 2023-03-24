public class EnemyRobot : MovingNPC
{
    public int enemyIndex;
    void Start()
    {
        ManagePositions.instance.enemyPositions.Add(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
