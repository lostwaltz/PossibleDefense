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

    public GameObject CreateEnemy(string id, Vector3 spawnPos, Vector3[] waypoints)
    {
        Enemy enemy = ObjectPoolManager.Instance.GetPooledObject<Enemy>(id);
        return enemy.gameObject;
    }

    //// 방법 1. 리소스 로드를 통해 동적 로드 후 넘겨주기
    //BaseProjectile baseProjectileResource = Resources.Load<BaseProjectile>("ResourcePath");
    //// 키값을 통해 새로운 풀 생성
    //ObjectPoolManager.Instance.CreateNewPool("Example1", baseProjectileResource, 10, 10); 
    //    //  키값을 통해 접근 하면 됨.
    //    BaseProjectile example1Object = ObjectPoolManager.Instance.GetPooledObject<BaseProjectile>("Example1");

}
