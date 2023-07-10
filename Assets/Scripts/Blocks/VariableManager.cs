using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VariableManager : MonoBehaviour
{
    private Dictionary<string, string> _variables;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _blockObject;
    [SerializeField] private int _createdVariables = 0;
    [SerializeField] private GameObject _variablePanel;

    // Start is called before the first frame update
    void Start()
    {
        _variables = new Dictionary<string, string>();
    }

    public void SetVariable(string values)
    {
        string[] result = values.Split();
        _variables[result[0]] = result[1];
        Debug.Log(result[0] + " " + result[1]);
        CreateVariableButton(result[0], result[1]);
    }

    private void CreateVariableButton(string name, string value)
    {
        // Create new Button instance.
        GameObject newButton = Instantiate(_button);
        newButton.transform.SetParent(_variablePanel.transform);

        // Calculate position for the button.
        int childCount = newButton.transform.parent.childCount;
        Vector2 pos = newButton.transform.parent.GetChild(childCount-2).position;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), new Vector2(0, yPos), Camera.main, out Vector2 canvasLocation);
        newButton.transform.localPosition = new Vector2(70, 160 - _createdVariables * 50);
        newButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = name;

        // Create new variable block instance.
        GameObject newBlock = Instantiate(_blockObject);
        childCount = newBlock.transform.childCount;
        newBlock.transform.GetChild(childCount-1).GetComponent<TextMeshProUGUI>().text = name;
        newBlock.name = name;
        
        // Create prefab from newly created variable.
        string path = "Assets/Prefabs/Blocks/variables/" + newBlock.name + ".prefab";
        GameObject prefabBlock = PrefabUtility.SaveAsPrefabAsset(newBlock, path);
        DestroyImmediate(newBlock);


        // Add onClick events for block spawning.
        newButton.GetComponent<Button>().onClick.RemoveAllListeners();
        newButton.GetComponent<Button>().onClick.AddListener(delegate {SpawnBlockFromScript(path);});
        
        _createdVariables++;
    }

    public void SpawnBlockFromScript(string path) {
        GameObject.FindGameObjectWithTag("SideBarCanvas").GetComponent<SpawnBlock>().SpawnBlockFromPath(path);
    }
}
