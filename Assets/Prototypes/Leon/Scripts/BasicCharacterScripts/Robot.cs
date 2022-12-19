using UnityEngine;

public class Robot : MovingCharacter
{
    public int robotIndex;
    void Start()
    {
        ManagePositions.instance.robotPositions.Add(new Vector2(0,0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
