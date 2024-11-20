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
        //TODO :: 자식오브젝트로 database에서 해당 자식오브젝트로 변경?
        GameObject newEnemy = SpawnManager.Instance.ObjectPoolLegacy.SpawnFromPool(id.ToString());
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
