using UnityEngine;
using System;
using System.Collections.Generic;
public class EnemyRobot : MovingNPC
{
    private int _health = 20;
    public int enemyIndex;
    void Start()
    {
        ManagePositions.instance.enemyPositions.Add(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Enemy health: " + _health);
        _health -= amount;
        if (_health <= 0) {
            Destroy(gameObject);
        }
    }
}
