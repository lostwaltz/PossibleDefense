using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestTowerSpawner : MonoBehaviour
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
        if (StageManager.Instance.CurGold >= StageManager.Instance.SummonTowerCost)
        {
            StageManager.Instance.CurGold = Mathf.Max( 0 , StageManager.Instance.CurGold - StageManager.Instance.SummonTowerCost);

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
    }

    //확률 보정 코드 , 확률이 점차적으로 증가하며 확률에 성공했을때 확률 초기화를 해주는 코드
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