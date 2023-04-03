using System;
using UnityEngine;

public class FindingCompanion : MonoBehaviour
{
    public event EventHandler foundCompanion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            foundCompanion?.Invoke(this, EventArgs.Empty);
        }
    }
}
