public class Robot : MovingCharacter
{
    private WeaponModule _leftAttackModule;
    private WeaponModule _rightAttackModule;
    private MoveModule _moveModule;
    private ShellModule _leftShellModule;
    private ShellModule _rightShellModule;
    public int robotIndex;

    public Robot(WeaponModule wl, WeaponModule wr, MoveModule m, ShellModule sl, ShellModule sr) {
        _leftAttackModule = wl;
        _rightAttackModule = wr;
        _moveModule = m;
        _leftShellModule = sl;
        _rightShellModule = sr;
    }

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
