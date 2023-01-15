using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustColliderSize : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RectTransform rect = GetComponent<RectTransform>();
        GetComponent<BoxCollider2D>().offset = new Vector2(rect.sizeDelta.x / 2, 0);
        GetComponent<BoxCollider2D>().size = rect.sizeDelta;
    }
}
