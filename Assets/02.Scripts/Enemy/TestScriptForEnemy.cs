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

    public void SpawnMonster(int id)
    {
        Vector3 start = Vector3.zero;
        Vector3[] ways = SpawnManager.Instance.wayPoints;
        SpawnManager.Instance.SetSpawner(start, 2, ways, id, 10);
    }
}
