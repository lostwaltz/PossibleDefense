using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> 
{
    public ObjectPool ObjectPool {  get; private set; }
    [SerializeField] private EnemyDatabase enemyDatabase;
    private EnemyFactory enemyFactory;

    [field: SerializeField] public Vector3 SpawnPoint {  get; private set; }
    public Vector3[] wayPoints;
    public float SpawnDelay = 2f;
    public int maxSpawnCount = 10;

    private Coroutine coroutine;
    private WaitForSeconds spawnTime;

    private void Start()
    {
        ObjectPool = GetComponent<ObjectPool>();
        enemyFactory = new EnemyFactory(enemyDatabase);

        SetSpawner(SpawnPoint, SpawnDelay, wayPoints, 103, maxSpawnCount);
    }

    IEnumerator SpawnEnemy(int id)
    {
        for(int i = 0; i < maxSpawnCount; i++)
        {
            //TODO :: factory instead factory.spawn(int id)
            enemyFactory.CreateEnemy(id, SpawnPoint, wayPoints);
            //GameObject newEnemy = ObjectPool.SpawnFromPool("100");
            //newEnemy.transform.position = SpawnPoint;

            //if (newEnemy.TryGetComponent<Enemy>(out Enemy enemy))
            //{
            //    enemy.Initialize(wayPoints, EnemyType.Slime);
            //}

            //newEnemy.SetActive(true);
            yield return spawnTime;
        }
    }

    public void SetSpawner(Vector3 spawnPos, float SpawnDelay)
    {
        spawnTime = new WaitForSeconds(SpawnDelay);
        this.SpawnPoint = spawnPos;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        //coroutine = StartCoroutine(SpawnEnemy());
    }

    public void SetSpawner(Vector3 spawnPos, float SpawnDelay, Vector3[] waypoints, int id, int spawnCount)
    {
        this.maxSpawnCount = spawnCount;
        this.wayPoints = waypoints;
        spawnTime = new WaitForSeconds(SpawnDelay);
        this.SpawnPoint = spawnPos;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(SpawnEnemy(id));
    }
}
