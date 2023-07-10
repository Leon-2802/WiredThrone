using UnityEngine;
using System;
using System.Collections.Generic;
public class Robot : MovingCharacter
{
    public static Robot instance;
    public WeaponModule _leftAttackModule;
    public WeaponModule _rightAttackModule;
    public MoveModule _moveModule;
    public ShellModule _leftShellModule;
    public ShellModule _rightShellModule;
    public List<Block> _logic;
    public int robotIndex;
    public string laname;
    public string raname;
    public string lsname;
    public string rsname;
    public int count;

    void Start()
    {
        laname = _leftAttackModule.Name;
        raname = _rightAttackModule.Name;
        lsname = _leftShellModule.Name;
        rsname = _rightShellModule.Name;
        count = _logic.Count;
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        if (ManagePositions.instance)
        {
            robotIndex = ManagePositions.instance.robotPositions.Count;
            gameObject.transform.position = new Vector3(5.888f, 0.73f, -13);
            gameObject.transform.localScale = new Vector3(0.23f, 0.23f, 0.23f);
            ManagePositions.instance.robotPositions.Add(this.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AttackLeft()
    {
        Debug.Log("AttackLeft");
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= 2; x++)
            {
                if (_leftAttackModule.Shape[x, y])
                {
                    Vector2 curPos = ManagePositions.instance.robotPositions[robotIndex];
                    Vector2 hitPos = new Vector2(curPos.x + x - 1, curPos.y + y - 1);
                    Debug.Log("Hitpos: " + hitPos + " enemyPos: " + ManagePositions.instance.enemyPositions[0]);
                    if (ManagePositions.instance.EnemyOnBlock(hitPos))
                    {
                        EnemyRobot enemy = ManagePositions.instance.GetEnemyOnBlock(hitPos);
                        Debug.Log(enemy.name);
                        enemy.TakeDamage(_leftAttackModule.Dmg);
                    }
                }
            }
        }
    }

    public void AttackRight()
    {

    }

    public void Move(Block block)
    {
        if ((string)block.GetName() == "MoveToClosestEnemy")
        {
            Debug.Log("MoveToClosestEnemy");
            int maxMoves = (_moveModule.MaxDistance > (int)block.GetValue()) ? (int)block.GetValue() : _moveModule.MaxDistance;
            Debug.Log("Max moves: " + maxMoves);
            Vector2 closest = ManagePositions.instance.VectorToClosestEnemy(ManagePositions.instance.robotPositions[robotIndex]);
            float dist = closest.x + closest.y;

            ManagePositions.instance.TranslatePositionGivenMoves(gameObject, closest, maxMoves, robotIndex);

        }
    }

    public void Heal()
    {

    }
    public void Evaluate()
    {
        Debug.Log("blocks: " + _logic.Count);
        foreach (var block in _logic)
        {
            switch (block)
            {
                case Block<int> intblock:
                    Debug.Log(intblock.Value);
                    break;
                case Block<float> floatblock:
                    Debug.Log(floatblock.Value);
                    break;
                case Block<string> stringblock:
                    Debug.Log(stringblock.Value);
                    break;
                case Block<Vector3> vector3block:
                    Debug.Log(vector3block.Value);
                    break;
                case Block<bool> boolblock:
                    Debug.Log(boolblock.Value);
                    break;
                case Block<Action> actionBlock:
                    switch (actionBlock.Value)
                    {
                        case Action.AttackLeft:
                            AttackLeft();
                            break;
                        case Action.AttackRight:
                            AttackRight();
                            break;
                        case Action.Move:
                            Move(block);
                            break;
                        case Action.Heal:
                            Heal();
                            break;
                    }
                    break;
            }
        }
    }
}
