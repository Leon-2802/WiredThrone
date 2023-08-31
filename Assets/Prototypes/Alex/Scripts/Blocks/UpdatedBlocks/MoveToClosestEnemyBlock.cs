using UnityEngine;

public class MoveToClosestEnemyBlock : MonoBehaviour
{

    [SerializeField] private SideConnection _sideConnection;

    private void Start()
    {
        _sideConnection = GetComponent<SideConnection>();
    }

    public Block<Action> ReturnBlock(Vector3 position)
    {
        if (ManagePositions.instance)
        {
            Vector2 moveVector = ManagePositions.instance.VectorToClosestEnemy(position);
            return new Block<Action>("actionenum", Action.Move, moveVector);
        }
        return new Block<Action>("actionenum", Action.Move, Vector2.zero);
    }


}