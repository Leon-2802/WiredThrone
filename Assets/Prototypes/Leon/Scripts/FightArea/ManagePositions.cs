using UnityEngine;

public class ManagePositions : MonoBehaviour
{
    public static ManagePositions instance;
    public Vector2 robotPosition;
    private void Awake() 
    {
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else    
            instance = this;
    }

    private void Start() 
    {
        robotPosition = new Vector2(0, 0);    
    }
}
