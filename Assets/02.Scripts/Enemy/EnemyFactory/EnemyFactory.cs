using UnityEngine;

public class EnemyFactory
{
    private EnemyDatabase database;

    public EnemyFactory(EnemyDatabase database)
    {
        this.database = database;
    }

    public GameObject CreateEnemy(int id, Vector3 spawnPos, Vector3[] waypoints)
    {
        GameObject newEnemy = SpawnManager.Instance.ObjectPool.SpawnFromPool(id.ToString());
        EnemySO enemyData = database.GetEnemy(id);
        newEnemy.transform.position = spawnPos;
        newEnemy.SetActive(true);

        if(newEnemy.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Initialize(waypoints, enemyData);
        }
        return newEnemy;
    }
}
