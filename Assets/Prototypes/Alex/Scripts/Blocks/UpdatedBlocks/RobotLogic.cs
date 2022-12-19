using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLogic : MonoBehaviour
{
    private Block[] _blocks;

    // Start is called before the first frame update
    void Start()
    {
        Block<Action> block1 = new Block<Action>("actionenum", Action.Move, new Vector2(0, -1));
        _blocks = new Block[] {
            block1
        };
    }

    // Update is called once per frame
    void Update()
    {
        Evaluate();
    }

    private void Evaluate()
    {
        foreach (var block in _blocks)
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
                    Robot component = gameObject.GetComponent<Robot>();
                    switch (actionBlock.Value)
                    {
                        case Action.AttackLeft:
                            component.AttackLeft();
                            break;
                        case Action.AttackRight:
                            component.AttackRight();
                            break;
                        case Action.Move:
                            component.Move();
                            break;
                        case Action.Heal:
                            component.Heal();
                            break;
                    }
                    break;
            }
        }
    }
}
