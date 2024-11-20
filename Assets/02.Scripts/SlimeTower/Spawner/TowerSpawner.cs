using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerChanceData _towerChanceData;
    [SerializeField] private Transform[] tile;
    [SerializeField] private Button spawnButton;
   
    private List<float> _chanceList;

     public List<BaseSlimeTower> SlimeTowers = new List<BaseSlimeTower>();
    
    
     private void Awake()
    {
        NormalizeTowerProbabilities();
     }

     
    public GameObject SpawnRandomTower()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        float cumulativeChance = 0f;
        GameObject tower = new GameObject();
        
        for (int i = 0; i < _chanceList.Count; i++)
        {
            cumulativeChance += _chanceList[i];
            if (randomValue <= cumulativeChance)
            {
                  //tower = In
            }
        }
        return tower;
    }

    public GameObject InstantiateRandomTower(TowerGrade grade)
    {
         GameObject[] towers = Resources.LoadAll<GameObject>($"Towers/{grade}");
    
        if (towers.Length == 0)
        {
            Debug.LogWarning($"해당 등급에 타워가 존재하지 ㅇ낫습니다: {grade}");
            return null;
        }

         var randomIndex = Random.Range(0, towers.Length);
        GameObject selectedTower = towers[randomIndex];

         return Instantiate(selectedTower);
    }

    private void NormalizeTowerProbabilities()
    {
        float totalProbability = 0f;
        _chanceList = _towerChanceData.GetChanceList();

        foreach (var chance in _chanceList)
        {
            totalProbability += chance;
        }

        if (totalProbability > 1f)
        {
            for (int i = 0; i < _chanceList.Count; i++)
            {
                _chanceList[i] /= totalProbability;
            }
        }
    }
}