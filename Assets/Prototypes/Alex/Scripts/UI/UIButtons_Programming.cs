using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons_Programming : MonoBehaviour
{
    public void TogglePanel()
    {
        
        foreach (var panel in GameObject.FindGameObjectsWithTag("SidebarPanel"))
        {
            if (panel != this.gameObject) panel.SetActive(false);
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
