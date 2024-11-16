using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{  
    [SerializeField]private Transform [] wayPoints;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private int maxMonsterCount = 10;
    private WaitForSeconds spawnTime = new WaitForSeconds(1f);
    
    public List<Monster> monsters = new List<Monster>();
    
    
    
    private void Start()
    {
        StartCoroutine(SpawnEnemyBySpawnTime());
    }
 
    
    
     private void ShowDefeatNotification()
    {
        Debug.Log("지셨습니다.");
    }




    private void DeleteMob(Monster mob)
    {
        monsters.Remove(mob);
    }


    private IEnumerator SpawnEnemyBySpawnTime()
    {

        while (monsters.Count <= maxMonsterCount)
        { 
            Monster mob = Instantiate(monsterPrefab, wayPoints[wayPoints.Length - 1].position, Quaternion.identity).GetComponent<Monster>();
            mob.SetWayPoints(wayPoints);
            mob.Die += DeleteMob;
            monsters.Add(mob);
            yield return spawnTime;
        }

        ShowDefeatNotification(); 
    }

}
