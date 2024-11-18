using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> //Destroy되는 싱글톤
{
    //임시 오브젝트 풀
    public ObjectPool ObjectPool {  get; private set; }

    [field: SerializeField] public Vector3 SpawnPoint {  get; private set; }
    public Vector3[] SpawnPoints;
    public float SpawnDelay = 2f;
    public int maxSpawnCount = 10;

    private Coroutine coroutine;
    private WaitForSeconds spawnTime;

    private void Start()
    {
        ObjectPool = GetComponent<ObjectPool>();

        spawnTime = new WaitForSeconds(SpawnDelay);
        coroutine = StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < maxSpawnCount; i++)
        { 
            //오브젝트풀에서 캐릭터 받아와서 초기화
            GameObject newEnemy = ObjectPool.SpawnFromPool("Enemy");
            newEnemy.transform.position = SpawnPoint;

            if (newEnemy.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Initialize(SpawnPoints, EnemyType.Slime);
            }

            newEnemy.SetActive(true);
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

        coroutine = StartCoroutine(SpawnEnemy());
    }
}
