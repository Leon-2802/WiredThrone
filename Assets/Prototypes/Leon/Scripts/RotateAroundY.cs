using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y + Time.deltaTime * rotateSpeed), transform.eulerAngles.z);
    }
}
