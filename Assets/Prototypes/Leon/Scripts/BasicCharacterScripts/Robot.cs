public class Robot : MovingCharacter
{
    private WeaponModule _leftAttackModule;
    private WeaponModule _rightAttackModule;
    private MoveModule _moveModule;
    public int robotIndex;

    // Start is called before the first frame update
    void Start()
    {
        robotIndex = ManagePositions.instance.robotPositions.Count;
        ManagePositions.instance.robotPositions.Add(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackLeft()
    {

    }

    public void AttackRight()
    {

    }

    public void Move()
    {

    }

    public void Heal()
    {

    }
}
