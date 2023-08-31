using UnityEngine;

public class AttackBlock : MonoBehaviour
{
    [SerializeField] private Side _side;

    public Side Value
    {
        get { return _side; }
    }
}