using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntButton : MonoBehaviour
{
    [SerializeField] private GameObject _blockToSpawn;
    [SerializeField] private Transform _posToSpawn;
    // Start is called before the first frame update



    public void SpawnBlock()
    {
        Instantiate(_blockToSpawn, _posToSpawn);
    }
}