using UnityEngine;

public class TestRobot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ManagePositions.instance.TranslatePositionGivenMoves(this.gameObject, new Vector2(2, 3), 4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
