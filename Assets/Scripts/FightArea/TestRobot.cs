using UnityEngine;

public class TestRobot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ManagePositions.instance.TranslatePosition(this.gameObject, new Vector2(2, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
