using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TestTowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerChanceData _towerChanceData;
    [SerializeField] private Transform[] tile;


    private Dictionary<TowerGrade, GameObject[]> towerPrefabsDictionary = new Dictionary<TowerGrade, GameObject[]>();
    private List<float> _chanceList;

    //TODO TILE정보를 받아와서 처리할 수 있도록 변경

    private void Awake()
    {
        SetTowerPrefabs();
        NormalizeTowerProbabilities();
      
    }

    public void Initialize(Func<GameObject> _spawnTowerEvent)
    {
        _spawnTowerEvent += SpawnTowerByProbability;
    }

    private void SetTowerPrefabs()
    {
        var grades = Enum.GetValues(typeof(TowerGrade));

        foreach (TowerGrade grade in grades)
        {
            GameObject[] gradePrefabs = Resources.LoadAll<GameObject>($"Towers/{grade}");

            if (gradePrefabs.Length == 0)
            {
                Debug.LogWarning($"해당 등급에 타워 프리팹이 없습니다: {grade}");
                continue;
            }

            towerPrefabsDictionary[grade] = gradePrefabs;
        }
    }

    //생성된 타워를 게임오브젝트로 받는 메서드 -> 스테이지매니저에서 사용될 메서드
    public GameObject SpawnTowerByProbability()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeChance = 0f;

        for (int i = 0; i < _chanceList.Count; i++)
        {
            cumulativeChance += _chanceList[i];
            if (randomValue <= cumulativeChance)
            {
                return InstantiateTowerByGrade(i);
            }
        }

        return null;
    }

    private GameObject InstantiateTowerByGrade(int gradeIndex)
    {
        TowerGrade grade = (TowerGrade)gradeIndex;

        if (!towerPrefabsDictionary.TryGetValue(grade, out GameObject[] gradePrefabs) || gradePrefabs.Length == 0)
        {
            Debug.LogWarning($"해당 등급에 타워 프리팹이 없습니다: {grade}");
            return null;
        }

        int randomIndex = Random.Range(0, gradePrefabs.Length);

        GameObject selectedPrefab = gradePrefabs[randomIndex];

        if (selectedPrefab == null)
        {
            Debug.LogWarning($"선택된 타워 프리팹이 null입니다: Grade={grade}, PrefabIndex={randomIndex}");
            return null;
        }

        return Instantiate(selectedPrefab);
    }

    //private void SpawnRandomTower()
    //{
    //    if (StageManager.Instance.CurGold >= StageManager.Instance.SummonTowerCost)
    //    {
    //        StageManager.Instance.CurGold = Mathf.Max( 0 , StageManager.Instance.CurGold - StageManager.Instance.SummonTowerCost);

    //        float randomValue = UnityEngine.Random.Range(0f, 1f);
    //        float cumulativeChance = 0f;

    //        for (int i = 0; i < _chanceList.Count; i++)
    //        {
    //            cumulativeChance += _chanceList[i];
    //            if (randomValue <= cumulativeChance)
    //            {
    //                //SlimeTowers.Add(소환된 슬라임 타워 넣기);
    //                Debug.Log($"타워 {i} 소환됨");
    //                break;
    //            }
    //        }
    //    }
    //}

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