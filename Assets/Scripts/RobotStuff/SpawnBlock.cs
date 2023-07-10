using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;

    public void SpawnNewBlock(GameObject block)
    {
        if (block != null)
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
            GameObject instantiatedPrefab = Instantiate(block);

            instantiatedPrefab.transform.SetParent(canvas.transform);
            instantiatedPrefab.transform.position = _spawnPos.position;
            instantiatedPrefab.transform.localScale *= canvas.transform.localScale.x;  // Scale Object based on current canvas scale.
        }
        else
        {
            Debug.Log("prefab not found");
        }
    }

    public void SpawnBlockFromPath(string path)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        GameObject objectAtPath = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        
        GameObject instantiatedPrefab = Instantiate(objectAtPath);
        instantiatedPrefab.transform.SetParent(canvas.transform);
        instantiatedPrefab.transform.position = _spawnPos.position;
        instantiatedPrefab.transform.localScale *= canvas.transform.localScale.x;  // Scale Object based on current canvas scale.
    }
}
