using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject firstRow;
    [SerializeField] private GameObject secondRow;
    [SerializeField] private GameObject thirdRow;
    [SerializeField] private Color _originalColor;

    private void Start()
    {
        _originalColor = firstRow.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].color;
        //RobotManager.instance.GetRobots[0].transform.position = new Vector3(7, 0.868f, -14);
    }
    public void SetMaterial()
    {
        for (int i = 0; i < firstRow.transform.childCount; i++)
        {
            firstRow.transform.GetChild(i).GetComponent<MeshRenderer>().materials[0].color = Color.magenta;
            //firstRow.transform.GetChild(i).gameObject.AddComponent<MouseSelect>();
        }
        for (int i = 0; i < secondRow.transform.childCount; i++)
        {
            secondRow.transform.GetChild(i).GetComponent<MeshRenderer>().materials[0].color = Color.magenta;
            //secondRow.transform.GetChild(i).gameObject.AddComponent<MouseSelect>();
        }
        for (int i = 0; i < thirdRow.transform.childCount; i++)
        {
            thirdRow.transform.GetChild(i).GetComponent<MeshRenderer>().materials[0].color = Color.magenta;
            //thirdRow.transform.GetChild(i).gameObject.AddComponent<MouseSelect>();
        }
    }

    public void ResetMaterial()
    {
        for (int i = 0; i < firstRow.transform.childCount; i++)
        {

            firstRow.transform.GetChild(i).GetComponent<MeshRenderer>().materials[0].color = _originalColor;
        }
        for (int i = 0; i < secondRow.transform.childCount; i++)
        {
            secondRow.transform.GetChild(i).GetComponent<MeshRenderer>().materials[0].color = _originalColor;
        }
        for (int i = 0; i < thirdRow.transform.childCount; i++)
        {
            thirdRow.transform.GetChild(i).GetComponent<MeshRenderer>().materials[0].color = _originalColor;
        }
    }

    public void Begin()
    {
        List<Robot> robots = RobotManager.instance.GetRobots;
        robots[0].Evaluate();
    }
}