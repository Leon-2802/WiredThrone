using UnityEngine;

public class EnableScripts : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] scripts;

    public void EnableScriptsArray()
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}
