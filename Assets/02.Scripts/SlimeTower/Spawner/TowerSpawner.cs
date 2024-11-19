using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerChanceData _towerChanceData;
    [SerializeField] private Transform[] tile;
    [SerializeField] private Button spawnButton;
   
    private List<float> _chanceList;

    // 
    public List<BaseSlimeTower> SlimeTowers = new List<BaseSlimeTower>();
    
    
    
    //TODO TILE정보를 받아와서 처리할 수 있도록 변경

    private void Awake()
    {
        NormalizeTowerProbabilities();
        spawnButton.onClick.AddListener(SpawnRandomTower);
    }

    private void SpawnRandomTower()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        float cumulativeChance = 0f;

        for (int i = 0; i < _chanceList.Count; i++)
        {
            cumulativeChance += _chanceList[i];
            if (randomValue <= cumulativeChance)
            {
                //SlimeTowers.Add(소환된 슬라임 타워 넣기);
                Debug.Log($"타워 {i} 소환됨");
                break;
            }
        }
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