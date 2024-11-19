using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptForEnemy : MonoBehaviour
{
    public void DealAll()
    {
        Enemy[] monsters = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in monsters)
        {
            if(enemy.gameObject.activeSelf.Equals(true))
            enemy.TakeDamage(30);
        }
    }

    public void Slow()
    {
        Enemy[] monsters = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in monsters)
        {
            if (enemy.gameObject.activeSelf.Equals(true))
            {
                //slow enemy
            }
                
        }
    }
}
