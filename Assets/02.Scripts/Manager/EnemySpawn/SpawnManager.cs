using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> //Destroy�Ǵ� �̱���
{
    //�ӽ� ������Ʈ Ǯ
    public ObjectPool ObjectPool {  get; private set; }

    [field: SerializeField] public Vector3 SpawnPoint {  get; private set; }
    public Transform[] SpawnPoints;
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
            //������ƮǮ���� ĳ���� �޾ƿͼ� �ʱ�ȭ
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
